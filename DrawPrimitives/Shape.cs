using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Security.Policy;

namespace DrawPrimitives
{
    public enum ShapeType { Line, Rectangle, Ellipse, TextBox };

    public abstract class Shape : IDisposable, ICloneable
    {
        public Rectangle Bounds;
        public Pen? Pen { get; set; }
        public Brush? Brush { get; set; }
        public object? Tag { get; set; }

        protected List<ShapeAnchor> anchors = new List<ShapeAnchor>();

        public Shape() { }
        public Shape(Shape ob)
        {
            Bounds = ob.Bounds;
            Pen = ob.Pen;
            Brush = ob.Brush;
            Tag = ob.Tag;
            anchors.AddRange(ob.anchors);
        }

        public abstract void DrawStroke(Graphics g);
        public abstract void DrawFill(Graphics g);

        public void DrawBounds(Graphics g, Pen pen, Brush? brush)
        {
            var rBounds = GetNormalizedBounds();
            g.DrawRectangle(pen, rBounds);
            if (brush != null)
                g.FillRectangle(brush, rBounds);
        }

        public void DrawAnchors(Graphics g, bool fill = false)
        {
            foreach (var ob in anchors)
            {
                Rectangle b = GetAnchorBounds(ob.Position);
                if (ob.Position == AnchorPosition.OverShape)
                {
                    g.DrawLine(ob.Pen, new Point(b.X + b.Width / 2, b.Y), new Point(b.X + b.Width / 2 - ob.Offset.Width, b.Y + b.Height - ob.Offset.Height));
                }
                switch (ob.Shape)
                {
                    case AnchorShape.Rectangle:
                        if (fill && ob.Brush != null)
                            g.FillRectangle(ob.Brush, b);
                        g.DrawRectangle(ob.Pen, b);
                        break;
                    case AnchorShape.Round:
                        if (fill && ob.Brush != null)
                            g.FillEllipse(ob.Brush, b);
                        g.DrawEllipse(ob.Pen, b);
                        break;
                    case AnchorShape.Triangle:
                        var points = new Point[]
                        {
                        new Point(b.X + b.Width / 2, b.Y),
                        new Point(b.X + b.Width, b.Y + b.Height),
                        new Point(b.X, b.Y + b.Height),
                        };
                        if (fill && ob.Brush != null)
                            g.FillPolygon(ob.Brush, points);
                        g.DrawPolygon(ob.Pen, points);
                        break;
                }
            }
        }

        public void DrawDiagonals(Graphics g, Pen pen)
        {
            g.DrawLine(pen, Bounds.Location, new Point(Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height));
            g.DrawLine(pen, new Point(Bounds.X + Bounds.Width, Bounds.Y), new Point(Bounds.X, Bounds.Y + Bounds.Height));
        }

        public ShapeAnchor[] GetAnchors()
        {
            return anchors.ToArray();
        }

        public Rectangle GetAnchorBounds(AnchorPosition index)
        {
            var anchor = this[index];
            Point pos = Bounds.Location;
            switch (anchor.Position)
            {
                case AnchorPosition.RightTop:
                    pos = new Point(Bounds.X + Bounds.Width, Bounds.Y);
                    break;
                case AnchorPosition.RightBottom:
                    pos = new Point(Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height);
                    break;
                case AnchorPosition.LeftBottom:
                    pos = new Point(Bounds.X, Bounds.Y + Bounds.Height);
                    break;
                case AnchorPosition.Right:
                    pos = new Point(Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height / 2);
                    break;
                case AnchorPosition.Left:
                    pos = new Point(Bounds.X, Bounds.Y + Bounds.Height / 2);
                    break;
                case AnchorPosition.Bottom:
                    pos = new Point(Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height);
                    break;
                case AnchorPosition.Top:
                    pos = new Point(Bounds.X + Bounds.Width / 2, Bounds.Y);
                    break;
                case AnchorPosition.OverShape:
                    pos = new Point(Bounds.X + Bounds.Width / 2, Bounds.Y - anchor.Size.Height);
                    break;
            }
            return new Rectangle(pos.X - anchor.Size.Width / 2 + anchor.Offset.Width, pos.Y - anchor.Size.Height / 2 + anchor.Offset.Height, anchor.Size.Width, anchor.Size.Height);
        }

        public void AddAnchors(params ShapeAnchor[] anchors)
        {
            this.anchors.Clear();
            this.anchors.AddRange(anchors);
        }

        public ShapeAnchor this[AnchorPosition index]
        {
            get
            {
                for (int i = 0; i < anchors.Count; i++)
                {
                    if (anchors[i].Position == index)
                        return anchors[i];
                }
                throw new IndexOutOfRangeException();
            }
        }

        public Rectangle GetRealBounds()
        {
            if (Pen == null)
                return Bounds;
            else if (Pen.Alignment == PenAlignment.Inset)
                return Bounds;
            else
            {
                int penWidth = (int)Pen.Width;
                return new Rectangle(Bounds.X - penWidth / 2, Bounds.Y - penWidth / 2, Bounds.Width + penWidth, Bounds.Height + penWidth);
            }
        }

        public Rectangle GetNormalizedBounds()
        {
            return Bounds.Normalized();
        }

        public void NormalizeBounds()
        {
            Bounds = Bounds.Normalized();
        }

        public void Dispose()
        {
            if(Pen != null)
            {
                Pen.Dispose();
            }
            if(Brush != null)
            {
                Brush.Dispose();
            }
        }

        public object Clone()
        {
            var ob = (Shape)this.MemberwiseClone();
            ob.AddAnchors(this.anchors.Select(i => (ShapeAnchor)i.Clone()).ToArray());
            return ob;
        }
    }
}
