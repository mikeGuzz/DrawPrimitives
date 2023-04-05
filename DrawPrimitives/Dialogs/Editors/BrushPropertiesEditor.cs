using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawPrimitives.Helpers;
using DrawPrimitives.My;
using DrawPrimitives.Shapes;

namespace DrawPrimitives.Dialogs.Editors
{
    public partial class BrushPropertiesEditor : Form
    {
        private static readonly int NoMatches = -1;
        private enum BrushType { SolidColor, Hatch, Tetxure };

        public BrushHolder? BrushHolder
        {
            get
            {
                switch ((BrushType)type_comboBox.SelectedIndex)
                {
                    case BrushType.SolidColor:
                        return new SolidBrushHolder(new SolidBrush(Color.FromArgb((int)mainColorOpacity_numericUpDown.Value, mainColor_pictureBox.BackColor)));
                    case BrushType.Hatch:
                        if (hatchStyle_comboBox.SelectedIndex != NoMatches)
                        {
                            var style = (HatchStyle)hatchStyle_comboBox.SelectedIndex;
                            return new HatchBrushHolder(new HatchBrush(style,
                                Color.FromArgb((int)hatchColorOpacity_numericUpDown.Value, hatchColor_pictureBox.BackColor),
                                Color.FromArgb((int)mainColorOpacity_numericUpDown.Value, mainColor_pictureBox.BackColor)));
                        }
                        break;
                    case BrushType.Tetxure:
                        if (wrapMode_comboBox.SelectedIndex != NoMatches)
                        {
                            TextureBrush tmp;
                            if (File.Exists(path_textBox.Text))
                                tmp = new TextureBrush(new Bitmap(path_textBox.Text));
                            else
                                return null;
                            var holder = new TextureBrushHolder(tmp, path_textBox.Text);
                            if (Enum.IsDefined(typeof(WrapMode), wrapMode_comboBox.SelectedIndex))
                                tmp.WrapMode = (WrapMode)wrapMode_comboBox.SelectedIndex;
                            else
                                holder.Stretch = true;
                            return holder;
                        }
                        break;
                }
                return null;
            }
            set
            {
                if (value == null)
                    return;
                if (value.GetBrush() is SolidBrush solid)
                {
                    mainColor_pictureBox.BackColor = Color.FromArgb(255, solid.Color);
                    mainColorOpacity_numericUpDown.Value = solid.Color.A;

                    type_comboBox.SelectedIndex = (int)BrushType.SolidColor;
                }
                else if (value.GetBrush() is HatchBrush hatch)
                {
                    mainColor_pictureBox.BackColor = Color.FromArgb(255, hatch.BackgroundColor);
                    mainColorOpacity_numericUpDown.Value = hatch.BackgroundColor.A;
                    hatchColor_pictureBox.BackColor = Color.FromArgb(255, hatch.ForegroundColor);
                    hatchColorOpacity_numericUpDown.Value = hatch.ForegroundColor.A;
                    hatchStyle_comboBox.SelectedIndex = (int)hatch.HatchStyle;

                    type_comboBox.SelectedIndex = (int)BrushType.Hatch;
                }
                else if (value is TextureBrushHolder textureH)
                {
                    path_textBox.Text = textureH.Path;
                    wrapMode_comboBox.SelectedIndex = textureH.Stretch ? Enum.GetValues(typeof(WrapMode)).Length : (int)((TextureBrush)textureH.Brush).WrapMode;

                    type_comboBox.SelectedIndex = (int)BrushType.Tetxure;
                }
                else
                {
                    type_comboBox.SelectedIndex = NoMatches;
                }
            }
        }

        public BrushPropertiesEditor()
        {
            InitializeComponent();
            Setup();
        }


        public BrushPropertiesEditor(BrushHolder brushHolder)
        {
            InitializeComponent();
            Setup();

            BrushHolder = brushHolder;
        }

        private void Setup()
        {
            foreach (var ob in Enum.GetValues(typeof(HatchStyle)).Cast<int>().Distinct())
            {
                var str = Enum.GetName(typeof(HatchStyle), ob);
                if (str != null)
                    hatchStyle_comboBox.Items.Add(str.SplitCamelCase());
            }
            foreach (var ob in Enum.GetNames(typeof(WrapMode)))
            {
                wrapMode_comboBox.Items.Add(ob.SplitCamelCase());
            }
            wrapMode_comboBox.Items.Add("Stretch");
            foreach (var ob in Enum.GetNames(typeof(BrushType)))
            {
                type_comboBox.Items.Add(ob.SplitCamelCase());
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

        private void UpdatePreview(object sender, EventArgs e)
        {
            preview_pictureBox.Invalidate();
            UpdateOKButtonState();
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
            var b = BrushHolder;
            if(b != null)
            {
                e.Graphics.FillRectangle(b.GetBrush(preview_pictureBox.ClientRectangle), preview_pictureBox.ClientRectangle);
            }
            else
                e.Graphics.DrawString("Render error.", Font, Brushes.DarkRed, new Point(15, 15));
        }

        private void button2_Click(object sender, EventArgs e)//load image button
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|BMP files (*.bmp)|*.bmp|HEIC files (*.heic)|*.heic|TIFF files (*.tif)|*.tif|All files (*.*)|*.*";
            dialog.DefaultExt = ".png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path_textBox.Text = dialog.FileName;
                preview_pictureBox.Invalidate();
                UpdateOKButtonState();
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
            UpdatePreview(sender, e);
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

        private void UpdateOKButtonState()
        {
            ok_button.Enabled = BrushHolder != null;
        }
    }
}
