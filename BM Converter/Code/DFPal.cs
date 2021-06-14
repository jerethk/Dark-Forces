using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BM_Converter
{
    class DFPal
    {
        public DFPal()
        {
            this.Colours = new PalColour[256];
        }

        public PalColour[] Colours { get; set; }

        public bool LoadfromFile(string filename)
        {
            FileInfo palFile = new FileInfo(filename);

            // Check file is correct length
            if (palFile.Length != 768)
            {
                return false;
            }

            // Load Palette (PAL) file
            try
            {
                BinaryReader fileReader = new BinaryReader(palFile.Open(FileMode.Open));
                for (int i = 0; i < 256; i++)
                {
                    // note - DF pal colour range is from 0 to 63, so multiply by 4
                    this.Colours[i].R = (fileReader.ReadByte() * 4);
                    this.Colours[i].G = (fileReader.ReadByte() * 4);
                    this.Colours[i].B = (fileReader.ReadByte() * 4);
                }

                fileReader.Close();
                fileReader.Dispose();
            }
            catch (IOException)
            {
                return false;
            }

            return true;
        }
    }

    public struct PalColour
    {
        public PalColour(byte Red, byte Green, byte Blue)
        {
            this.R = Red;
            this.G = Green;
            this.B = Blue;
        }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }
}

