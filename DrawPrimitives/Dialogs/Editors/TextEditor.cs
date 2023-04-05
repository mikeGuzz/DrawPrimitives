using DrawPrimitives.My;
using DrawPrimitives.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawPrimitives.Dialogs.Editors
{
    public partial class TextEditor : Form
    {
        private Font font { get; set; }
        private string text
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }
        private Color color { get; set; }

        public Font _Font
        {
            get => font;
            set
            {
                font = value;
                UpdateFontLabel();
            }
        }
        public string _Text
        {
            get => text;
            set
            {
                text = value;
                textBox.Text = value;
                UpdateFontLabel();
            }
        }
        public Color Color
        {
            get => color;
            set
            {
                color = value;
                UpdateFontLabel();
            }
        }
        public StringAlignment TextAlignment
        {
            get => (StringAlignment)textAlig_comboBox.SelectedIndex;
            set
            {
                textAlig_comboBox.SelectedIndex = (int)value;
            }
        }
        public StringAlignment LineAlignment
        {
            get => (StringAlignment)lineAlig_comboBox.SelectedIndex;
            set
            {
                lineAlig_comboBox.SelectedIndex = (int)value;
            }
        }

        public TextFormat TextFormat
        {
            get
            {
                var tmp = new TextFormat(font);
                tmp.Format = new StringFormat()
                {
                    Alignment = TextAlignment,
                    LineAlignment = LineAlignment,
                };
                tmp.Text = text;
                tmp.Color = color;
                return tmp;
            }
            set
            {
                font = value.Font;
                text = value.Text;
                color = value.Color;

                TextAlignment = value.Format.Alignment;
                LineAlignment = value.Format.LineAlignment;
                textAlig_comboBox.SelectedIndex = (int)TextAlignment;
                lineAlig_comboBox.SelectedIndex = (int)LineAlignment;

                UpdateFontLabel();
            }
        }

        public TextEditor()
        {
            InitializeComponent();
            Setup();

            font = SystemFonts.DefaultFont;
            text = string.Empty;
            color = Color.Black;

            textAlig_comboBox.SelectedIndex = 0;
            lineAlig_comboBox.SelectedIndex = 0;

            UpdateFontLabel();
        }

        public TextEditor(TextFormat format)
        {
            InitializeComponent();
            Setup();

            font = format.Font;
            text = format.Text;
            color = format.Color;

            TextAlignment = format.Format.Alignment;
            LineAlignment = format.Format.LineAlignment;
            textAlig_comboBox.SelectedIndex = (int)TextAlignment;
            lineAlig_comboBox.SelectedIndex = (int)LineAlignment;

            UpdateFontLabel();
        }

        private void Setup()
        {
            MinimumSize = new Size(418, 283);

            foreach(var ob in Enum.GetNames(typeof(StringAlignment)))
            {
                textAlig_comboBox.Items.Add(ob.SplitCamelCase());
                lineAlig_comboBox.Items.Add(ob.SplitCamelCase());
            }
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void UpdateFontLabel()
        {
            fontProperties_label.Text = $"Font family: {font.FontFamily.Name}\nSize: {font.Size}\nStyle: {font.Style}\nColor: {color.Name}";
        }

        private void pickColor_button_Click(object sender, EventArgs e)
        {
            var dialog = new ColorDialog();
            dialog.FullOpen = true;
            dialog.Color = color;
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                color = dialog.Color;
                UpdateFontLabel();
            }
        }

        private void loadFont_button_Click(object sender, EventArgs e)
        {
            var dialog = new FontDialog();
            dialog.Font = font;
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                font = dialog.Font;
                UpdateFontLabel();
            }
        }
    }
}
