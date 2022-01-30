
namespace WAX_converter
{
    partial class SequenceBuilderWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxFrames = new System.Windows.Forms.ListBox();
            this.listBoxSeqFrames = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.displayBox = new System.Windows.Forms.PictureBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClearSeq = new System.Windows.Forms.Button();
            this.checkBoxZoom = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ALL FRAMES";
            // 
            // listBoxFrames
            // 
            this.listBoxFrames.FormattingEnabled = true;
            this.listBoxFrames.ItemHeight = 15;
            this.listBoxFrames.Location = new System.Drawing.Point(18, 36);
            this.listBoxFrames.Name = "listBoxFrames";
            this.listBoxFrames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFrames.Size = new System.Drawing.Size(85, 499);
            this.listBoxFrames.TabIndex = 1;
            this.listBoxFrames.SelectedIndexChanged += new System.EventHandler(this.listBoxFrames_SelectedIndexChanged);
            this.listBoxFrames.Enter += new System.EventHandler(this.listBoxFrames_Enter);
            // 
            // listBoxSeqFrames
            // 
            this.listBoxSeqFrames.FormattingEnabled = true;
            this.listBoxSeqFrames.ItemHeight = 15;
            this.listBoxSeqFrames.Location = new System.Drawing.Point(226, 36);
            this.listBoxSeqFrames.Name = "listBoxSeqFrames";
            this.listBoxSeqFrames.Size = new System.Drawing.Size(85, 499);
            this.listBoxSeqFrames.TabIndex = 3;
            this.listBoxSeqFrames.SelectedIndexChanged += new System.EventHandler(this.listBoxSeqFrames_SelectedIndexChanged);
            this.listBoxSeqFrames.Enter += new System.EventHandler(this.listBoxSeqFrames_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(189, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "FRAMES IN SEQUENCE";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(128, 128);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(77, 65);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add to Sequence";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(128, 210);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(77, 65);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Remove from  Sequence";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // displayBox
            // 
            this.displayBox.BackColor = System.Drawing.Color.Black;
            this.displayBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.displayBox.Location = new System.Drawing.Point(343, 61);
            this.displayBox.Name = "displayBox";
            this.displayBox.Size = new System.Drawing.Size(416, 415);
            this.displayBox.TabIndex = 6;
            this.displayBox.TabStop = false;
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDone.Location = new System.Drawing.Point(460, 498);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 37);
            this.btnDone.TabIndex = 7;
            this.btnDone.Text = "DONE";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(561, 498);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 37);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClearSeq
            // 
            this.btnClearSeq.Location = new System.Drawing.Point(128, 291);
            this.btnClearSeq.Name = "btnClearSeq";
            this.btnClearSeq.Size = new System.Drawing.Size(77, 65);
            this.btnClearSeq.TabIndex = 9;
            this.btnClearSeq.Text = "CLEAR  Sequence";
            this.btnClearSeq.UseVisualStyleBackColor = true;
            this.btnClearSeq.Click += new System.EventHandler(this.btnClearSeq_Click);
            // 
            // checkBoxZoom
            // 
            this.checkBoxZoom.AutoSize = true;
            this.checkBoxZoom.Location = new System.Drawing.Point(343, 36);
            this.checkBoxZoom.Name = "checkBoxZoom";
            this.checkBoxZoom.Size = new System.Drawing.Size(58, 19);
            this.checkBoxZoom.TabIndex = 10;
            this.checkBoxZoom.Text = "Zoom";
            this.checkBoxZoom.UseVisualStyleBackColor = true;
            this.checkBoxZoom.CheckedChanged += new System.EventHandler(this.checkBoxZoom_CheckedChanged);
            // 
            // SequenceBuilderWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.checkBoxZoom);
            this.Controls.Add(this.btnClearSeq);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.displayBox);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.listBoxSeqFrames);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxFrames);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "SequenceBuilderWindow";
            this.Text = "SequenceBuilderWindow";
            this.Load += new System.EventHandler(this.SequenceBuilderWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxFrames;
        private System.Windows.Forms.ListBox listBoxSeqFrames;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.PictureBox displayBox;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClearSeq;
        private System.Windows.Forms.CheckBox checkBoxZoom;
    }
}