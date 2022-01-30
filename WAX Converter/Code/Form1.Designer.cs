
namespace WAX_converter
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.RadioGroup = new System.Windows.Forms.GroupBox();
            this.radioCell = new System.Windows.Forms.RadioButton();
            this.radioFrame = new System.Windows.Forms.RadioButton();
            this.radioSequence = new System.Windows.Forms.RadioButton();
            this.radioAction = new System.Windows.Forms.RadioButton();
            this.LabelPal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelWax = new System.Windows.Forms.Label();
            this.WaxDetails = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLoadPal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuOpenWax = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCloseWax = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSaveBMP = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuOpenFme = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBuildWax = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelphelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.openWaxDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelSeqFrame = new System.Windows.Forms.Label();
            this.SeqNextFrame = new System.Windows.Forms.Button();
            this.SeqPrevFrame = new System.Windows.Forms.Button();
            this.SeqNumber = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.ActionInfo = new System.Windows.Forms.TextBox();
            this.ActionNumber = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ViewNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.SeqInfo = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBoxZoom = new System.Windows.Forms.CheckBox();
            this.CellNumber = new System.Windows.Forms.NumericUpDown();
            this.CellInfo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.displayBox = new System.Windows.Forms.PictureBox();
            this.FrameInfo = new System.Windows.Forms.TextBox();
            this.FrameNumber = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.openPalDialog = new System.Windows.Forms.OpenFileDialog();
            this.exportDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFmeDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel2.SuspendLayout();
            this.RadioGroup.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SeqNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewNumber)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CellNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrameNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.RadioGroup);
            this.panel2.Controls.Add(this.LabelPal);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.labelWax);
            this.panel2.Controls.Add(this.WaxDetails);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(944, 171);
            this.panel2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(12, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Palette:";
            // 
            // RadioGroup
            // 
            this.RadioGroup.Controls.Add(this.radioCell);
            this.RadioGroup.Controls.Add(this.radioFrame);
            this.RadioGroup.Controls.Add(this.radioSequence);
            this.RadioGroup.Controls.Add(this.radioAction);
            this.RadioGroup.Enabled = false;
            this.RadioGroup.Location = new System.Drawing.Point(409, 59);
            this.RadioGroup.Name = "RadioGroup";
            this.RadioGroup.Size = new System.Drawing.Size(331, 78);
            this.RadioGroup.TabIndex = 7;
            this.RadioGroup.TabStop = false;
            // 
            // radioCell
            // 
            this.radioCell.AutoSize = true;
            this.radioCell.Location = new System.Drawing.Point(180, 47);
            this.radioCell.Name = "radioCell";
            this.radioCell.Size = new System.Drawing.Size(87, 19);
            this.radioCell.TabIndex = 3;
            this.radioCell.TabStop = true;
            this.radioCell.Text = "View by cell";
            this.radioCell.UseVisualStyleBackColor = true;
            this.radioCell.CheckedChanged += new System.EventHandler(this.radioCell_CheckedChanged);
            // 
            // radioFrame
            // 
            this.radioFrame.AutoSize = true;
            this.radioFrame.Location = new System.Drawing.Point(180, 22);
            this.radioFrame.Name = "radioFrame";
            this.radioFrame.Size = new System.Drawing.Size(100, 19);
            this.radioFrame.TabIndex = 2;
            this.radioFrame.TabStop = true;
            this.radioFrame.Text = "View by frame";
            this.radioFrame.UseVisualStyleBackColor = true;
            this.radioFrame.CheckedChanged += new System.EventHandler(this.radioFrame_CheckedChanged);
            // 
            // radioSequence
            // 
            this.radioSequence.AutoSize = true;
            this.radioSequence.Location = new System.Drawing.Point(19, 47);
            this.radioSequence.Name = "radioSequence";
            this.radioSequence.Size = new System.Drawing.Size(119, 19);
            this.radioSequence.TabIndex = 1;
            this.radioSequence.TabStop = true;
            this.radioSequence.Text = "View by sequence";
            this.radioSequence.UseVisualStyleBackColor = true;
            this.radioSequence.CheckedChanged += new System.EventHandler(this.radioSequence_CheckedChanged);
            // 
            // radioAction
            // 
            this.radioAction.AutoSize = true;
            this.radioAction.Checked = true;
            this.radioAction.Location = new System.Drawing.Point(19, 22);
            this.radioAction.Name = "radioAction";
            this.radioAction.Size = new System.Drawing.Size(102, 19);
            this.radioAction.TabIndex = 0;
            this.radioAction.TabStop = true;
            this.radioAction.Text = "View by action";
            this.radioAction.UseVisualStyleBackColor = true;
            this.radioAction.CheckedChanged += new System.EventHandler(this.radioAction_CheckedChanged);
            // 
            // LabelPal
            // 
            this.LabelPal.AutoSize = true;
            this.LabelPal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LabelPal.Location = new System.Drawing.Point(85, 30);
            this.LabelPal.Name = "LabelPal";
            this.LabelPal.Size = new System.Drawing.Size(120, 15);
            this.LabelPal.TabIndex = 5;
            this.LabelPal.Text = "Secbase.PAL (default)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "WAX file:";
            // 
            // labelWax
            // 
            this.labelWax.AutoSize = true;
            this.labelWax.Location = new System.Drawing.Point(85, 10);
            this.labelWax.Name = "labelWax";
            this.labelWax.Size = new System.Drawing.Size(27, 15);
            this.labelWax.TabIndex = 3;
            this.labelWax.Text = "----";
            // 
            // WaxDetails
            // 
            this.WaxDetails.AcceptsReturn = true;
            this.WaxDetails.BackColor = System.Drawing.SystemColors.Control;
            this.WaxDetails.Location = new System.Drawing.Point(12, 59);
            this.WaxDetails.Multiline = true;
            this.WaxDetails.Name = "WaxDetails";
            this.WaxDetails.ReadOnly = true;
            this.WaxDetails.Size = new System.Drawing.Size(359, 96);
            this.WaxDetails.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuBuildWax,
            this.MenuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(944, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuLoadPal,
            this.toolStripSeparator1,
            this.MenuOpenWax,
            this.MenuCloseWax,
            this.MenuSaveBMP,
            this.toolStripSeparator6,
            this.MenuOpenFme,
            this.toolStripSeparator2,
            this.MenuQuit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(37, 20);
            this.MenuFile.Text = "File";
            this.MenuFile.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuFile_ItemClicked);
            // 
            // MenuLoadPal
            // 
            this.MenuLoadPal.Name = "MenuLoadPal";
            this.MenuLoadPal.Size = new System.Drawing.Size(149, 22);
            this.MenuLoadPal.Text = "Load PAL";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(146, 6);
            // 
            // MenuOpenWax
            // 
            this.MenuOpenWax.Name = "MenuOpenWax";
            this.MenuOpenWax.Size = new System.Drawing.Size(149, 22);
            this.MenuOpenWax.Text = "Open WAX";
            // 
            // MenuCloseWax
            // 
            this.MenuCloseWax.Enabled = false;
            this.MenuCloseWax.Name = "MenuCloseWax";
            this.MenuCloseWax.Size = new System.Drawing.Size(149, 22);
            this.MenuCloseWax.Text = "Close WAX";
            // 
            // MenuSaveBMP
            // 
            this.MenuSaveBMP.Enabled = false;
            this.MenuSaveBMP.Name = "MenuSaveBMP";
            this.MenuSaveBMP.Size = new System.Drawing.Size(149, 22);
            this.MenuSaveBMP.Text = "Export to PNG";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(146, 6);
            // 
            // MenuOpenFme
            // 
            this.MenuOpenFme.Name = "MenuOpenFme";
            this.MenuOpenFme.Size = new System.Drawing.Size(149, 22);
            this.MenuOpenFme.Text = "Open FME";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(146, 6);
            // 
            // MenuQuit
            // 
            this.MenuQuit.Name = "MenuQuit";
            this.MenuQuit.Size = new System.Drawing.Size(149, 22);
            this.MenuQuit.Text = "Quit";
            // 
            // MenuBuildWax
            // 
            this.MenuBuildWax.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBuild});
            this.MenuBuildWax.Name = "MenuBuildWax";
            this.MenuBuildWax.Size = new System.Drawing.Size(75, 20);
            this.MenuBuildWax.Text = "Build WAX";
            this.MenuBuildWax.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuBuildWax_ItemClicked);
            // 
            // MenuBuild
            // 
            this.MenuBuild.Name = "MenuBuild";
            this.MenuBuild.Size = new System.Drawing.Size(101, 22);
            this.MenuBuild.Text = "Build";
            // 
            // MenuHelp
            // 
            this.MenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuHelphelp,
            this.MenuAbout});
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Size = new System.Drawing.Size(44, 20);
            this.MenuHelp.Text = "Help";
            this.MenuHelp.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuHelp_ItemClicked);
            // 
            // MenuHelphelp
            // 
            this.MenuHelphelp.Name = "MenuHelphelp";
            this.MenuHelphelp.Size = new System.Drawing.Size(107, 22);
            this.MenuHelphelp.Text = "Help";
            // 
            // MenuAbout
            // 
            this.MenuAbout.Name = "MenuAbout";
            this.MenuAbout.Size = new System.Drawing.Size(107, 22);
            this.MenuAbout.Text = "About";
            // 
            // openWaxDialog
            // 
            this.openWaxDialog.DefaultExt = "wax";
            this.openWaxDialog.Filter = "Dark Forces WAX files|*.wax";
            this.openWaxDialog.Title = "Open WAX file";
            this.openWaxDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openWaxDialog_FileOk);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelSeqFrame);
            this.panel1.Controls.Add(this.SeqNextFrame);
            this.panel1.Controls.Add(this.SeqPrevFrame);
            this.panel1.Controls.Add(this.SeqNumber);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.ActionInfo);
            this.panel1.Controls.Add(this.ActionNumber);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ViewNumber);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.SeqInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 195);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 486);
            this.panel1.TabIndex = 3;
            // 
            // labelSeqFrame
            // 
            this.labelSeqFrame.AutoSize = true;
            this.labelSeqFrame.Location = new System.Drawing.Point(80, 323);
            this.labelSeqFrame.Name = "labelSeqFrame";
            this.labelSeqFrame.Size = new System.Drawing.Size(17, 15);
            this.labelSeqFrame.TabIndex = 11;
            this.labelSeqFrame.Text = "--";
            // 
            // SeqNextFrame
            // 
            this.SeqNextFrame.Location = new System.Drawing.Point(117, 317);
            this.SeqNextFrame.Name = "SeqNextFrame";
            this.SeqNextFrame.Size = new System.Drawing.Size(31, 26);
            this.SeqNextFrame.TabIndex = 10;
            this.SeqNextFrame.Text = ">";
            this.SeqNextFrame.UseVisualStyleBackColor = true;
            this.SeqNextFrame.Click += new System.EventHandler(this.SeqNextFrame_Click);
            // 
            // SeqPrevFrame
            // 
            this.SeqPrevFrame.Location = new System.Drawing.Point(28, 317);
            this.SeqPrevFrame.Name = "SeqPrevFrame";
            this.SeqPrevFrame.Size = new System.Drawing.Size(31, 26);
            this.SeqPrevFrame.TabIndex = 9;
            this.SeqPrevFrame.Text = "<";
            this.SeqPrevFrame.UseVisualStyleBackColor = true;
            this.SeqPrevFrame.Click += new System.EventHandler(this.SeqPrevFrame_Click);
            // 
            // SeqNumber
            // 
            this.SeqNumber.Enabled = false;
            this.SeqNumber.Location = new System.Drawing.Point(84, 200);
            this.SeqNumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SeqNumber.Name = "SeqNumber";
            this.SeqNumber.Size = new System.Drawing.Size(64, 23);
            this.SeqNumber.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "Sequence";
            // 
            // ActionInfo
            // 
            this.ActionInfo.Location = new System.Drawing.Point(12, 73);
            this.ActionInfo.Multiline = true;
            this.ActionInfo.Name = "ActionInfo";
            this.ActionInfo.ReadOnly = true;
            this.ActionInfo.Size = new System.Drawing.Size(154, 104);
            this.ActionInfo.TabIndex = 2;
            // 
            // ActionNumber
            // 
            this.ActionNumber.BackColor = System.Drawing.SystemColors.Window;
            this.ActionNumber.Enabled = false;
            this.ActionNumber.Location = new System.Drawing.Point(84, 9);
            this.ActionNumber.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.ActionNumber.Name = "ActionNumber";
            this.ActionNumber.ReadOnly = true;
            this.ActionNumber.Size = new System.Drawing.Size(64, 23);
            this.ActionNumber.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Action";
            // 
            // ViewNumber
            // 
            this.ViewNumber.Enabled = false;
            this.ViewNumber.Location = new System.Drawing.Point(84, 35);
            this.ViewNumber.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.ViewNumber.Name = "ViewNumber";
            this.ViewNumber.Size = new System.Drawing.Size(64, 23);
            this.ViewNumber.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "View";
            // 
            // SeqInfo
            // 
            this.SeqInfo.Location = new System.Drawing.Point(12, 229);
            this.SeqInfo.Multiline = true;
            this.SeqInfo.Name = "SeqInfo";
            this.SeqInfo.ReadOnly = true;
            this.SeqInfo.Size = new System.Drawing.Size(154, 65);
            this.SeqInfo.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.checkBoxZoom);
            this.panel3.Controls.Add(this.CellNumber);
            this.panel3.Controls.Add(this.CellInfo);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.displayBox);
            this.panel3.Controls.Add(this.FrameInfo);
            this.panel3.Controls.Add(this.FrameNumber);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(193, 195);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(751, 486);
            this.panel3.TabIndex = 4;
            // 
            // checkBoxZoom
            // 
            this.checkBoxZoom.AutoSize = true;
            this.checkBoxZoom.Location = new System.Drawing.Point(92, 421);
            this.checkBoxZoom.Name = "checkBoxZoom";
            this.checkBoxZoom.Size = new System.Drawing.Size(86, 19);
            this.checkBoxZoom.TabIndex = 10;
            this.checkBoxZoom.Text = "Zoom to fit";
            this.checkBoxZoom.UseVisualStyleBackColor = true;
            this.checkBoxZoom.CheckedChanged += new System.EventHandler(this.checkBoxZoom_CheckedChanged);
            // 
            // CellNumber
            // 
            this.CellNumber.Enabled = false;
            this.CellNumber.Location = new System.Drawing.Point(94, 193);
            this.CellNumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.CellNumber.Name = "CellNumber";
            this.CellNumber.Size = new System.Drawing.Size(58, 23);
            this.CellNumber.TabIndex = 9;
            this.CellNumber.ValueChanged += new System.EventHandler(this.CellNumber_ValueChanged);
            // 
            // CellInfo
            // 
            this.CellInfo.Location = new System.Drawing.Point(17, 222);
            this.CellInfo.Multiline = true;
            this.CellInfo.Name = "CellInfo";
            this.CellInfo.ReadOnly = true;
            this.CellInfo.Size = new System.Drawing.Size(161, 133);
            this.CellInfo.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Cell";
            // 
            // displayBox
            // 
            this.displayBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.displayBox.BackColor = System.Drawing.Color.Black;
            this.displayBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.displayBox.Location = new System.Drawing.Point(216, 11);
            this.displayBox.Name = "displayBox";
            this.displayBox.Size = new System.Drawing.Size(523, 463);
            this.displayBox.TabIndex = 6;
            this.displayBox.TabStop = false;
            // 
            // FrameInfo
            // 
            this.FrameInfo.Location = new System.Drawing.Point(17, 37);
            this.FrameInfo.Multiline = true;
            this.FrameInfo.Name = "FrameInfo";
            this.FrameInfo.ReadOnly = true;
            this.FrameInfo.Size = new System.Drawing.Size(161, 122);
            this.FrameInfo.TabIndex = 5;
            // 
            // FrameNumber
            // 
            this.FrameNumber.Enabled = false;
            this.FrameNumber.Location = new System.Drawing.Point(94, 8);
            this.FrameNumber.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.FrameNumber.Name = "FrameNumber";
            this.FrameNumber.Size = new System.Drawing.Size(58, 23);
            this.FrameNumber.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Frame";
            // 
            // openPalDialog
            // 
            this.openPalDialog.DefaultExt = "pal";
            this.openPalDialog.Filter = "Dark Forces Palette Files|*.pal";
            this.openPalDialog.Title = "Open PAL";
            this.openPalDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openPalDialog_FileOk);
            // 
            // exportDialog
            // 
            this.exportDialog.AddExtension = false;
            this.exportDialog.Title = "Export name";
            this.exportDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.exportDialog_FileOk);
            // 
            // openFmeDialog
            // 
            this.openFmeDialog.DefaultExt = "wax";
            this.openFmeDialog.Filter = "Dark Forces FME files|*.fme";
            this.openFmeDialog.Title = "Open FME file";
            this.openFmeDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFmeDialog_FileOk);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 681);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(960, 720);
            this.Name = "MainWindow";
            this.Text = "WAX Converter (version 1.0)";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.RadioGroup.ResumeLayout(false);
            this.RadioGroup.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SeqNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewNumber)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CellNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrameNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuOpenWax;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.Label labelWax;
        private System.Windows.Forms.ToolStripMenuItem MenuSaveBMP;
        private System.Windows.Forms.ToolStripMenuItem MenuQuit;
        private System.Windows.Forms.OpenFileDialog openWaxDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.TextBox WaxDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ActionNumber;
        private System.Windows.Forms.TextBox ActionInfo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown ViewNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SeqInfo;
        private System.Windows.Forms.NumericUpDown FrameNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FrameInfo;
        private System.Windows.Forms.ToolStripMenuItem MenuLoadPal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuCloseWax;
        private System.Windows.Forms.OpenFileDialog openPalDialog;
        private System.Windows.Forms.Label LabelPal;
        private System.Windows.Forms.PictureBox displayBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown CellNumber;
        private System.Windows.Forms.TextBox CellInfo;
        private System.Windows.Forms.GroupBox RadioGroup;
        private System.Windows.Forms.RadioButton radioCell;
        private System.Windows.Forms.RadioButton radioFrame;
        private System.Windows.Forms.RadioButton radioSequence;
        private System.Windows.Forms.RadioButton radioAction;
        private System.Windows.Forms.NumericUpDown SeqNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SaveFileDialog exportDialog;
        private System.Windows.Forms.Button SeqNextFrame;
        private System.Windows.Forms.Button SeqPrevFrame;
        private System.Windows.Forms.ToolStripMenuItem MenuBuildWax;
        private System.Windows.Forms.ToolStripMenuItem MenuBuild;
        private System.Windows.Forms.CheckBox checkBoxZoom;
        private System.Windows.Forms.ToolStripMenuItem MenuHelphelp;
        private System.Windows.Forms.ToolStripMenuItem MenuAbout;
        private System.Windows.Forms.Label labelSeqFrame;
        private System.Windows.Forms.ToolStripMenuItem MenuOpenFme;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog openFmeDialog;
    }
}

