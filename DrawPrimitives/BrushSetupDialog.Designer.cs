namespace DrawPrimitives
{
    partial class BrushSetupDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mainColorOpacity_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pickMainColor_button = new System.Windows.Forms.Button();
            this.mainColor_pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hatch_groupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hatchStyle_comboBox = new System.Windows.Forms.ComboBox();
            this.secondColorOpacity_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.pickSecondColor_button = new System.Windows.Forms.Button();
            this.secondColor_pictureBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.preview_pictureBox = new System.Windows.Forms.PictureBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.useHatch_checkBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainColorOpacity_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainColor_pictureBox)).BeginInit();
            this.hatch_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondColorOpacity_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondColor_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.mainColorOpacity_numericUpDown);
            this.groupBox1.Controls.Add(this.pickMainColor_button);
            this.groupBox1.Controls.Add(this.mainColor_pictureBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main color";
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
            this.hatch_groupBox.Controls.Add(this.label3);
            this.hatch_groupBox.Controls.Add(this.hatchStyle_comboBox);
            this.hatch_groupBox.Controls.Add(this.secondColorOpacity_numericUpDown);
            this.hatch_groupBox.Controls.Add(this.label6);
            this.hatch_groupBox.Controls.Add(this.pickSecondColor_button);
            this.hatch_groupBox.Controls.Add(this.secondColor_pictureBox);
            this.hatch_groupBox.Controls.Add(this.label5);
            this.hatch_groupBox.Enabled = false;
            this.hatch_groupBox.Location = new System.Drawing.Point(12, 123);
            this.hatch_groupBox.Name = "hatch_groupBox";
            this.hatch_groupBox.Size = new System.Drawing.Size(221, 117);
            this.hatch_groupBox.TabIndex = 4;
            this.hatch_groupBox.TabStop = false;
            this.hatch_groupBox.Text = "Hatch setting";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Opacity: ";
            // 
            // hatchStyle_comboBox
            // 
            this.hatchStyle_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hatchStyle_comboBox.FormattingEnabled = true;
            this.hatchStyle_comboBox.Location = new System.Drawing.Point(65, 88);
            this.hatchStyle_comboBox.Name = "hatchStyle_comboBox";
            this.hatchStyle_comboBox.Size = new System.Drawing.Size(149, 23);
            this.hatchStyle_comboBox.TabIndex = 1;
            this.hatchStyle_comboBox.SelectedIndexChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // secondColorOpacity_numericUpDown
            // 
            this.secondColorOpacity_numericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.secondColorOpacity_numericUpDown.Location = new System.Drawing.Point(65, 51);
            this.secondColorOpacity_numericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.secondColorOpacity_numericUpDown.Name = "secondColorOpacity_numericUpDown";
            this.secondColorOpacity_numericUpDown.Size = new System.Drawing.Size(89, 23);
            this.secondColorOpacity_numericUpDown.TabIndex = 10;
            this.secondColorOpacity_numericUpDown.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.secondColorOpacity_numericUpDown.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Style: ";
            // 
            // pickSecondColor_button
            // 
            this.pickSecondColor_button.Location = new System.Drawing.Point(160, 22);
            this.pickSecondColor_button.Name = "pickSecondColor_button";
            this.pickSecondColor_button.Size = new System.Drawing.Size(54, 23);
            this.pickSecondColor_button.TabIndex = 9;
            this.pickSecondColor_button.Text = "Pick";
            this.pickSecondColor_button.UseVisualStyleBackColor = true;
            this.pickSecondColor_button.Click += new System.EventHandler(this.pickSecondColor_button_Click);
            // 
            // secondColor_pictureBox
            // 
            this.secondColor_pictureBox.BackColor = System.Drawing.Color.Black;
            this.secondColor_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.secondColor_pictureBox.Location = new System.Drawing.Point(65, 22);
            this.secondColor_pictureBox.Name = "secondColor_pictureBox";
            this.secondColor_pictureBox.Size = new System.Drawing.Size(89, 23);
            this.secondColor_pictureBox.TabIndex = 8;
            this.secondColor_pictureBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Color: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 246);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "Preview:";
            // 
            // preview_pictureBox
            // 
            this.preview_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.preview_pictureBox.Location = new System.Drawing.Point(68, 246);
            this.preview_pictureBox.Name = "preview_pictureBox";
            this.preview_pictureBox.Size = new System.Drawing.Size(164, 54);
            this.preview_pictureBox.TabIndex = 14;
            this.preview_pictureBox.TabStop = false;
            this.preview_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.preview_pictureBox_Paint);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(158, 306);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 15;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(77, 306);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 16;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // useHatch_checkBox
            // 
            this.useHatch_checkBox.AutoSize = true;
            this.useHatch_checkBox.Location = new System.Drawing.Point(12, 98);
            this.useHatch_checkBox.Name = "useHatch_checkBox";
            this.useHatch_checkBox.Size = new System.Drawing.Size(78, 19);
            this.useHatch_checkBox.TabIndex = 17;
            this.useHatch_checkBox.Text = "Use hatch";
            this.useHatch_checkBox.UseVisualStyleBackColor = true;
            this.useHatch_checkBox.CheckedChanged += new System.EventHandler(this.useHatch_checkBox_CheckedChanged);
            // 
            // BrushSetupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 340);
            this.Controls.Add(this.useHatch_checkBox);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.preview_pictureBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.hatch_groupBox);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BrushSetupDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BrushSetupDialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainColorOpacity_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainColor_pictureBox)).EndInit();
            this.hatch_groupBox.ResumeLayout(false);
            this.hatch_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondColorOpacity_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondColor_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private Label label4;
        private NumericUpDown mainColorOpacity_numericUpDown;
        private Button pickMainColor_button;
        private PictureBox mainColor_pictureBox;
        private Label label1;
        private GroupBox hatch_groupBox;
        private Label label3;
        private NumericUpDown secondColorOpacity_numericUpDown;
        private Button pickSecondColor_button;
        private PictureBox secondColor_pictureBox;
        private Label label5;
        private ComboBox hatchStyle_comboBox;
        private Label label6;
        private Label label7;
        private PictureBox preview_pictureBox;
        private Button cancel_button;
        private Button ok_button;
        private CheckBox useHatch_checkBox;
    }
}