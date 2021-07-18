using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WAX_converter
{
    public partial class BuildWindow : Form
    {
        public BuildWindow()
        {
            this.InitializeComponent();

            palette = new DFPal();
            ImageList = new List<Bitmap>();
            FrameList = new List<Frame>();
            SequenceList = new List<Sequence>();
            ActionList = new List<Action>();

            transparentColour = Color.FromArgb(255, 0, 0, 0);
            transpColourBox.BackColor = transparentColour;
        }

        private DFPal palette;
        private List<Bitmap> ImageList;
        private List<Frame> FrameList;
        private List<Sequence> SequenceList;
        private List<Action> ActionList;
        private Color transparentColour;

        private string[] logicAnim = new string[1] {"0 Animation"};
        private string[] logicScenery = new string[2] { "0 Start", "1 Destroyed" };
        private string[] logicEnemy = new string[13] { "0 Moving", "1 Attack", "2 Dying1", "3 Dying2", "4 Dead", "5 Standing", "6 Recoil", "7 Attack2", "8 Recoil2", "9 Kell jump", "10 not used", "11 not used", "12 Pain" };
        private string[] logicDT = new string[14] { "0 Moving", "1 Attack", "2 Dying1", "3 Dying2", "4 Dead", "5 Standing", "6 Recoil", "7 Attack2", "8 Recoil2", "9 not used", "10 not used", "11 not used", "12 Pain", "13 Blocking / flying" };
        private string[] logicRemote = new string[4] { "0 Searching", "1 Inactive", "2 Dying1", "3 Dying2" };

        // -------------------------------------------------------------------

        private void BuildWindow_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < 14; i++)    // create 14 actions, which is the maximum needed
            {
                ActionList.Add(new Action());
            }

            comboBoxLogic.SelectedIndex = 0;

            dataGridViews.Rows.Add(32);
            for (int i = 0; i < 32; i++)
            {
                dataGridViews.Rows[i].Cells[0].Value = i;
                dataGridViews.Rows[i].Cells[1].Value = 0;
            }
        }
        
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            DialogResult choice = MessageBox.Show("Are you sure you want to leave? Your work will be lost!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (choice == DialogResult.Yes)
            {
                this.Close();
                this.Dispose();
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpWindow hlpwin = new HelpWindow();
            hlpwin.ShowDialog();
        }

        private void ButtonPal_Click(object sender, EventArgs e)
        {
            openPalDialog.ShowDialog();
        }

        private void openPalDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (palette.LoadfromFile(openPalDialog.FileName))
            {
                labelPal.Text = Path.GetFileName(openPalDialog.FileName);
            }
            else
            {
                MessageBox.Show("Error loading PAL. The file may not have been a valid PAL file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxIlluminated_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIlluminated.Checked)
            {
                MessageBox.Show("Glow-in-dark colours (PAL indexes 1-31) will be included when performing colour matching.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonTransparent_Click(object sender, EventArgs e)
        {
            Bitmap img;
            if (ImageList.Count >= 1)
            {
                img = ImageList[0];     // send the first image to the colour picker
            }
            else
            {
                img = null;
            }
            
            ColourChooser TransparentDialog = new ColourChooser(img, this.transparentColour);
            TransparentDialog.ShowDialog();
            this.transparentColour = TransparentDialog.chosenColour;
            transpColourBox.BackColor = this.transparentColour;
            TransparentDialog.Dispose();
        }

        // --- CELL AREA --------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void listboxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ImageList.Count >= 1 && listboxImages.SelectedIndex >= 0)
            {
                displayBox2.Image = ImageList[listboxImages.SelectedIndex];
            }
        }

        private void ButtonAddImage_Click(object sender, EventArgs e)
        {
            loadImageDialog.ShowDialog();
        }

        private void loadImageDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                foreach (string fname in loadImageDialog.FileNames)
                {
                    Bitmap newImage = new Bitmap(fname);
                    ImageList.Add(newImage);

                    listboxImages.Items.Add("Cell " + listboxImages.Items.Count);
                }
                
                listboxImages.SelectedIndex = listboxImages.Items.Count - 1;
                labelNCells.Text = "n = " + ImageList.Count;
                buttonAddFrame.Enabled = true;
                if (SequenceList.Count == 0) buttonRemoveFrame.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load image!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            
        }

        private void ButtonRemoveImage_Click(object sender, EventArgs e)
        {
            if (ImageList.Count > 0)
            {
                int index = listboxImages.SelectedIndex;

                if (index > 0)
                {
                    listboxImages.SelectedIndex -= 1;       // move the selection up 1 unless at position 0
                }

                ImageList.RemoveAt(index);
                listboxImages.Items.RemoveAt(listboxImages.Items.Count - 1);
                labelNCells.Text = "n = " + ImageList.Count;

            }

            if (ImageList.Count == 0)
            {
                displayBox2.Image = null;
            }
            else
            {
                displayBox2.Image = ImageList[listboxImages.SelectedIndex];
            }
        }

        private void ButtonMoveUp_Click(object sender, EventArgs e)
        {
            int index = listboxImages.SelectedIndex;
            
            if (index > 0)
            {
                ImageList.Reverse(index - 1, 2);
                listboxImages.SelectedIndex -= 1;
            }
        }

        private void ButtonMoveDown_Click(object sender, EventArgs e)
        {
            int index = listboxImages.SelectedIndex;
            
            if (index < ImageList.Count - 1)
            {
                ImageList.Reverse(index, 2);
                listboxImages.SelectedIndex += 1;
            }
        }

// --- FRAME AREA --------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void listboxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listboxFrames.SelectedIndex >= 0)
            {
                listboxImages.SelectedIndex = FrameList[listboxFrames.SelectedIndex].CellIndex;
                textBoxCell.Text = FrameList[listboxFrames.SelectedIndex].CellIndex.ToString();
                InsertX.Value = FrameList[listboxFrames.SelectedIndex].InsertX;
                InsertY.Value = FrameList[listboxFrames.SelectedIndex].InsertY;

                if (FrameList[listboxFrames.SelectedIndex].Flip == 1)
                {
                    checkBoxFlip.Checked = true;
                }
                else
                {
                    checkBoxFlip.Checked = false;
                }
            }
        }

        private void buttonAddFrame_Click(object sender, EventArgs e)
        {
            if (ImageList.Count > 0)
            {
                MessageBox.Show("Select the cells to turn into frames, then press DONE. Multiselect using CTRL and SHIFT keys.");
                btnDoneAddingFrames.Visible = true;
                listboxImages.SelectionMode = SelectionMode.MultiExtended;    // allow multiselection

                ButtonMoveUp.Enabled = false;
                ButtonMoveDown.Enabled = false;
                ButtonRemoveImage.Enabled = false;
                ButtonAddImage.Enabled = false;
                panel1.Enabled = false;
                panel3.Enabled = false;
                panel5.Enabled = false;
                panel6.Enabled = false;
                panel7.Enabled = false;
            }
        }

        private void btnDoneAddingFrames_Click(object sender, EventArgs e)
        {
            if (listboxImages.SelectedItems.Count > 0)
            {
                // makes all selected cells into frames
                foreach (object o in listboxImages.SelectedItems)
                {
                    int selectedCell = listboxImages.Items.IndexOf(o);

                    Frame newFrame = new Frame();
                    newFrame.CellIndex = selectedCell;
                    newFrame.InsertX = ImageList[selectedCell].Width / 2 * -1;      // set sensible default values based on size of image
                    newFrame.InsertY = ImageList[selectedCell].Height * -1;
                    newFrame.Flip = 0;

                    FrameList.Add(newFrame);
                    listboxFrames.Items.Add(listboxFrames.Items.Count);
                    listboxFrames.SelectedIndex = listboxFrames.Items.Count - 1;
                }

                InsertX.Enabled = true;
                InsertY.Enabled = true;
                checkBoxFlip.Enabled = true;
                buttonAddSequence.Enabled = true;
                buttonRemoveSequence.Enabled = true;
                labelNFrames.Text = "n = " + FrameList.Count;
            }

            // once Frames have been added, disable ability to move & remove cells
            if (FrameList.Count == 0)
            {
                ButtonMoveUp.Enabled = true;
                ButtonMoveDown.Enabled = true;
                ButtonRemoveImage.Enabled = true;
            }

            ButtonAddImage.Enabled = true;
            panel1.Enabled = true;
            panel3.Enabled = true;
            panel5.Enabled = true;
            panel6.Enabled = true;
            panel7.Enabled = true;
            btnDoneAddingFrames.Visible = false;
            listboxImages.SelectionMode = SelectionMode.One;
        }

        private void buttonRemoveFrame_Click(object sender, EventArgs e)
        {
            int index = listboxFrames.SelectedIndex;

            if (FrameList.Count > 0 && index >= 0)
            {
                if (index > 0)
                {
                    listboxFrames.SelectedIndex -= 1;       // move the selection up 1 unless at position 0
                }

                FrameList.RemoveAt(index);
                listboxFrames.Items.RemoveAt(listboxFrames.Items.Count - 1);
                labelNFrames.Text = "n = " + FrameList.Count;
            }

            if (FrameList.Count == 0)
            {
                ButtonMoveUp.Enabled = true;
                ButtonMoveDown.Enabled = true;
                ButtonRemoveImage.Enabled = true;
                InsertX.Enabled = false;
                InsertY.Enabled = false;
                checkBoxFlip.Enabled = false;
            }
        }

        private void InsertX_ValueChanged(object sender, EventArgs e)
        {
            int selectedFrame = listboxFrames.SelectedIndex;
            FrameList[selectedFrame].InsertX = (int) InsertX.Value;
        }

        private void InsertY_ValueChanged(object sender, EventArgs e)
        {
            int selectedFrame = listboxFrames.SelectedIndex;
            FrameList[selectedFrame].InsertY = (int)InsertY.Value;
        }

        private void checkBoxFlip_CheckedChanged(object sender, EventArgs e)
        {
            int selectedFrame = listboxFrames.SelectedIndex;
            
            if (checkBoxFlip.Checked)
            {
                FrameList[selectedFrame].Flip = 1;
            }
            else
            {
                FrameList[selectedFrame].Flip = 0;
            } 
        }

// --- SEQUENCE AREA --------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void listboxSeqs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SequenceList.Count > 0)
            {
                listboxSeqFrames.DataSource = SequenceList[listboxSeqs.SelectedIndex].frameIndexes;
            }
        }

        private void buttonAddSequence_Click(object sender, EventArgs e)
        {
            if (FrameList.Count > 0)
            {
                Sequence newSeq = new Sequence();
                SequenceList.Add(newSeq);
                listboxSeqs.Items.Add(listboxSeqs.Items.Count);
                listboxSeqs.SelectedIndex = listboxSeqs.Items.Count - 1;

                listboxSeqFrames.Enabled = true;
                buttonSetFrame.Enabled = true;
                buttonClearFrame.Enabled = true;
                buttonRemoveFrame.Enabled = false;
                labelNSeqs.Text = "n = " + SequenceList.Count;

                dataGridViews.Enabled = true;
                buttonSetAllViews.Enabled = true;
                Wwidth.Enabled = true;
                Wheight.Enabled = true;
                FRate.Enabled = true;
            }
        }

        private void buttonRemoveSequence_Click(object sender, EventArgs e)
        {
            int index = listboxSeqs.SelectedIndex;

            if (SequenceList.Count > 1 && index >= 0)
            {
                DialogResult answer = MessageBox.Show("The entire selected sequence will be deleted. Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer == DialogResult.Yes)
                {
                    if (index > 0)
                    {
                        listboxSeqs.SelectedIndex -= 1;       // move the selection up 1 unless at position 0
                    }

                    SequenceList.RemoveAt(index);
                    listboxSeqs.Items.RemoveAt(listboxSeqs.Items.Count - 1);

                    labelNSeqs.Text = "n = " + SequenceList.Count;

                    // Go through all the actions and re-index every sequence after the removed sequence, then update the datagrid
                    for (int a = 0; a < 14; a++)
                    {
                        for (int v = 0; v < 32; v++)
                        {
                            int value = ActionList[a].seqIndexes[v];
                            if (value > index || value >= SequenceList.Count)
                            {
                                ActionList[a].seqIndexes[v] -= 1;
                            }
                        }
                    }

                    for (int i = 0; i < 32; i++)
                    {
                        dataGridViews.Rows[i].Cells[1].Value = ActionList[comboBoxAction.SelectedIndex].seqIndexes[i];
                    }
                }
            }
        }

        private void listboxSeqFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listboxSeqFrames.Items.Count > 0)
            {
                int thisSequence = listboxSeqs.SelectedIndex;
                int selectedFrame = SequenceList[thisSequence].frameIndexes[listboxSeqFrames.SelectedIndex];

                if (selectedFrame >= 0)
                {
                    listboxFrames.SelectedIndex = selectedFrame;
                }
            }
        }

        private void buttonSetFrame_Click(object sender, EventArgs e)
        {
            if (listboxSeqs.SelectedIndex >= 0)
            {
                MessageBox.Show("Select the frames to assign to this sequence, then press DONE. Multiselect using CTRL and SHIFT keys. To cancel, press DONE with no frames selected.");
                btnDoneSettingFrames.Visible = true;
                listboxFrames.SelectionMode = SelectionMode.MultiExtended;    // allow multiselection

                buttonAddFrame.Enabled = false;
                buttonRemoveFrame.Enabled = false;
                InsertX.Enabled = false;
                InsertY.Enabled = false;
                checkBoxFlip.Enabled = false;
                panel1.Enabled = false;
                panel2.Enabled = false;
                panel5.Enabled = false;
                panel6.Enabled = false;
                panel7.Enabled = false;
            }
        }

        private void btnDoneSettingFrames_Click(object sender, EventArgs e)
        {
            int nFrames = listboxFrames.SelectedItems.Count;
            
            if (nFrames > 0)
            {
                int selectedSequence = listboxSeqs.SelectedIndex;
                if (nFrames > 32) nFrames = 32;

                for (int f = 0; f < nFrames; f++)
                {
                    Object o = listboxFrames.SelectedItems[f];
                    int selectedFrame = listboxFrames.Items.IndexOf(o);
                    SequenceList[selectedSequence].frameIndexes[f] = selectedFrame;
                }
                
                for  (int f = nFrames; f < 32; f++)
                {
                    SequenceList[selectedSequence].frameIndexes[f] = -1;
                }

                listboxSeqFrames.DataSource = new int[32];   // for some reason, need to do this to update the listbox!
                listboxSeqFrames.DataSource = SequenceList[listboxSeqs.SelectedIndex].frameIndexes;
                listboxSeqFrames.SelectedIndex = 0;
            }

            btnDoneSettingFrames.Visible = false;
            listboxFrames.SelectionMode = SelectionMode.One;

            buttonAddFrame.Enabled = true;
            buttonRemoveFrame.Enabled = false;
            InsertX.Enabled = true;
            InsertY.Enabled = true;
            checkBoxFlip.Enabled = true;
            panel1.Enabled = true;
            panel2.Enabled = true;
            panel5.Enabled = true;
            panel6.Enabled = true;
            panel7.Enabled = true;
        }

        private void buttonClearFrame_Click(object sender, EventArgs e)
        {
            int selectedSequence = listboxSeqs.SelectedIndex;

            if (selectedSequence >= 0)
            {
                for (int i = 31; i >= 0; i--)   // count backwards until the last frame in sequence
                {
                    if (SequenceList[selectedSequence].frameIndexes[i] != -1)
                    {
                        SequenceList[selectedSequence].frameIndexes[i] = -1;
                        listboxSeqFrames.DataSource = new int[32];   // for some reason, need to do this to update the listbox!
                        listboxSeqFrames.DataSource = SequenceList[listboxSeqs.SelectedIndex].frameIndexes;
                        break;
                    }
                }
            }
        }

