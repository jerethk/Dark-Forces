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
                saveBMPDialog.FileName = Path.GetFileNameWithoutExtension(openWaxDialog.FileName);
                saveBMPDialog.ShowDialog();
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
                MessageBox.Show("This utility was written by Jereth, one of the crusty old members of the Code Alliance. Find me on the DF-21 Discord.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.ClickedItem == MenuHelphelp)
            {
                HelpWindow helpwin = new HelpWindow();
                helpwin.ShowDialog();
                helpwin.Close();
                helpwin.Dispose();
            }
        }

        private void openPalDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (palette.LoadfromFile(openPalDialog.FileName))
            {
                LabelPal.Text = $"{openPalDialog.FileName}";
                MenuOpenWax.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error loading PAL. The file may not have been a valid PAL file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openWaxDialog_FileOk(object sender, CancelEventArgs e)
        {
            wax = new Waxfile();
            if (wax.LoadFromFile(openWaxDialog.FileName, palette))
            {
                labelWax.Text = openWaxDialog.FileName;
                RadioGroup.Enabled = true;
                SeqFrame = 0;
                ActionNumber.Value = 0;
                ViewNumber.Value = 0;
                SeqNumber.Value = 0;
                FrameNumber.Value = 0;
                CellNumber.Value = 0;
                CellNumber.Maximum = wax.Ncells - 1;
                FrameNumber.Maximum = wax.Nframes - 1;
                SeqNumber.Maximum = wax.Nseqs - 1;
                ActionNumber.Maximum = wax.numActions - 1;
                radioAction.Checked = false;
                radioAction.Checked = true;

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

            /*****************************************************************************/
            //output.Text = wax.numActions.ToString();
           /*****************************************************************************/
        }

        // ---------------------------------------------------------------------------------------------------------

        private void radioAction_CheckedChanged(object sender, EventArgs e)
        {
            UpdateActionInfo();

            ActionNumber.Value = 0;

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
            FrameNumber.Value = 0;

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
            // display Action info
            string[] strings = new string[5];
            strings[0] = $"Wwidth: {wax.Actions[(int)ActionNumber.Value].Wwidth}";
            strings[1] = $"Wheight: {wax.Actions[(int)ActionNumber.Value].Wheight}";
            strings[2] = $"Framerate: {wax.Actions[(int)ActionNumber.Value].FrameRate}";
            //strings[3] = $"{wax.Actions[(int)ActionNumber.Value].Nframes}";
            //strings[4] = $"{wax.Actions[(int)ActionNumber.Value].pad2} {wax.Actions[(int)ActionNumber.Value].pad3} {wax.Actions[(int)ActionNumber.Value].pad4}";
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
            SeqFrame = 0;
            
            UpdateSeqInfo();
        }

        private void SeqNumber_ValueChanged(object sender, EventArgs e)
        {
            int thisSequence = (int) SeqNumber.Value;
            int thisFrame = wax.Sequences[thisSequence].frameIndexes[0];

            FrameNumber.Value = thisFrame;
            UpdateSeqInfo();
        }

        private void UpdateSeqInfo()
        {
            string[] s = new string[3];
            s[0] = $"This sequence has {wax.Sequences[(int) SeqNumber.Value].numFrames} frames";
            //s[2] = $"{wax.Sequences[(int)SeqNumber.Value].pad1} {wax.Sequences[(int)SeqNumber.Value].pad2} {wax.Sequences[(int)SeqNumber.Value].pad3} {wax.Sequences[(int)SeqNumber.Value].pad4}";
            SeqInfo.Lines = s;

            UpdateFrame();
        }

        private void SeqNextFrame_Click(object sender, EventArgs e)
        {
            int thisSequence = (int) SeqNumber.Value;
            int maxFrame = wax.Sequences[thisSequence].numFrames - 1;

            if (SeqFrame < maxFrame)
            {
                SeqFrame++;
                int thisFrame = wax.Sequences[thisSequence].frameIndexes[SeqFrame];
                FrameNumber.Value = thisFrame;
                UpdateFrame();
            }
        }

        private void SeqPrevFrame_Click(object sender, EventArgs e)
        {
            int thisSequence = (int)SeqNumber.Value;

            if (SeqFrame > 0)
            {
                SeqFrame--;
                int thisFrame = wax.Sequences[thisSequence].frameIndexes[SeqFrame];
                FrameNumber.Value = thisFrame;
                UpdateFrame();
            }
        }

        /*
        private void UpdateSeqFrame()
        {
            int thisSequence = (int)SeqNumber.Value;
            //int thisSequence = wax.Actions[(int)ActionNumber.Value].seqIndexes[(int)ViewNumber.Value];
            int thisFrame = wax.Sequences[thisSequence].frameIndexes[(int)SeqFrameNumber.Value];

            FrameNumber.Value = thisFrame;
            UpdateFrame();
        } */

        // ---------------------------------------------------------------------------------------------------------

        private void FrameNumber_ValueChanged_1(object sender, EventArgs e)
        {
            UpdateFrame();
        }

        private void UpdateFrame()
        {
            string[] strings = new string[7];
            strings[0] = $"InsertX: {wax.Frames[(int)FrameNumber.Value].InsertX}";
            strings[1] = $"InsertY: {wax.Frames[(int)FrameNumber.Value].InsertY}";
            strings[2] = $"Flip: {wax.Frames[(int)FrameNumber.Value].Flip}";
            strings[3] = $"Cell Address: 0x{Convert.ToString(wax.Frames[(int)FrameNumber.Value].CellAddress, 16)}";
            //strings[4] = $"{wax.Frames[(int)FrameNumber.Value].UnitWidth} {wax.Frames[(int)FrameNumber.Value].UnitHeight}";
            //strings[5] = $"{wax.Frames[(int)FrameNumber.Value].pad3} {wax.Frames[(int)FrameNumber.Value].pad4}";
            FrameInfo.Lines = strings;

            CellNumber.Value = wax.Frames[(int)FrameNumber.Value].CellIndex;
            UpdateCell();
        }

        private void CellNumber_ValueChanged(object sender, EventArgs e)
        {
            UpdateCell();
        }

        private void UpdateCell()
        {
            string[] strings = new string[8];
            strings[0] = $"SizeX: {wax.Cells[(int)CellNumber.Value].SizeX}";
            strings[1] = $"SizeY: {wax.Cells[(int)CellNumber.Value].SizeY}";
            strings[2] = $"Compressed: {wax.Cells[(int)CellNumber.Value].Compressed}";
            strings[3] = $"DataSize: {wax.Cells[(int)CellNumber.Value].DataSize}";
            strings[4] = $"ColOffs: {wax.Cells[(int)CellNumber.Value].ColOffs}";
            //strings[5] = $"{wax.Cells[(int)CellNumber.Value].pad1}";
            //strings[6] = $"ADDRESS: 0x{Convert.ToString(wax.Cells[(int)CellNumber.Value].address, 16)}";
            CellInfo.Lines = strings;

            displayBox.Image = wax.Cells[(int)CellNumber.Value].bitmap;
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
            if (wax.exportToBMP(saveBMPDialog.FileName))
            {
                MessageBox.Show("Successfully saved BMPs.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error saving BMPs.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
