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

namespace DrawPrimitives.Shapes
{
    [Serializable]
    [XmlInclude(typeof(EllipseShape))]
    [XmlInclude(typeof(RectangleShape))]
    public abstract class Shape : IDisposable, ICloneable
    {
        public static Pen? DefaultPen { get; set; }
        public static Brush? DefaultBrush { get; set; }

        [JsonInclude]
        public Rectangle Bounds;
        [JsonIgnore]
        [XmlIgnore]
        public Pen? Pen { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public Brush? Brush { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public bool IsSelected { get; set; }
        public bool FlipX { get; set; }
        public bool FlipY { get; set; }
        public bool UsePen { get; set; } = true;
        public bool UseBrush { get; set; } = true;

        [JsonPropertyName(name: "Pen")]
        [XmlElement(ElementName = "Pen")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public PenSerializeHelper? PenSerialized
        {
            get//serialize
            {
                return new PenSerializeHelper(Pen);
            }
            set//deserialize
            {
                Pen = value?.ToPen();
            }
        }

        [JsonPropertyName(name: "Brush")]
        [XmlElement(ElementName = "Brush")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public BrushSerializeHelper? BrushSerialized
        {
            get//serialize
            {
                if (Brush == null)
                    return null;
                if (Brush is SolidBrush solid)
                    return new SolidBrushSerializeHelper(solid);
                else if (Brush is HatchBrush hatch)
                    return new HatchBrushSerializeHelper(hatch);
                return null;
            }
            set//deserialize
            {
                Brush = value?.ToBrush();
            }
        }

        public Shape() { }

        public Shape(Shape ob)
        {
            Bounds = ob.Bounds;
            Pen = ob.Pen;
            Brush = ob.Brush;
        }

        public virtual void DrawStroke(Graphics g)
        {
            if (!UsePen || Pen != null)
                return;
        }
        public virtual void DrawFill(Graphics g)
        {
            if (!UseBrush || Brush == null)
                return;
            if (Brush is TextureBrush textureBrush)
            {
                var transform = new Matrix();
                transform.Translate(Bounds.X, Bounds.Y);
                textureBrush.Transform = transform;
            }
        }

        public virtual void DrawBounds(Graphics g, Pen pen, Brush? brush)
        {
            var bounds = GetWithoutNegative();
            if (brush != null)
                g.FillRectangle(brush, bounds);
            g.DrawRectangle(pen, bounds);
        }

        public void DrawDiagonals(Graphics g, Pen pen)
        {
            g.DrawLine(pen, Bounds.Location, new Point(Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height));
            g.DrawLine(pen, new Point(Bounds.X + Bounds.Width, Bounds.Y), new Point(Bounds.X, Bounds.Y + Bounds.Height));
        }

        public virtual bool IsHit(Point p)
        {
            var rBounds = Bounds.WithoutNegative();
            return p.X >= rBounds.X && p.Y >= rBounds.Y && p.X <= rBounds.X + rBounds.Width && p.Y <= rBounds.Y + rBounds.Height;
        }

        public Rectangle GetWithoutNegative()
        {
            return Bounds.WithoutNegative();
        }

        public void MakeWithoutNegative()
        {
            Bounds = Bounds.WithoutNegative();
        }

        public void Dispose()
        {
            Pen?.Dispose();
            Brush?.Dispose();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Shape)) return false;
            var ob = (Shape)obj;
            return ob.Bounds == Bounds;
        }

        public static bool operator ==(Shape a, Shape b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Shape a, Shape b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return Bounds.GetHashCode();
        }
    }
}
