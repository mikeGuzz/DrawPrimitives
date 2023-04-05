using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.My
{
    public sealed class TextFormat : ICloneable, IDisposable
    {
        public string Text { get; set; }
        public StringFormat Format { get; set; }
        public Font Font { get; set; }
        public Color Color { get; set; } = Color.Black;

        public TextFormat()
        {
            Text = string.Empty;
            Format = new StringFormat();
            Font = SystemFonts.DefaultFont;
        }

        public TextFormat(string text)
        {
            Text = text;
            Format = new StringFormat();
            Font = SystemFonts.DefaultFont;
        }

        public TextFormat(Font font)
        {
            Text = string.Empty;
            Format = new StringFormat();
            Font = font;
        }

        public TextFormat(StringFormat format)
        {
            Text = string.Empty;
            Format = format;
            Font = SystemFonts.DefaultFont;
        }

        public TextFormat(StringFormat format, Font font)
        {
            Text = string.Empty;
            Format = format;
            Font = font;
        }

        public TextFormat(StringFormat format, Font font, string text)
        {
            Text = text;
            Format = format;
            Font = font;
        }

        public object Clone()
        {
            var tmp = new TextFormat((StringFormat)Format.Clone(), (Font)Font.Clone(), Text);
            tmp.Color = Color;
            return tmp;
        }

        public void Dispose()
        {
            Format.Dispose();
            Font.Dispose();
        }

        public override string ToString()
        {
            return $"Font family: {Font.FontFamily.Name}\nSize: {Font.Size}\nStyle: {Font.Style}\nColor: {Color.Name}\nText align: {Format.Alignment}\nLine align: {Format.LineAlignment}";
        }

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(null, obj))
                return false;
            if(ReferenceEquals(this, obj))
                return true;
            if(obj.GetType() != GetType())
                return false;
            var b = (TextFormat)obj;
            return b.Font.Equals(Font)
                && b.Format.Equals(Format)
                && b.Text.Equals(Text)
                && b.Color.Equals(Color);
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode() ^ Font.GetHashCode() ^ Color.GetHashCode();
        }
    }
}
