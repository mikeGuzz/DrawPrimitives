namespace DrawPrimitives.Dialogs.Editors
{
    partial class TextEditor
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.loadFont_button = new System.Windows.Forms.Button();
            this.fontProperties_label = new System.Windows.Forms.Label();
            this.pickColor_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lineAlig_comboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textAlig_comboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.AcceptsReturn = true;
            this.textBox.AcceptsTab = true;
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.Location = new System.Drawing.Point(12, 12);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(310, 231);
            this.textBox.TabIndex = 0;
            // 
            // loadFont_button
            // 
            this.loadFont_button.Location = new System.Drawing.Point(131, 95);
            this.loadFont_button.Name = "loadFont_button";
            this.loadFont_button.Size = new System.Drawing.Size(75, 23);
            this.loadFont_button.TabIndex = 1;
            this.loadFont_button.Text = "Load Font";
            this.loadFont_button.UseVisualStyleBackColor = true;
            this.loadFont_button.Click += new System.EventHandler(this.loadFont_button_Click);
            // 
            // fontProperties_label
            // 
            this.fontProperties_label.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fontProperties_label.Location = new System.Drawing.Point(6, 19);
            this.fontProperties_label.Name = "fontProperties_label";
            this.fontProperties_label.Size = new System.Drawing.Size(200, 73);
            this.fontProperties_label.TabIndex = 0;
            this.fontProperties_label.Text = "Font family: \r\nSize: \r\nStyle: \r\nColor: ";
            // 
            // pickColor_button
            // 
            this.pickColor_button.Location = new System.Drawing.Point(50, 95);
            this.pickColor_button.Name = "pickColor_button";
            this.pickColor_button.Size = new System.Drawing.Size(75, 23);
            this.pickColor_button.TabIndex = 2;
            this.pickColor_button.Text = "Pick Color";
            this.pickColor_button.UseVisualStyleBackColor = true;
            this.pickColor_button.Click += new System.EventHandler(this.pickColor_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lineAlig_comboBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textAlig_comboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.fontProperties_label);
            this.groupBox1.Controls.Add(this.pickColor_button);
            this.groupBox1.Controls.Add(this.loadFont_button);
            this.groupBox1.Location = new System.Drawing.Point(328, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 191);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Font properties";
            // 
            // lineAlig_comboBox
            // 
            this.lineAlig_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lineAlig_comboBox.FormattingEnabled = true;
            this.lineAlig_comboBox.Location = new System.Drawing.Point(96, 158);
            this.lineAlig_comboBox.Name = "lineAlig_comboBox";
            this.lineAlig_comboBox.Size = new System.Drawing.Size(110, 23);
            this.lineAlig_comboBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Line aligment: ";
            // 
            // textAlig_comboBox
            // 
            this.textAlig_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textAlig_comboBox.FormattingEnabled = true;
            this.textAlig_comboBox.Location = new System.Drawing.Point(96, 129);
            this.textAlig_comboBox.Name = "textAlig_comboBox";
            this.textAlig_comboBox.Size = new System.Drawing.Size(110, 23);
            this.textAlig_comboBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Text aligment: ";
            // 
            // cancel_button
            // 
            this.cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel_button.Location = new System.Drawing.Point(467, 220);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 4;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // ok_button
            // 
            this.ok_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok_button.Location = new System.Drawing.Point(386, 220);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 5;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // TextEditor
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(553, 255);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextEditor";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Text Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox;
        private Button loadFont_button;
        private Label fontProperties_label;
        private Button pickColor_button;
        private GroupBox groupBox1;
        private Button cancel_button;
        private Button ok_button;
        private ComboBox lineAlig_comboBox;
        private Label label3;
        private ComboBox textAlig_comboBox;
        private Label label2;
    }
}