using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace DrawPrimitives
{
    public abstract class Shape : IDisposable, ICloneable
    {
        public Rectangle Bounds;
        public Pen? Pen { get; set; }
        public Brush? Brush { get; set; }
        public object? Tag { get; set; }

        protected List<ShapeAnchor> anchors = new List<ShapeAnchor>();

        public Shape() { }

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
                ob.Draw(g, this, fill);
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

        public void AddRangeAnchors(params ShapeAnchor[] anchors)
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
            var temp = (Shape)this.MemberwiseClone();
            temp.AddRangeAnchors(anchors.Select(i => (ShapeAnchor)i).ToArray());
            return temp;
        }
    }
}
