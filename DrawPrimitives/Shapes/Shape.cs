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
    public abstract class Shape : IDisposable, ICloneable, IDrawable
    {
        public static Pen? DefaultPen { get; set; }
        public static Brush? DefaultBrush { get; set; }

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

        public Shape()
        {
            Brush = DefaultBrush;
            Pen = DefaultPen;
        }

        public Shape(Pen? pen, Brush? brush)
        {
            Brush = brush;
            Pen = pen;
        }

        public Shape(Shape ob)
        {
            Pen = ob.Pen;
            Brush = ob.Brush;
            UseBrush = ob.UseBrush;
            UsePen = ob.UsePen;
        }

        public abstract void DrawStroke(Graphics g);
        public abstract void DrawFill(Graphics g);
        public abstract void DrawStroke(Graphics g, Point offset, SizeF scale);
        public abstract void DrawFill(Graphics g, Point offset, SizeF scale);
        public abstract Rectangle GetBounds();
        public virtual Rectangle GetBounds(Point offset, SizeF scale)
        {
            var r = GetBounds();
            return new Rectangle((int)((r.X * scale.Width) + offset.X), (int)((r.Y * scale.Height) + offset.Y), (int)(r.Width * scale.Width), (int)(r.Height * scale.Height));
        }
        public abstract void SetPosition(Point p);
        public abstract void SetPosition(int x, int y);
        public abstract void SetSize(Size s);
        public abstract void SetSize(int w, int h);
        public abstract void Bound(Rectangle r);
        public abstract void Bound(int x, int y, int w, int h);


        //{
        //    if (!UseBrush || Brush == null)
        //        return;
        //    if (Brush is TextureBrush textureBrush)
        //    {
        //        var transform = new Matrix();
        //        transform.Translate(Bounds.X, Bounds.Y);
        //        textureBrush.Transform = transform;
        //    }
        //}

        public virtual bool IsHit(Point p)
        {
            var rBounds = GetBounds().WithoutNegative();
            return p.X >= rBounds.X && p.Y >= rBounds.Y && p.X <= rBounds.X + rBounds.Width && p.Y <= rBounds.Y + rBounds.Height;
        }

        public virtual bool IsHit(Point p, Point offset, SizeF scale)
        {
            var rBounds = GetBounds(offset, scale).WithoutNegative();
            return p.X >= rBounds.X && p.Y >= rBounds.Y && p.X <= rBounds.X + rBounds.Width && p.Y <= rBounds.Y + rBounds.Height;
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
            var shape = (Shape)obj;
            return shape.GetBounds() == GetBounds() && UsePen == shape.UsePen && UseBrush == shape.UseBrush;
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
            int hash = UsePen.GetHashCode() ^ UseBrush.GetHashCode();
            if(Pen != null)
                hash ^= Pen.GetHashCode();
            if(Brush != null)
                hash ^= Brush.GetHashCode();
            return hash;
        }
    }
}
