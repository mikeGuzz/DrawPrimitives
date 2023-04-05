using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Security.Policy;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;
using System.Data;
using System.Xml.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Formats.Asn1;
using System.Xml.Linq;
using System.CodeDom;
using System.ComponentModel;
using DrawPrimitives.Helpers;
using DrawPrimitives.My;

namespace DrawPrimitives.Shapes
{
    [XmlInclude(typeof(EllipseShape))]
    [XmlInclude(typeof(RectangleShape))]
    [XmlInclude(typeof(HexagonShape))]
    [XmlInclude(typeof(DiamondShape))]
    [XmlInclude(typeof(PentagonShape))]
    [XmlInclude(typeof(CubeShape))]
    [XmlInclude(typeof(IsoscelesTriangleShape))]
    [XmlInclude(typeof(RightTriangleShape))]
    [XmlInclude(typeof(TrapeziumShape))]
    public abstract class Shape : IDisposable, ICloneable, IDrawable
    {
        public static Size MinimumSize = new Size(9, 9);

        public static Pen DefaultPen { get; set; } = new Pen(Color.Black, 4);
        public static BrushHolder DefaultBrushHolder { get; set; } = new SolidBrushHolder(new SolidBrush(Color.Black));
        public static TextFormat DefaultTextFormat { get; set; } = new TextFormat(new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        }, new Font("Calibri", 18));

