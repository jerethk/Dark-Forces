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
        public static DFBM buildBM(bool multiBM, DFPal pal, List<Bitmap> SourceImages, char transparency, byte FRate, bool IncludeIlluminated)
        {
            DFBM newBM = new DFBM();

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

                switch (transparency) {
                    case 'o':
                        newBM.transparent = 0x36;
                        break;
                    case 't':
                        newBM.transparent = 0x3E;
                        break;
                    case 'w':
                        newBM.transparent = 0x08;
                        break;
                    default:
                        newBM.transparent = 0x36;
                        break;
                }

                if (isPowerOfTwo(newBM.SizeY))
                {
                    newBM.logSizeY = (byte)Math.Log2(newBM.SizeY);
                }
                else
                {
                    // presumably a weapon so the value is set to 0
                    newBM.logSizeY = 0;
                }

                newBM.compressed = 0;
                newBM.DataSize = newBM.SizeX * newBM.SizeY;

                // Create BM image data
                newBM.PixelData = DFBM.BitmaptoBM(source, pal, IncludeIlluminated);
            }
            else
            {
                // Multi BM

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
