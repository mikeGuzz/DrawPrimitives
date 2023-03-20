namespace DrawPrimitives
{
    partial class BrushDialog
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
            this.mainColor_groupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mainColorOpacity_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pickMainColor_button = new System.Windows.Forms.Button();
            this.mainColor_pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hatch_groupBox = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hatchColorOpacity_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.hatchColorPick_button = new System.Windows.Forms.Button();
            this.hatchColor_pictureBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.hatchStyle_comboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.preview_pictureBox = new System.Windows.Forms.PictureBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.type_comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.texture_groupBox = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.wrapMode_comboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.path_textBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.mainColor_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainColorOpacity_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainColor_pictureBox)).BeginInit();
            this.hatch_groupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hatchColorOpacity_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hatchColor_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview_pictureBox)).BeginInit();
            this.texture_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainColor_groupBox
            // 
            this.mainColor_groupBox.Controls.Add(this.label4);
            this.mainColor_groupBox.Controls.Add(this.mainColorOpacity_numericUpDown);
            this.mainColor_groupBox.Controls.Add(this.pickMainColor_button);
            this.mainColor_groupBox.Controls.Add(this.mainColor_pictureBox);
            this.mainColor_groupBox.Controls.Add(this.label1);
            this.mainColor_groupBox.Enabled = false;
            this.mainColor_groupBox.Location = new System.Drawing.Point(12, 12);
            this.mainColor_groupBox.Name = "mainColor_groupBox";
            this.mainColor_groupBox.Size = new System.Drawing.Size(221, 80);
            this.mainColor_groupBox.TabIndex = 0;
            this.mainColor_groupBox.TabStop = false;
            this.mainColor_groupBox.Text = "Main color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Opacity: ";
            // 
            // mainColorOpacity_numericUpDown
            // 
            this.mainColorOpacity_numericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.mainColorOpacity_numericUpDown.Location = new System.Drawing.Point(66, 51);
            this.mainColorOpacity_numericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.mainColorOpacity_numericUpDown.Name = "mainColorOpacity_numericUpDown";
            this.mainColorOpacity_numericUpDown.Size = new System.Drawing.Size(89, 23);
            this.mainColorOpacity_numericUpDown.TabIndex = 10;
            this.mainColorOpacity_numericUpDown.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.mainColorOpacity_numericUpDown.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // pickMainColor_button
            // 
            this.pickMainColor_button.Location = new System.Drawing.Point(161, 22);
            this.pickMainColor_button.Name = "pickMainColor_button";
            this.pickMainColor_button.Size = new System.Drawing.Size(54, 23);
            this.pickMainColor_button.TabIndex = 9;
            this.pickMainColor_button.Text = "Pick";
            this.pickMainColor_button.UseVisualStyleBackColor = true;
            this.pickMainColor_button.Click += new System.EventHandler(this.pickMainColor_button_Click);
            // 
            // mainColor_pictureBox
            // 
            this.mainColor_pictureBox.BackColor = System.Drawing.Color.Black;
            this.mainColor_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainColor_pictureBox.Location = new System.Drawing.Point(66, 22);
            this.mainColor_pictureBox.Name = "mainColor_pictureBox";
            this.mainColor_pictureBox.Size = new System.Drawing.Size(89, 23);
            this.mainColor_pictureBox.TabIndex = 8;
            this.mainColor_pictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Color: ";
            // 
            // hatch_groupBox
            // 
            this.hatch_groupBox.Controls.Add(this.groupBox2);
            this.hatch_groupBox.Controls.Add(this.hatchStyle_comboBox);
            this.hatch_groupBox.Controls.Add(this.label6);
            this.hatch_groupBox.Enabled = false;
            this.hatch_groupBox.Location = new System.Drawing.Point(12, 98);
            this.hatch_groupBox.Name = "hatch_groupBox";
            this.hatch_groupBox.Size = new System.Drawing.Size(234, 141);
            this.hatch_groupBox.TabIndex = 4;
            this.hatch_groupBox.TabStop = false;
            this.hatch_groupBox.Text = "Hatch";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.hatchColorOpacity_numericUpDown);
            this.groupBox2.Controls.Add(this.hatchColorPick_button);
            this.groupBox2.Controls.Add(this.hatchColor_pictureBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(6, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 80);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hatch color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Opacity: ";
            // 
            // hatchColorOpacity_numericUpDown
            // 
            this.hatchColorOpacity_numericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.hatchColorOpacity_numericUpDown.Location = new System.Drawing.Point(66, 51);
            this.hatchColorOpacity_numericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.hatchColorOpacity_numericUpDown.Name = "hatchColorOpacity_numericUpDown";
            this.hatchColorOpacity_numericUpDown.Size = new System.Drawing.Size(89, 23);
            this.hatchColorOpacity_numericUpDown.TabIndex = 10;
            this.hatchColorOpacity_numericUpDown.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // hatchColorPick_button
            // 
            this.hatchColorPick_button.Location = new System.Drawing.Point(161, 22);
            this.hatchColorPick_button.Name = "hatchColorPick_button";
            this.hatchColorPick_button.Size = new System.Drawing.Size(54, 23);
            this.hatchColorPick_button.TabIndex = 9;
            this.hatchColorPick_button.Text = "Pick";
            this.hatchColorPick_button.UseVisualStyleBackColor = true;
            this.hatchColorPick_button.Click += new System.EventHandler(this.hatchColorPick_button_Click);
            // 
            // hatchColor_pictureBox
            // 
            this.hatchColor_pictureBox.BackColor = System.Drawing.Color.Black;
            this.hatchColor_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.hatchColor_pictureBox.Location = new System.Drawing.Point(66, 22);
            this.hatchColor_pictureBox.Name = "hatchColor_pictureBox";
            this.hatchColor_pictureBox.Size = new System.Drawing.Size(89, 23);
            this.hatchColor_pictureBox.TabIndex = 8;
            this.hatchColor_pictureBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Color: ";
            // 
            // hatchStyle_comboBox
            // 
            this.hatchStyle_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hatchStyle_comboBox.FormattingEnabled = true;
            this.hatchStyle_comboBox.Location = new System.Drawing.Point(50, 108);
            this.hatchStyle_comboBox.Name = "hatchStyle_comboBox";
            this.hatchStyle_comboBox.Size = new System.Drawing.Size(178, 23);
            this.hatchStyle_comboBox.TabIndex = 1;
            this.hatchStyle_comboBox.SelectedIndexChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Style: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 428);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "Preview:";
            // 
            // preview_pictureBox
            // 
            this.preview_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.preview_pictureBox.Location = new System.Drawing.Point(78, 428);
            this.preview_pictureBox.Name = "preview_pictureBox";
            this.preview_pictureBox.Size = new System.Drawing.Size(168, 54);
            this.preview_pictureBox.TabIndex = 14;
            this.preview_pictureBox.TabStop = false;
            this.preview_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.preview_pictureBox_Paint);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(172, 488);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 15;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(91, 488);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 16;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // type_comboBox
            // 
            this.type_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type_comboBox.FormattingEnabled = true;
            this.type_comboBox.Items.AddRange(new object[] {
            "Solid Color",
            "Hatch",
            "Texture"});
            this.type_comboBox.Location = new System.Drawing.Point(78, 399);
            this.type_comboBox.Name = "type_comboBox";
            this.type_comboBox.Size = new System.Drawing.Size(168, 23);
            this.type_comboBox.TabIndex = 18;
            this.type_comboBox.SelectedIndexChanged += new System.EventHandler(this.type_comboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 402);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "Type: ";
            // 
            // texture_groupBox
            // 
            this.texture_groupBox.Controls.Add(this.label9);
            this.texture_groupBox.Controls.Add(this.wrapMode_comboBox);
            this.texture_groupBox.Controls.Add(this.label8);
            this.texture_groupBox.Controls.Add(this.path_textBox);
            this.texture_groupBox.Controls.Add(this.button2);
            this.texture_groupBox.Enabled = false;
            this.texture_groupBox.Location = new System.Drawing.Point(12, 245);
            this.texture_groupBox.Name = "texture_groupBox";
            this.texture_groupBox.Size = new System.Drawing.Size(234, 148);
            this.texture_groupBox.TabIndex = 20;
            this.texture_groupBox.TabStop = false;
            this.texture_groupBox.Text = "Texture";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 15);
            this.label9.TabIndex = 25;
            this.label9.Text = "Path: ";
            // 
            // wrapMode_comboBox
            // 
            this.wrapMode_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wrapMode_comboBox.FormattingEnabled = true;
            this.wrapMode_comboBox.Location = new System.Drawing.Point(87, 117);
            this.wrapMode_comboBox.Name = "wrapMode_comboBox";
            this.wrapMode_comboBox.Size = new System.Drawing.Size(141, 23);
            this.wrapMode_comboBox.TabIndex = 24;
            this.wrapMode_comboBox.SelectedIndexChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "Wrap mode: ";
            // 
            // path_textBox
            // 
            this.path_textBox.Location = new System.Drawing.Point(6, 37);
            this.path_textBox.Multiline = true;
            this.path_textBox.Name = "path_textBox";
            this.path_textBox.ReadOnly = true;
            this.path_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.path_textBox.Size = new System.Drawing.Size(221, 45);
            this.path_textBox.TabIndex = 22;
            this.path_textBox.WordWrap = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(153, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BrushSetupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 521);
            this.Controls.Add(this.texture_groupBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.type_comboBox);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.preview_pictureBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.hatch_groupBox);
            this.Controls.Add(this.mainColor_groupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BrushSetupDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BrushSetupDialog";
            this.mainColor_groupBox.ResumeLayout(false);
            this.mainColor_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainColorOpacity_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainColor_pictureBox)).EndInit();
            this.hatch_groupBox.ResumeLayout(false);
            this.hatch_groupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hatchColorOpacity_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hatchColor_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview_pictureBox)).EndInit();
            this.texture_groupBox.ResumeLayout(false);
            this.texture_groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox mainColor_groupBox;
        private Label label4;
        private NumericUpDown mainColorOpacity_numericUpDown;
        private Button pickMainColor_button;
        private PictureBox mainColor_pictureBox;
        private Label label1;
        private GroupBox hatch_groupBox;
        private ComboBox hatchStyle_comboBox;
        private Label label6;
        private Label label7;
        private PictureBox preview_pictureBox;
        private Button cancel_button;
        private Button ok_button;
        private GroupBox groupBox2;
        private Label label3;
        private NumericUpDown hatchColorOpacity_numericUpDown;
        private Button hatchColorPick_button;
        private PictureBox hatchColor_pictureBox;
        private Label label5;
        private ComboBox type_comboBox;
        private Label label2;
        private GroupBox texture_groupBox;
        private TextBox path_textBox;
        private Button button2;
        private Label label8;
        private ComboBox wrapMode_comboBox;
        private Label label9;
    }
}