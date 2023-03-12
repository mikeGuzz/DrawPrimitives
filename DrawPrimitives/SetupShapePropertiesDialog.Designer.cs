namespace DrawPrimitives
{
    partial class SetupShapePropertiesDialog
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
            this.size_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.position_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.brushPick_button = new System.Windows.Forms.Button();
            this.fill_pictureBox = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pickPen_button = new System.Windows.Forms.Button();
            this.pen_pictureBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_groupBox = new System.Windows.Forms.GroupBox();
            this.pickFont_button = new System.Windows.Forms.Button();
            this.fontInfo_label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fill_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pen_pictureBox)).BeginInit();
            this.text_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.size_textBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.position_textBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transform";
            // 
            // size_textBox
            // 
            this.size_textBox.Location = new System.Drawing.Point(68, 51);
            this.size_textBox.MaxLength = 200;
            this.size_textBox.Name = "size_textBox";
            this.size_textBox.Size = new System.Drawing.Size(125, 23);
            this.size_textBox.TabIndex = 3;
            this.size_textBox.Text = "0, 0";
            this.size_textBox.Leave += new System.EventHandler(this.NumTextBoxFocusLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Size: ";
            // 
            // position_textBox
            // 
            this.position_textBox.Location = new System.Drawing.Point(68, 22);
            this.position_textBox.MaxLength = 200;
            this.position_textBox.Name = "position_textBox";
            this.position_textBox.Size = new System.Drawing.Size(125, 23);
            this.position_textBox.TabIndex = 1;
            this.position_textBox.Text = "0, 0";
            this.position_textBox.Leave += new System.EventHandler(this.NumTextBoxFocusLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Position: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.brushPick_button);
            this.groupBox2.Controls.Add(this.fill_pictureBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.pickPen_button);
            this.groupBox2.Controls.Add(this.pen_pictureBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(201, 195);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Graphics";
            // 
            // brushPick_button
            // 
            this.brushPick_button.Location = new System.Drawing.Point(142, 163);
            this.brushPick_button.Name = "brushPick_button";
            this.brushPick_button.Size = new System.Drawing.Size(49, 23);
            this.brushPick_button.TabIndex = 5;
            this.brushPick_button.Text = "Pick";
            this.brushPick_button.UseVisualStyleBackColor = true;
            this.brushPick_button.Click += new System.EventHandler(this.pick_button_Click);
            // 
            // fill_pictureBox
            // 
            this.fill_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fill_pictureBox.Location = new System.Drawing.Point(49, 107);
            this.fill_pictureBox.Name = "fill_pictureBox";
            this.fill_pictureBox.Size = new System.Drawing.Size(142, 50);
            this.fill_pictureBox.TabIndex = 4;
            this.fill_pictureBox.TabStop = false;
            this.fill_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.fill_pictureBox_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fill: ";
            // 
            // pickPen_button
            // 
            this.pickPen_button.Location = new System.Drawing.Point(142, 78);
            this.pickPen_button.Name = "pickPen_button";
            this.pickPen_button.Size = new System.Drawing.Size(49, 23);
            this.pickPen_button.TabIndex = 2;
            this.pickPen_button.Text = "Pick";
            this.pickPen_button.UseVisualStyleBackColor = true;
            this.pickPen_button.Click += new System.EventHandler(this.pickPen_button_Click);
            // 
            // pen_pictureBox
            // 
            this.pen_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pen_pictureBox.Location = new System.Drawing.Point(49, 22);
            this.pen_pictureBox.Name = "pen_pictureBox";
            this.pen_pictureBox.Size = new System.Drawing.Size(142, 50);
            this.pen_pictureBox.TabIndex = 1;
            this.pen_pictureBox.TabStop = false;
            this.pen_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pen_pictureBox_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Pen: ";
            // 
            // text_groupBox
            // 
            this.text_groupBox.Controls.Add(this.pickFont_button);
            this.text_groupBox.Controls.Add(this.fontInfo_label);
            this.text_groupBox.Controls.Add(this.label6);
            this.text_groupBox.Controls.Add(this.textBox);
            this.text_groupBox.Location = new System.Drawing.Point(12, 301);
            this.text_groupBox.Name = "text_groupBox";
            this.text_groupBox.Size = new System.Drawing.Size(201, 135);
            this.text_groupBox.TabIndex = 2;
            this.text_groupBox.TabStop = false;
            this.text_groupBox.Text = "Text";
            // 
            // pickFont_button
            // 
            this.pickFont_button.Location = new System.Drawing.Point(145, 105);
            this.pickFont_button.Name = "pickFont_button";
            this.pickFont_button.Size = new System.Drawing.Size(49, 23);
            this.pickFont_button.TabIndex = 8;
            this.pickFont_button.Text = "Pick";
            this.pickFont_button.UseVisualStyleBackColor = true;
            this.pickFont_button.Click += new System.EventHandler(this.pickFont_button_Click);
            // 
            // fontInfo_label
            // 
            this.fontInfo_label.AutoSize = true;
            this.fontInfo_label.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fontInfo_label.Location = new System.Drawing.Point(49, 84);
            this.fontInfo_label.Name = "fontInfo_label";
            this.fontInfo_label.Size = new System.Drawing.Size(36, 15);
            this.fontInfo_label.TabIndex = 2;
            this.fontInfo_label.Text = "None";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "Font: ";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(6, 22);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(185, 59);
            this.textBox.TabIndex = 0;
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(138, 442);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 3;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(57, 442);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 4;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // SetupShapePropertiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 473);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.text_groupBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupShapePropertiesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shape setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fill_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pen_pictureBox)).EndInit();
            this.text_groupBox.ResumeLayout(false);
            this.text_groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private TextBox size_textBox;
        private Label label2;
        private TextBox position_textBox;
        private GroupBox groupBox2;
        private Button brushPick_button;
        private PictureBox fill_pictureBox;
        private Label label4;
        private Button pickPen_button;
        private PictureBox pen_pictureBox;
        private Label label3;
        private GroupBox text_groupBox;
        private Button pickFont_button;
        private Label fontInfo_label;
        private Label label6;
        private TextBox textBox;
        private Button cancel_button;
        private Button ok_button;
    }
}