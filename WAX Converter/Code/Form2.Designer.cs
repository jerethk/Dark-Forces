using System;
using System.Collections.Generic;
using System.Text;

namespace WAX_converter
{
    partial class BuildWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildWindow));
            this.listboxImages = new System.Windows.Forms.ListBox();
            this.ButtonAddImage = new System.Windows.Forms.Button();
            this.ButtonPal = new System.Windows.Forms.Button();
            this.openPalDialog = new System.Windows.Forms.OpenFileDialog();
            this.loadImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.displayBox2 = new System.Windows.Forms.PictureBox();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxIlluminated = new System.Windows.Forms.CheckBox();
            this.buttonSaveWIP = new System.Windows.Forms.Button();
            this.buttonLoadWIP = new System.Windows.Forms.Button();
            this.transpColourBox = new System.Windows.Forms.PictureBox();
            this.buttonTransparent = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelNCells = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonRemoveImage = new System.Windows.Forms.Button();
            this.ButtonMoveUp = new System.Windows.Forms.Button();
            this.ButtonMoveDown = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelNFrames = new System.Windows.Forms.Label();
            this.InsertY = new System.Windows.Forms.NumericUpDown();
            this.InsertX = new System.Windows.Forms.NumericUpDown();
            this.checkBoxFlip = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCell = new System.Windows.Forms.TextBox();
            this.buttonRemoveFrame = new System.Windows.Forms.Button();
            this.buttonAddFrame = new System.Windows.Forms.Button();
            this.listboxFrames = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonClearFrame = new System.Windows.Forms.Button();
            this.buttonSetFrame = new System.Windows.Forms.Button();
            this.listboxSeqFrames = new System.Windows.Forms.ListBox();
            this.labelNSeqs = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.listboxSeqs = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonRemoveSequence = new System.Windows.Forms.Button();
            this.buttonAddSequence = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dataGridViews = new System.Windows.Forms.DataGridView();
            this.ViewColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSetAllViews = new System.Windows.Forms.Button();
            this.FRate = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.Wheight = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.Wwidth = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxAction = new System.Windows.Forms.ComboBox();
            this.comboBoxLogic = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.checkBoxCompress = new System.Windows.Forms.CheckBox();
            this.labelPal = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.labelNotes = new System.Windows.Forms.Label();
            this.buttonCreateWAX = new System.Windows.Forms.Button();
            this.saveWaxDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveWIPDialog = new System.Windows.Forms.SaveFileDialog();
            this.openWIPDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel8 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.displayBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transpColourBox)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InsertY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsertX)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViews)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wheight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wwidth)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // listboxImages
            // 
            this.listboxImages.FormattingEnabled = true;
            this.listboxImages.ItemHeight = 15;
            this.listboxImages.Location = new System.Drawing.Point(20, 43);
            this.listboxImages.Name = "listboxImages";
            this.listboxImages.Size = new System.Drawing.Size(77, 619);
            this.listboxImages.TabIndex = 1;
            this.listboxImages.SelectedIndexChanged += new System.EventHandler(this.listboxImages_SelectedIndexChanged);
            // 
            // ButtonAddImage
            // 
            this.ButtonAddImage.Location = new System.Drawing.Point(118, 43);
            this.ButtonAddImage.Name = "ButtonAddImage";
            this.ButtonAddImage.Size = new System.Drawing.Size(95, 31);
            this.ButtonAddImage.TabIndex = 2;
            this.ButtonAddImage.Text = "Add Image(s)";
            this.ButtonAddImage.UseVisualStyleBackColor = true;
            this.ButtonAddImage.Click += new System.EventHandler(this.ButtonAddImage_Click);
            // 
            // ButtonPal
            // 
            this.ButtonPal.Location = new System.Drawing.Point(12, 12);
            this.ButtonPal.Name = "ButtonPal";
            this.ButtonPal.Size = new System.Drawing.Size(75, 23);
            this.ButtonPal.TabIndex = 3;
            this.ButtonPal.Text = "Load PAL";
            this.ButtonPal.UseVisualStyleBackColor = true;
            this.ButtonPal.Click += new System.EventHandler(this.ButtonPal_Click);
            // 
            // openPalDialog
            // 
            this.openPalDialog.DefaultExt = "PAL";
            this.openPalDialog.Filter = "DF Palette files|*.PAL";
            this.openPalDialog.Title = "Load PAL";
            this.openPalDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openPalDialog_FileOk);
            // 
            // loadImageDialog
            // 
            this.loadImageDialog.Filter = "PNG file|*.PNG|Bitmap file|*.BMP|JPEG file|*.JPG|All files|*.*";
            this.loadImageDialog.Multiselect = true;
            this.loadImageDialog.Title = "Add Image(s)";
            this.loadImageDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.loadImageDialog_FileOk);
            // 
            // displayBox2
            // 
            this.displayBox2.BackColor = System.Drawing.Color.Black;
            this.displayBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.displayBox2.Location = new System.Drawing.Point(117, 237);
            this.displayBox2.Name = "displayBox2";
            this.displayBox2.Size = new System.Drawing.Size(420, 425);
            this.displayBox2.TabIndex = 4;
            this.displayBox2.TabStop = false;
            // 
            // ButtonExit
            // 
            this.ButtonExit.Location = new System.Drawing.Point(731, 12);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(127, 23);
            this.ButtonExit.TabIndex = 5;
            this.ButtonExit.Text = "Exit build window";
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxIlluminated);
            this.panel1.Controls.Add(this.buttonSaveWIP);
            this.panel1.Controls.Add(this.buttonLoadWIP);
            this.panel1.Controls.Add(this.transpColourBox);
            this.panel1.Controls.Add(this.buttonTransparent);
            this.panel1.Controls.Add(this.ButtonExit);
            this.panel1.Controls.Add(this.ButtonPal);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 47);
            this.panel1.TabIndex = 6;
            // 
            // checkBoxIlluminated
            // 
            this.checkBoxIlluminated.AutoSize = true;
            this.checkBoxIlluminated.Location = new System.Drawing.Point(102, 15);
            this.checkBoxIlluminated.Name = "checkBoxIlluminated";
            this.checkBoxIlluminated.Size = new System.Drawing.Size(170, 19);
            this.checkBoxIlluminated.TabIndex = 12;
            this.checkBoxIlluminated.Text = "Include illuminated colours";
            this.checkBoxIlluminated.UseVisualStyleBackColor = true;
            this.checkBoxIlluminated.CheckedChanged += new System.EventHandler(this.checkBoxIlluminated_CheckedChanged);
            // 
            // buttonSaveWIP
            // 
            this.buttonSaveWIP.Location = new System.Drawing.Point(530, 12);
            this.buttonSaveWIP.Name = "buttonSaveWIP";
            this.buttonSaveWIP.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveWIP.TabIndex = 11;
            this.buttonSaveWIP.Text = "Save WIP";
            this.buttonSaveWIP.UseVisualStyleBackColor = true;
            this.buttonSaveWIP.Click += new System.EventHandler(this.buttonSaveWIP_Click);
            // 
            // buttonLoadWIP
            // 
            this.buttonLoadWIP.Location = new System.Drawing.Point(611, 12);
            this.buttonLoadWIP.Name = "buttonLoadWIP";
            this.buttonLoadWIP.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadWIP.TabIndex = 10;
            this.buttonLoadWIP.Text = "Load WIP";
            this.buttonLoadWIP.UseVisualStyleBackColor = true;
            this.buttonLoadWIP.Click += new System.EventHandler(this.buttonLoadWIP_Click);
            // 
            // transpColourBox
            // 
            this.transpColourBox.Location = new System.Drawing.Point(437, 12);
            this.transpColourBox.Name = "transpColourBox";
            this.transpColourBox.Size = new System.Drawing.Size(23, 23);
            this.transpColourBox.TabIndex = 9;
            this.transpColourBox.TabStop = false;
            // 
            // buttonTransparent
            // 
            this.buttonTransparent.Location = new System.Drawing.Point(314, 12);
            this.buttonTransparent.Name = "buttonTransparent";
            this.buttonTransparent.Size = new System.Drawing.Size(117, 23);
            this.buttonTransparent.TabIndex = 8;
            this.buttonTransparent.Text = "Transparent colour";
            this.buttonTransparent.UseVisualStyleBackColor = true;
            this.buttonTransparent.Click += new System.EventHandler(this.buttonTransparent_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.labelNCells);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.ButtonRemoveImage);
            this.panel2.Controls.Add(this.ButtonMoveUp);
            this.panel2.Controls.Add(this.ButtonMoveDown);
            this.panel2.Controls.Add(this.ButtonAddImage);
            this.panel2.Controls.Add(this.listboxImages);
            this.panel2.Controls.Add(this.displayBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(388, 714);
            this.panel2.TabIndex = 7;
            // 
            // labelNCells
            // 
            this.labelNCells.AutoSize = true;
            this.labelNCells.Location = new System.Drawing.Point(118, 15);
            this.labelNCells.Name = "labelNCells";
            this.labelNCells.Size = new System.Drawing.Size(34, 15);
            this.labelNCells.TabIndex = 9;
            this.labelNCells.Text = "n = 0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(20, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "CELLS";
            // 
            // ButtonRemoveImage
            // 
            this.ButtonRemoveImage.Location = new System.Drawing.Point(118, 182);
            this.ButtonRemoveImage.Name = "ButtonRemoveImage";
            this.ButtonRemoveImage.Size = new System.Drawing.Size(95, 34);
            this.ButtonRemoveImage.TabIndex = 7;
            this.ButtonRemoveImage.Text = "Remove";
            this.ButtonRemoveImage.UseVisualStyleBackColor = true;
            this.ButtonRemoveImage.Click += new System.EventHandler(this.ButtonRemoveImage_Click);
            // 
            // ButtonMoveUp
            // 
            this.ButtonMoveUp.Location = new System.Drawing.Point(117, 95);
            this.ButtonMoveUp.Name = "ButtonMoveUp";
            this.ButtonMoveUp.Size = new System.Drawing.Size(96, 28);
            this.ButtonMoveUp.TabIndex = 6;
            this.ButtonMoveUp.Text = "Move up";
            this.ButtonMoveUp.UseVisualStyleBackColor = true;
            this.ButtonMoveUp.Click += new System.EventHandler(this.ButtonMoveUp_Click);
            // 
            // ButtonMoveDown
            // 
            this.ButtonMoveDown.Location = new System.Drawing.Point(118, 129);
            this.ButtonMoveDown.Name = "ButtonMoveDown";
            this.ButtonMoveDown.Size = new System.Drawing.Size(95, 28);
            this.ButtonMoveDown.TabIndex = 5;
            this.ButtonMoveDown.Text = "Move down";
            this.ButtonMoveDown.UseVisualStyleBackColor = true;
            this.ButtonMoveDown.Click += new System.EventHandler(this.ButtonMoveDown_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.labelNFrames);
            this.panel3.Controls.Add(this.InsertY);
            this.panel3.Controls.Add(this.InsertX);
            this.panel3.Controls.Add(this.checkBoxFlip);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.textBoxCell);
            this.panel3.Controls.Add(this.buttonRemoveFrame);
            this.panel3.Controls.Add(this.buttonAddFrame);
            this.panel3.Controls.Add(this.listboxFrames);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(265, 296);
            this.panel3.TabIndex = 8;
            // 
            // labelNFrames
            // 
            this.labelNFrames.AutoSize = true;
            this.labelNFrames.Location = new System.Drawing.Point(80, 15);
            this.labelNFrames.Name = "labelNFrames";
            this.labelNFrames.Size = new System.Drawing.Size(34, 15);
            this.labelNFrames.TabIndex = 10;
            this.labelNFrames.Text = "n = 0";
            // 
            // InsertY
            // 
            this.InsertY.Enabled = false;
            this.InsertY.Location = new System.Drawing.Point(144, 180);
            this.InsertY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.InsertY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.InsertY.Name = "InsertY";
            this.InsertY.Size = new System.Drawing.Size(75, 23);
            this.InsertY.TabIndex = 10;
            this.InsertY.ValueChanged += new System.EventHandler(this.InsertY_ValueChanged);
            // 
            // InsertX
            // 
            this.InsertX.Enabled = false;
            this.InsertX.Location = new System.Drawing.Point(144, 154);
            this.InsertX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.InsertX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.InsertX.Name = "InsertX";
            this.InsertX.Size = new System.Drawing.Size(75, 23);
            this.InsertX.TabIndex = 9;
            this.InsertX.ValueChanged += new System.EventHandler(this.InsertX_ValueChanged);
            // 
            // checkBoxFlip
            // 
            this.checkBoxFlip.AutoSize = true;
            this.checkBoxFlip.Enabled = false;
            this.checkBoxFlip.Location = new System.Drawing.Point(87, 216);
            this.checkBoxFlip.Name = "checkBoxFlip";
            this.checkBoxFlip.Size = new System.Drawing.Size(45, 19);
            this.checkBoxFlip.TabIndex = 8;
            this.checkBoxFlip.Text = "Flip";
            this.checkBoxFlip.UseVisualStyleBackColor = true;
            this.checkBoxFlip.CheckedChanged += new System.EventHandler(this.checkBoxFlip_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Insert Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(87, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Insert X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cell";
            // 
            // textBoxCell
            // 
            this.textBoxCell.Location = new System.Drawing.Point(144, 115);
            this.textBoxCell.Name = "textBoxCell";
            this.textBoxCell.ReadOnly = true;
            this.textBoxCell.Size = new System.Drawing.Size(75, 23);
            this.textBoxCell.TabIndex = 4;
            // 
            // buttonRemoveFrame
            // 
            this.buttonRemoveFrame.Enabled = false;
            this.buttonRemoveFrame.Location = new System.Drawing.Point(87, 72);
            this.buttonRemoveFrame.Name = "buttonRemoveFrame";
            this.buttonRemoveFrame.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveFrame.TabIndex = 3;
            this.buttonRemoveFrame.Text = "Remove";
            this.buttonRemoveFrame.UseVisualStyleBackColor = true;
            this.buttonRemoveFrame.Click += new System.EventHandler(this.buttonRemoveFrame_Click);
            // 
            // buttonAddFrame
            // 
            this.buttonAddFrame.Enabled = false;
            this.buttonAddFrame.Location = new System.Drawing.Point(87, 43);
            this.buttonAddFrame.Name = "buttonAddFrame";
            this.buttonAddFrame.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFrame.TabIndex = 2;
            this.buttonAddFrame.Text = "Add";
            this.buttonAddFrame.UseVisualStyleBackColor = true;
            this.buttonAddFrame.Click += new System.EventHandler(this.buttonAddFrame_Click);
            // 
            // listboxFrames
            // 
            this.listboxFrames.FormattingEnabled = true;
            this.listboxFrames.ItemHeight = 15;
            this.listboxFrames.Location = new System.Drawing.Point(13, 43);
            this.listboxFrames.Name = "listboxFrames";
            this.listboxFrames.Size = new System.Drawing.Size(56, 229);
            this.listboxFrames.TabIndex = 1;
            this.listboxFrames.SelectedIndexChanged += new System.EventHandler(this.listboxFrames_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "FRAMES";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(265, 714);
            this.panel4.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.buttonClearFrame);
            this.panel5.Controls.Add(this.buttonSetFrame);
            this.panel5.Controls.Add(this.listboxSeqFrames);
            this.panel5.Controls.Add(this.labelNSeqs);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.listboxSeqs);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.buttonRemoveSequence);
            this.panel5.Controls.Add(this.buttonAddSequence);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 296);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(265, 418);
            this.panel5.TabIndex = 9;
            // 
            // buttonClearFrame
            // 
            this.buttonClearFrame.Enabled = false;
            this.buttonClearFrame.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonClearFrame.Location = new System.Drawing.Point(100, 338);
            this.buttonClearFrame.Name = "buttonClearFrame";
            this.buttonClearFrame.Size = new System.Drawing.Size(106, 27);
            this.buttonClearFrame.TabIndex = 19;
            this.buttonClearFrame.Text = "Clear Last Frame";
            this.buttonClearFrame.UseVisualStyleBackColor = true;
            this.buttonClearFrame.Click += new System.EventHandler(this.buttonClearFrame_Click);
            // 
            // buttonSetFrame
            // 
            this.buttonSetFrame.Enabled = false;
            this.buttonSetFrame.Location = new System.Drawing.Point(100, 305);
            this.buttonSetFrame.Name = "buttonSetFrame";
            this.buttonSetFrame.Size = new System.Drawing.Size(106, 27);
            this.buttonSetFrame.TabIndex = 18;
            this.buttonSetFrame.Text = "Set Frame";
            this.buttonSetFrame.UseVisualStyleBackColor = true;
            this.buttonSetFrame.Click += new System.EventHandler(this.buttonSetFrame_Click);
            // 
            // listboxSeqFrames
            // 
            this.listboxSeqFrames.Enabled = false;
            this.listboxSeqFrames.FormattingEnabled = true;
            this.listboxSeqFrames.ItemHeight = 15;
            this.listboxSeqFrames.Location = new System.Drawing.Point(87, 130);
            this.listboxSeqFrames.Name = "listboxSeqFrames";
            this.listboxSeqFrames.Size = new System.Drawing.Size(132, 169);
            this.listboxSeqFrames.TabIndex = 17;
            this.listboxSeqFrames.SelectedIndexChanged += new System.EventHandler(this.listboxSeqFrames_SelectedIndexChanged);
            // 
            // labelNSeqs
            // 
            this.labelNSeqs.AutoSize = true;
            this.labelNSeqs.Location = new System.Drawing.Point(113, 14);
            this.labelNSeqs.Name = "labelNSeqs";
            this.labelNSeqs.Size = new System.Drawing.Size(34, 15);
            this.labelNSeqs.TabIndex = 11;
            this.labelNSeqs.Text = "n = 0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(87, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "Frames (max 32)";
            // 
            // listboxSeqs
            // 
            this.listboxSeqs.FormattingEnabled = true;
            this.listboxSeqs.ItemHeight = 15;
            this.listboxSeqs.Location = new System.Drawing.Point(13, 42);
            this.listboxSeqs.Name = "listboxSeqs";
            this.listboxSeqs.Size = new System.Drawing.Size(56, 319);
            this.listboxSeqs.TabIndex = 12;
            this.listboxSeqs.SelectedIndexChanged += new System.EventHandler(this.listboxSeqs_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(13, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "SEQUENCES";
            // 
            // buttonRemoveSequence
            // 
            this.buttonRemoveSequence.Enabled = false;
            this.buttonRemoveSequence.Location = new System.Drawing.Point(87, 71);
            this.buttonRemoveSequence.Name = "buttonRemoveSequence";
            this.buttonRemoveSequence.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveSequence.TabIndex = 14;
            this.buttonRemoveSequence.Text = "Remove";
            this.buttonRemoveSequence.UseVisualStyleBackColor = true;
            this.buttonRemoveSequence.Click += new System.EventHandler(this.buttonRemoveSequence_Click);
            // 
            // buttonAddSequence
            // 
            this.buttonAddSequence.Enabled = false;
            this.buttonAddSequence.Location = new System.Drawing.Point(87, 42);
            this.buttonAddSequence.Name = "buttonAddSequence";
            this.buttonAddSequence.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSequence.TabIndex = 13;
            this.buttonAddSequence.Text = "Add";
            this.buttonAddSequence.UseVisualStyleBackColor = true;
            this.buttonAddSequence.Click += new System.EventHandler(this.buttonAddSequence_Click);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.dataGridViews);
            this.panel6.Controls.Add(this.buttonSetAllViews);
            this.panel6.Controls.Add(this.FRate);
            this.panel6.Controls.Add(this.label15);
            this.panel6.Controls.Add(this.Wheight);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.Wwidth);
            this.panel6.Controls.Add(this.label13);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.comboBoxAction);
            this.panel6.Controls.Add(this.comboBoxLogic);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(291, 476);
            this.panel6.TabIndex = 10;
            // 
            // dataGridViews
            // 
            this.dataGridViews.AllowUserToAddRows = false;
            this.dataGridViews.AllowUserToDeleteRows = false;
            this.dataGridViews.AllowUserToResizeColumns = false;
            this.dataGridViews.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViews.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViews.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ViewColumn,
            this.Sequence});
            this.dataGridViews.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViews.Enabled = false;
            this.dataGridViews.Location = new System.Drawing.Point(16, 115);
            this.dataGridViews.MultiSelect = false;
            this.dataGridViews.Name = "dataGridViews";
            this.dataGridViews.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViews.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViews.RowHeadersVisible = false;
            this.dataGridViews.RowTemplate.Height = 25;
            this.dataGridViews.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViews.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViews.Size = new System.Drawing.Size(187, 193);
            this.dataGridViews.TabIndex = 24;
            this.dataGridViews.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViews_CellValidated);
            this.dataGridViews.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViews_CellValidating);
            this.dataGridViews.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViews_RowEnter);
            // 
            // ViewColumn
            // 
            this.ViewColumn.HeaderText = "View";
            this.ViewColumn.Name = "ViewColumn";
            this.ViewColumn.ReadOnly = true;
            this.ViewColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ViewColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ViewColumn.Width = 50;
            // 
            // Sequence
            // 
            this.Sequence.HeaderText = "Sequence";
            this.Sequence.Name = "Sequence";
            this.Sequence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // buttonSetAllViews
            // 
            this.buttonSetAllViews.Enabled = false;
            this.buttonSetAllViews.Location = new System.Drawing.Point(36, 327);
            this.buttonSetAllViews.Name = "buttonSetAllViews";
            this.buttonSetAllViews.Size = new System.Drawing.Size(143, 28);
            this.buttonSetAllViews.TabIndex = 23;
            this.buttonSetAllViews.Text = "Set all views";
            this.buttonSetAllViews.UseVisualStyleBackColor = true;
            this.buttonSetAllViews.Click += new System.EventHandler(this.buttonSetAllViews_Click);
            // 
            // FRate
            // 
            this.FRate.Enabled = false;
            this.FRate.Location = new System.Drawing.Point(104, 433);
            this.FRate.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.FRate.Name = "FRate";
            this.FRate.Size = new System.Drawing.Size(75, 23);
            this.FRate.TabIndex = 21;
            this.FRate.ValueChanged += new System.EventHandler(this.FRate_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 435);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 15);
            this.label15.TabIndex = 22;
            this.label15.Text = "Frame rate";
            // 
            // Wheight
            // 
            this.Wheight.Enabled = false;
            this.Wheight.Location = new System.Drawing.Point(104, 404);
            this.Wheight.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.Wheight.Name = "Wheight";
            this.Wheight.Size = new System.Drawing.Size(75, 23);
            this.Wheight.TabIndex = 12;
            this.Wheight.ValueChanged += new System.EventHandler(this.Wheight_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 406);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 15);
            this.label14.TabIndex = 20;
            this.label14.Text = "World height";
            // 
            // Wwidth
            // 
            this.Wwidth.Enabled = false;
            this.Wwidth.Location = new System.Drawing.Point(104, 378);
            this.Wwidth.Maximum = new decimal(new int[] {
            200000,
            0,
            0,
            0});
            this.Wwidth.Name = "Wwidth";
            this.Wwidth.Size = new System.Drawing.Size(75, 23);
            this.Wwidth.TabIndex = 11;
            this.Wwidth.ValueChanged += new System.EventHandler(this.Wwidth_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 380);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 15);
            this.label13.TabIndex = 19;
            this.label13.Text = "World width";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 15);
            this.label10.TabIndex = 14;
            this.label10.Text = "Action";
            // 
            // comboBoxAction
            // 
            this.comboBoxAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAction.FormattingEnabled = true;
            this.comboBoxAction.Location = new System.Drawing.Point(88, 73);
            this.comboBoxAction.Name = "comboBoxAction";
            this.comboBoxAction.Size = new System.Drawing.Size(115, 23);
            this.comboBoxAction.TabIndex = 13;
            this.comboBoxAction.SelectedIndexChanged += new System.EventHandler(this.comboBoxAction_SelectedIndexChanged);
            // 
            // comboBoxLogic
            // 
            this.comboBoxLogic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLogic.FormattingEnabled = true;
            this.comboBoxLogic.Items.AddRange(new object[] {
            "Anim",
            "Scenery / Barrel",
            "Enemy",
            "Dark Trooper",
            "Remote"});
            this.comboBoxLogic.Location = new System.Drawing.Point(88, 40);
            this.comboBoxLogic.Name = "comboBoxLogic";
            this.comboBoxLogic.Size = new System.Drawing.Size(115, 23);
            this.comboBoxLogic.TabIndex = 12;
            this.comboBoxLogic.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogic_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "Logic";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(16, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 15);
            this.label9.TabIndex = 11;
            this.label9.Text = "ACTIONS";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.checkBoxCompress);
            this.panel7.Controls.Add(this.panel6);
            this.panel7.Controls.Add(this.labelPal);
            this.panel7.Controls.Add(this.label17);
            this.panel7.Controls.Add(this.labelNotes);
            this.panel7.Controls.Add(this.buttonCreateWAX);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(265, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(291, 714);
            this.panel7.TabIndex = 11;
            // 
            // checkBoxCompress
            // 
            this.checkBoxCompress.AutoSize = true;
            this.checkBoxCompress.Location = new System.Drawing.Point(76, 497);
            this.checkBoxCompress.Name = "checkBoxCompress";
            this.checkBoxCompress.Size = new System.Drawing.Size(105, 19);
            this.checkBoxCompress.TabIndex = 11;
            this.checkBoxCompress.Text = "Compress cells";
            this.checkBoxCompress.UseVisualStyleBackColor = true;
            // 
            // labelPal
            // 
            this.labelPal.AutoSize = true;
            this.labelPal.Location = new System.Drawing.Point(80, 615);
            this.labelPal.Name = "labelPal";
            this.labelPal.Size = new System.Drawing.Size(120, 15);
            this.labelPal.TabIndex = 3;
            this.labelPal.Text = "Secbase.PAL (default)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 615);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 15);
            this.label17.TabIndex = 2;
            this.label17.Text = "Palette:";
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(18, 648);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(41, 15);
            this.labelNotes.TabIndex = 1;
            this.labelNotes.Text = "Notes:";
            // 
            // buttonCreateWAX
            // 
            this.buttonCreateWAX.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonCreateWAX.Location = new System.Drawing.Point(80, 532);
            this.buttonCreateWAX.Name = "buttonCreateWAX";
            this.buttonCreateWAX.Size = new System.Drawing.Size(101, 56);
            this.buttonCreateWAX.TabIndex = 0;
            this.buttonCreateWAX.Text = "Create WAX!";
            this.buttonCreateWAX.UseVisualStyleBackColor = true;
            this.buttonCreateWAX.Click += new System.EventHandler(this.buttonCreateWAX_Click);
            // 
            // saveWaxDialog
            // 
            this.saveWaxDialog.DefaultExt = "WAX";
            this.saveWaxDialog.Filter = "WAX file|*.wax";
            this.saveWaxDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveWaxDialog_FileOk);
            // 
            // saveWIPDialog
            // 
            this.saveWIPDialog.DefaultExt = "wwip";
            this.saveWIPDialog.Filter = "WAX work-in-progress|*.WWIP";
            this.saveWIPDialog.Title = "Save WAX work-in-progress";
            this.saveWIPDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveWIPDialog_FileOk);
            // 
            // openWIPDialog
            // 
            this.openWIPDialog.Filter = "WAX work-in-progress file|*.WWIP";
            this.openWIPDialog.Title = "Open WAX work-in-progress";
            this.openWIPDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openWIPDialog_FileOk);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel4);
            this.panel8.Controls.Add(this.panel7);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(388, 47);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(556, 714);
            this.panel8.TabIndex = 0;
            // 
            // BuildWindow
            // 
            this.ClientSize = new System.Drawing.Size(944, 761);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(960, 800);
            this.Name = "BuildWindow";
            this.Text = "Build WAX";
            this.Shown += new System.EventHandler(this.BuildWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.displayBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transpColourBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InsertY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InsertX)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViews)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wheight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wwidth)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ListBox listboxImages;
        private System.Windows.Forms.Button ButtonAddImage;
        private System.Windows.Forms.Button ButtonPal;
        private System.Windows.Forms.OpenFileDialog openPalDialog;
        private System.Windows.Forms.OpenFileDialog loadImageDialog;
        private System.Windows.Forms.PictureBox displayBox2;
        private System.Windows.Forms.Button ButtonExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button ButtonMoveUp;
        private System.Windows.Forms.Button ButtonMoveDown;
        private System.Windows.Forms.Button ButtonRemoveImage;
        private System.Windows.Forms.Button buttonTransparent;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listboxFrames;
        private System.Windows.Forms.Button buttonAddFrame;
        private System.Windows.Forms.Button buttonRemoveFrame;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCell;
        private System.Windows.Forms.CheckBox checkBoxFlip;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown InsertY;
        private System.Windows.Forms.NumericUpDown InsertX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listboxSeqs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonRemoveSequence;
        private System.Windows.Forms.Button buttonAddSequence;
        private System.Windows.Forms.Label labelNCells;
        private System.Windows.Forms.Label labelNFrames;
        private System.Windows.Forms.Label labelNSeqs;
        private System.Windows.Forms.ListBox listboxSeqFrames;
        private System.Windows.Forms.Button buttonClearFrame;
        private System.Windows.Forms.Button buttonSetFrame;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxLogic;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxAction;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown Wheight;
        private System.Windows.Forms.NumericUpDown Wwidth;
        private System.Windows.Forms.NumericUpDown FRate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button buttonCreateWAX;
        private System.Windows.Forms.SaveFileDialog saveWaxDialog;
        private System.Windows.Forms.Button buttonSetAllViews;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Label labelPal;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.PictureBox transpColourBox;
        private System.Windows.Forms.Button buttonSaveWIP;
        private System.Windows.Forms.Button buttonLoadWIP;
        private System.Windows.Forms.SaveFileDialog saveWIPDialog;
        private System.Windows.Forms.OpenFileDialog openWIPDialog;
        private System.Windows.Forms.DataGridView dataGridViews;
        private System.Windows.Forms.CheckBox checkBoxIlluminated;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.DataGridViewTextBoxColumn ViewColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sequence;
        private System.Windows.Forms.CheckBox checkBoxCompress;
    }
}
