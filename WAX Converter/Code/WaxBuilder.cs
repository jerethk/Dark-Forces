using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WAX_converter
{

    // A static class containing static methods used to create a new WAX file //
    public static class WaxBuilder
    {
        public static Waxfile BuildWax(int LogicType, List<Action> SourceActionList, List<Sequence> SourceSequenceList, List<Frame> SourceFrameList, List<Bitmap> SourceImageList, DFPal palette, Color transparentColour, bool includeIlluminatedColours, bool compress)
        {
            ProgressBarWindow progressMeter = new ProgressBarWindow();
            progressMeter.Show();
            
            Waxfile newWax = new Waxfile();

            switch (LogicType)
            {
                case 0: // anim
                    newWax.numActions = 1;
                    break;
                case 1: // scenery
                    newWax.numActions = 2;
                    break;
                case 2: // enemy
                    newWax.numActions = 13;
                    break; 
                case 3: // dark trooper
                    newWax.numActions = 14;
                    break;
                case 4: // remote
                    newWax.numActions = 4;
                    break;
            }

            // Header
            newWax.Nseqs = SourceSequenceList.Count;
            newWax.Nframes = SourceFrameList.Count;
            newWax.Ncells = SourceImageList.Count;

            newWax.actionAddresses = new int[32];

            for (int a = 0; a < newWax.numActions; a++)     // used action addresses
            {
                newWax.actionAddresses[a] = 160 + a * 156;
            }

            for (int a = newWax.numActions; a < 32; a++)    // empty action addresses
            {
                newWax.actionAddresses[a] = 0;
            }

            // Actions
            newWax.Actions = SourceActionList;
            for (int a = 0; a < newWax.numActions; a++)
            {
                for (int v = 0; v < 32; v++)    // calculate the address for each view (x32)
                {
                    newWax.Actions[a].viewAddresses[v] = 160 + newWax.numActions * 156 + (newWax.Actions[a].seqIndexes[v] * 144);
                }
            }

            // Sequences
            newWax.Sequences = SourceSequenceList;
            for (int s = 0; s < newWax.Nseqs; s++)
            {
                for (int f = 0; f < 32; f++)    // calculate the frame addresses
                {
                    if (newWax.Sequences[s].frameIndexes[f] == -1)  // empty frame
                    {
                        newWax.Sequences[s].frameAddresses[f] = 0;
                    }
                    else
                    {
                        newWax.Sequences[s].frameAddresses[f] = 160 + newWax.numActions * 156 + newWax.Nseqs * 144 + (newWax.Sequences[s].frameIndexes[f] * 32);
                    }
                }
            }

            // create Cells from bitmap images. Show progress meter because this can be lengthy
            progressMeter.progressBar.Maximum = SourceImageList.Count + 1;
            progressMeter.progressBar.Value = 1;

            int firstCellAddress = 160 + newWax.numActions * 156 + newWax.Nseqs * 144 + newWax.Nframes * 32;
            for (int i = 0; i < SourceImageList.Count; i++)  
            {
                Cell newCell = new Cell();
                newCell.SizeX = SourceImageList[i].Width;
                newCell.SizeY = SourceImageList[i].Height;
                newCell.ColOffs = 0;    // always zero

                newCell.Pixels = new short[newCell.SizeX, newCell.SizeY];
                newCell.createCellImage(SourceImageList[i], palette, transparentColour, includeIlluminatedColours);       // result is stored in the cell object's Pixels property

                if (compress)
                {
                    newCell.compressCell();     // result is stored in the cell object's compressedData property (List of bytes)
                    newCell.Compressed = 1;
                    newCell.DataSize = 24 + (4 * newCell.SizeX) + newCell.compressedData.Count;
                }
                else
                {
                    newCell.Compressed = 0;
                    newCell.DataSize = 0;
                }
                
                if (i == 0)
                {
                    newCell.address = firstCellAddress;
                }
                else
                {
                    if (compress)
                    {
                        newCell.address = newWax.Cells[i - 1].address + newWax.Cells[i - 1].DataSize;   // address of prev cell + datasize of previous cell
                    }
                    else // not compressed
                    {
                        newCell.address = newWax.Cells[i - 1].address + 24 + newWax.Cells[i - 1].SizeX * newWax.Cells[i - 1].SizeY;   // header (24 bytes) + image size (x * y)
                    }
                }

                newWax.Cells.Add(newCell);
                progressMeter.progressBar.Value++;
            }

            // Frames
            newWax.Frames = SourceFrameList;
            for (int f = 0; f < SourceFrameList.Count; f++)
            {
                // set cell addresses
                int cellIndex = newWax.Frames[f].CellIndex;
                newWax.Frames[f].CellAddress = newWax.Cells[cellIndex].address;  
            }

            progressMeter.Close();
            progressMeter.Dispose();
            return newWax;
        }
        
        public static short matchPixeltoPal(Color pixelColour, DFPal palette, bool includeIlluminatedColours)
        {
            // Color quantizes to the DF PAL using Euclidean distance technique
            int sourceRed = pixelColour.R;
            int sourceGreen = pixelColour.G;
            int sourceBlue = pixelColour.B;

            short startColour;
            if (includeIlluminatedColours)
            {
                startColour = 1;        // first 32 colours are always bright / illuminated
            }
            else
            {
                startColour = 32;
            }

            /*
            if (sourceRed == 0 && sourceGreen == 0 && sourceBlue == 0)
            {
                return 0;   // set black to PAL index 0 (transparent)
            }
            else  */
            
            double smallestDistance = 500;
            short bestMatch = 0;

            for (short i = startColour; i < 256; i++)    
            {
                int deltaRed = sourceRed - palette.Colours[i].R;
                int deltaGreen = sourceGreen - palette.Colours[i].G;
                int deltaBlue = sourceBlue - palette.Colours[i].B;
                double distance = Math.Sqrt(deltaRed * deltaRed + deltaGreen * deltaGreen + deltaBlue * deltaBlue);

                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    bestMatch = i;
                }
            }

            return bestMatch;
        }



        /*
        public static Bitmap quantizeBitmap(Bitmap sourceImage, DFPal palette)
        {
            // Color quantizes to the PAL using Euclidean distance technique
            
            Bitmap newImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int x = 0; x < sourceImage.Width; x ++ )
            {
                for (int y = 0; y < sourceImage.Height; y++ )
                {
                    int sourceRed = sourceImage.GetPixel(x, y).R;
                    int sourceGreen = sourceImage.GetPixel(x, y).G;
                    int sourceBlue = sourceImage.GetPixel(x, y).B;

                    double smallestDistance = 500;
                    int bestMatch = 0;

                    for (int i = 32; i < 256; i++)     // exclude first 32 colours because these are usually always bright on the colourmap
                    {
                        int deltaRed = sourceRed - palette.Colours[i].R;
                        int deltaGreen = sourceGreen - palette.Colours[i].G;
                        int deltaBlue = sourceBlue - palette.Colours[i].B;
                        double distance = Math.Sqrt(deltaRed * deltaRed + deltaGreen * deltaGreen + deltaBlue * deltaBlue);

                        if (distance < smallestDistance)
                        {
                            smallestDistance = distance;
                            bestMatch = i;
                        }
                    }

                    int newRed = palette.Colours[bestMatch].R;
                    int newGreen = palette.Colours[bestMatch].G;
                    int newBlue = palette.Colours[bestMatch].B;

                    newImage.SetPixel(x, y, Color.FromArgb(255, newRed, newGreen, newBlue));
                }
            }

            return newImage;
        }
        */
    }
}
