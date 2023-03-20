using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawPrimitives
{
    public partial class BrushDialog : Form
    {
        public BrushDialog(string text)
        {
            InitializeComponent();
            Setup();
            Text = text;
        }

        public BrushDialog(string text, SolidBrush startValue)
        {
            InitializeComponent();
            Setup();
            Text = text;

            mainColor_pictureBox.BackColor = Color.FromArgb(255, startValue.Color);
            mainColorOpacity_numericUpDown.Value = startValue.Color.A;

            type_comboBox.SelectedIndex = 0;
        }

        public BrushDialog(string text, HatchBrush startValue)
        {
            InitializeComponent();
            Setup();
            Text = text;

            mainColor_pictureBox.BackColor = Color.FromArgb(255, startValue.BackgroundColor);
            mainColorOpacity_numericUpDown.Value = startValue.BackgroundColor.A;
            hatchColor_pictureBox.BackColor = Color.FromArgb(255, startValue.ForegroundColor);
            hatchColorOpacity_numericUpDown.Value = startValue.ForegroundColor.A;
            hatchStyle_comboBox.SelectedItem = startValue.HatchStyle.ToString();

            type_comboBox.SelectedIndex = 1;
        }

        public BrushDialog(string text, TextureBrush startValue)
        {
            InitializeComponent();
            Setup();
            Text = text;

            path_textBox.Text = "Image already uploaded.";
            path_textBox.Tag = startValue.Image.Clone();
            wrapMode_comboBox.SelectedItem = startValue.WrapMode.ToString();

            type_comboBox.SelectedIndex = 2;
        }

        private void Setup()
        {
            foreach(var ob in Enum.GetNames(typeof(HatchStyle)))
            {
                hatchStyle_comboBox.Items.Add(ob);
            }
            hatchStyle_comboBox.MaxDropDownItems = 3;
            foreach (var ob in Enum.GetNames(typeof(WrapMode)))
            {
                var item = wrapMode_comboBox.Items.Add(ob);
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
            switch (type_comboBox.SelectedIndex)
            {
                case 0:
                    return new SolidBrush(Color.FromArgb((int)mainColorOpacity_numericUpDown.Value, mainColor_pictureBox.BackColor));
                case 1:
                    if (Enum.TryParse(typeof(HatchStyle), (string)hatchStyle_comboBox.SelectedItem, out var res) && res is HatchStyle hatchStyleRes)
                        return new HatchBrush(hatchStyleRes,
                            Color.FromArgb((int)hatchColorOpacity_numericUpDown.Value, hatchColor_pictureBox.BackColor),
                            Color.FromArgb((int)mainColorOpacity_numericUpDown.Value, mainColor_pictureBox.BackColor));
                    break;
                case 2:
                    if(Enum.TryParse(typeof(WrapMode), (string)wrapMode_comboBox.SelectedItem, out res) && res is WrapMode wrapModeRes)
                    {
                        if (File.Exists(path_textBox.Text))
                            return new TextureBrush(new Bitmap(path_textBox.Text), wrapModeRes);
                        if(path_textBox.Tag is Bitmap image)
                            return new TextureBrush(image, wrapModeRes);
                    }
                    break;
            }
            throw new InvalidOperationException();
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
            if(type_comboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Invalid brush type.", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (hatchStyle_comboBox.SelectedIndex == -1 && type_comboBox.SelectedIndex == 1)
            {
                MessageBox.Show("Invalid hatch style type.", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists(path_textBox.Text) && path_textBox.Tag is not Bitmap && type_comboBox.SelectedIndex == 2)
            {
                MessageBox.Show("Invalid texture image path.", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(wrapMode_comboBox.SelectedIndex == -1 && type_comboBox.SelectedIndex == 2)
            {
                MessageBox.Show("Invalid wrap mode type.", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void preview_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                var brush = GetValue();
                e.Graphics.FillRectangle(brush, preview_pictureBox.ClientRectangle);
            }
            catch
            {
                e.Graphics.DrawString("Render error.", new Font("Consolas", 10), new SolidBrush(Color.DarkRed), new Point(15, 15));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|BMP files (*.bmp)|*.bmp|HEIC files (*.heic)|*.heic|TIFF files (*.tif)|*.tif|All files (*.*)|*.*";
            dialog.DefaultExt = ".png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path_textBox.Text = dialog.FileName;
                preview_pictureBox.Invalidate();
            }
        }

        private void type_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (type_comboBox.SelectedIndex)
            {
                case 0:
                    mainColor_groupBox.Enabled = true;
                    hatch_groupBox.Enabled = false;
                    texture_groupBox.Enabled = false;
                    break;
                case 1:
                    mainColor_groupBox.Enabled = true;
                    hatch_groupBox.Enabled = true;
                    texture_groupBox.Enabled = false;
                    break;
                case 2:
                    mainColor_groupBox.Enabled = false;
                    hatch_groupBox.Enabled = false;
                    texture_groupBox.Enabled = true;
                    break;
                default:
                    mainColor_groupBox.Enabled = hatch_groupBox.Enabled = texture_groupBox.Enabled = false;
                    break;
            }
            preview_pictureBox.Invalidate();
        }

        private void hatchColorPick_button_Click(object sender, EventArgs e)
        {
            var dialog = new ColorDialog();
            dialog.FullOpen = true;
            dialog.Color = hatchColor_pictureBox.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                hatchColor_pictureBox.BackColor = dialog.Color;
            }
            preview_pictureBox.Invalidate();
        }
    }
}
