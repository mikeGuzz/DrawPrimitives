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
    public partial class PenSetupDialog : Form
    {
        public PenSetupDialog(string text)
        {
            InitializeComponent();
            Setup();
            colorPrev_pictureBox.BackColor = Color.Black;
            dashCap_comboBox.SelectedIndex = 0;
            dashStyle_comboBox.SelectedIndex = 0;
            startCap_comboBox.SelectedIndex = 0;
            endCap_comboBox.SelectedIndex = 0;
            this.Text = text;
        }

        public PenSetupDialog(string text, Pen p)
        {
            InitializeComponent();
            Setup();
            dashCap_comboBox.SelectedItem = p.DashCap.ToString();
            dashStyle_comboBox.SelectedItem = p.DashStyle.ToString();
            startCap_comboBox.SelectedItem = p.StartCap.ToString();
            endCap_comboBox.SelectedItem = p.EndCap.ToString();
            opacity_numericUpDown.Value = p.Color.A;
            colorPrev_pictureBox.BackColor = Color.FromArgb(255, p.Color);
            width_numericUpDown.Value = (decimal)p.Width;
            if(p.DashStyle != DashStyle.Solid)
            {
                dashLength_numericUpDown.Value = Convert.ToDecimal(p.DashPattern[0]);
                spaceLength_numericUpDown.Value = Convert.ToDecimal(p.DashPattern[1]);
            }
            this.Text = text;
        }

        private void Setup()
        {
            foreach (var ob in Enum.GetNames(typeof(DashCap)))
            {
                dashCap_comboBox.Items.Add(ob);
            }
            foreach (var ob in Enum.GetNames(typeof(DashStyle)))
            {
                dashStyle_comboBox.Items.Add(ob);
            }
            foreach (var ob in Enum.GetNames(typeof(LineCap)))
            {
                startCap_comboBox.Items.Add(ob);
                endCap_comboBox.Items.Add(ob);
            }
        }

        public Pen GetValue()
        {
            Pen pen = new Pen(Color.FromArgb((int)opacity_numericUpDown.Value, colorPrev_pictureBox.BackColor), (float)width_numericUpDown.Value);
            if (Enum.TryParse(typeof(DashCap), (string)dashCap_comboBox.SelectedItem, out object? value) && value != null)
                pen.DashCap = (DashCap)value;
            if (Enum.TryParse(typeof(DashStyle), (string)dashStyle_comboBox.SelectedItem, out value) && value != null)
                pen.DashStyle = (DashStyle)value;
            if(pen.DashStyle == DashStyle.Custom)
                pen.DashPattern = new float[] { (float)dashLength_numericUpDown.Value, (float)spaceLength_numericUpDown.Value };
            if (Enum.TryParse(typeof(LineCap), (string)startCap_comboBox.SelectedItem, out value) && value != null)
                pen.StartCap = (LineCap)value;
            if (Enum.TryParse(typeof(LineCap), (string)endCap_comboBox.SelectedItem, out value) && value != null)
                pen.EndCap = (LineCap)value;
            return pen;
        }

        private void pickColor_button_Click(object sender, EventArgs e)
        {
            var dialog = new ColorDialog();
            dialog.FullOpen = true;
            dialog.Color = colorPrev_pictureBox.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                colorPrev_pictureBox.BackColor = dialog.Color;
            }
            preview_pictureBox.Invalidate();
        }

        private void preview_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            var pen = GetValue();
            var g = e.Graphics;
            var bounds = preview_pictureBox.ClientRectangle;

            preview_pictureBox.BackColor = Color.FromArgb(pen.Color.ToArgb() ^ 0xffffff);//color inversion for contrast
            g.DrawLine(pen, new Point((int)pen.Width / 2, bounds.Height / 2), new Point(bounds.Width - ((int)pen.Width / 2), bounds.Height / 2));
        }

        private void dashStyle_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var state = Enum.TryParse(typeof(DashStyle), (string)dashStyle_comboBox.SelectedItem, out var value) && value != null && (DashStyle)value == DashStyle.Custom;
            spaceLength_numericUpDown.Enabled = state;
            dashLength_numericUpDown.Enabled = state;
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
    }
}
