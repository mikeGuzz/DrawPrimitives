namespace DrawPrimitives.Dialogs.Editors
{
    partial class TransformEditor
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
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.y_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.x_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.h_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.w_numericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.y_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.x_numericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.h_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.w_numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(135, 145);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(57, 23);
            this.ok_button.TabIndex = 5;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(198, 145);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(57, 23);
            this.cancel_button.TabIndex = 6;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // y_numericUpDown
            // 
            this.y_numericUpDown.Location = new System.Drawing.Point(150, 22);
            this.y_numericUpDown.Name = "y_numericUpDown";
            this.y_numericUpDown.Size = new System.Drawing.Size(86, 23);
            this.y_numericUpDown.TabIndex = 7;
            // 
            // x_numericUpDown
            // 
            this.x_numericUpDown.Location = new System.Drawing.Point(32, 22);
            this.x_numericUpDown.Name = "x_numericUpDown";
            this.x_numericUpDown.Size = new System.Drawing.Size(86, 23);
            this.x_numericUpDown.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "X: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.y_numericUpDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.x_numericUpDown);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 52);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(124, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Y: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.h_numericUpDown);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.w_numericUpDown);
            this.groupBox2.Location = new System.Drawing.Point(12, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 52);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "H: ";
            // 
            // h_numericUpDown
            // 
            this.h_numericUpDown.Location = new System.Drawing.Point(150, 22);
            this.h_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.h_numericUpDown.Name = "h_numericUpDown";
            this.h_numericUpDown.Size = new System.Drawing.Size(86, 23);
            this.h_numericUpDown.TabIndex = 7;
            this.h_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.h_numericUpDown.ValueChanged += new System.EventHandler(this.SizeNumericValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "W:";
            // 
            // w_numericUpDown
            // 
            this.w_numericUpDown.Location = new System.Drawing.Point(32, 22);
            this.w_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.w_numericUpDown.Name = "w_numericUpDown";
            this.w_numericUpDown.Size = new System.Drawing.Size(86, 23);
            this.w_numericUpDown.TabIndex = 8;
            this.w_numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.w_numericUpDown.ValueChanged += new System.EventHandler(this.SizeNumericValueChanged);
            // 
            // TransformEditor
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(267, 180);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransformEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TransformEditor";
            ((System.ComponentModel.ISupportInitialize)(this.y_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.x_numericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.h_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.w_numericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button ok_button;
        private Button cancel_button;
        private NumericUpDown y_numericUpDown;
        private NumericUpDown x_numericUpDown;
        private Label label3;
        private GroupBox groupBox1;
        private Label label4;
        private GroupBox groupBox2;
        private Label label1;
        private NumericUpDown h_numericUpDown;
        private Label label2;
        private NumericUpDown w_numericUpDown;
    }
}