using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DrawPrimitives.My
{
    public class Canvas
    {
        public static Size DefaultSize { get; set; } = new Size(720, 576);

        public Size Size { get; set; }
        public int ColorArgb { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public Color Color
        {
            get => Color.FromArgb(ColorArgb);
            set => ColorArgb = value.ToArgb();
        }

        [XmlIgnore]
        [JsonIgnore]
        public int Width
        {
            get => Size.Width;
            set => Size = new Size(value, Size.Height);
        }

        [XmlIgnore]
        [JsonIgnore]
        public int Height
        {
            get => Size.Height;
            set => Size = new Size(Size.Width, value);
        }

        public Canvas()
        {
            Size = DefaultSize;
            Color = Color.White;
        }

        public Canvas(int w, int h)
        {
            Size = new Size(w, h);
            Color = Color.White;
        }

        public Canvas(Size size)
        {
            Size = size;
            Color = Color.White;
        }

        public Canvas(Size size, Color color)
        {
            Size = size;
            Color = color;
        }

        public void Reset()
        {
            Color = Color.White;
            Size = DefaultSize;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            var canv = (Canvas)obj;
            return ColorArgb == canv.ColorArgb && Size == canv.Size;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Size.GetHashCode() ^ ColorArgb.GetHashCode();
        }

        public static bool operator ==(Canvas? a, Canvas? b)
        {
            if (ReferenceEquals(a, null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(Canvas? a, Canvas? b)
        {
            return !(a == b);
        }
    }
}