        [JsonIgnore]
        [XmlIgnore]
        public Pen Pen { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public BrushHolder BrushHolder { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public TextFormat TextFormat { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public bool IsSelected { get; set; }

        public SmoothingMode SmoothingMode { get; set; } = SmoothingMode.HighSpeed;
        public bool FlipX { get; set; }
        public bool FlipY { get; set; }
        public bool UsePen { get; set; } = true;
        public bool UseBrush { get; set; } = true;
        public bool UseText { get; set; } = true;

        [JsonPropertyName(name: "Pen")]
        [XmlElement(ElementName = "Pen")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public PenSerializeHelper PenSerialized
        {
            get//serialize
            {
                return new PenSerializeHelper(Pen);
            }
            set//deserialize
            {
                Pen = value.ToPen();
            }
        }

        [JsonPropertyName(name: "Brush")]
        [XmlElement(ElementName = "Brush")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public BrushSerializeHelper BrushSerialized
        {
            get//serialize
            {
                if (BrushHolder is SolidBrushHolder solid)
                    return new SolidBrushSerializeHelper(solid);
                else if (BrushHolder is HatchBrushHolder hatch)
                    return new HatchBrushSerializeHelper(hatch);
                else if (BrushHolder is TextureBrushHolder texture)
                    return new TextureBrushSerializeHelper(texture);
                throw new InvalidOperationException();
            }
            set//deserialize
            {
                var holder = value.GetBrushHolder();
                if (holder != null)
                    BrushHolder = holder;
            }
        }

        [JsonPropertyName(name: "TextFormat")]
        [XmlElement(ElementName = "TextFormat")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public TextFormatSerializeHelper TextFormatSerialized
        {
            get//serialize
            {
                return new TextFormatSerializeHelper(TextFormat);
            }
            set//deserialize
            {
                TextFormat = value.ToTextFormat();
            }
        }

        public Shape()
        {
            Pen = (Pen)DefaultPen.Clone();
            BrushHolder = (BrushHolder)DefaultBrushHolder.Clone();
            TextFormat = (TextFormat)DefaultTextFormat.Clone();
        }

        public Shape(Pen pen)
        {
            Pen = pen;
            BrushHolder = (BrushHolder)DefaultBrushHolder.Clone();
            TextFormat = (TextFormat)DefaultTextFormat.Clone();
        }

        public Shape(BrushHolder brushHolder)
        {
            Pen = (Pen)DefaultPen.Clone();
            BrushHolder = brushHolder;
            TextFormat = (TextFormat)DefaultTextFormat.Clone();
        }

        public Shape(Pen pen, BrushHolder brushHolder)
        {
            Pen = pen;
            BrushHolder = brushHolder;
            TextFormat = (TextFormat)DefaultTextFormat.Clone();
        }

        public Shape(Pen pen, BrushHolder brushHolder, TextFormat textFormat)
        {
            Pen = pen;
            BrushHolder = brushHolder;
            TextFormat = textFormat;
        }

        public void Draw(Graphics g) => DrawByRect(g, GetBounds());
        public void Draw(Graphics g, SizeF scale) => DrawByRect(g, GetBounds(scale)); 
        public void Draw(Graphics g, float scale) => Draw(g, new SizeF(scale, scale));

        protected void SetupGraphics(Graphics g, Rectangle bounds)
        {
            g.SmoothingMode = SmoothingMode;
            g.ScaleTransform(FlipX ? -1 : 1, FlipY ? -1 : 1);
            g.TranslateTransform(FlipX ? (-bounds.Right - bounds.X) : 0, FlipY ? (-bounds.Bottom - bounds.Y) : 0);
        }

        private void DrawByRect(Graphics g, Rectangle rect)
        {
            var tmpSmoothingMode = g.SmoothingMode;
            var tmpTransform = g.Transform.Clone();
            SetupGraphics(g, rect);

            Draw(g, rect);

            g.Transform = tmpTransform;
            g.SmoothingMode = tmpSmoothingMode;
        }

        protected abstract void Draw(Graphics g, Rectangle rect);

        protected virtual void DrawString(Graphics g, Rectangle rect)
        {
            if (!UseText)
                return;
            g.DrawString(TextFormat.Text, TextFormat.Font, new SolidBrush(TextFormat.Color), rect, TextFormat.Format);
        }

        public abstract Rectangle GetBounds();
        public virtual Rectangle GetBounds(SizeF scale)
        {
            var r = GetBounds();
            return new Rectangle((int)(r.X * scale.Width), (int)(r.Y * scale.Height), (int)(r.Width * scale.Width), (int)(r.Height * scale.Height));
        }
        public virtual Rectangle GetBounds(float scale)
        {
            return GetBounds(new SizeF(scale, scale));
        }

        public abstract void SetPosition(Point p);
        public void SetPosition(int x, int y)
        {
            SetPosition(new Point(x, y));
        }

        public abstract void SetSize(Size s);
        public void SetSize(int w, int h)
        {
            SetSize(new Size(w, h));
        }

        public abstract void Bound(Rectangle r);
        public void Bound(int x, int y, int w, int h)
        {
            Bound(new Rectangle(x, y, w, h));
        }

        public virtual bool IsHit(Point p)
        {
            var rBounds = GetBounds().WithoutNegative();
            return p.X >= rBounds.X && p.Y >= rBounds.Y && p.X <= rBounds.X + rBounds.Width && p.Y <= rBounds.Y + rBounds.Height;
        }

        public virtual bool IsHit(Point p, SizeF scale)
        {
            var rBounds = GetBounds(scale).WithoutNegative();
            return p.X >= rBounds.X && p.Y >= rBounds.Y && p.X <= rBounds.X + rBounds.Width && p.Y <= rBounds.Y + rBounds.Height;
        }

        public virtual bool IsHit(Point p, float scale)
        {
            return IsHit(p, new SizeF(scale, scale));
        }

        public void ResetBrushHolder()
        {
            BrushHolder = (BrushHolder)DefaultBrushHolder.Clone();
        }

        public void ResetPen()
        {
            Pen = (Pen)DefaultPen.Clone();
        }

        public void ResetTextFormat()
        {
            TextFormat = (TextFormat)DefaultTextFormat.Clone();
        }

        public virtual void Dispose()
        {
            Pen.Dispose();
            BrushHolder.Dispose();
            TextFormat.Dispose();
        }

        public abstract object Clone();

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (ReferenceEquals(null, obj))
                return false;
            if (obj.GetType() != GetType()) 
                return false;
            var shape = (Shape)obj;
            return shape.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            int hash = GetBounds().GetHashCode();
            hash ^= Pen.GetPenHashCode();
            hash ^= BrushHolder.GetHashCode();
            hash ^= TextFormat.GetHashCode();
            hash ^= UsePen.GetHashCode();
            hash ^= UseBrush.GetHashCode();
            hash ^= UseText.GetHashCode();
            hash ^= FlipX.GetHashCode();
            hash ^= FlipY.GetHashCode();
            return hash;
        }

        public static bool operator ==(Shape a, Shape? b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Shape a, Shape? b)
        {
            return !a.Equals(b);
        }
    }
}
