namespace DrawPrimitives
{
    partial class NonSavedFileDialog
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
            this.label = new System.Windows.Forms.Label();
            this.cancel_button = new System.Windows.Forms.Button();
            this.dontSave_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label.Location = new System.Drawing.Point(12, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(338, 71);
            this.label.TabIndex = 0;
            this.label.Text = "Do you want to save changes?";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(275, 96);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 1;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // dontSave_button
            // 
            this.dontSave_button.Location = new System.Drawing.Point(194, 96);
            this.dontSave_button.Name = "dontSave_button";
            this.dontSave_button.Size = new System.Drawing.Size(75, 23);
            this.dontSave_button.TabIndex = 2;
            this.dontSave_button.Text = "Don\'t save";
            this.dontSave_button.UseVisualStyleBackColor = true;
            this.dontSave_button.Click += new System.EventHandler(this.dontSave_button_Click);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(113, 96);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(75, 23);
            this.save_button.TabIndex = 3;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // NonSavedFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 126);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.dontSave_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NonSavedFileDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NonSavedFileDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private Label label;
        private Button cancel_button;
        private Button dontSave_button;
        private Button save_button;
    }
}