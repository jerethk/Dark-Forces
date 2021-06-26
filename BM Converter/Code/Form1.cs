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

namespace BM_Converter
{
    public partial class Form1 : Form
    {
        DFPal palette;
        DFBM BM;
        List<Bitmap> Images;
        int SubBMSelected = 0;

        public Form1()
        {
            InitializeComponent();

            palette = new DFPal();
            BM = new DFBM();
            Images = new List<Bitmap>();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This app is © 2021 Jereth K.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Load PAL  -------------------------------------------------------------------------
        private void BtnLoadPAL_Click(object sender, EventArgs e)
        {
            OpenPALDialog.ShowDialog();
        }
        
        private void OpenPALDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (this.palette.LoadfromFile(OpenPALDialog.FileName))
            {
                MessageBox.Show("PAL Loaded.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                labelPal.Text = $"PAL: {Path.GetFileName(OpenPALDialog.FileName)}";
                BtnLoadBM.Enabled = true;
                btnBulkConvert.Enabled = true;
            } 
            else
            {
                MessageBox.Show("Error. May not have been a valid PAL file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load BM  -------------------------------------------------------------------------
        private void BtnLoadBM_Click(object sender, EventArgs e)
        {
            OpenBMDialog.ShowDialog();
        }

        private void OpenBMDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (!BM.loadFromFile(OpenBMDialog.FileName))
            {
                MessageBox.Show("Error loading BM file. Please check that it is a valid DF BM file and not open in another program.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // successful load!
            {
                // Display Info
                string transparency;
                switch (BM.transparent) {
                    case 0x36:
                        transparency = "Non-transparent";
                        break;
                    case 0x3E:
                        transparency = "Transparent";
                        break;
                    case 0x08:
                        transparency = "Weapon";
                        break;
                    default:
                        transparency = "Error - unknown";
                        break;
                }

                string[] s = new string[18];
                s[0] = Path.GetFileName(OpenBMDialog.FileName);
                s[2] = $"SizeX: {BM.SizeX}";
                s[3] = $"SizeY: {BM.SizeY}";
                s[4] = $"Compressed: {BM.compressed}";
                s[5] = $"Transparency: {transparency}";

                if (BM.multiBM)
                {
                    s[6] = $"Number of sub BMs: {BM.numImages}";
                    s[7] = $"Frame rate: {BM.FrameRate}";
                    //s[8] = $"{BM.SecondByte}";
                }

                s[10] = $"idemX: {BM.idemX.ToString()}";
                s[11] = $"idemY: {BM.idemY.ToString()}";
                s[12] = $"logSizeY: {BM.logSizeY.ToString()}";
                s[13] = $"DataSize: {BM.DataSize.ToString()}";
                textBoxBMInfo.Lines = s;

                // Convert image(s) to Bitmap(s) and store in List
                Images.Clear();
                if (!BM.multiBM)
                {
                    Bitmap newBitmap = DFBM.BMtoBitmap(BM.SizeX, BM.SizeY, BM.PixelData, palette);
                    Images.Add(newBitmap);
                    displayBox.Image = Images[0];
                }
                else
                {
                    for (int i = 0; i < BM.numImages; i++)
                    {
                        Bitmap newBitmap = DFBM.BMtoBitmap(BM.SubBMs[i].SizeX, BM.SubBMs[i].SizeY, BM.SubBMs[i].PixelData, palette);
                        Images.Add(newBitmap);
                    }
                }

                // Set up multi BM interface
                SubBMSelected = 0;
                if (BM.multiBM)
                {
                    btnPrevSub.Enabled = true;
                    btnNextSub.Enabled = true;
                    displayBox.Image = Images[0];
                    UpdateSubBMInfo();
                }
                else
                {
                    btnPrevSub.Enabled = false;
                    btnNextSub.Enabled = false;
                    textBoxSubBMInfo.Text = null;
                }

                btnExport.Enabled = true;
            }
        }
        
        private void checkBoxZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxZoom.Checked)
            {
                displayBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else displayBox.SizeMode = PictureBoxSizeMode.Normal;
        }

        // Multi BM Interface ------------------------------------------------------------
        private void btnPrevSub_Click(object sender, EventArgs e)
        {
            if (SubBMSelected > 0)
            {
                SubBMSelected--;
                UpdateSubBMInfo();
            }
        }

        private void btnNextSub_Click(object sender, EventArgs e)
        {
            if (SubBMSelected < BM.numImages - 1)
            {
                SubBMSelected++;
                UpdateSubBMInfo();
            }
        }

        private void UpdateSubBMInfo()
        {
            int a = SubBMSelected;

            string transparency;
            switch (BM.SubBMs[a].transparent)
            {
                case 0x36:
                    transparency = "Non-transparent";
                    break;
                case 0x3E:
                    transparency = "Transparent";
                    break;
                case 0x08:
                    transparency = "Weapon";
                    break;
                default:
                    transparency = "Error - unknown";
                    break;
            }

            string[] s = new string[10];
            s[0] = $"Sub BM {SubBMSelected + 1} of {BM.numImages}";
            s[1] = $"SizeX: {BM.SubBMs[a].SizeX}";
            s[2] = $"SizeY: {BM.SubBMs[a].SizeY}";
            s[3] = $"Transparency: {transparency}";

            s[5] = $"idemX: {BM.SubBMs[a].idemX}";
            s[6] = $"idemY: {BM.SubBMs[a].idemY}";
            s[7] = $"logSizeY: {BM.SubBMs[a].logSizeY}";
            s[8] = $"DataSize: {BM.SubBMs[a].DataSize}";
            textBoxSubBMInfo.Lines = s;

            displayBox.Image = Images[a];
        }

        // Export -------------------------------------------------------------------------------
        private void btnExport_Click(object sender, EventArgs e)
        {
            saveBMPDialog.FileName = Path.GetFileNameWithoutExtension(OpenBMDialog.FileName);
            saveBMPDialog.ShowDialog();
        }

        private void saveBMPDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (!BM.multiBM)
            {
                // single BM
                try
                {
                    Images[0].Save(saveBMPDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch (IOException)
                {
                    MessageBox.Show("Failed to export. An error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
            else
            {
                // multi BM
                string f = saveBMPDialog.FileName;
                string dir = Path.GetDirectoryName(f);
                string fil = Path.GetFileNameWithoutExtension(f);

                try
                {
                    for (int i = 0; i < BM.numImages; i++)
                    {
                        string saveName = $"{dir}/{fil}_{i}.png";
                        Images[i].Save(saveName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Failed to export. An error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Bulk convert  ------------------------------------------------------------------
        private void btnBulkConvert_Click(object sender, EventArgs e)
        {
            openBulkDialog.ShowDialog();
        }

        private void openBulkDialog_FileOk(object sender, CancelEventArgs e)
        {
            DialogResult answer = MessageBox.Show("This will convert all selected BMs into PNG files. It will skip any multi BMs that you selected. The PNGs will be saved in the same directory as the BMs. Proceed?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (answer == DialogResult.OK)
            {
                int numSuccessful = 0;
                int numFailed = 0;
                
                foreach (string BMFile in openBulkDialog.FileNames)
                {
                    string dir = Path.GetDirectoryName(BMFile);
                    string fname = Path.GetFileNameWithoutExtension(BMFile);
                    string exportFile = $"{dir}/{fname}.png";

                    DFBM ConvertingBM = new DFBM();
                    if (ConvertingBM.loadFromFile(BMFile))
                    {
                        if (ConvertingBM.multiBM)
                        {
                            // multi BM so skip it. Don't increment either success or fail counter
                            continue;
                        }
                        else
                        {
                            Bitmap bitmap = DFBM.BMtoBitmap(ConvertingBM.SizeX, ConvertingBM.SizeY, ConvertingBM.PixelData, palette);

                            try
                            {
                                bitmap.Save(exportFile, System.Drawing.Imaging.ImageFormat.Png);
                                numSuccessful++;
                            }
                            catch (IOException)
                            {
                                // something went wrong with output
                                numFailed++;
                            }
                        }
                    }
                    else
                    {
                        // that BM failed to load
                        numFailed++;
                    }
                }

                MessageBox.Show($"Successfully converted {numSuccessful} BMs. {numFailed} BMs failed to convert.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Create BM ------------------------------------------------------------------------------
        private void btnCreateBM_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("Not yet implemented!", "!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            Form2 BMCreator = new Form2();
            BMCreator.Show();
        }
    }
}
