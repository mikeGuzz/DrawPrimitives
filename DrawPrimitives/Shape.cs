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

namespace DrawPrimitives
{
    public sealed class PenSerializeHelper
    {
        public float Width { get; set; }
        public int ArgbColor { get; set; }
        public PenAlignment Alignment { get; set; }
        public DashCap DashCap { get; set; }
        public float DashOffset { get; set; }
        public float[]? DashPattern { get; set; }
        public DashStyle DashStyle { get; set; }
        public LineCap StartCap { get; set; }
        public LineCap EndCap { get; set; }
        public LineJoin LineJoin { get; set; }
        public float MiterLimit { get; set; }
        public Matrix? Transform { get; set; }
        public PenType PenType { get; set; }

        public PenSerializeHelper() { }

        public PenSerializeHelper(Pen? p)
        {
            if (p == null)
                return;
            ArgbColor = p.Color.ToArgb();
            Width = p.Width;
            Alignment = p.Alignment;
            DashCap = p.DashCap;
            DashOffset = p.DashOffset;
            if (p.DashStyle == DashStyle.Custom)
                DashPattern = p.DashPattern;
            DashStyle = p.DashStyle;
            StartCap = p.StartCap;
            EndCap = p.EndCap;
            LineJoin = p.LineJoin;
            MiterLimit = p.MiterLimit;
            Transform = p.Transform;
        }

        public Pen ToPen()
        {
            var pen = new Pen(Color.FromArgb(ArgbColor), Width);
            pen.DashCap = DashCap; 
            pen.DashOffset = DashOffset;
            if(DashPattern != null)
                pen.DashPattern = DashPattern;
            pen.StartCap = StartCap;
            pen.EndCap = EndCap;
            pen.Alignment = Alignment;
            pen.LineJoin = LineJoin;
            pen.DashStyle = DashStyle;
            pen.MiterLimit = MiterLimit;
            if (Transform != null)
                pen.Transform = Transform;
            return pen;
        }
    }

    [Serializable]
    [XmlInclude(typeof(EllipseShape))]
    [XmlInclude(typeof(RectangleShape))]
    public abstract class Shape : IDisposable, ICloneable
    {
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

        [JsonPropertyName(name: "Pen")]
        [XmlElement(ElementName = "Pen")]
        public PenSerializeHelper PenSerializeHelper
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

        public Shape() { }
        public Shape(Shape ob)
        {
            Bounds = ob.Bounds;
            Pen = ob.Pen;
            Brush = ob.Brush;
        }

        public abstract void DrawStroke(Graphics g);
        public abstract void DrawFill(Graphics g);

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
            return p.X >= rBounds.X && p.Y >= rBounds.Y && p.X <= (rBounds.X + rBounds.Width) && p.Y <= (rBounds.Y + rBounds.Height);
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
            if(!(obj is Shape)) return false;
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
