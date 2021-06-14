﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

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

        public bool multiBM { get; set; }       // custom fields for convenience
        public int fileLength { get; set; }    
        public int numImages { get; set; }     


        // Constructor        
        public DFBM()
        {
            FileId = new byte[4];
            this.FileId[0] = 0x42;
            this.FileId[1] = 0x4d;
            this.FileId[2] = 0x20;
            this.FileId[3] = 0x1e;

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
                        if (this.compressed == 0)
                        {
                            // Uncompressed BM
                            this.PixelData = new byte[this.SizeX, this.SizeY];

                            for (int x = 0; x < this.SizeX; x++)
                            {
                                for (int y= 0; y < this.SizeY; y++)
                                {
                                    this.PixelData[x, y] = reader.ReadByte();
                                }
                            }
                        }
                    }

                    result = true;
                }

                reader.Close();
                reader.Dispose();
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

            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bitmap;
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