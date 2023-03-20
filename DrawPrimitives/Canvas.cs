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
        public Size Size { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public Color BackColor { get; set; }

        [JsonPropertyName(name: "Color")]
        [XmlElement(ElementName = "Color")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int ColorSerialized
        {
            get => BackColor.ToArgb();
            set => BackColor = Color.FromArgb(value);
        }

        public Canvas() { }
        
        public Canvas(Size size)
        {
            Size = size;
        }

        public Canvas(Size size, Color color)
        {
            Size = size;
            BackColor = color;
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
