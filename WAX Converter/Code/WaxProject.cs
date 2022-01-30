using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAX_converter
{
    // Class for saving and loading project files
    static class WaxProject
    {
        public static bool Save(string FileName, int LogicType, List<Action> Actions, List<Sequence> Sequences, List<Frame> Frames, int numImages)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FileName, false))
                {
                    writer.WriteLine("# WAX project file.");
                    writer.WriteLine("# Be very careful if you edit this file manually.");
                    writer.WriteLine("");

                    writer.WriteLine($"Cells {numImages}");
                    writer.WriteLine("");

                    // Write frame data
                    writer.WriteLine($"Frames {Frames.Count}");
                    for (int f = 0; f < Frames.Count; f++)
                    {
                        writer.WriteLine($"{Frames[f].CellIndex} {Frames[f].InsertX} {Frames[f].InsertY} {Frames[f].Flip}");
                    }
                    writer.WriteLine("");

                    // Write sequence data
                    writer.WriteLine($"Sequences {Sequences.Count}");
                    for (int s = 0; s < Sequences.Count; s++)
                    {
                        string str = "";
                        for (int f = 0; f < 32; f++)
                        {
                            str += Sequences[s].frameIndexes[f] + " ";
                        }
                        writer.WriteLine(str);
                    }
                    writer.WriteLine("");

                    // Write action data
                    writer.WriteLine($"Actions");
                    writer.WriteLine($"LogicType {LogicType}");

                    for (int a = 0; a < Actions.Count; a++)
                    {
                        writer.WriteLine($"{Actions[a].Wwidth} {Actions[a].Wheight} {Actions[a].FrameRate}");

                        string str = "";
                        for (int v = 0; v < 32; v++)
                        {
                            str += Actions[a].seqIndexes[v] + " ";
                        }
                        writer.WriteLine(str);
                    }

                    // write blank actions as necessary (always put 14 actions in a project file)
                    for (int a = Actions.Count; a < 14; a++) 
                    {
                        writer.WriteLine("70000 70000 1");
                        writer.WriteLine("0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0");
                    }

                    writer.WriteLine("");
                }
            }
            catch (IOException e)
            {
                MessageBox.Show($"IOException {e.Message}");
                return false;
            }

            return true;
        }

        public static bool Load(string FileName, out int logicType, List<Action> Actions, List<Sequence> Sequences, List<Frame> Frames, List<Bitmap> Images)
        {
            string FileNameNoExtension = Path.GetFileNameWithoutExtension(FileName);
            string Dir = Path.GetDirectoryName(FileName);
            string cellImageDirectory = Dir + "/" + FileNameNoExtension;

            logicType = 0;
            int numCells = 0;
            int numFrames = 0;
            int numSeqs = 0;

            try
            {
                // Read from project text file
                using (StreamReader fileReader = new StreamReader(FileName))
                {
                    string[] nextLine = new string[0];

                    // cells
                    bool foundCellInfo = false;
                    while (!foundCellInfo)
                    {
                        nextLine = getNextLine(fileReader);
                        if (nextLine[0] == "Cells") foundCellInfo = true;
                    }
                    numCells = Int32.Parse(nextLine[1]);

                    // check the images exist; abort if they don't
                    for (int i = 0; i < numCells; i++)
                    {
                        if (!File.Exists(cellImageDirectory + "/" + i + ".png"))
                        {
                            MessageBox.Show("Error: Image file(s) not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    // load the bitmaps
                    for (int i = 0; i < numCells; i++)
                    {
                        Bitmap loadedBMP = new Bitmap(cellImageDirectory + "/" + i + ".png");
                        Images.Add(loadedBMP);
                    }

                    // frames
                    bool foundFrameInfo = false;
                    while (!foundFrameInfo)
                    {
                        nextLine = getNextLine(fileReader);
                        if (nextLine[0] == "Frames") foundFrameInfo = true;
                    }
                    numFrames = Int32.Parse(nextLine[1]);

                    for (int f = 0; f < numFrames; f++)
                    {
                        nextLine = getNextLine(fileReader);

                        Frame nextFrame = new Frame();
                        nextFrame.CellIndex = Int32.Parse(nextLine[0]);
                        nextFrame.InsertX = Int32.Parse(nextLine[1]);
                        nextFrame.InsertY = Int32.Parse(nextLine[2]);
                        nextFrame.Flip = Int32.Parse(nextLine[3]);
                        Frames.Add(nextFrame);
                    }

                    // Sequences
                    bool foundSeqInfo = false;
                    while (!foundSeqInfo)
                    {
                        nextLine = getNextLine(fileReader);
                        if (nextLine[0] == "Sequences") foundSeqInfo = true;
                    }
                    numSeqs = Int32.Parse(nextLine[1]);

                    for (int s = 0; s < numSeqs; s++)
                    {
                        nextLine = getNextLine(fileReader);

                        Sequence nextSequence = new Sequence();
                        for (int f = 0; f < 32; f++)
                        {
                            nextSequence.frameIndexes[f] = Int32.Parse(nextLine[f]);
                        }
                        Sequences.Add(nextSequence);
                    }

                    // actions
                    bool foundActionInfo = false;
                    while (!foundActionInfo)
                    {
                        nextLine = getNextLine(fileReader);
                        if (nextLine[0] == "Actions") foundActionInfo = true;
                    }

                    nextLine = getNextLine(fileReader);
                    if (nextLine[0] == "LogicType")
                    {
                        logicType = Int32.Parse(nextLine[1]);
                    }

                    for (int a = 0; a < 14; a++)
                    {
                        Action nextAction = new Action();

                        nextLine = getNextLine(fileReader);
                        nextAction.Wwidth = Int32.Parse(nextLine[0]);
                        nextAction.Wheight = Int32.Parse(nextLine[1]);
                        nextAction.FrameRate = Int32.Parse(nextLine[2]);

                        nextLine = getNextLine(fileReader);
                        for (int v = 0; v < 32; v++)
                        {
                            nextAction.seqIndexes[v] = Int32.Parse(nextLine[v]);
                        }
                        Actions.Add(nextAction);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Exception: {e.Message}");
                return false;
            }

            return true;


            // Nested method to get the next line of data as an array of strings; skips blank and comment lines
            string[] getNextLine(StreamReader reader)
            {
                bool emptyLine = true;
                string nextRawLine = "";

                try
                {
                    while (emptyLine)
                    {
                        nextRawLine = reader.ReadLine();
                        if (nextRawLine == null) return null;

                        nextRawLine = nextRawLine.Trim();       // trim spaces from start and end

                        if (nextRawLine == "")      // empty line so skip it
                        {
                            emptyLine = true;
                        }
                        else if (nextRawLine[0] == '#')    // comment line so skip it
                        {
                            emptyLine = true;
                        }
                        else
                        {
                            emptyLine = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Exception occurred: {e.Message}");
                }

                string[] result = nextRawLine.Split();
                return result;
            }
        }

    }
}
