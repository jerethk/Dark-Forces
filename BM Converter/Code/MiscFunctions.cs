using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BM_Converter
{
    static class MiscFunctions
    {
        public static bool isPowerOfTwo(int number)
        {
            double log = Math.Log2(number);
            double remainder = log % 1;

            if (remainder == 0)
            {
                return true;
            }
            else return false;
        }
        
        // Builds a BM object from source images
        public static DFBM buildBM(bool multiBM, DFPal pal, List<Bitmap> SourceImages, char transparency, byte FRate, bool IncludeIlluminated, bool compress)
        {
            DFBM newBM = new DFBM();

            byte trn;
            switch (transparency)
            {
                case 'o':
                    trn = 0x36;
                    break;
                case 't':
                    trn = 0x3E;
                    break;
                case 'w':
                    trn = 0x08;
                    break;
                default:
                    trn = 0x36;
                    break;
            }

            if (!multiBM)
            {
                // Single BM
                newBM.multiBM = false;
                Bitmap source = SourceImages[0];

                // Create BM header. The FileID is automatically created by the object constructor
                newBM.SizeX = (short)source.Width;
                newBM.SizeY = (short)source.Height;
                newBM.idemX = newBM.SizeX;
                newBM.idemY = newBM.SizeY;
                newBM.transparent = trn;                

                if (isPowerOfTwo(newBM.SizeY))
                {
                    newBM.logSizeY = (byte)Math.Log2(newBM.SizeY);
                }
                else
                {
                    // presumably a weapon so the value is set to 0
                    newBM.logSizeY = 0;
                }

                // Create BM image data
                newBM.PixelData = DFBM.BitmaptoBM(source, pal, IncludeIlluminated);

                if (compress)
                {
                    if (transparency == 'o')
                    {
                        newBM.compressed = 1;   // RLE compression for non-transparent texture
                        newBM.compressRLE();
                    }
                    else
                    {
                        newBM.compressed = 2;   // RLE0 compression for transparent texture
                        newBM.compressRLE0();
                    }
                }
                else
                {
                    newBM.compressed = 0;
                    newBM.DataSize = newBM.SizeX * newBM.SizeY;
                }

            }
            else
            {
                // Multi BM
                newBM.multiBM = true;
                newBM.numImages = SourceImages.Count;
                
                newBM.SizeX = 1;
                newBM.idemX = -2;
                newBM.idemY = (short)newBM.numImages;
                newBM.transparent = trn;
                newBM.compressed = 0;
                newBM.DataSize = 0;
                newBM.FrameRate = FRate;
                newBM.SecondByte = 2;

                newBM.Offsets = new int[newBM.numImages];
                newBM.SubBMs = new List<SubBM>();

                for (int i = 0; i < newBM.numImages; i++)
                {
                    SubBM newSubBM = new SubBM();
                    newSubBM.SizeX = (short)SourceImages[i].Width;
                    newSubBM.SizeY = (short)SourceImages[i].Height;
                    newSubBM.idemX = newSubBM.SizeX;
                    newSubBM.idemY = newSubBM.SizeY;
                    newSubBM.DataSize = newSubBM.SizeX * newSubBM.SizeY;
                    newSubBM.transparent = trn;

                    if (isPowerOfTwo (newSubBM.SizeY))
                    {
                        newSubBM.logSizeY = (byte)Math.Log2(newSubBM.SizeY);
                    }
                    else
                    {
                        newSubBM.logSizeY = 0;
                    }

                    newSubBM.PixelData = DFBM.BitmaptoBM(SourceImages[i], pal, IncludeIlluminated);
                    newBM.SubBMs.Add(newSubBM);
                }

                newBM.logSizeY = newBM.SubBMs[0].logSizeY;  // sub BMs should all be the same size

                // calculate the "sizeY" value, which in a multi BM is the length of the entire file minus the header (32 bytes). Also work out the sub BM offsets.
                int counter = (2 + 4 * newBM.numImages);    // the 2 bytes following the header + the table of offsets
                for (int i=0; i < newBM.numImages; i++)
                {
                    newBM.Offsets[i] = counter - 2;
                    counter += 28;  // len of sub BM header = 28
                    counter += newBM.SubBMs[i].DataSize;
                }
                newBM.SizeY = (short)counter;
            }

            return newBM;
        }

        // Color matches to the PAL using Euclidean distance technique
        public static byte matchPixeltoPal(Color pixelColour, DFPal palette, bool includeIlluminatedColours)
        {
            int sourceRed = pixelColour.R;
            int sourceGreen = pixelColour.G;
            int sourceBlue = pixelColour.B;

            int startColour;
            if (includeIlluminatedColours)
            {
                startColour = 1;        // first 32 colours are always bright / illuminated
            }
            else
            {
                startColour = 32;
            }

            double smallestDistance = 500;
            int bestMatch = 0;

            for (int i = startColour; i < 256; i++)
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

            return (byte)bestMatch;
        }
    }
}
