using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Imaging;
using System.Text;


namespace WAX_converter
{
    /* WAX header = 160 bytes
     * WAX action = 156 bytes
     * WAX sequence = 144 bytes
     * WAX frame = 32 bytes
     * WAX cell = 24 bytes + data
     * */

    public class Waxfile : Object
    {
        // Constructor -------------------------
        public Waxfile()
        {
            this.Version = 0x11000;
            this.actionAddresses = new int[32];
            this.Actions = new List<Action>();
            this.Sequences = new List<Sequence>();
            this.Frames = new List<Frame>();
            this.Cells = new List<Cell>();
        }

        // Properties  ---------------------------
        public int Version { get; set; }
        public int Nseqs { get; set; }
        public int Nframes { get; set; }
        public int Ncells { get; set; }
        public int Xscale { get; set; }
        public int Yscale { get; set; }
        public int XtraLight { get; set; }
        public int pad4 { get; set; }
        public int[] actionAddresses { get; set; }
        
        public int numActions { get; set; }
        public List<Action> Actions { get; set; }
        public List<Sequence> Sequences { get; set; }
        public List<Frame> Frames { get; set; }
        public List<Cell> Cells { get; set; }

        // Methods ----------------------------------------------------
        public bool LoadFromFile(string filename, DFPal palette)
        {
            try
            {
                BinaryReader fileReader = new BinaryReader(File.Open(filename, FileMode.Open));

                // Read the header
                this.Version = fileReader.ReadInt32();
                this.Nseqs = fileReader.ReadInt32();
                this.Nframes = fileReader.ReadInt32();
                this.Ncells = fileReader.ReadInt32();
                this.Xscale = fileReader.ReadInt32();
                this.Yscale = fileReader.ReadInt32();
                this.XtraLight = fileReader.ReadInt32();
                this.pad4 = fileReader.ReadInt32();

                for (int i = 0; i < 32; i++)
                {
                    this.actionAddresses[i] = fileReader.ReadInt32();
                    if (this.actionAddresses[i] == 0)       // an address of 0 indicates the end of the action list
                    {
                        this.numActions = i;
                        break;
                    }
                }

                // Read the wax actions
                for (int i = 0; i < this.numActions; i++)
                {
                    Action action = new Action();
                    fileReader.BaseStream.Seek(this.actionAddresses[i], SeekOrigin.Begin);
                    action.Wwidth = fileReader.ReadInt32();
                    action.Wheight = fileReader.ReadInt32();
                    action.FrameRate = fileReader.ReadInt32();
                    action.Nframes = fileReader.ReadInt32();
                    action.pad2 = fileReader.ReadInt32();
                    action.pad3 = fileReader.ReadInt32();
                    action.pad4 = fileReader.ReadInt32();

                    for (int v = 0; v < 32; v++)
                    {
                        action.viewAddresses[v] = fileReader.ReadInt32();
                        action.seqIndexes[v] = (action.viewAddresses[v] - 160 - this.numActions * 156) / 144;       // address minus file header minus actions, divided by size of seq header
                    }

                    this.Actions.Add(action);
                }

                // Read the sequences
                fileReader.BaseStream.Seek(160 + this.numActions * 156, SeekOrigin.Begin);
                for (int s = 0; s < this.Nseqs; s++)
                {
                    Sequence seq = new Sequence();
                    seq.pad1 = fileReader.ReadInt32();
                    seq.pad2 = fileReader.ReadInt32();
                    seq.pad3 = fileReader.ReadInt32();
                    seq.pad4 = fileReader.ReadInt32();

                    for (int f = 0; f < 32; f++)
                    {
                        seq.frameAddresses[f] = fileReader.ReadInt32();
                        if (seq.frameAddresses[f] > 0)
                        {
                            seq.frameIndexes[f] = (seq.frameAddresses[f] - 160 - this.numActions * 156 - this.Nseqs * 144) / 32;      // address minus file header minus actions minus sequences, divided by size of frame
                        }
                        else            // address of 0 indicates no frame; set index to -1
                        {
                            seq.frameIndexes[f] = -1;
                        }
                    }

                    for (int f = 0; f < 32; f++)    // set the number of frames for this sequence by finding the first empty frame
                    {
                        if (seq.frameIndexes[f] == -1)
                        {
                            seq.numFrames = f;
                            break;
                        }
                    }

                    this.Sequences.Add(seq);
                }

                // Read the frames
                fileReader.BaseStream.Seek(160 + this.numActions * 156 + this.Nseqs * 144, SeekOrigin.Begin);
                for (int f = 0; f < this.Nframes; f++)
                {
                    Frame frame = new Frame();
                    frame.InsertX = fileReader.ReadInt32();
                    frame.InsertY = fileReader.ReadInt32();
                    frame.Flip = fileReader.ReadInt32();
                    frame.CellAddress = fileReader.ReadInt32();
                    frame.UnitWidth = fileReader.ReadInt32();
                    frame.UnitHeight = fileReader.ReadInt32();
                    frame.pad3 = fileReader.ReadInt32();
                    frame.pad4 = fileReader.ReadInt32();

                    this.Frames.Add(frame);
                }

                // Read the cells
                fileReader.BaseStream.Seek(160 + this.numActions * 156 + this.Nseqs * 144 + this.Nframes * 32, SeekOrigin.Begin);

                for (int c = 0; c < this.Ncells; c++)
                {
                    Cell Cell = new Cell();
                    Cell.address = (int) fileReader.BaseStream.Position;
                    Cell.SizeX = fileReader.ReadInt32();
                    Cell.SizeY = fileReader.ReadInt32();
                    Cell.Compressed = fileReader.ReadInt32();
                    Cell.DataSize = fileReader.ReadInt32();
                    Cell.ColOffs = fileReader.ReadInt32();
                    Cell.pad1 = fileReader.ReadInt32();

                    Cell.Pixels = new short[Cell.SizeX, Cell.SizeY];

                    // Read the image data
                    if (Cell.Compressed == 0)
                    {
                        // uncompressed
                        for (int x = 0; x < Cell.SizeX; x++)
                        {
                            for (int y = 0; y < Cell.SizeY; y++)
                            {
                                Cell.Pixels[x, y] = fileReader.ReadByte();
                            }
                        }
                    }
                    else if (Cell.Compressed == 1)
                    {
                        // read column offsets
                        Cell.columnOffsets = new int[Cell.SizeX];
                        for (int x = 0; x < Cell.SizeX; x++)
                        {
                            Cell.columnOffsets[x] = fileReader.ReadInt32();
                        }

                        // read compressed data
                        int compressedDataLength = Cell.DataSize - 24 - (Cell.SizeX * 4);   // DataSize minus header minus offset table
                        Cell.compressedData = new List<byte>();                        
                        
                        for (int i = 0; i < compressedDataLength; i++)
                        {
                            Cell.compressedData.Add(fileReader.ReadByte());
                        }

                        // uncompress data
                        Cell.uncompressImage();
                    }

                    // Create bitmap from cell image
                    Cell.createBitmap(palette /*, frame.Flip */);

                    this.Cells.Add(Cell);
                }

                fileReader.Close();
                fileReader.Dispose();

                // Assign cell index to each frame by matching to cell addresses
                for (int f = 0; f < this.Nframes; f++)
                {
                    for (int c = 0; c < this.Ncells; c++)
                    {
                        if (this.Frames[f].CellAddress == this.Cells[c].address)
                        {
                            this.Frames[f].CellIndex = c;
                            break;
                        }
                    }
                }
            }
            catch (IOException)
            {
                return false;
            }

            return true;
        }