// --- ACTIONS AREA --------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void comboBoxLogic_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] strings;

            // Change available actions based on logic
            switch (comboBoxLogic.SelectedIndex)
            {
                case 0:
                    strings = logicAnim;
                    break;
                case 1:
                    strings = logicScenery;
                    break;
                case 2:
                    strings = logicEnemy;
                    break;
                case 3:
                    strings = logicDT;
                    break;
                case 4:
                    strings = logicRemote;
                    break;
                default:
                    strings = new string[1] { "" };
                    break;
            }

            comboBoxAction.DataSource = strings;
        }

        private void comboBoxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxAction.SelectedIndex;
            
            Wwidth.Value = ActionList[index].Wwidth;
            Wheight.Value = ActionList[index].Wheight;
            FRate.Value = ActionList[index].FrameRate;

            for (int i = 0; i < 32; i++)
            {
                dataGridViews.Rows[i].Cells[1].Value = ActionList[index].seqIndexes[i];
            }

            if (SequenceList.Count > 0)
            {
                int seq = ActionList[index].seqIndexes[0];
                listboxSeqs.SelectedIndex = seq;
            }

        }

        /*
        private void ViewNumber_ValueChanged(object sender, EventArgs e)
        {
            int action = comboBoxAction.SelectedIndex;
            int view = (int) ViewNumber.Value;
            int seq = ActionList[action].seqIndexes[view];

            ViewSequence.Value = seq;
        }

        private void ViewSequence_ValueChanged(object sender, EventArgs e)
        {
            int action = comboBoxAction.SelectedIndex;
            int view = (int)ViewNumber.Value;
            int viewSeq = (int) ViewSequence.Value;

            ActionList[action].seqIndexes[view] = viewSeq;
            listboxSeqs.SelectedIndex = viewSeq;
        }*/

        private void dataGridViews_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // only allow an integer value
            int newValue;
            bool validEntry = Int32.TryParse(e.FormattedValue.ToString(), out newValue);

            if (!validEntry)
            {
                e.Cancel = true;
            }
        }

        private void dataGridViews_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                int newValue = Int32.Parse(dataGridViews.Rows[e.RowIndex].Cells[1].Value.ToString());

                if (newValue < 0 || SequenceList.Count == 0)
                {
                    newValue = 0;
                }
                else if (newValue >= SequenceList.Count)
                {
                    newValue = SequenceList.Count - 1;
                }

                dataGridViews.Rows[e.RowIndex].Cells[1].Value = newValue;

                int action = comboBoxAction.SelectedIndex;
                int view = e.RowIndex;
                ActionList[action].seqIndexes[view] = newValue;
            }

        }

        private void dataGridViews_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (SequenceList.Count > 0)
            {
                int action = comboBoxAction.SelectedIndex;
                int view = e.RowIndex;
                int seq = ActionList[action].seqIndexes[view];
                listboxSeqs.SelectedIndex = seq;
            }
        }

        private void buttonSetAllViews_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("This will set all 32 views for this action to the selected sequence. Continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
            if (answer == DialogResult.Yes)
            {
                int action = comboBoxAction.SelectedIndex;
                int seq = listboxSeqs.SelectedIndex;

                for (int v = 0; v < 32; v++)
                {
                    ActionList[action].seqIndexes[v] = seq;
                    dataGridViews.Rows[v].Cells[1].Value = seq;
                }
            }
        }

        private void Wwidth_ValueChanged(object sender, EventArgs e)
        {
            ActionList[comboBoxAction.SelectedIndex].Wwidth = (int) Wwidth.Value;
        }

        private void Wheight_ValueChanged(object sender, EventArgs e)
        {
            ActionList[comboBoxAction.SelectedIndex].Wheight = (int)Wheight.Value;
        }

        private void FRate_ValueChanged(object sender, EventArgs e)
        {
            ActionList[comboBoxAction.SelectedIndex].FrameRate = (int)FRate.Value;
        }


