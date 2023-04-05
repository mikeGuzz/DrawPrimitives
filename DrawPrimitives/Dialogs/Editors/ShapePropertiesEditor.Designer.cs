namespace DrawPrimitives.Dialogs.Editors
{
    partial class ShapePropertiesEditor
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
            this.transform_button = new System.Windows.Forms.Button();
            this.transform_label = new System.Windows.Forms.Label();
            this.fill_groupBox = new System.Windows.Forms.GroupBox();
            this.fill_button = new System.Windows.Forms.Button();
            this.fill_label = new System.Windows.Forms.Label();
            this.fill_checkBox = new System.Windows.Forms.CheckBox();
            this.outline_checkBox = new System.Windows.Forms.CheckBox();
            this.outline_groupBox = new System.Windows.Forms.GroupBox();
            this.outline_button = new System.Windows.Forms.Button();
            this.outline_label = new System.Windows.Forms.Label();
            this.text_groupBox = new System.Windows.Forms.GroupBox();
            this.text_label = new System.Windows.Forms.Label();
            this.text_textBox = new System.Windows.Forms.TextBox();
            this.text_button = new System.Windows.Forms.Button();
            this.text_checkBox = new System.Windows.Forms.CheckBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.flipX_checkBox = new System.Windows.Forms.CheckBox();
            this.flipY_checkBox = new System.Windows.Forms.CheckBox();
            this.smoothMode_checkBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.fill_groupBox.SuspendLayout();
            this.outline_groupBox.SuspendLayout();
            this.text_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.transform_button);
            this.groupBox1.Controls.Add(this.transform_label);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transform";
            // 
            // transform_button
            // 
            this.transform_button.Location = new System.Drawing.Point(134, 59);
            this.transform_button.Name = "transform_button";
            this.transform_button.Size = new System.Drawing.Size(57, 23);
            this.transform_button.TabIndex = 1;
            this.transform_button.Text = "Edit";
            this.transform_button.UseVisualStyleBackColor = true;
            this.transform_button.Click += new System.EventHandler(this.transform_button_Click);
            // 
            // transform_label
            // 
            this.transform_label.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.transform_label.Location = new System.Drawing.Point(6, 19);
            this.transform_label.Name = "transform_label";
            this.transform_label.Size = new System.Drawing.Size(185, 37);
            this.transform_label.TabIndex = 0;
            this.transform_label.Text = "X: 0; Y: 0\r\nW: 0; H: 0";
            // 
            // fill_groupBox
            // 
            this.fill_groupBox.Controls.Add(this.fill_button);
            this.fill_groupBox.Controls.Add(this.fill_label);
            this.fill_groupBox.Location = new System.Drawing.Point(215, 216);
            this.fill_groupBox.Name = "fill_groupBox";
            this.fill_groupBox.Size = new System.Drawing.Size(197, 126);
            this.fill_groupBox.TabIndex = 2;
            this.fill_groupBox.TabStop = false;
            this.fill_groupBox.Text = "Fill";
            // 
            // fill_button
            // 
            this.fill_button.Location = new System.Drawing.Point(134, 97);
            this.fill_button.Name = "fill_button";
            this.fill_button.Size = new System.Drawing.Size(57, 23);
            this.fill_button.TabIndex = 1;
            this.fill_button.Text = "Edit";
            this.fill_button.UseVisualStyleBackColor = true;
            this.fill_button.Click += new System.EventHandler(this.fill_button_Click);
            // 
            // fill_label
            // 
            this.fill_label.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fill_label.Location = new System.Drawing.Point(6, 19);
            this.fill_label.Name = "fill_label";
            this.fill_label.Size = new System.Drawing.Size(185, 75);
            this.fill_label.TabIndex = 0;
            // 
            // fill_checkBox
            // 
            this.fill_checkBox.AutoSize = true;
            this.fill_checkBox.Location = new System.Drawing.Point(215, 191);
            this.fill_checkBox.Name = "fill_checkBox";
            this.fill_checkBox.Size = new System.Drawing.Size(69, 19);
            this.fill_checkBox.TabIndex = 3;
            this.fill_checkBox.Text = "Draw fill";
            this.fill_checkBox.UseVisualStyleBackColor = true;
            this.fill_checkBox.CheckedChanged += new System.EventHandler(this.fill_checkBox_CheckedChanged);
            // 
            // outline_checkBox
            // 
            this.outline_checkBox.AutoSize = true;
            this.outline_checkBox.Location = new System.Drawing.Point(215, 12);
            this.outline_checkBox.Name = "outline_checkBox";
            this.outline_checkBox.Size = new System.Drawing.Size(93, 19);
            this.outline_checkBox.TabIndex = 5;
            this.outline_checkBox.Text = "Draw outline";
            this.outline_checkBox.UseVisualStyleBackColor = true;
            this.outline_checkBox.CheckedChanged += new System.EventHandler(this.outline_checkBox_CheckedChanged);
            // 
            // outline_groupBox
            // 
            this.outline_groupBox.Controls.Add(this.outline_button);
            this.outline_groupBox.Controls.Add(this.outline_label);
            this.outline_groupBox.Location = new System.Drawing.Point(215, 37);
            this.outline_groupBox.Name = "outline_groupBox";
            this.outline_groupBox.Size = new System.Drawing.Size(197, 148);
            this.outline_groupBox.TabIndex = 4;
            this.outline_groupBox.TabStop = false;
            this.outline_groupBox.Text = "Outline";
            // 
            // outline_button
            // 
            this.outline_button.Location = new System.Drawing.Point(134, 119);
            this.outline_button.Name = "outline_button";
            this.outline_button.Size = new System.Drawing.Size(57, 23);
            this.outline_button.TabIndex = 1;
            this.outline_button.Text = "Edit";
            this.outline_button.UseVisualStyleBackColor = true;
            this.outline_button.Click += new System.EventHandler(this.outline_button_Click);
            // 
            // outline_label
            // 
            this.outline_label.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.outline_label.Location = new System.Drawing.Point(6, 19);
            this.outline_label.Name = "outline_label";
            this.outline_label.Size = new System.Drawing.Size(185, 97);
            this.outline_label.TabIndex = 0;
            this.outline_label.Text = "Color: \r\nWidth: \r\nAlignment: \r\nDash cap: \r\nDash style: ";
            // 
            // text_groupBox
            // 
            this.text_groupBox.Controls.Add(this.text_label);
            this.text_groupBox.Controls.Add(this.text_textBox);
            this.text_groupBox.Controls.Add(this.text_button);
            this.text_groupBox.Location = new System.Drawing.Point(12, 131);
            this.text_groupBox.Name = "text_groupBox";
            this.text_groupBox.Size = new System.Drawing.Size(197, 265);
            this.text_groupBox.TabIndex = 6;
            this.text_groupBox.TabStop = false;
            this.text_groupBox.Text = "Text";
            // 
            // text_label
            // 
            this.text_label.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.text_label.Location = new System.Drawing.Point(6, 19);
            this.text_label.Name = "text_label";
            this.text_label.Size = new System.Drawing.Size(185, 105);
            this.text_label.TabIndex = 2;
            this.text_label.Text = "Font family: \r\nSize: \r\nStyle: \r\nColor: \r\nText align: \r\nLine align: ";
            // 
            // text_textBox
            // 
            this.text_textBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.text_textBox.Location = new System.Drawing.Point(6, 127);
            this.text_textBox.Multiline = true;
            this.text_textBox.Name = "text_textBox";
            this.text_textBox.ReadOnly = true;
            this.text_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_textBox.Size = new System.Drawing.Size(185, 103);
            this.text_textBox.TabIndex = 3;
            // 
            // text_button
            // 
            this.text_button.Location = new System.Drawing.Point(134, 236);
            this.text_button.Name = "text_button";
            this.text_button.Size = new System.Drawing.Size(57, 23);
            this.text_button.TabIndex = 2;
            this.text_button.Text = "Edit";
            this.text_button.UseVisualStyleBackColor = true;
            this.text_button.Click += new System.EventHandler(this.text_button_Click);
            // 
            // text_checkBox
            // 
            this.text_checkBox.AutoSize = true;
            this.text_checkBox.Location = new System.Drawing.Point(12, 106);
            this.text_checkBox.Name = "text_checkBox";
            this.text_checkBox.Size = new System.Drawing.Size(76, 19);
            this.text_checkBox.TabIndex = 7;
            this.text_checkBox.Text = "Draw text";
            this.text_checkBox.UseVisualStyleBackColor = true;
            this.text_checkBox.CheckedChanged += new System.EventHandler(this.text_checkBox_CheckedChanged);
            // 
            // cancel_button
            // 
            this.cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel_button.Location = new System.Drawing.Point(337, 411);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 8;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // ok_button
            // 
            this.ok_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok_button.Location = new System.Drawing.Point(256, 411);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 9;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // flipX_checkBox
            // 
            this.flipX_checkBox.AutoSize = true;
            this.flipX_checkBox.Location = new System.Drawing.Point(215, 348);
            this.flipX_checkBox.Name = "flipX_checkBox";
            this.flipX_checkBox.Size = new System.Drawing.Size(101, 19);
            this.flipX_checkBox.TabIndex = 10;
            this.flipX_checkBox.Text = "Horizontal flip";
            this.flipX_checkBox.UseVisualStyleBackColor = true;
            // 
            // flipY_checkBox
            // 
            this.flipY_checkBox.AutoSize = true;
            this.flipY_checkBox.Location = new System.Drawing.Point(322, 348);
            this.flipY_checkBox.Name = "flipY_checkBox";
            this.flipY_checkBox.Size = new System.Drawing.Size(84, 19);
            this.flipY_checkBox.TabIndex = 11;
            this.flipY_checkBox.Text = "Vertical flip";
            this.flipY_checkBox.UseVisualStyleBackColor = true;
            // 
            // smoothMode_checkBox
            // 
            this.smoothMode_checkBox.AutoSize = true;
            this.smoothMode_checkBox.Location = new System.Drawing.Point(215, 377);
            this.smoothMode_checkBox.Name = "smoothMode_checkBox";
            this.smoothMode_checkBox.Size = new System.Drawing.Size(85, 19);
            this.smoothMode_checkBox.TabIndex = 12;
            this.smoothMode_checkBox.Text = "Smoothing";
            this.smoothMode_checkBox.UseVisualStyleBackColor = true;
            // 
            // ShapePropertiesEditor
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(423, 441);
            this.Controls.Add(this.smoothMode_checkBox);
            this.Controls.Add(this.flipY_checkBox);
            this.Controls.Add(this.flipX_checkBox);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.text_checkBox);
            this.Controls.Add(this.text_groupBox);
            this.Controls.Add(this.outline_checkBox);
            this.Controls.Add(this.outline_groupBox);
            this.Controls.Add(this.fill_checkBox);
            this.Controls.Add(this.fill_groupBox);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShapePropertiesEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shape Editor";
            this.groupBox1.ResumeLayout(false);
            this.fill_groupBox.ResumeLayout(false);
            this.outline_groupBox.ResumeLayout(false);
            this.text_groupBox.ResumeLayout(false);
            this.text_groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private Button transform_button;
        private Label transform_label;
        private GroupBox fill_groupBox;
        private Button fill_button;
        private Label fill_label;
        private CheckBox fill_checkBox;
        private CheckBox outline_checkBox;
        private GroupBox outline_groupBox;
        private Button outline_button;
        private Label outline_label;
        private GroupBox text_groupBox;
        private TextBox text_textBox;
        private Button text_button;
        private CheckBox text_checkBox;
        private Button cancel_button;
        private Button ok_button;
        private Label text_label;
        private CheckBox flipX_checkBox;
        private CheckBox flipY_checkBox;
        private CheckBox smoothMode_checkBox;
    }
}