        public bool exportToPNG(string filename)
        {
            try
            {
                string dir = Path.GetDirectoryName(filename);
                string baseFilename = Path.GetFileNameWithoutExtension(filename);

                for (int i = 0; i < this.Ncells; i++)
                {
                    string leadingZeroes = "";
                    if (i < 10)
                    {
                        leadingZeroes = "00";
                    }
                    else if (i >= 10 && i < 100)
                    {
                        leadingZeroes = "0";
                    }

                    string saveName = dir + "/" + baseFilename + leadingZeroes + i + ".PNG";
                    this.Cells[i].bitmap.Save(saveName, ImageFormat.Png);
                } 
            }
            catch (IOException)
            {
                return false;
            }

            return true;
        }

        public bool save(string filename, bool compress)
        {
            try
            {
                BinaryWriter fileWriter = new BinaryWriter(File.Open(filename, FileMode.Create));

                // write the header
                fileWriter.Write(this.Version);
                fileWriter.Write(this.Nseqs);
                fileWriter.Write(this.Nframes);
                fileWriter.Write(this.Ncells);
                fileWriter.Write(this.Xscale);
                fileWriter.Write(this.Yscale);
                fileWriter.Write(this.XtraLight);
                fileWriter.Write(this.pad4);

                for (int a = 0; a < 32; a++)
                {
                    fileWriter.Write(this.actionAddresses[a]);
                }

                // write the actions
                for (int a = 0; a < this.numActions; a++)
                {
                    fileWriter.Write(this.Actions[a].Wwidth);
                    fileWriter.Write(this.Actions[a].Wheight);
                    fileWriter.Write(this.Actions[a].FrameRate);
                    fileWriter.Write(this.Actions[a].Nframes);
                    fileWriter.Write(this.Actions[a].pad2);
                    fileWriter.Write(this.Actions[a].pad3);
                    fileWriter.Write(this.Actions[a].pad4);

                    for (int v = 0; v < 32; v++)
                    {
                        fileWriter.Write(this.Actions[a].viewAddresses[v]);
                    }
                }

                // write the sequences
                for (int s=0; s < this.Nseqs; s++)
                {
                    fileWriter.Write(this.Sequences[s].pad1);
                    fileWriter.Write(this.Sequences[s].pad2); 
                    fileWriter.Write(this.Sequences[s].pad3);
                    fileWriter.Write(this.Sequences[s].pad4);

                    for (int f = 0; f < 32; f++)
                    {
                        fileWriter.Write(this.Sequences[s].frameAddresses[f]);
                    }
                }

                // write the frames
                for (int f = 0; f < this.Nframes; f++)
                {
                    fileWriter.Write(this.Frames[f].InsertX);
                    fileWriter.Write(this.Frames[f].InsertY);
                    fileWriter.Write(this.Frames[f].Flip);
                    fileWriter.Write(this.Frames[f].CellAddress);
                    fileWriter.Write(this.Frames[f].UnitWidth);
                    fileWriter.Write(this.Frames[f].UnitHeight);
                    fileWriter.Write(this.Frames[f].pad3);
                    fileWriter.Write(this.Frames[f].pad4);
                }

                // write the cells
                for (int c = 0; c < this.Ncells; c++)
                {
                    fileWriter.Write(this.Cells[c].SizeX);
                    fileWriter.Write(this.Cells[c].SizeY);
                    fileWriter.Write(this.Cells[c].Compressed);
                    fileWriter.Write(this.Cells[c].DataSize);
                    fileWriter.Write(this.Cells[c].ColOffs);
                    fileWriter.Write(this.Cells[c].pad1);


                    if (compress)
                    {
                        foreach (int i in this.Cells[c].columnOffsets)
                        {
                            fileWriter.Write(i);
                        }

                        foreach (byte b in this.Cells[c].compressedData)
                        {
                            fileWriter.Write(b);
                        }
                    }
                    else    // uncompressed
                    {
                        for (int x = 0; x < this.Cells[c].SizeX; x++)
                        {
                            for (int y = 0; y < this.Cells[c].SizeY; y++)
                            {
                                byte b;
                                if (this.Cells[c].Pixels[x, y] == -1)
                                {
                                    b = 0;  // transparent = index 0
                                } 
                                else
                                {
                                    b = (byte)this.Cells[c].Pixels[x, y];
                                }
                                
                                fileWriter.Write(b);
                            }
                        }
                    }
                }

                fileWriter.Close();
                fileWriter.Dispose();
            }
            catch (IOException)
            {
                return false;
            }

            return true;
        }
    }
     
