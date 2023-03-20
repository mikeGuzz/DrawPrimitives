namespace DrawPrimitives.Dialog.SetupDialogs
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
            this.width_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.height_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pickColor_button = new System.Windows.Forms.Button();
            this.color_pictureBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.width_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.height_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Width: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Height: ";
            // 
            // width_numericUpDown
            // 
            this.width_numericUpDown.Location = new System.Drawing.Point(67, 12);
            this.width_numericUpDown.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.width_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.width_numericUpDown.Name = "width_numericUpDown";
            this.width_numericUpDown.Size = new System.Drawing.Size(71, 23);
            this.width_numericUpDown.TabIndex = 3;
            this.width_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "px";
            // 
            // height_numericUpDown
            // 
            this.height_numericUpDown.Location = new System.Drawing.Point(67, 41);
            this.height_numericUpDown.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.height_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.height_numericUpDown.Name = "height_numericUpDown";
            this.height_numericUpDown.Size = new System.Drawing.Size(71, 23);
            this.height_numericUpDown.TabIndex = 5;
            this.height_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "px";
            // 
            // pickColor_button
            // 
            this.pickColor_button.Location = new System.Drawing.Point(144, 70);
            this.pickColor_button.Name = "pickColor_button";
            this.pickColor_button.Size = new System.Drawing.Size(55, 23);
            this.pickColor_button.TabIndex = 1;
            this.pickColor_button.Text = "Pick";
            this.pickColor_button.UseVisualStyleBackColor = true;
            this.pickColor_button.Click += new System.EventHandler(this.pickColor_button_Click);
            // 
            // color_pictureBox
            // 
            this.color_pictureBox.BackColor = System.Drawing.Color.White;
            this.color_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.color_pictureBox.Location = new System.Drawing.Point(67, 70);
            this.color_pictureBox.Name = "color_pictureBox";
            this.color_pictureBox.Size = new System.Drawing.Size(71, 23);
            this.color_pictureBox.TabIndex = 0;
            this.color_pictureBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(124, 112);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "Color: ";
            // 
            // SetupCanvasDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 149);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pickColor_button);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.color_pictureBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.height_numericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.width_numericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupCanvasDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SetupCanvasDialog";
            ((System.ComponentModel.ISupportInitialize)(this.width_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.height_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.color_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label2;
        private Label label3;
        private NumericUpDown width_numericUpDown;
        private Label label4;
        private NumericUpDown height_numericUpDown;
        private Label label1;
        private Button pickColor_button;
        private PictureBox color_pictureBox;
        private Button button1;
        private Button button2;
        private Label label6;
    }
}