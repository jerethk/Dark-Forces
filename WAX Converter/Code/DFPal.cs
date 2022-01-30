using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WAX_converter
{
    public class DFPal
    {
        public DFPal()
        {
            this.Colours = new PalColour[256];

            // Loads secbase.pal as default palette
            string secbasePal = "0 0 0 252 252 252 208 236 252 168 220 252 124 204 252 84 192 252 252 0 0 204 0 0 144 0 0 68 0 0 0 252 0 0 200 0 0 152 0 0 92 0 0 52 0 0 88 252 0 36 240 0 16 188 0 4 140 248 224 96 244 184 52 244 136 12 216 88 12 180 44 4 252 0 252 252 0 252 252 0 252 252 0 252 252 0 252 252 0 252 252 0 252 252 0 252 232 232 232 224 224 224 216 216 216 208 208 208 204 204 204 196 196 196 188 188 188 180 180 180 172 172 172 168 168 168 160 160 160 152 152 152 144 144 144 140 140 140 132 132 132 124 124 124 116 116 116 112 112 112 104 104 104 100 100 100 92 92 92 88 88 88 80 80 80 76 76 76 68 68 68 64 64 64 56 56 56 52 52 52 44 44 44 40 40 40 32 32 32 28 28 28 100 108 132 92 100 120 84 92 112 76 84 104 72 76 96 64 68 84 56 64 76 52 56 68 44 48 60 36 40 48 28 32 40 24 24 32 16 16 24 8 12 12 4 4 4 0 0 0 252 228 176 236 208 152 220 192 132 208 176 116 192 160 96 180 144 80 164 132 68 148 116 52 136 100 40 120 88 28 108 76 20 92 64 12 76 52 8 64 40 4 48 28 0 36 20 0 224 108 12 212 100 8 200 92 8 188 88 4 176 80 4 168 72 4 156 68 4 144 60 4 132 56 0 120 52 0 112 44 0 100 40 0 88 32 0 76 28 0 64 24 0 56 20 0 128 228 100 112 212 84 100 200 68 92 188 56 80 176 44 68 164 32 60 148 24 52 136 12 44 124 8 36 112 0 32 100 0 48 84 4 56 72 4 56 56 8 44 40 8 32 24 8 252 212 200 244 196 180 240 184 164 232 172 148 224 160 132 220 148 116 212 136 104 208 128 92 200 116 76 196 108 64 188 100 52 184 92 40 176 84 32 172 80 20 164 72 12 160 68 4 148 92 0 136 76 0 124 60 0 112 48 0 104 36 0 92 28 0 80 16 0 72 12 0 0 0 252 0 0 224 0 0 200 0 0 176 0 0 148 0 0 124 0 0 100 0 0 76 252 0 0 224 0 0 196 0 0 168 0 0 140 0 0 112 0 0 84 0 0 56 0 0 252 128 0 224 108 0 200 92 0 176 76 0 152 60 0 128 44 0 104 32 0 80 24 0 192 112 68 180 104 60 168 96 56 156 88 52 148 84 48 136 76 44 124 68 36 116 64 32 104 56 28 92 48 24 80 44 20 72 36 16 60 32 12 48 24 8 40 20 8 60 32 16 184 232 252 168 224 248 156 220 248 140 212 244 128 204 240 116 196 240 100 192 236 88 184 236 76 176 232 64 172 232 52 164 228 40 156 228 28 148 224 16 140 224 4 136 220 0 128 220 100 108 132 96 104 128 92 100 120 88 96 116 84 92 112 80 88 108 80 84 104 76 80 100 72 76 96 68 76 92 64 72 88 60 68 84 60 64 80 56 60 72 52 56 68 48 52 64 44 48 60 40 44 56 40 44 52 36 40 48 32 36 44 28 32 40 24 28 36 24 24 32 20 20 24 16 16 20 12 12 16 8 8 12 4 8 8 4 4 4 0 0 0 0 0 0 0 0 144 0 0 132 0 0 120 0 0 112 0 0 100 0 0 88 0 0 80 0 0 68 0 0 60 0 0 48 0 0 36 0 0 28 0 0 16 0 0 4 0 0 0 252 252 252";
            string[] palArray = secbasePal.Split();

            for (int i = 0; i < 256; i++)
            {
                this.Colours[i].R = Convert.ToInt32(palArray[(i * 3) + 0]);
                this.Colours[i].G = Convert.ToInt32(palArray[(i * 3) + 1]);
                this.Colours[i].B = Convert.ToInt32(palArray[(i * 3) + 2]);
            }
        }

        public PalColour[] Colours { get; set; }

        public bool LoadfromFile(string filename)
        {
            FileInfo palFile = new FileInfo(filename);

            // Check file is correct length
            if (palFile.Length != 768) {
                return false;
            }

            // Load Palette (PAL) file
            try
            {
                using (BinaryReader fileReader = new BinaryReader(palFile.Open(FileMode.Open)))
                {
                    for (int i = 0; i < 256; i++)
                    {
                        // note - DF pal colour range is from 0 to 63, so multiply by 4
                        this.Colours[i].R = (fileReader.ReadByte() * 4);
                        this.Colours[i].G = (fileReader.ReadByte() * 4);
                        this.Colours[i].B = (fileReader.ReadByte() * 4);
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show($"IOException {e.Message}");
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
