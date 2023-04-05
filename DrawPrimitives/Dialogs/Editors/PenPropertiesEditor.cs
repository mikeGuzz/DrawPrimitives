using DrawPrimitives.My;
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

namespace DrawPrimitives.Dialogs.Editors
{
    public partial class PenPropertiesEditor : Form
    {
        public Pen Pen
        {
            get
            {
                Pen pen = new Pen(Color.FromArgb((int)opacity_numericUpDown.Value, colorPrev_pictureBox.BackColor), (float)width_numericUpDown.Value);
                pen.DashCap = (DashCap)dashCap_comboBox.SelectedIndex;
                pen.DashCap = (DashCap)dashCap_comboBox.SelectedIndex;
                pen.DashStyle = (DashStyle)dashStyle_comboBox.SelectedIndex;
                pen.Alignment = (PenAlignment)aligment_comboBox.SelectedIndex;
                return pen;
            }
            set
            {
                dashCap_comboBox.SelectedIndex = (int)value.DashCap;
                dashStyle_comboBox.SelectedIndex = (int)value.DashStyle;
                aligment_comboBox.SelectedIndex = (int)value.Alignment;
                opacity_numericUpDown.Value = value.Color.A;
                colorPrev_pictureBox.BackColor = Color.FromArgb(255, value.Color);
                width_numericUpDown.Value = (decimal)value.Width;
            }
        }

        public PenPropertiesEditor()
        {
            InitializeComponent();
            Setup();
            Init();
        }

        public PenPropertiesEditor(Pen? p)
        {
            InitializeComponent();
            Setup();
            if (p != null)
                Pen = p;
            else
                Init();
        }

        private void Init()
        {
            colorPrev_pictureBox.BackColor = Color.Black;
            dashCap_comboBox.SelectedIndex = 0;
            dashStyle_comboBox.SelectedIndex = 0;
        }

        private void Setup()
        {
            foreach (var ob in Enum.GetNames(typeof(DashCap)))
            {
                dashCap_comboBox.Items.Add(ob.SplitCamelCase());
            }
            var coll = Enum.GetNames(typeof(DashStyle));
            for (int i = 0; i < coll.Length - 1; i++)//last one is Custom
            {
                dashStyle_comboBox.Items.Add(coll[i].SplitCamelCase());
            }
            foreach(var ob in Enum.GetNames(typeof(PenAlignment)))
            {
                aligment_comboBox.Items.Add(ob.SplitCamelCase());
            }
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
            DrawPreview(e.Graphics, Pen, preview_pictureBox.ClientRectangle);
        }

        public static void DrawPreview(Graphics g, Pen pen, Rectangle bounds)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(pen.Color.ToArgb() ^ 0xffffff)), bounds);
            g.DrawLine(pen, new Point(bounds.Left, bounds.Height / 2), new Point(bounds.Right, bounds.Height / 2));
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