// --- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void buttonCreateWAX_Click(object sender, EventArgs e)
        {
            if (ImageList.Count < 1)
            {
                MessageBox.Show("You need to add cells!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (SequenceList.Count < 1)
            {
                MessageBox.Show("You need to add sequences!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (palette == null)
            {
                MessageBox.Show("Palette not loaded!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                saveWaxDialog.ShowDialog();
            }
        }

        private void saveWaxDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Waxfile newWax = WaxBuilder.BuildWax(comboBoxLogic.SelectedIndex, ActionList, SequenceList, FrameList, ImageList, palette, transparentColour, checkBoxIlluminated.Checked, checkBoxCompress.Checked);

            if (newWax.save(saveWaxDialog.FileName, checkBoxCompress.Checked))
            {
                MessageBox.Show("Successfully saved!", "WAX saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to save new WAX!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            newWax = null;
        }


// --- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void buttonSaveWIP_Click(object sender, EventArgs e)
        {
            saveWIPDialog.ShowDialog();
        }

        private void saveWIPDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool success = true;
            string FileNameNoExt = Path.GetFileNameWithoutExtension(saveWIPDialog.FileName);
            string Dir = Path.GetDirectoryName(saveWIPDialog.FileName);
            string cellImageDirectory = Dir + "/" + FileNameNoExt;
            StreamWriter writer = null;

            try
            {
                // save images as PNG in subfolder
                Directory.CreateDirectory(cellImageDirectory);
                for (int i = 0; i < ImageList.Count; i++)
                {
                    string imageSavePath = cellImageDirectory + "/" + i + ".png";
                    ImageList[i].Save(imageSavePath, System.Drawing.Imaging.ImageFormat.Png);
                }

                // save rest of stuff in Text file
                writer = new StreamWriter(saveWIPDialog.FileName, false);  // overwrite
                writer.WriteLine("# WAX work-in-progress file.");
                writer.WriteLine("# Be very careful if you edit this file manually.");
                writer.WriteLine("");

                writer.WriteLine($"Cells {ImageList.Count}");
                writer.WriteLine("");

                writer.WriteLine($"Frames {FrameList.Count}");
                for (int f = 0; f < FrameList.Count; f++)
                {
                    writer.WriteLine($"{FrameList[f].CellIndex} {FrameList[f].InsertX} {FrameList[f].InsertY} {FrameList[f].Flip}");
                }
                writer.WriteLine("");

                writer.WriteLine($"Sequences {SequenceList.Count}");
                for (int s = 0; s < SequenceList.Count; s++)
                {
                    string str = "";
                    for (int f = 0; f < 32; f++)
                    {
                        str += SequenceList[s].frameIndexes[f] + " ";
                    }
                    writer.WriteLine(str);
                }
                writer.WriteLine("");

                writer.WriteLine($"Actions");
                writer.WriteLine($"LogicType {comboBoxLogic.SelectedIndex}");

                for (int a = 0; a < ActionList.Count; a++)
                {
                    writer.WriteLine($"{ActionList[a].Wwidth} {ActionList[a].Wheight} {ActionList[a].FrameRate}");

                    string str = "";
                    for (int v = 0; v < 32; v++)
                    {
                        str += ActionList[a].seqIndexes[v] + " ";
                    }
                    writer.WriteLine(str);
                }
                writer.WriteLine("");

                writer.Close();
                writer.Dispose();
            }
            catch (IOException)
            {
                success = false;
                writer.Close();
                writer.Dispose();
                MessageBox.Show("Error saving WIP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (success)
            {
                MessageBox.Show($"Work in progress saved as {Path.GetFileName(saveWIPDialog.FileName)}. Cell images have been saved in {FileNameNoExt}\\ subdirectory.", "WIP saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

// --- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void buttonLoadWIP_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("You will lose any work you have already done here.", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (answer == DialogResult.OK)
            {
                openWIPDialog.ShowDialog();
            }
        }

        private void openWIPDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool success = true;
            
            string FileNameNoExt = Path.GetFileNameWithoutExtension(openWIPDialog.FileName);
            string Dir = Path.GetDirectoryName(openWIPDialog.FileName);
            string cellImageDirectory = Dir + "/" + FileNameNoExt;
            StreamReader fileReader = null;

            int numCells = 0;
            int numFrames = 0;
            int numSeqs = 0;
            int logicType = -1;
            List<Frame> loadedFrames = new List<Frame>();
            List<Sequence> loadedSeqs = new List<Sequence>();
            List<Action> loadedActions = new List<Action>();
            List<Bitmap> loadedBitmaps = new List<Bitmap>();

            try
            {
                // Read from WWIP text file
                fileReader = new StreamReader(openWIPDialog.FileName);
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
                       success = false;
                    }
                }
                
                if (!success)
                {
                    MessageBox.Show("Error: Image file(s) not found.");
                }
                else
                {
                    // load the bitmaps
                    for (int i = 0; i < numCells; i++)
                    {
                        Bitmap loadedBMP = new Bitmap(cellImageDirectory + "/" + i + ".png");
                        loadedBitmaps.Add(loadedBMP);
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
                        loadedFrames.Add(nextFrame);
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
                        loadedSeqs.Add(nextSequence);
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
                        loadedActions.Add(nextAction);
                    }

                    fileReader.Close();
                    fileReader.Dispose();

                    
                    // If all successful, load everything into the UI
                    ImageList = loadedBitmaps;
                    FrameList = loadedFrames;
                    SequenceList = loadedSeqs;
                    ActionList = loadedActions;

                    listboxImages.Items.Clear();
                    for (int i = 0; i < ImageList.Count; i++)
                    {
                        listboxImages.Items.Add("Cell " + i);
                    }
                    ButtonAddImage.Enabled = true;
                    ButtonRemoveImage.Enabled = false;
                    ButtonMoveUp.Enabled = false;
                    ButtonMoveDown.Enabled = false;

                    listboxFrames.Items.Clear();
                    for (int f = 0; f < FrameList.Count; f++)
                    {
                        listboxFrames.Items.Add(f.ToString());
                    }
                    if (FrameList.Count > 0)
                    {
                        listboxFrames.SelectedIndex = 0;
                    }
                    buttonAddFrame.Enabled = true;
                    buttonRemoveFrame.Enabled = false;
                    InsertX.Enabled = true;
                    InsertY.Enabled = true;
                    checkBoxFlip.Enabled = true;

                    listboxSeqs.Items.Clear();
                    for (int s = 0; s < SequenceList.Count; s++)
                    {
                        listboxSeqs.Items.Add(s.ToString());
                    }
                    if (SequenceList.Count > 0)
                    {
                        listboxSeqFrames.Enabled = true;
                    }
                    buttonAddSequence.Enabled = true;
                    buttonRemoveSequence.Enabled = true;

                    labelNCells.Text = $"n = {ImageList.Count}";
                    labelNFrames.Text = $"n = {FrameList.Count}";
                    labelNSeqs.Text = $"n = {SequenceList.Count}";
                    comboBoxLogic.SelectedIndex = 0;    //
                    comboBoxLogic.SelectedIndex = 1;    // changing this forces an update of the controls
                    comboBoxLogic.SelectedIndex = logicType;
                    dataGridViews.Enabled = true;
                    buttonSetAllViews.Enabled = true;
                    Wwidth.Enabled = true;
                    Wheight.Enabled = true;
                    FRate.Enabled = true;
                    buttonCreateWAX.Enabled = true;
                }
            }
            catch (Exception)
            {
                success = false;
                fileReader.Close();
                fileReader.Dispose();
            }

            if (!success)
            {
                MessageBox.Show("Failed to load. The WIP file(s) may have been corrupted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Nested method to get the next line of data as an array of strings; skips blank and comment lines
            string[] getNextLine(StreamReader reader)
            {
                bool emptyLine = true;
                string nextRawLine = "";

                while (emptyLine)
                {
                    nextRawLine = reader.ReadLine();
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

                string[] result = nextRawLine.Split();
                return result;
            }
        }

        
        // -----------


    }


}
