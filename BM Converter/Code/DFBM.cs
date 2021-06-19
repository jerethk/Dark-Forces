using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace BM_Converter
{
    class DFBM
    {
        public byte[] FileId { get; set; }      // header
        public short SizeX { get; set; }
        public short SizeY { get; set; }
        public short idemX { get; set; }
        public short idemY { get; set; }
        public byte transparent { get; set; }
        public byte logSizeY { get; set; }
        public short compressed { get; set; }
        public int DataSize { get; set; }
        public byte[] pad { get; set; }

        public byte[] CompressedData { get; set; }      // data for single BM
        public byte[,] PixelData { get; set; }

        public byte FrameRate { get; set; }     // fields for multi BMs
        public byte SecondByte { get; set; }
        public int[] Offsets { get; set; }
        public List<SubBM> SubBMs { get; set; }

        public bool multiBM { get; set; }       // custom fields for convenience, not used in an actual BM file
        public int fileLength { get; set; }    
        public int numImages { get; set; }     


        // Constructor        
        public DFBM()
        {
            FileId = new byte[4] { 0x42, 0x4d, 0x20, 0x1e };
            pad = new byte[12];
        }

        public bool loadFromFile(string FileName)
        {
            bool result = false;

            try
            {
                BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open));

                this.FileId = reader.ReadBytes(4);

                if (FileId[0] != 0x42 || FileId[1] != 0x4d || FileId[2] != 0x20 || FileId[3] != 0x1e)
                {
                    // Not a valid BM file
                    result = false;
                }
                else
                {
                    this.SizeX = reader.ReadInt16();
                    this.SizeY = reader.ReadInt16();
                    this.idemX = reader.ReadInt16(); 
                    this.idemY = reader.ReadInt16();
                    this.transparent = reader.ReadByte();
                    this.logSizeY = reader.ReadByte();
                    this.compressed = reader.ReadInt16();
                    this.DataSize = reader.ReadInt32();
                    this.pad = reader.ReadBytes(12);

                    this.multiBM = false;
                    this.numImages = 1;

                    if (this.SizeX == 1 && this.SizeY > 1)
                    {
                        // multi BM !
                        this.multiBM = true;
                        this.numImages = this.idemY;
                        this.fileLength = this.SizeY + 32;

                        this.FrameRate = reader.ReadByte();
                        this.SecondByte = reader.ReadByte();

                        this.Offsets = new int[this.numImages];
                        for (int i = 0; i < this.numImages; i++)
                        {
                            this.Offsets[i] = reader.ReadInt32();
                        }

                        this.SubBMs = new List<SubBM>();
                        for (int i = 0; i < this.numImages; i++)
                        {
                            SubBM newSubBM = new SubBM();
                            newSubBM.SizeX = reader.ReadInt16();
                            newSubBM.SizeY = reader.ReadInt16();
                            newSubBM.idemX = reader.ReadInt16();
                            newSubBM.idemY = reader.ReadInt16();
                            newSubBM.DataSize = reader.ReadInt32();
                            newSubBM.logSizeY = reader.ReadByte();
                            newSubBM.pad1 = reader.ReadBytes(3);
                            newSubBM.u1 = reader.ReadBytes(3);
                            newSubBM.pad2 = reader.ReadBytes(5);
                            newSubBM.transparent = reader.ReadByte();
                            newSubBM.pad3 = reader.ReadBytes(3);

                            newSubBM.PixelData = new byte[newSubBM.SizeX, newSubBM.SizeY];
                            for (int x = 0; x < newSubBM.SizeX; x++)
                            {
                                for (int y = 0; y < newSubBM.SizeY; y++)
                                {
                                    newSubBM.PixelData[x, y] = reader.ReadByte();
                                }
                            }

                            this.SubBMs.Add(newSubBM);
                        }

                    }
                    else
                    {
                        // single BM
                        this.PixelData = new byte[this.SizeX, this.SizeY];

                        if (this.compressed == 0)
                        {
                            // Uncompressed BM
                            for (int x = 0; x < this.SizeX; x++)
                            {
                                for (int y= 0; y < this.SizeY; y++)
                                {
                                    this.PixelData[x, y] = reader.ReadByte();
                                }
                            }
                        }
                        else
                        {
                            // read compressed data
                            this.CompressedData = new byte[this.DataSize];
                            this.CompressedData = reader.ReadBytes(this.DataSize);
                        }

                        if (this.compressed == 1)
                        {
                            uncompressRLE();
                        }
                        else if (this.compressed == 2)
                        {
                            uncompressRLE0();
                        }

                    }

                }

                reader.Close();
                reader.Dispose();
                result = true;
            }
            catch (IOException)
            {
                result = false;
            }

            return result;
        }

        // Static method to convert a BM image into a Bitmap object
        public static Bitmap BMtoBitmap(int SizeX, int SizeY, byte[,] PixelData, DFPal pal)
        {
            Bitmap bitmap = new Bitmap(SizeX, SizeY);
            
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    int R = pal.Colours[PixelData[x, y]].R;
                    int G = pal.Colours[PixelData[x, y]].G;
                    int B = pal.Colours[PixelData[x, y]].B;

                    Color colour = Color.FromArgb(255, R, G, B);
                    bitmap.SetPixel(x, y, colour);
                }
            }

            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);  // need to flip because BMs are defined bottom-up
            return bitmap;
        }

        // Static method to convert a Bitmap object into a BM image
        public static byte[,] BitmaptoBM(Bitmap bitmap, DFPal pal, bool IncludeIlluminated)
        {
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            Byte[,] PixelArray = new byte[bitmap.Width, bitmap.Height];
            
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixelColour = bitmap.GetPixel(x, y);
                    byte palIndex;

                    if (pixelColour.R == 0 && pixelColour.G == 0 && pixelColour.B == 0)    // black = PAL index 0, will be set to transparent if transparent or weapon texture
                    {
                        palIndex = 0;
                    }
                    else
                    {
                        palIndex = MiscFunctions.matchPixeltoPal(pixelColour, pal, IncludeIlluminated);
                    }

                    PixelArray[x, y] = palIndex;
                }

            }

            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY); 
            return PixelArray;
        }

        public bool SaveToFile(string filename)
        {
            bool success = false;

            try
            {
                BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create));
                
                // write BM header
                foreach (byte b in this.FileId) writer.Write(b);
                writer.Write(this.SizeX);
                writer.Write(this.SizeY);
                writer.Write(this.idemX);
                writer.Write(this.idemY);
                writer.Write(this.transparent);
                writer.Write(this.logSizeY);
                writer.Write(this.compressed);
                writer.Write(this.DataSize);
                foreach (byte b in this.pad) writer.Write(b);

                if (!this.multiBM)
                {
                    // single BM image
                    for (int x = 0; x < this.SizeX; x++)
                    {
                        for (int y = 0; y < this.SizeY; y++)
                        {
                            writer.Write(this.PixelData[x, y]);
                        }
                    }
                }
                else
                {
                    // multi BM
                    writer.Write(this.FrameRate);
                    writer.Write(this.SecondByte);
                    foreach (int i in this.Offsets) writer.Write(i);

                    foreach (SubBM sub in this.SubBMs) 
                    {
                        //Sub BM header
                        writer.Write(sub.SizeX);
                        writer.Write(sub.SizeY);
                        writer.Write(sub.idemX);
                        writer.Write(sub.idemY);
                        writer.Write(sub.DataSize);
                        writer.Write(sub.logSizeY);
                        foreach (byte b in sub.pad1) writer.Write(b);
                        foreach (byte b in sub.u1) writer.Write(b);
                        foreach (byte b in sub.pad2) writer.Write(b);
                        writer.Write(sub.transparent);
                        foreach (byte b in sub.pad3) writer.Write(b);

                        //Sub BM image
                        for (int x = 0; x < sub.SizeX; x++)
                        {
                            for (int y = 0; y < sub.SizeY; y++)
                            {
                                writer.Write(sub.PixelData[x, y]);
                            }
                        }
                    }
                }

                writer.Close();
                writer.Dispose();
                success = true;
            }
            catch (IOException)
            {
                success = false;
            }

            return success;
        }

        // Uncompresses image encoded with RLE method (header.compressed == 1)
        private void uncompressRLE()
        {
            int dataPosition = 0;
            
            for (int x = 0; x < this.SizeX; x++)
            {
                int y = 0;
                int numPixels;

                while (y < this.SizeY)
                {
                    byte b = this.CompressedData[dataPosition];

                    if (b <= 128)
                    {
                        numPixels = b;
                        dataPosition++;

                        for (int i = 0; i < numPixels; i++)
                        {
                            this.PixelData[x, y] = this.CompressedData[dataPosition];
                            y++;
                            dataPosition++;
                        }
                    }
                    else
                    {
                        // run of same coloured pixels
                        numPixels = b - 128;
                        dataPosition++;
                        byte colour = this.CompressedData[dataPosition];

                        for (int i = 0; i < numPixels; i++)
                        {
                            this.PixelData[x, y] = colour;
                            y++;
                        }

                        dataPosition++;
                    }

                }

            }
        }

        // Uncompresses image encoded with RLE0 method (header.compressed == 2)
        private void uncompressRLE0()
        {
            int dataPosition = 0;
            
            for (int x = 0; x < this.SizeX; x++)
            {
                int y = 0;
                int numPixels;

                while (y < this.SizeY)
                {
                    byte b = this.CompressedData[dataPosition];
                    if (b > 128)    // transparent section
                    {
                        numPixels = b - 128;
                        for (int i = 0; i < numPixels; i++)
                        {
                            this.PixelData[x, y] = 0;  // transparency = palette index 0
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
                            this.PixelData[x, y] = CompressedData[dataPosition];
                            y++;
                            dataPosition++;
                        }
                    }
                }

            }
        }
    }

    class SubBM
    {
        public short SizeX { get; set; }
        public short SizeY { get; set; }
        public short idemX { get; set; }
        public short idemY { get; set; }
        public int DataSize { get; set; }
        public byte logSizeY { get; set; }
        public byte[] pad1 { get; set; }
        public byte[] u1 { get; set; }
        public byte[] pad2 { get; set; }
        public byte transparent { get; set; }
        public byte[] pad3 { get; set; }
        public byte[,] PixelData { get; set; }

        public SubBM()
        {
            pad1 = new byte[3];
            u1 = new byte[3];
            pad2 = new byte[5];
            pad3 = new byte[3];
        }
    }
}
