using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DrawPrimitives
{
    public class Canvas
    {
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public double Zoom { get; set; } = 1d;
        public Size Size { get; set; }

        public Point Offset => new Point(OffsetX, OffsetY);
        public SizeF SZoom => new SizeF((float)Zoom, (float)Zoom);

        [XmlIgnore]
        [JsonIgnore]
        public Color BackColor { get; set; } = Color.White;
        [JsonPropertyName(name: "Color")]
        [XmlElement(ElementName = "Color")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int ColorSerialized
        {
            get => BackColor.ToArgb();
            set => BackColor = Color.FromArgb(value);
        }
        
        public Canvas(Size size)
        {
            Size = size;
        }

        public Canvas(Size size, Color color)
        {
            Size = size;
            BackColor = color;
        }

        public Rectangle GetFileBounds(Rectangle clientBounds, bool includeZoom, bool includeOffset)
        {
            var zoom = includeZoom ? Zoom : 1d;
            var w = (int)(Size.Width * zoom);
            var h = (int)(Size.Height * zoom);
            var r = new Rectangle(clientBounds.X + (includeOffset ? OffsetX : 0),
                clientBounds.Y + (includeOffset ? OffsetY : 0), w, h);
            return r;
        }

        public override bool Equals(object? obj)
        {
            if(obj is not Canvas)
                return false;
            var canv = (Canvas)obj;
            return BackColor == canv.BackColor && Size == canv.Size;
        }

        public override int GetHashCode()
        {
            return Size.GetHashCode() ^ BackColor.GetHashCode();
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
