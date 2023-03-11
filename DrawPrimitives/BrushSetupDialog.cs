using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawPrimitives
{
    public partial class BrushSetupDialog : Form
    {
        public BrushSetupDialog(string text)
        {
            InitializeComponent();
            Setup();
            this.Text = text;
        }

        public BrushSetupDialog(string text, Brush startValue)
        {
            InitializeComponent();
            Setup();
            this.Text = text;
            if(startValue is SolidBrush solid)
            {
                mainColor_pictureBox.BackColor = Color.FromArgb(255, solid.Color);
                mainColorOpacity_numericUpDown.Value = solid.Color.A;
            }
            else if(startValue is HatchBrush hatch)
            {
                mainColor_pictureBox.BackColor = hatch.ForegroundColor;
                mainColor_pictureBox.BackColor = Color.FromArgb(255, hatch.BackgroundColor);
                secondColor_pictureBox.BackColor = hatch.BackgroundColor;
                secondColorOpacity_numericUpDown.Value = hatch.ForegroundColor.A;
                hatchStyle_comboBox.SelectedItem = hatch.HatchStyle.ToString();
                useHatch_checkBox.Checked = true;
                hatch_groupBox.Enabled = true;
            }
        }

        private void Setup()
        {
            foreach(var ob in Enum.GetNames(typeof(HatchStyle)))
            {
                hatchStyle_comboBox.Items.Add(ob);
            }
        }

        private void pickMainColor_button_Click(object sender, EventArgs e)
        {
            var dialog = new ColorDialog();
            dialog.FullOpen = true;
            dialog.Color = mainColor_pictureBox.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mainColor_pictureBox.BackColor = dialog.Color;
            }
            preview_pictureBox.Invalidate();
        }

        public Brush GetValue()
        {
            var color1 = Color.FromArgb((int)mainColorOpacity_numericUpDown.Value, mainColor_pictureBox.BackColor);
            var color2 = Color.FromArgb((int)secondColorOpacity_numericUpDown.Value, secondColor_pictureBox.BackColor);
            if(useHatch_checkBox.Checked)
            {
                if (Enum.TryParse(typeof(HatchStyle), (string)hatchStyle_comboBox.SelectedItem, out object? value) && value != null)
                    return new HatchBrush((HatchStyle)value, color2, color1);
            }
            return new SolidBrush(color1);
        }

        private void pickSecondColor_button_Click(object sender, EventArgs e)
        {
            var dialog = new ColorDialog();
            dialog.FullOpen = true;
            dialog.Color = secondColor_pictureBox.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                secondColor_pictureBox.BackColor = dialog.Color;
            }
            preview_pictureBox.Invalidate();
        }

        private void UpdatePreview(object sender, EventArgs e)
        {
            preview_pictureBox.Invalidate();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void preview_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(GetValue(), preview_pictureBox.ClientRectangle);
        }

        private void useHatch_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            hatch_groupBox.Enabled = useHatch_checkBox.Checked;
            preview_pictureBox.Invalidate();
        }
    }
}
