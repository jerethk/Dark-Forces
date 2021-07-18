using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WAX_converter
{
    class HelpWindow : Form
    {
        private TextBox text;

        public HelpWindow() 
        {
            InitializeComponent();

            string[] helpText = new string[40];
            helpText[0] = "Actions = the sprite's animated actions (walking, shooting, dying, etc.)";
            helpText[1] = "Views = 32 angles from which the sprite can be viewed in-game";
            helpText[2] = "Sequence = a series of up to 32 frames";
            helpText[4] = "WAX BUILDER WINDOW ";
            helpText[5] = "1. Select a PAL file. (Secbase.pal is loaded by default)";
            helpText[6] = "2. Add cells from image files (PNG, BMP or JPEG)";
            helpText[7] = "3. Create frames from cells";
            helpText[8] = "4. Create sequences from frames. Each sequence can have up to 32 frames";
            helpText[9] = "5. Compile sequences into actions";
            helpText[10] = "6. Don't forget to set a transparency colour! This is the colour that will be set to transparent in your WAX.";
            helpText[12] = "NOTES ";
            helpText[13] = "The number of actions you need (from 1 to 14) depends on the logic you plan to use. ";
            helpText[14] = "For example, most enemy logics require 13 actions to be defined. ";
            helpText[15] = "Each of an action's 32 views can be assigned a different sequence. Typically, only 8 viewing angles are designed but you are free to make 32. ";
            helpText[16] = "You can save your work-in-progress (WIP), which creates a text file (extension WWIP) containing the work you've done so far.";
            helpText[17] = "Wwidth and Wheight represent the size that the sprite appears in-game. Smaller values result in a smaller in-game sprites. For reference, DF enemy sprites (like stormtrooper) are about 70 pixels high, and have Wheight of about 70000. You can make a higher resolution enemy by creating images 140 pixels high and setting Wheight to 35000.";
            helpText[20] = "TECHNICAL NOTES";
            helpText[21] = "Using compression for cells that are larger than 256 pixels in height seems to cause problems in-game (???)";
            this.text.Lines = helpText;
        }
        
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpWindow));
            this.text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // text
            // 
            this.text.AcceptsReturn = true;
            this.text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text.Location = new System.Drawing.Point(23, 11);
            this.text.Multiline = true;
            this.text.Name = "text";
            this.text.PlaceholderText = "blah bl ah blah";
            this.text.ReadOnly = true;
            this.text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text.Size = new System.Drawing.Size(681, 421);
            this.text.TabIndex = 0;
            this.text.Text = "blah bl ah blah";
            // 
            // HelpWindow
            // 
            this.ClientSize = new System.Drawing.Size(730, 456);
            this.Controls.Add(this.text);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HelpWindow";
            this.Text = "\"Help\"";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
