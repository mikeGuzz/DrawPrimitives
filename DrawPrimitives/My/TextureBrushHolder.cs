using DrawPrimitives.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.My
{
    public class TextureBrushHolder : BrushHolder
    {
        public TextureBrush Brush { get; set; }
        public string Path { get; set; }
        public bool Stretch { get; set; }
        public Size Offset { get; set; }

        public TextureBrushHolder(TextureBrush brush, string path) : base()
        {
            Brush = brush;
            Path = path;
        }

        public override Brush GetBrush(Rectangle bounds)
        {
            using (var t = new Matrix())
            {
                t.Translate(bounds.X + Offset.Width, bounds.Y + Offset.Height);
                if (Stretch)
                {
                    float w = (float)Math.Abs(bounds.Width) / Brush.Image.Width, h = (float)Math.Abs(bounds.Height) / Brush.Image.Height;
                    if (w != 0 && h != 0)
                        t.Scale(w, h);
                }
                Brush.Transform = t;
            }
            return Brush;
        }

        public override Brush GetBrush()
        {
            return Brush;
        }

        public override void Dispose()
        {
            Brush.Dispose();
        }

        public override object Clone()
        {
            var tmp = new TextureBrushHolder((TextureBrush)Brush.Clone(), Path);
            tmp.Stretch = Stretch;
            tmp.Offset = Offset;
            return tmp;
        }

        public override int GetHashCode()
        {
            return Stretch.GetHashCode() ^ Offset.GetHashCode() ^ Brush.WrapMode.GetHashCode() ^ Path.GetHashCode();
        }

        public override string ToString()
        {
            var mode = Stretch ? nameof(Stretch) : Brush.WrapMode.ToString();
            return $"Type: Texture\nWrap mode: {mode}";
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            var b = (TextureBrushHolder)obj;
            return b.Offset == Offset
                && b.Path == Path
                && (b.Stretch == Stretch || b.Brush.WrapMode == Brush.WrapMode);
        }
    }
}
