namespace DrawPrimitives.Dialogs.Editors
{
    partial class CanvasDialog
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.w_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.h_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.opacity_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pickColor_button = new System.Windows.Forms.Button();
            this.colorPrev_pictureBox = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.w_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.h_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opacity_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPrev_pictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Width: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Height: ";
            // 
            // w_numericUpDown
            // 
            this.w_numericUpDown.Location = new System.Drawing.Point(73, 99);
            this.w_numericUpDown.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.w_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.w_numericUpDown.Name = "w_numericUpDown";
            this.w_numericUpDown.Size = new System.Drawing.Size(71, 23);
            this.w_numericUpDown.TabIndex = 3;
            this.w_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "px";
            // 
            // h_numericUpDown
            // 
            this.h_numericUpDown.Location = new System.Drawing.Point(73, 128);
            this.h_numericUpDown.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.h_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.h_numericUpDown.Name = "h_numericUpDown";
            this.h_numericUpDown.Size = new System.Drawing.Size(71, 23);
            this.h_numericUpDown.TabIndex = 5;
            this.h_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "px";
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(54, 172);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 10;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(135, 172);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 11;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "Opacity: ";
            // 
            // opacity_numericUpDown
            // 
            this.opacity_numericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.opacity_numericUpDown.Location = new System.Drawing.Point(72, 51);
            this.opacity_numericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.opacity_numericUpDown.Name = "opacity_numericUpDown";
            this.opacity_numericUpDown.Size = new System.Drawing.Size(60, 23);
            this.opacity_numericUpDown.TabIndex = 15;
            this.opacity_numericUpDown.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // pickColor_button
            // 
            this.pickColor_button.Location = new System.Drawing.Point(138, 22);
            this.pickColor_button.Name = "pickColor_button";
            this.pickColor_button.Size = new System.Drawing.Size(54, 23);
            this.pickColor_button.TabIndex = 14;
            this.pickColor_button.Text = "Pick";
            this.pickColor_button.UseVisualStyleBackColor = true;
            this.pickColor_button.Click += new System.EventHandler(this.pickColor_button_Click);
            // 
            // colorPrev_pictureBox
            // 
            this.colorPrev_pictureBox.BackColor = System.Drawing.Color.Black;
            this.colorPrev_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorPrev_pictureBox.Location = new System.Drawing.Point(72, 22);
            this.colorPrev_pictureBox.Name = "colorPrev_pictureBox";
            this.colorPrev_pictureBox.Size = new System.Drawing.Size(60, 23);
            this.colorPrev_pictureBox.TabIndex = 13;
            this.colorPrev_pictureBox.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "Color: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.colorPrev_pictureBox);
            this.groupBox1.Controls.Add(this.opacity_numericUpDown);
            this.groupBox1.Controls.Add(this.pickColor_button);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 81);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Background Fill";
            // 
            // CanvasDialog
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(220, 207);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.h_numericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.w_numericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CanvasDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Canvas Editor";
            ((System.ComponentModel.ISupportInitialize)(this.w_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.h_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opacity_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorPrev_pictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label2;
        private Label label3;
        private NumericUpDown w_numericUpDown;
        private Label label4;
        private NumericUpDown h_numericUpDown;
        private Label label1;
        private Button ok_button;
        private Button cancel_button;
        private Label label5;
        private NumericUpDown opacity_numericUpDown;
        private Button pickColor_button;
        private PictureBox colorPrev_pictureBox;
        private Label label6;
        private GroupBox groupBox1;
    }
}