    public class Action
    {
        // Constructor
        public Action()
        {
            Wwidth = 65536;             // set default values
            Wheight = 65536;
            FrameRate = 6;
            Nframes = 0;
            pad2 = 0;
            pad3 = 0;
            pad4 = 0;
            viewAddresses = new int[32];      
            seqIndexes = new int[32];       
        }

        // Properties & fields
        public int Wwidth { get; set; }
        public int Wheight { get; set; }
        public int FrameRate { get; set; }
        public int Nframes { get; set; }
        public int pad2 { get; set; }
        public int pad3 { get; set; }
        public int pad4 { get; set; }
        public int[] viewAddresses { get; set; }    // address in WAX file for the 32 views

        public int[] seqIndexes { get; set; }       // sequence# corresponding to each view
    }

    public class Sequence
    {
        public Sequence()
        {
            frameAddresses = new int[32];       
            frameIndexes = new int[32];
            pad1 = 0;
            pad2 = 0;
            pad3 = 0;
            pad4 = 0;

            for (int i = 0; i < 32; i++)
            {
                this.frameIndexes[i] = -1;      // set to -1 by default (empty)
            }
        }

        // Properties & fields
        public int pad1 { get; set; }
        public int pad2 { get; set; }
        public int pad3 { get; set; }
        public int pad4 { get; set; }
        public int[] frameAddresses { get; set; }       // address in WAX file for frames (up to 32 per sequence)

        public int numFrames { get; set; }
        public int[] frameIndexes { get; set; }
    }

    public class Frame
    {
        public Frame()
        {
            UnitWidth = 0;
            UnitHeight = 0;
            pad3 = 0;
            pad4 = 0;
        }

        // Properties & fields
        public int InsertX {get; set;}
        public int InsertY { get; set; }
        public int Flip { get; set; }
        public int CellAddress { get; set; }        // address in WAX file
        public int UnitWidth { get; set; }
        public int UnitHeight { get; set; }
        public int pad3 { get; set; }
        public int pad4 { get; set; }

        public int CellIndex { get; set; }
    }

}
