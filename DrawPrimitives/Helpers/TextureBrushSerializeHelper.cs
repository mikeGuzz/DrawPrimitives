using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DrawPrimitives.Helpers
{
    public sealed class TextureBrushSerializeHelper : BrushSerializeHelper
    {
        [JsonIgnore]
        [XmlIgnore]
        public Bitmap? Image { get; set; }
        public WrapMode WrapMode { get; set; }
        public Matrix? Transform { get; set; }

        [JsonPropertyName(name: "Image")]
        [XmlElement(ElementName = "Image")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public byte[] ImageSerialized
        {
            get//serialize
            {
                if (Image == null) return new byte[0];
                using (MemoryStream ms = new MemoryStream())
                {
                    Image.Save(ms, ImageFormat.Bmp);
                    return ms.ToArray();
                }
            }
            set//deserialize
            {
                if (value == null)
                    Image = null;
                else
                {
                    using (MemoryStream ms = new MemoryStream(value))
                    {
                        Image = new Bitmap(ms);
                    }
                }
            }
        }

        public TextureBrushSerializeHelper() : base() { }

        public TextureBrushSerializeHelper(TextureBrush brush) : base()
        {
            Image = new Bitmap(brush.Image);
            WrapMode = brush.WrapMode;
            Transform = brush.Transform;
        }

        public override Brush ToBrush()
        {
            if (Image == null)
                throw new InvalidOperationException(nameof(Image));
            var b = new TextureBrush(Image, WrapMode);
            if (Transform != null)
                b.Transform = Transform;
            return b;
        }
    }
}
