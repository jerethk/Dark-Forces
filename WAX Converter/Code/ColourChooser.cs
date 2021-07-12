using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WAX_converter
{
    class ColourChooser : Form
    {
        public Color chosenColour { get; set; }
        
        private NumericUpDown numRed;
        private Label label1;
        private Label label2;
        private NumericUpDown numGreen;
        private NumericUpDown numBlue;
        private Label label3;
        private PictureBox colourBox;
        private Button button1;
        private PictureBox pictureBox1;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColourChooser));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numRed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numGreen = new System.Windows.Forms.NumericUpDown();
            this.numBlue = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.colourBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colourBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(13, 109);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(436, 360);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // numRed
            // 
            this.numRed.Location = new System.Drawing.Point(33, 16);
            this.numRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numRed.Name = "numRed";
            this.numRed.Size = new System.Drawing.Size(65, 23);
            this.numRed.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "R";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "G";
            // 
            // numGreen
            // 
            this.numGreen.Location = new System.Drawing.Point(140, 18);
            this.numGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numGreen.Name = "numGreen";
            this.numGreen.Size = new System.Drawing.Size(65, 23);
            this.numGreen.TabIndex = 4;
            // numBlue
            // 
            this.numBlue.Location = new System.Drawing.Point(252, 16);
            this.numBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numBlue.Name = "numBlue";
            this.numBlue.Size = new System.Drawing.Size(65, 23);
            this.numBlue.TabIndex = 6;
            
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "B";
            // 
            // colourBox
            // 
            this.colourBox.Location = new System.Drawing.Point(354, 16);
            this.colourBox.Name = "colourBox";
            this.colourBox.Size = new System.Drawing.Size(40, 40);
            this.colourBox.TabIndex = 7;
            this.colourBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(194, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 33);
            this.button1.TabIndex = 8;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ColourChooser
            // 
            this.ClientSize = new System.Drawing.Size(464, 481);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.colourBox);
            this.Controls.Add(this.numBlue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numGreen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numRed);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(480, 520);
            this.MinimumSize = new System.Drawing.Size(480, 520);
            this.Name = "ColourChooser";
            this.Text = "Choose transparent colour";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colourBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //-------------------------------------------------------------------------

        // Form constructor        
        public ColourChooser(Bitmap sampleImage, Color existingtransparentColour)
        {
            InitializeComponent();

            pictureBox1.Image = sampleImage;
            this.chosenColour = Color.FromArgb(255, existingtransparentColour.R, existingtransparentColour.G, existingtransparentColour.B);
            
            numRed.Value = chosenColour.R;
            numGreen.Value = chosenColour.G;
            numBlue.Value = chosenColour.B;
            colourBox.BackColor = chosenColour;

            // event handlers for RGB number spiners
            this.numRed.ValueChanged += new System.EventHandler(this.numRed_ValueChanged);
            this.numGreen.ValueChanged += new System.EventHandler(this.numGreen_ValueChanged);
            this.numBlue.ValueChanged += new System.EventHandler(this.numBlue_ValueChanged);            // 
        }

        private void numRed_ValueChanged(object sender, EventArgs e)
        {
            this.chosenColour = Color.FromArgb(255, (int)numRed.Value, (int)numGreen.Value, (int)numBlue.Value);
            colourBox.BackColor = chosenColour;
        }

        private void numGreen_ValueChanged(object sender, EventArgs e)
        {
            this.chosenColour = Color.FromArgb(255, (int)numRed.Value, (int)numGreen.Value, (int)numBlue.Value);
            colourBox.BackColor = chosenColour;

        }

        private void numBlue_ValueChanged(object sender, EventArgs e)
        {
            this.chosenColour = Color.FromArgb(255, (int)numRed.Value, (int)numGreen.Value, (int)numBlue.Value);
            colourBox.BackColor = chosenColour;

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (e.X < pictureBox1.Image.Width && e.Y < pictureBox1.Image.Height)
                {
                    Bitmap bitmap = new Bitmap(pictureBox1.Image);
                    this.chosenColour = bitmap.GetPixel(e.X, e.Y);

                    // remove the event handlers to change the values, then re-add them (otherwise it stuffs up)
                    this.numRed.ValueChanged -= this.numRed_ValueChanged; 
                    this.numGreen.ValueChanged -= this.numGreen_ValueChanged; 
                    this.numBlue.ValueChanged -= this.numBlue_ValueChanged;   

                    numRed.Value = chosenColour.R;
                    numGreen.Value = chosenColour.G;
                    numBlue.Value = chosenColour.B;

                    this.numRed.ValueChanged += new System.EventHandler(this.numRed_ValueChanged);
                    this.numGreen.ValueChanged += new System.EventHandler(this.numGreen_ValueChanged);
                    this.numBlue.ValueChanged += new System.EventHandler(this.numBlue_ValueChanged);            
                    colourBox.BackColor = chosenColour;

                    bitmap.Dispose();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
