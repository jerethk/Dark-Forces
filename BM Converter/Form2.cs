using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BM_Converter
{
    public partial class Form2 : Form
    {
        List<Bitmap> SourceImages;
        DFPal palette;

        public Form2()
        {
            InitializeComponent();

            SourceImages = new List<Bitmap>();
            palette = new DFPal();
        }

        private void btnLoadPal_Click(object sender, EventArgs e)
        {
            openPALDialog.ShowDialog();
        }

        private void openPALDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (palette.LoadfromFile(openPALDialog.FileName))
            {
                labelPal.Text = $"PAL: {Path.GetFileName(openPALDialog.FileName)}";
                btnCreateBM.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error. May not have been a valid PAL file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxIncludeIlluminated_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIncludeIlluminated.Checked)
            {
                MessageBox.Show("Glow-in-dark colours (1-31) will be included when performing colour matching.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure you want to leave? You will lose any work you have done.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); 

            if (answer == DialogResult.Yes)
            {
                this.Close();
                this.Dispose();
            }
        }

        private void radioBtnSingleBM_Click(object sender, EventArgs e)
        {
            if (SourceImages.Count > 1)
            {
                MessageBox.Show("You have added multiple images!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                radioBtnMultiBM.Checked = true;
            }
        }
        
        private void radioBtnSingleBM_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxCompressed.Enabled = true;
            numericFramerate.Enabled = false;
            radioBtnWeapon.Enabled = true;
        }

        private void radioBtnMultiBM_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxCompressed.Enabled = false;
            checkBoxCompressed.Checked = false;
            numericFramerate.Enabled = true;
            numericFramerate.Value = 0;
            radioBtnOpaque.Checked = true;
            radioBtnWeapon.Enabled = false;
        }

        // Add & remove image ------------------------------------------------------------------------------------
        private void btnAddImage_Click(object sender, EventArgs e)
        {
            if (SourceImages.Count == 0 || radioBtnMultiBM.Checked)
            {
                LoadImageDialog.ShowDialog();
            }
        }

        private void LoadImageDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Bitmap newImage;
            
            try
            {
                newImage = new Bitmap(LoadImageDialog.FileName);
                bool proceed = false;

                // Check image wd&ht is power of 2
                if (!MiscFunctions.isPowerOfTwo(newImage.Width) || !MiscFunctions.isPowerOfTwo(newImage.Height))
                {
                    DialogResult answer = MessageBox.Show("Your image width or height is not a power of 2. This is only allowed for weapon textures. Continue?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (answer == DialogResult.Yes)
                    {
                        proceed = true;
                    }
                }
                else
                {
                    proceed = true;
                }

                if (proceed)
                {
                    SourceImages.Add(newImage);
                    listBoxImages.Items.Add(Path.GetFileName(LoadImageDialog.FileName));
                    listBoxImages.SelectedIndex = listBoxImages.Items.Count - 1;
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Error loading image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void listBoxImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxImages.SelectedIndex >= 0)
            {
                displayBox.Image = SourceImages[listBoxImages.SelectedIndex];
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            int idx = listBoxImages.SelectedIndex;

            if (SourceImages.Count > 0 && idx >= 0)
            {
                SourceImages.RemoveAt(idx);
                listBoxImages.Items.RemoveAt(idx);
                displayBox.Image = null;

                if (SourceImages.Count > 0 && idx > 0)
                {
                    listBoxImages.SelectedIndex = idx - 1;
                }
            }
        }

        // Create BM ------------------------------------------------------------------------
        private void btnCreateBM_Click(object sender, EventArgs e)
        {
            if (SourceImages.Count > 0)
            {
                saveBMDialog.ShowDialog();
            }
            
        }

        private void saveBMDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // pass transparency info as a char
            char transparency = 'o';    // default
            if (radioBtnTransparent.Checked)
            {
                transparency = 't';
            } 
            else if (radioBtnWeapon.Checked)
            {
                transparency = 'w';
            }

            DFBM newBM = MiscFunctions.buildBM(radioBtnMultiBM.Checked, palette, SourceImages, transparency, (byte) numericFramerate.Value, checkBoxIncludeIlluminated.Checked, checkBoxCompressed.Checked);
            
            if (newBM.SaveToFile(saveBMDialog.FileName))
            {
                MessageBox.Show("Successfully saved BM file!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error saving BM file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
