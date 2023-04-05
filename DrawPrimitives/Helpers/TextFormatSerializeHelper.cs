using DrawPrimitives.My;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Helpers
{
    public sealed class TextFormatSerializeHelper
    {
        public string Text { get; set; }
        public string FontFamily { get; set; }
        public float FontSize { get; set; }
        public StringAlignment TextAlignemnt { get; set; }
        public StringAlignment LineAlignemnt { get; set; }
        public int ColorArgb { get; set; }
        public FontStyle FontStyle { get; set; }

        public TextFormatSerializeHelper()
        {
            Text = string.Empty; 
            FontFamily = string.Empty;
        }

        public TextFormatSerializeHelper(TextFormat format)
        {
            Text = format.Text;
            FontFamily = format.Font.FontFamily.Name;
            FontSize = format.Font.Size;
            TextAlignemnt = format.Format.Alignment;
            LineAlignemnt = format.Format.LineAlignment;
            ColorArgb = format.Color.ToArgb();
            FontStyle = format.Font.Style;
        }

        public TextFormat ToTextFormat()
        {
            var ob = new TextFormat(new StringFormat()
            {
                Alignment = TextAlignemnt,
                LineAlignment = LineAlignemnt,
            }, new Font(FontFamily, FontSize, FontStyle), Text);
            ob.Color = Color.FromArgb(ColorArgb);
            return ob;
        }
    }
}
