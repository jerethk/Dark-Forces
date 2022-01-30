using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAX_converter
{
    public partial class SequenceBuilderWindow : Form
    {
        private List<Bitmap> ImageList;
        private List<Frame> FrameList;
        private Sequence Sequence;
        private int[] backupFrameIndexes;
        
        public SequenceBuilderWindow(List<Bitmap> images, List<Frame> frames, Sequence seq)
        {
            InitializeComponent();

            this.ImageList = images;
            this.FrameList = frames;
            this.Sequence = seq;

            // Assign backup sequence (to be used if 
            this.backupFrameIndexes = new int[32];
            for (int i = 0; i < 32; i++)
            {
                backupFrameIndexes[i] = this.Sequence.frameIndexes[i];
            }
        }

        private void SequenceBuilderWindow_Load(object sender, EventArgs e)
        {
            for (int f = 0; f < FrameList.Count; f++)
            {
                listBoxFrames.Items.Add(f.ToString());
            }

            listBoxSeqFrames.DataSource = new BindingSource(Sequence.frameIndexes, "");
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Sequence.frameIndexes = this.backupFrameIndexes;
            this.Close();
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

        // ListBox controls ----------------------------------------------------------------------------

        private void listBoxSeqFrames_Enter(object sender, EventArgs e)
        {
            // Turn off multi-select in Frame listbox
            listBoxFrames.SelectionMode = SelectionMode.One;
        }

        private void listBoxFrames_Enter(object sender, EventArgs e)
        {
            // Turn ON multi-select in Frame listbox
            listBoxFrames.SelectionMode = SelectionMode.MultiExtended;
        }

        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxFrames.SelectedIndex;

            if (index >= 0)
            {
                int cellIndex = FrameList[index].CellIndex;

                displayBox.Image = ImageList[cellIndex];
            }
            else
            {
                displayBox.Image = null;
            }
        }

        private void listBoxSeqFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxSeqFrames.SelectedIndex;

            if (index >= 0)
            {
                listBoxFrames.SelectedIndex = this.Sequence.frameIndexes[index];
            }
        }

        // Edit buttons ----------------------------------------------------------------------------

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (listBoxFrames.SelectedIndex >= 0)
            {
                // find the first empty frame in sequence
                int startPosition = 0;
                
                for (int sf = 0; sf < 32; sf++)
                {
                    if (this.Sequence.frameIndexes[sf] == -1)
                    {
                        startPosition = sf;
                        break;
                    }
                    else if (sf == 31)  // every frame is filled
                    {
                        return;
                    }
                }

                // Add the selected frames
                int nextPosition = startPosition;
                foreach (int i in listBoxFrames.SelectedIndices)
                {
                    this.Sequence.frameIndexes[nextPosition] = i;
                    nextPosition += 1;

                    if (nextPosition == 32) break;
                }

                listBoxSeqFrames.DataSource = new BindingSource(Sequence.frameIndexes, "");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = listBoxSeqFrames.SelectedIndex;
            int frameNumber = this.Sequence.frameIndexes[index];
            
            if (frameNumber >= 0)
            {
                var answer = MessageBox.Show($"Are you sure you want to remove frame {frameNumber} from the sequence?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (answer == DialogResult.Yes)
                {
                    // shift subsequent frames up
                    for (int i = index; i < 32; i++)
                    {
                        if (i == 31)
                        {
                            this.Sequence.frameIndexes[i] = -1;
                        }
                        else
                        {
                            this.Sequence.frameIndexes[i] = this.Sequence.frameIndexes[i + 1];
                        }
                    }

                    listBoxSeqFrames.DataSource = new BindingSource(Sequence.frameIndexes, "");
                }
            }
        }

        private void btnClearSeq_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Are you sure you want to clear this entire sequence?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer == DialogResult.Yes)
            {
                for (int i = 0; i < 32; i++)
                {
                    this.Sequence.frameIndexes[i] = -1;
                }

                listBoxSeqFrames.DataSource = new BindingSource(Sequence.frameIndexes, "");
            }
        }

    }
}
