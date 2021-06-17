
namespace BM_Converter
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCreateBM = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.btnBulkConvert = new System.Windows.Forms.Button();
            this.BtnLoadBM = new System.Windows.Forms.Button();
            this.BtnLoadPAL = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.OpenPALDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenBMDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveBMPDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxZoom = new System.Windows.Forms.CheckBox();
            this.btnNextSub = new System.Windows.Forms.Button();
            this.btnPrevSub = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSubBMInfo = new System.Windows.Forms.TextBox();
            this.displayBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPal = new System.Windows.Forms.Label();
            this.textBoxBMInfo = new System.Windows.Forms.TextBox();
            this.openBulkDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnCreateBM);
            this.panel1.Controls.Add(this.buttonHelp);
            this.panel1.Controls.Add(this.btnBulkConvert);
            this.panel1.Controls.Add(this.BtnLoadBM);
            this.panel1.Controls.Add(this.BtnLoadPAL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(664, 47);
            this.panel1.TabIndex = 0;
            // 
            // btnCreateBM
            // 
            this.btnCreateBM.Location = new System.Drawing.Point(423, 11);
            this.btnCreateBM.Name = "btnCreateBM";
            this.btnCreateBM.Size = new System.Drawing.Size(102, 24);
            this.btnCreateBM.TabIndex = 4;
            this.btnCreateBM.Text = "Create BM";
            this.btnCreateBM.UseVisualStyleBackColor = true;
            this.btnCreateBM.Click += new System.EventHandler(this.btnCreateBM_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonHelp.Location = new System.Drawing.Point(623, 12);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(23, 23);
            this.buttonHelp.TabIndex = 3;
            this.buttonHelp.Text = "?";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // btnBulkConvert
            // 
            this.btnBulkConvert.Enabled = false;
            this.btnBulkConvert.Location = new System.Drawing.Point(246, 12);
            this.btnBulkConvert.Name = "btnBulkConvert";
            this.btnBulkConvert.Size = new System.Drawing.Size(95, 23);
            this.btnBulkConvert.TabIndex = 2;
            this.btnBulkConvert.Text = "Bulk Convert";
            this.btnBulkConvert.UseVisualStyleBackColor = true;
            this.btnBulkConvert.Click += new System.EventHandler(this.btnBulkConvert_Click);
            // 
            // BtnLoadBM
            // 
            this.BtnLoadBM.Enabled = false;
            this.BtnLoadBM.Location = new System.Drawing.Point(93, 12);
            this.BtnLoadBM.Name = "BtnLoadBM";
            this.BtnLoadBM.Size = new System.Drawing.Size(75, 23);
            this.BtnLoadBM.TabIndex = 1;
            this.BtnLoadBM.Text = "Load BM";
            this.BtnLoadBM.UseVisualStyleBackColor = true;
            this.BtnLoadBM.Click += new System.EventHandler(this.BtnLoadBM_Click);
            // 
            // BtnLoadPAL
            // 
            this.BtnLoadPAL.Location = new System.Drawing.Point(12, 12);
            this.BtnLoadPAL.Name = "BtnLoadPAL";
            this.BtnLoadPAL.Size = new System.Drawing.Size(75, 23);
            this.BtnLoadPAL.TabIndex = 0;
            this.BtnLoadPAL.Text = "Load PAL";
            this.BtnLoadPAL.UseVisualStyleBackColor = true;
            this.BtnLoadPAL.Click += new System.EventHandler(this.BtnLoadPAL_Click);
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExport.Location = new System.Drawing.Point(58, 446);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(111, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export PNG";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // OpenPALDialog
            // 
            this.OpenPALDialog.DefaultExt = "pal";
            this.OpenPALDialog.Filter = "DF Palette files|*.pal";
            this.OpenPALDialog.Title = "Open PAL";
            this.OpenPALDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenPALDialog_FileOk);
            // 
            // OpenBMDialog
            // 
            this.OpenBMDialog.DefaultExt = "bm";
            this.OpenBMDialog.Filter = "Dark Forces BM file|*.bm";
            this.OpenBMDialog.Title = "Open BM";
            this.OpenBMDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenBMDialog_FileOk);
            // 
            // saveBMPDialog
            // 
            this.saveBMPDialog.DefaultExt = "png";
            this.saveBMPDialog.Filter = "PNG file|*.png";
            this.saveBMPDialog.Title = "Export";
            this.saveBMPDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveBMPDialog_FileOk);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExport);
            this.panel2.Controls.Add(this.checkBoxZoom);
            this.panel2.Controls.Add(this.btnNextSub);
            this.panel2.Controls.Add(this.btnPrevSub);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBoxSubBMInfo);
            this.panel2.Controls.Add(this.displayBox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.labelPal);
            this.panel2.Controls.Add(this.textBoxBMInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(664, 514);
            this.panel2.TabIndex = 1;
            // 
            // checkBoxZoom
            // 
            this.checkBoxZoom.AutoSize = true;
            this.checkBoxZoom.Location = new System.Drawing.Point(589, 44);
            this.checkBoxZoom.Name = "checkBoxZoom";
            this.checkBoxZoom.Size = new System.Drawing.Size(58, 19);
            this.checkBoxZoom.TabIndex = 8;
            this.checkBoxZoom.Text = "Zoom";
            this.checkBoxZoom.UseVisualStyleBackColor = true;
            this.checkBoxZoom.CheckedChanged += new System.EventHandler(this.checkBoxZoom_CheckedChanged);
            // 
            // btnNextSub
            // 
            this.btnNextSub.Enabled = false;
            this.btnNextSub.Location = new System.Drawing.Point(128, 249);
            this.btnNextSub.Name = "btnNextSub";
            this.btnNextSub.Size = new System.Drawing.Size(28, 29);
            this.btnNextSub.TabIndex = 7;
            this.btnNextSub.Text = ">";
            this.btnNextSub.UseVisualStyleBackColor = true;
            this.btnNextSub.Click += new System.EventHandler(this.btnNextSub_Click);
            // 
            // btnPrevSub
            // 
            this.btnPrevSub.Enabled = false;
            this.btnPrevSub.Location = new System.Drawing.Point(84, 249);
            this.btnPrevSub.Name = "btnPrevSub";
            this.btnPrevSub.Size = new System.Drawing.Size(28, 29);
            this.btnPrevSub.TabIndex = 6;
            this.btnPrevSub.Text = "<";
            this.btnPrevSub.UseVisualStyleBackColor = true;
            this.btnPrevSub.Click += new System.EventHandler(this.btnPrevSub_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sub BMs:";
            // 
            // textBoxSubBMInfo
            // 
            this.textBoxSubBMInfo.Location = new System.Drawing.Point(13, 284);
            this.textBoxSubBMInfo.Multiline = true;
            this.textBoxSubBMInfo.Name = "textBoxSubBMInfo";
            this.textBoxSubBMInfo.ReadOnly = true;
            this.textBoxSubBMInfo.Size = new System.Drawing.Size(212, 142);
            this.textBoxSubBMInfo.TabIndex = 4;
            // 
            // displayBox
            // 
            this.displayBox.BackColor = System.Drawing.Color.Black;
            this.displayBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.displayBox.Location = new System.Drawing.Point(247, 66);
            this.displayBox.Name = "displayBox";
            this.displayBox.Size = new System.Drawing.Size(400, 403);
            this.displayBox.TabIndex = 3;
            this.displayBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "BM Info:";
            // 
            // labelPal
            // 
            this.labelPal.AutoSize = true;
            this.labelPal.Location = new System.Drawing.Point(13, 21);
            this.labelPal.Name = "labelPal";
            this.labelPal.Size = new System.Drawing.Size(30, 15);
            this.labelPal.TabIndex = 1;
            this.labelPal.Text = "PAL:";
            // 
            // textBoxBMInfo
            // 
            this.textBoxBMInfo.Location = new System.Drawing.Point(13, 66);
            this.textBoxBMInfo.Multiline = true;
            this.textBoxBMInfo.Name = "textBoxBMInfo";
            this.textBoxBMInfo.ReadOnly = true;
            this.textBoxBMInfo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxBMInfo.Size = new System.Drawing.Size(212, 149);
            this.textBoxBMInfo.TabIndex = 0;
            this.textBoxBMInfo.WordWrap = false;
            // 
            // openBulkDialog
            // 
            this.openBulkDialog.DefaultExt = "bm";
            this.openBulkDialog.Filter = "Dark Forces BM files|*.BM";
            this.openBulkDialog.Multiselect = true;
            this.openBulkDialog.Title = "Choose BM files to convert";
            this.openBulkDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openBulkDialog_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 561);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(680, 600);
            this.Name = "Form1";
            this.Text = "BM Converter (version 0.7)";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnLoadBM;
        private System.Windows.Forms.Button BtnLoadPAL;
        private System.Windows.Forms.OpenFileDialog OpenPALDialog;
        private System.Windows.Forms.OpenFileDialog OpenBMDialog;
        private System.Windows.Forms.SaveFileDialog saveBMPDialog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelPal;
        private System.Windows.Forms.TextBox textBoxBMInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox displayBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSubBMInfo;
        private System.Windows.Forms.Button btnNextSub;
        private System.Windows.Forms.Button btnPrevSub;
        private System.Windows.Forms.CheckBox checkBoxZoom;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnBulkConvert;
        private System.Windows.Forms.OpenFileDialog openBulkDialog;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button btnCreateBM;
    }
}

