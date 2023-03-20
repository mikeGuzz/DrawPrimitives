using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public enum AnchorPosition { None, Left, Top, Right, Bottom, LeftTop, LeftBottom, RightTop, RightBottom, OverShape }
    public enum AnchorShape { Rectangle, Round, Triangle }

    public struct ShapeAnchor : ICloneable
    {
        public Size Size;
        public AnchorPosition Position { get; set; }
        public AnchorShape Shape { get; set; }
        public Pen Pen { get; set; }
        public Brush? Brush { get; set; } = null;
        public Size Offset = Size.Empty;

        public ShapeAnchor(Size size, AnchorPosition position, Pen pen)
        {
            Shape = AnchorShape.Rectangle;
            Brush = null;
            Size = size;
            Position = position;
            Pen = pen;
        }

        public ShapeAnchor(Size size, AnchorPosition position, Pen pen, Brush? brush)
        {
            Shape = AnchorShape.Rectangle;
            Size = size;
            Position = position;
            Pen = pen;
            Brush = brush;
        }

        public ShapeAnchor(Size size, AnchorPosition position, Pen pen, Brush? brush, AnchorShape shape)
        {
            Size = size;
            Position = position;
            Shape = shape;
            Pen = pen;
            Brush = brush;
        }

        public Rectangle GetBounds(Rectangle bounds)
        {
            Point pos = bounds.Location;
            switch (Position)
            {
                case AnchorPosition.RightTop:
                    pos = new Point(bounds.X + bounds.Width, bounds.Y);
                    break;
                case AnchorPosition.RightBottom:
                    pos = new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height);
                    break;
                case AnchorPosition.LeftBottom:
                    pos = new Point(bounds.X, bounds.Y + bounds.Height);
                    break;
                case AnchorPosition.Right:
                    pos = new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height / 2);
                    break;
                case AnchorPosition.Left:
                    pos = new Point(bounds.X, bounds.Y + bounds.Height / 2);
                    break;
                case AnchorPosition.Bottom:
                    pos = new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height);
                    break;
                case AnchorPosition.Top:
                    pos = new Point(bounds.X + bounds.Width / 2, bounds.Y);
                    break;
                case AnchorPosition.OverShape:
                    pos = new Point(bounds.X + bounds.Width / 2, bounds.Y - Size.Height);
                    break;
            }
            return new Rectangle(pos.X - Size.Width / 2 + Offset.Width, pos.Y - Size.Height / 2 + Offset.Height, Size.Width, Size.Height);
        }

        public void Draw(Graphics g, Rectangle bounds, bool fill = true)
        {
            bounds = GetBounds(bounds);
            if (Position == AnchorPosition.OverShape)
            {
                g.DrawLine(Pen, new Point(bounds.X + bounds.Width / 2, bounds.Y), new Point(bounds.X + bounds.Width / 2 - Offset.Width, bounds.Y + bounds.Height - Offset.Height));
            }
            switch (Shape)
            {
                case AnchorShape.Rectangle:
                    if (fill && Brush != null)
                        g.FillRectangle(Brush, bounds);
                    g.DrawRectangle(Pen, bounds);
                    break;
                case AnchorShape.Round:
                    if (fill && Brush != null)
                        g.FillEllipse(Brush, bounds);
                    g.DrawEllipse(Pen, bounds);
                    break;
                case AnchorShape.Triangle:
                    var points = new Point[]
                    {
                        new Point(bounds.X + bounds.Width / 2, bounds.Y),
                        new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height),
                        new Point(bounds.X, bounds.Y + bounds.Height),
                    };
                    if (fill && Brush != null)
                        g.FillPolygon(Brush, points);
                    g.DrawPolygon(Pen, points);
                    break;
            }
        }

        public object Clone()
        {
            var ob = (ShapeAnchor)MemberwiseClone();
            ob.Pen = (Pen)Pen.Clone();
            if (Brush != null)
                ob.Brush = (Brush)Brush.Clone();
            return ob;
        }
    }
}
