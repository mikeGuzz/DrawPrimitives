namespace DrawPrimitives
{
    partial class PenSetupDialog
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
            this.opacity_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.width_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pickColor_button = new System.Windows.Forms.Button();
            this.colorPrev_pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.spaceLength_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.dashLength_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.dashStyle_comboBox = new System.Windows.Forms.ComboBox();
            this.label = new System.Windows.Forms.Label();
            this.dashCap_comboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.endCap_comboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.startCap_comboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.preview_pictureBox = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacity_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.width_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPrev_pictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spaceLength_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashLength_numericUpDown)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preview_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.opacity_numericUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.width_numericUpDown);
            this.groupBox1.Controls.Add(this.pickColor_button);
            this.groupBox1.Controls.Add(this.colorPrev_pictureBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Opacity: ";
            // 
            // opacity_numericUpDown
            // 
            this.opacity_numericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.opacity_numericUpDown.Location = new System.Drawing.Point(66, 51);
            this.opacity_numericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.opacity_numericUpDown.Name = "opacity_numericUpDown";
            this.opacity_numericUpDown.Size = new System.Drawing.Size(89, 23);
            this.opacity_numericUpDown.TabIndex = 5;
            this.opacity_numericUpDown.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.opacity_numericUpDown.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Width: ";
            // 
            // width_numericUpDown
            // 
            this.width_numericUpDown.DecimalPlaces = 2;
            this.width_numericUpDown.Location = new System.Drawing.Point(66, 80);
            this.width_numericUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.width_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.width_numericUpDown.Name = "width_numericUpDown";
            this.width_numericUpDown.Size = new System.Drawing.Size(89, 23);
            this.width_numericUpDown.TabIndex = 3;
            this.width_numericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.width_numericUpDown.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // pickColor_button
            // 
            this.pickColor_button.Location = new System.Drawing.Point(161, 22);
            this.pickColor_button.Name = "pickColor_button";
            this.pickColor_button.Size = new System.Drawing.Size(54, 23);
            this.pickColor_button.TabIndex = 2;
            this.pickColor_button.Text = "Pick";
            this.pickColor_button.UseVisualStyleBackColor = true;
            this.pickColor_button.Click += new System.EventHandler(this.pickColor_button_Click);
            // 
            // colorPrev_pictureBox
            // 
            this.colorPrev_pictureBox.BackColor = System.Drawing.Color.Black;
            this.colorPrev_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorPrev_pictureBox.Location = new System.Drawing.Point(66, 22);
            this.colorPrev_pictureBox.Name = "colorPrev_pictureBox";
            this.colorPrev_pictureBox.Size = new System.Drawing.Size(89, 23);
            this.colorPrev_pictureBox.TabIndex = 1;
            this.colorPrev_pictureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Color: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.spaceLength_numericUpDown);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.dashLength_numericUpDown);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dashStyle_comboBox);
            this.groupBox2.Controls.Add(this.label);
            this.groupBox2.Controls.Add(this.dashCap_comboBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 139);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dash";
            // 
            // spaceLength_numericUpDown
            // 
            this.spaceLength_numericUpDown.DecimalPlaces = 2;
            this.spaceLength_numericUpDown.Location = new System.Drawing.Point(90, 109);
            this.spaceLength_numericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spaceLength_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spaceLength_numericUpDown.Name = "spaceLength_numericUpDown";
            this.spaceLength_numericUpDown.Size = new System.Drawing.Size(125, 23);
            this.spaceLength_numericUpDown.TabIndex = 12;
            this.spaceLength_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spaceLength_numericUpDown.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 15);
            this.label9.TabIndex = 11;
            this.label9.Text = "Space length:";
            // 
            // dashLength_numericUpDown
            // 
            this.dashLength_numericUpDown.DecimalPlaces = 2;
            this.dashLength_numericUpDown.Location = new System.Drawing.Point(90, 80);
            this.dashLength_numericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.dashLength_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.dashLength_numericUpDown.Name = "dashLength_numericUpDown";
            this.dashLength_numericUpDown.Size = new System.Drawing.Size(125, 23);
            this.dashLength_numericUpDown.TabIndex = 10;
            this.dashLength_numericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.dashLength_numericUpDown.ValueChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Dash length:";
            // 
            // dashStyle_comboBox
            // 
            this.dashStyle_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dashStyle_comboBox.FormattingEnabled = true;
            this.dashStyle_comboBox.Location = new System.Drawing.Point(90, 51);
            this.dashStyle_comboBox.Name = "dashStyle_comboBox";
            this.dashStyle_comboBox.Size = new System.Drawing.Size(125, 23);
            this.dashStyle_comboBox.TabIndex = 7;
            this.dashStyle_comboBox.SelectedIndexChanged += new System.EventHandler(this.dashStyle_comboBox_SelectedIndexChanged);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(6, 54);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(63, 15);
            this.label.TabIndex = 6;
            this.label.Text = "Dash style:";
            // 
            // dashCap_comboBox
            // 
            this.dashCap_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dashCap_comboBox.FormattingEnabled = true;
            this.dashCap_comboBox.Location = new System.Drawing.Point(90, 22);
            this.dashCap_comboBox.Name = "dashCap_comboBox";
            this.dashCap_comboBox.Size = new System.Drawing.Size(125, 23);
            this.dashCap_comboBox.TabIndex = 1;
            this.dashCap_comboBox.SelectedIndexChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Dash cap:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.endCap_comboBox);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.startCap_comboBox);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(12, 272);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 83);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cap";
            // 
            // endCap_comboBox
            // 
            this.endCap_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.endCap_comboBox.FormattingEnabled = true;
            this.endCap_comboBox.Location = new System.Drawing.Point(68, 51);
            this.endCap_comboBox.Name = "endCap_comboBox";
            this.endCap_comboBox.Size = new System.Drawing.Size(147, 23);
            this.endCap_comboBox.TabIndex = 14;
            this.endCap_comboBox.SelectedIndexChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "End cap:";
            // 
            // startCap_comboBox
            // 
            this.startCap_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startCap_comboBox.FormattingEnabled = true;
            this.startCap_comboBox.Location = new System.Drawing.Point(68, 22);
            this.startCap_comboBox.Name = "startCap_comboBox";
            this.startCap_comboBox.Size = new System.Drawing.Size(147, 23);
            this.startCap_comboBox.TabIndex = 12;
            this.startCap_comboBox.SelectedIndexChanged += new System.EventHandler(this.UpdatePreview);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Start cap:";
            // 
            // preview_pictureBox
            // 
            this.preview_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.preview_pictureBox.Location = new System.Drawing.Point(72, 361);
            this.preview_pictureBox.Name = "preview_pictureBox";
            this.preview_pictureBox.Size = new System.Drawing.Size(161, 45);
            this.preview_pictureBox.TabIndex = 5;
            this.preview_pictureBox.TabStop = false;
            this.preview_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.preview_pictureBox_Paint);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 361);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "Preview: ";
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(77, 412);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 7;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(158, 412);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 8;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // PenSetupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 444);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.preview_pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PenSetupDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tool setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.opacity_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.width_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPrev_pictureBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spaceLength_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashLength_numericUpDown)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preview_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private Label label2;
        private NumericUpDown width_numericUpDown;
        private Button pickColor_button;
        private PictureBox colorPrev_pictureBox;
        private Label label1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ComboBox dashStyle_comboBox;
        private Label label;
        private PictureBox preview_pictureBox;
        private ComboBox dashCap_comboBox;
        private Label label3;
        private NumericUpDown dashLength_numericUpDown;
        private Label label5;
        private ComboBox endCap_comboBox;
        private Label label7;
        private ComboBox startCap_comboBox;
        private Label label6;
        private Label label8;
        private Button ok_button;
        private Button cancel_button;
        private NumericUpDown opacity_numericUpDown;
        private Label label4;
        private NumericUpDown spaceLength_numericUpDown;
        private Label label9;
    }
}