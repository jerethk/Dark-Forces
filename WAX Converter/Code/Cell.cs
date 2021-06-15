using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WAX_converter
{
    public class Cell
    {
        // Properties
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int Compressed { get; set; }
        public int DataSize { get; set; }       // used for compressed cells only. Equals size of entire cell, including 24 byte header
        public int ColOffs { get; set; }
        public int pad1 { get; set; }
        
        public int address { get; set; }        // address where this cell is located in WAX file
        public short[,] Pixels { get; set; }     // the image as an array [x, y] of palette references, from the WAX file (same as FME). Use short (instead of byte) so transparent pixels can be recorded as -1
        public Bitmap bitmap { get; set; }
        public List<byte> compressedData { get; set; }   // this is used when creating a new compressed WAX

        // Methods
        public void uncompressImage(byte[] compressedData)
        {
            // get the column indexes - this comes at the start of the data
            int[] colIndexes = new int[this.SizeX];
            for (int col = 0; col < this.SizeX; col++)
            {
                colIndexes[col] = BitConverter.ToInt32(compressedData, 4 * col);
            }

            // uncompress data to the pixel array, column by column
            int dataPosition = 4 * this.SizeX;       // the starting point for the actual image data; follows the column indexes
            for (int x = 0; x < this.SizeX; x++)
            {
                int y = 0;
                int numPixels;

                while (y < this.SizeY)
                {
                    byte b = compressedData[dataPosition];
                    if (b > 128)    // transparent section
                    {
                        numPixels = b - 128;
                        for (int i = 0; i < numPixels; i++)
                        {
                            this.Pixels[x, y] = 31;  // record transparency as palette index 31, which is usually not used
                            y++;
                        }

                        dataPosition++;
                    }
                    else            // non-transparent section
                    {
                        numPixels = b;
                        dataPosition++;
                        for (int i = 0; i < numPixels; i++)
                        {
                            this.Pixels[x, y] = compressedData[dataPosition];
                            y++;
                            dataPosition++;
                        }
                    }
                }

            }
        }

        public void createBitmap(DFPal palette)
        {
            this.bitmap = new Bitmap(this.SizeX, this.SizeY); //, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            
            for (int x = 0; x < this.SizeX; x++)
            {
                for (int y = 0; y < this.SizeY; y++)
                {
                    short n = this.Pixels[x, y];
                    Color pixelColour;

                    if (n == -1)    // transparent pixel, make it black (magenta?? )
                    {
                        pixelColour = Color.FromArgb(255, 255, 0, 255);
                    }
                    else
                    {
                        int r = palette.Colours[n].R;
                        int g = palette.Colours[n].G;
                        int b = palette.Colours[n].B;
                        pixelColour = Color.FromArgb(255, r, g, b);
                    }

                    this.bitmap.SetPixel(x, y, pixelColour);     
                }
            }

            this.bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY); // need to flip it upside down, because wax stores image data from bottom to top
        }

        public void createCellImage(Bitmap bitmap, DFPal palette, Color transparentColour, bool includeIlluminatedColours)
        {
            //Color transparentColour = Color.FromArgb(255, 255, 0, 255);

            // flip upside down
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            
            for (int x = 0; x < this.SizeX; x++)
            {
                for (int y = 0; y < this.SizeY; y++)
                {
                    if (bitmap.GetPixel(x, y) == transparentColour)
                    {
                        this.Pixels[x, y] = 0;     // transparent = index 0
                    }
                    else
                    {
                        short colourIndex = WaxBuilder.matchPixeltoPal(bitmap.GetPixel(x, y), palette, includeIlluminatedColours);
                        this.Pixels[x, y] = colourIndex;
                    }
                }
            }

            // reflip so can use again
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
        }

        public void compressCell()
        {
            int[] columnIndexes = new int[this.SizeX];
            List<byte>[] columnData = new List<byte>[this.SizeX];   // an array of byte-lists.

            for (int x = 0; x < this.SizeX; x++)    // compress each column of the image
            {
                List<byte> thisColumn = new List<byte>(); 

                int y = 0;
                while (y < this.SizeY)
                {
                    // note: because of the way data is encoded, cannot do a run of more than 128 coloured pixels or 127 transparent pixels
                    // ... and I've discovered that trying to compress an image with height > 128 crashes the game !!!!
                    byte counter;

                    if (this.Pixels[x, y] == -1)    // transparent run
                    {
                        counter = 1;
                        
                        while (y < this.SizeY - 1 && this.Pixels[x, y+1] == -1 && counter < 127)    
                        {
                            y++;
                            counter++;
                        }

                        // write run of transparent pixels
                        thisColumn.Add((byte) (128 + counter));
                        
                        y += 1; 
                    }
                    else                        // non-transparent run
                    {
                        int startOfRun = y;
                        counter = 1;

                        while (y < this.SizeY - 1 && this.Pixels[x, y + 1] != -1 && counter < 128)
                        {
                            y++;
                            counter++;
                        }

                        thisColumn.Add(counter);    // number of non-transparent pixels

                        // go back to start of run and add the pixels
                        y = startOfRun;
                        for (int i = 0; i < counter; i++)
                        {
                            thisColumn.Add((byte) this.Pixels[x, y]);
                            y++;
                        }
                    }
                }

                columnData[x] = thisColumn;
            }

            // Set column indexes
            // index of first column, always 24 + (4 * sizeX)
            columnIndexes[0] = 24 + (4 * this.SizeX);

            // index of remaining columns
            for (int x = 1; x < this.SizeX; x++)
            {
                columnIndexes[x] = columnIndexes[x - 1] + columnData[x - 1].Count; // add the size of the previous column (number of bytes in the list) to the index of the previous column
            }

            // transfer the whole thing to new list of bytes
            this.compressedData = new List<byte>();
            
            for (int x = 0; x < this.SizeX; x++)    // first the indexes
            {
                byte[] fourBytes = new byte[4];
                fourBytes = BitConverter.GetBytes(columnIndexes[x]);

                for (int i = 0; i < 4; i++)
                {
                    this.compressedData.Add(fourBytes[i]);
                }
            }

            for (int x = 0; x < this.SizeX; x++)    // finally, the data
            {   
                for (int i = 0; i < columnData[x].Count; i++)
                {
                    this.compressedData.Add(columnData[x][i]);
                }
            }
        }

    }
}
