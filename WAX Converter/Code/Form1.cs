using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAX_converter
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            palette = new DFPal();
        }

        private Waxfile wax;
        private DFPal palette;
        private int SeqFrame = 0;       // the sequence frame currently being viewed

        private void menuFile_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == MenuOpenWax)
            {
                openWaxDialog.ShowDialog();
            }
            else if (e.ClickedItem == MenuCloseWax)
            {
                closeWax();
            }
            else if (e.ClickedItem == MenuLoadPal)
            {
                openPalDialog.ShowDialog();
            }
            else if (e.ClickedItem == MenuSaveBMP)
            {
                saveBMPDialog.ShowDialog();
            }
            else if (e.ClickedItem == MenuOpenFme)
            {
                openFmeDialog.ShowDialog();
            }
            else if (e.ClickedItem == MenuQuit)
            {
                Application.Exit();
            }
        }

        private void menuBuildWax_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == MenuBuild)
            {
                BuildWindow buildWindow = new BuildWindow();
                buildWindow.Show();
            }
        }

        private void menuHelp_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == MenuAbout)
            {
                MessageBox.Show("This utility was written by Jereth K. Find me on the DF-21 Discord.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.ClickedItem == MenuHelphelp)
            {
                HelpWindow helpwin = new HelpWindow();
                helpwin.ShowDialog();
                helpwin.Close();
                helpwin.Dispose();
            }
        }

        // -------------------------------------------------------------------------------------

        private void openPalDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (palette.LoadfromFile(openPalDialog.FileName))
            {
                LabelPal.Text = $"{Path.GetFileName(openPalDialog.FileName)}";
            }
            else
            {
                MessageBox.Show("Error loading PAL. The file may not have been a valid PAL file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openWaxDialog_FileOk(object sender, CancelEventArgs e)
        {
            Waxfile tryOpenWax = new Waxfile();
            if (tryOpenWax.LoadFromFile(openWaxDialog.FileName, palette))
            {
                this.wax = tryOpenWax;
                saveBMPDialog.FileName = Path.GetFileNameWithoutExtension(openWaxDialog.FileName);

                // remove event handlers (to prevent exceptions when resetting values)
                this.ActionNumber.ValueChanged -= this.ActionNumber_ValueChanged;
                this.ViewNumber.ValueChanged -= this.ViewNumber_ValueChanged;
                this.SeqNumber.ValueChanged -= this.SeqNumber_ValueChanged;
                this.FrameNumber.ValueChanged -= this.FrameNumber_ValueChanged_1;

                labelWax.Text = Path.GetFileName(openWaxDialog.FileName);
                RadioGroup.Enabled = true;
                panel1.Enabled = true;
                SeqFrame = 0;
                ActionNumber.Value = 0;
                ViewNumber.Value = 0;
                SeqNumber.Value = 0;
                FrameNumber.Value = 0;
                CellNumber.Value = 0;
                ActionNumber.Maximum = wax.numActions - 1;
                SeqNumber.Maximum = wax.Nseqs - 1;
                FrameNumber.Maximum = wax.Nframes - 1;
                CellNumber.Maximum = wax.Ncells - 1;
                radioAction.Checked = false;
                radioAction.Checked = true;

                // add event handlers
                this.ActionNumber.ValueChanged += new System.EventHandler(this.ActionNumber_ValueChanged);
                this.ViewNumber.ValueChanged += new System.EventHandler(this.ViewNumber_ValueChanged);
                this.SeqNumber.ValueChanged += new System.EventHandler(this.SeqNumber_ValueChanged);
                this.FrameNumber.ValueChanged += new System.EventHandler(this.FrameNumber_ValueChanged_1);

                // display Wax details
                string[] strings = new string[9];
                strings[0] = $"Version: 0x{Convert.ToString(wax.Version, 16)}";
                strings[1] = $"Num actions: {wax.numActions}";
                strings[2] = $"Num sequences: {wax.Nseqs}";
                strings[3] = $"Num frames: {wax.Nframes}";
                strings[4] = $"Num cells: {wax.Ncells}";
                //strings[5] = $"Xscale: {wax.Xscale}, Yscale: {wax.Yscale}";
                //strings[6] = $"XtraLight: {wax.XtraLight}";
                //strings[7] = $"{wax.pad4}";
                WaxDetails.Lines = strings;

                UpdateActionInfo();
                UpdateSeqInfo();
                UpdateFrame();
                MenuCloseWax.Enabled = true;
                MenuSaveBMP.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error loading WAX file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------------------------------------------------------------------------------------------------

        private void radioAction_CheckedChanged(object sender, EventArgs e)
        {
            UpdateActionInfo();

            //ActionNumber.Value = 0;

            ActionNumber.Enabled = true;
            ViewNumber.Enabled = true;
            SeqNumber.Enabled = false;
            SeqNextFrame.Enabled = true;
            SeqPrevFrame.Enabled = true;
            FrameNumber.Enabled = false;
            CellNumber.Enabled = false;
        }

        private void radioSequence_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSeqInfo();
            UpdateFrame();

            ActionInfo.Text = "";
            ActionNumber.Enabled = false;
            ViewNumber.Enabled = false;
            SeqNumber.Enabled = true;
            SeqNextFrame.Enabled = true;
            SeqPrevFrame.Enabled = true;
            FrameNumber.Enabled = false;
            CellNumber.Enabled = false;
        }

        private void radioFrame_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFrame();

            ActionInfo.Text = "";
            SeqInfo.Text = "";

            ActionNumber.Enabled = false;
            ViewNumber.Enabled = false;
            SeqNumber.Enabled = false;
            SeqNextFrame.Enabled = false;
            SeqPrevFrame.Enabled = false;
            FrameNumber.Enabled = true;
            CellNumber.Enabled = false;
        }

        private void radioCell_CheckedChanged(object sender, EventArgs e)
        {
            ActionInfo.Text = "";
            SeqInfo.Text = "";
            FrameInfo.Text = "";

            ActionNumber.Enabled = false;
            ViewNumber.Enabled = false;
            SeqNextFrame.Enabled = false;
            SeqPrevFrame.Enabled = false;
            SeqNumber.Enabled = false;
            FrameNumber.Enabled = false;
            CellNumber.Enabled = true;
        }


        // ---------------------------------------------------------------------------------------------------------

        private void ActionNumber_ValueChanged(object sender, EventArgs e)
        {
            UpdateActionInfo();
        }

        private void UpdateActionInfo()
        {
            int thisAction = (int)ActionNumber.Value;

            // display Action info
            string[] strings = new string[5];
            strings[0] = $"Wwidth: {wax.Actions[thisAction].Wwidth}";
            strings[1] = $"Wheight: {wax.Actions[thisAction].Wheight}";
            strings[2] = $"Framerate: {wax.Actions[thisAction].FrameRate}";
            //strings[3] = $"{wax.Actions[thisAction].Nframes}";
            //strings[4] = $"{wax.Actions[thisAction].pad2} {wax.Actions[thisAction].pad3} {wax.Actions[thisAction].pad4}";
            ActionInfo.Lines = strings;

            ViewNumber.Value = 0;
            UpdateView();
        }

        private void ViewNumber_ValueChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            int thisSequence = wax.Actions[(int)ActionNumber.Value].seqIndexes[(int)ViewNumber.Value];
            SeqNumber.Value = thisSequence;

            UpdateSeqInfo();
        }

        private void SeqNumber_ValueChanged(object sender, EventArgs e)
        {
            UpdateSeqInfo();
        }

        private void UpdateSeqInfo()
        {
            if (wax.Sequences.Count > 0)
            {
                int thisSequence = (int)SeqNumber.Value;
                int thisFrame = wax.Sequences[thisSequence].frameIndexes[0];
                FrameNumber.Value = thisFrame;
                SeqFrame = 0;
                labelSeqFrame.Text = SeqFrame.ToString();

                string[] s = new string[3];
                s[0] = $"This sequence has {wax.Sequences[thisSequence].numFrames} frames";
                //s[2] = $"{wax.Sequences[thisSequence].pad1} {wax.Sequences[thisSequence].pad2} {wax.Sequences[thisSequence].pad3} {wax.Sequences[thisSequence].pad4}";
                SeqInfo.Lines = s;

                UpdateFrame();
            }
        }

        private void SeqNextFrame_Click(object sender, EventArgs e)
        {
            int thisSequence = (int)SeqNumber.Value;
            int maxFrame = wax.Sequences[thisSequence].numFrames - 1;

            if (SeqFrame < maxFrame)
            {
                SeqFrame++;
            }
            else
            {
                SeqFrame = 0;
            }

            labelSeqFrame.Text = SeqFrame.ToString();
            int thisFrame = wax.Sequences[thisSequence].frameIndexes[SeqFrame];
            FrameNumber.Value = thisFrame;
            UpdateFrame();
        }

        private void SeqPrevFrame_Click(object sender, EventArgs e)
        {
            int thisSequence = (int)SeqNumber.Value;

            if (SeqFrame > 0)
            {
                SeqFrame--;
            }
            else
            {
                SeqFrame = wax.Sequences[thisSequence].numFrames - 1;
            }

            labelSeqFrame.Text = SeqFrame.ToString();
            int thisFrame = wax.Sequences[thisSequence].frameIndexes[SeqFrame];
            FrameNumber.Value = thisFrame;
            UpdateFrame();
        }

        // ---------------------------------------------------------------------------------------------------------

        private void FrameNumber_ValueChanged_1(object sender, EventArgs e)
        {
            UpdateFrame();
        }

        private void UpdateFrame()
        {
            int thisFrame = (int)FrameNumber.Value;

            string[] strings = new string[7];
            strings[0] = $"InsertX: {wax.Frames[thisFrame].InsertX}";
            strings[1] = $"InsertY: {wax.Frames[thisFrame].InsertY}";
            strings[2] = $"Flip: {wax.Frames[thisFrame].Flip}";
            strings[3] = $"Cell Address: 0x{Convert.ToString(wax.Frames[thisFrame].CellAddress, 16)}";
            //strings[4] = $"{wax.Frames[thisFrame].UnitWidth} {wax.Frames[thisFrame].UnitHeight}";
            //strings[5] = $"{wax.Frames[thisFrame].pad3} {wax.Frames[thisFrame].pad4}";
            FrameInfo.Lines = strings;

            CellNumber.Value = wax.Frames[thisFrame].CellIndex;
            UpdateCell();
        }

        private void CellNumber_ValueChanged(object sender, EventArgs e)
        {
            UpdateCell();
        }

        private void UpdateCell()
        {
            int thisCell = (int)CellNumber.Value;

            string[] strings = new string[8];
            strings[0] = $"SizeX: {wax.Cells[thisCell].SizeX}";
            strings[1] = $"SizeY: {wax.Cells[thisCell].SizeY}";
            strings[2] = $"Compressed: {wax.Cells[thisCell].Compressed}";
            strings[3] = $"DataSize: {wax.Cells[thisCell].DataSize}";
            //strings[4] = $"ColOffs: {wax.Cells[thisCell].ColOffs}";
            //strings[5] = $"{wax.Cells[thisCell].pad1}";
            //strings[6] = $"ADDRESS: 0x{Convert.ToString(wax.Cells[thisCell].address, 16)}";
            CellInfo.Lines = strings;

            displayBox.Image = wax.Cells[thisCell].bitmap;
        }

        // ---------------------------------------------------------------------------------------------------------

        private void closeWax()
        {
            wax = null;
            labelWax.Text = "";
            RadioGroup.Enabled = false;
            ActionNumber.Enabled = false;
            ViewNumber.Enabled = false;
            SeqNumber.Enabled = false;
            SeqNextFrame.Enabled = false;
            SeqPrevFrame.Enabled = false;
            FrameNumber.Enabled = false;
            CellNumber.Enabled = false;
            WaxDetails.Text = "";
            ActionInfo.Text = "";
            SeqInfo.Text = "";
            FrameInfo.Text = "";
            CellInfo.Text = "";
            displayBox.Image = null;
            MenuCloseWax.Enabled = false;
            MenuSaveBMP.Enabled = false;
        }

        private void saveBMPDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (wax.exportToPNG(saveBMPDialog.FileName))
            {
                MessageBox.Show("Successfully saved PNGs.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error saving PNGs.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxZoom.Checked)
            {
                displayBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                displayBox.SizeMode = PictureBoxSizeMode.Normal;
            }
        }

        private void openFmeDialog_FileOk(object sender, CancelEventArgs e)
        {
            // Load a FME file as a Waxfile object
            Waxfile tryOpenFme = new Waxfile();

            if (tryOpenFme.LoadFromFME(openFmeDialog.FileName, palette))
            {
                this.wax = tryOpenFme;
                saveBMPDialog.FileName = Path.GetFileNameWithoutExtension(openFmeDialog.FileName);

                // remove event handlers (to prevent exceptions when resetting values)
                this.ActionNumber.ValueChanged -= this.ActionNumber_ValueChanged;
                this.ViewNumber.ValueChanged -= this.ViewNumber_ValueChanged;
                this.SeqNumber.ValueChanged -= this.SeqNumber_ValueChanged;
                this.FrameNumber.ValueChanged -= this.FrameNumber_ValueChanged_1;

                // display FME details
                string[] strings = new string[2];
                strings[0] = $"FME file";
                WaxDetails.Lines = strings;

                panel1.Enabled = false;
                RadioGroup.Enabled = false;
                MenuSaveBMP.Enabled = true;
                FrameNumber.Value = 0;
                FrameNumber.Maximum = 0;
                CellNumber.Value = 0;
                CellNumber.Maximum = 0;
                labelWax.Text = Path.GetFileName(openFmeDialog.FileName);
                ActionInfo.Text = "";
                SeqInfo.Text = "";
                UpdateFrame();
            }
            else
            {
                MessageBox.Show("Error loading FME file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


