using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives
{
    public enum AnchorPosition { None, Left, Top, Right, Bottom, LeftTop, LeftBottom, RightTop, RightBottom, OverShape }
    public enum AnchorShape { Rectangle, Round, Triangle }

    public class ShapeAnchor : ICloneable
    {
        public Size Size;
        public AnchorPosition Position { get; set; }
        public AnchorShape Shape { get; set; }
        public Pen Pen { get; set; }
        public Brush? Brush { get; set; }
        public Size Offset = Size.Empty;

        public ShapeAnchor(Size size, AnchorPosition position, Pen pen)
        {
            Size = size;
            Position = position;
            Pen = pen;
            if (position == AnchorPosition.OverShape)
                Offset = new Size(0, -8);
        }

        public ShapeAnchor(Size size, AnchorPosition position, Pen pen, Brush? brush)
        {
            Size = size;
            Position = position;
            Pen = pen;
            Brush = brush;
            if (position == AnchorPosition.OverShape)
                Offset = new Size(0, -8);
        }

        public ShapeAnchor(Size size, AnchorPosition position, Pen pen, Brush? brush, AnchorShape shape)
        {
            Size = size;
            Position = position;
            Shape = shape;
            Pen = pen;
            Brush = brush;
            if (position == AnchorPosition.OverShape)
                Offset = new Size(0, -8);
        }

        //public void DrawImage(Graphics g, Shape shape)
        //{
        //    if (Image == null)
        //        return;
        //    var bounds = shape.GetRealBounds();
        //    Point pos = bounds.Location;
        //    switch (Position)
        //    {
        //        case AnchorPosition.RightTop:
        //            pos = new Point(bounds.X + bounds.Width, bounds.Y);
        //            break;
        //        case AnchorPosition.Top:
        //            pos = new Point(bounds.X + bounds.Width / 2, bounds.Y);
        //            break;
        //    }
        //    switch (Shape)
        //    {
        //        case AnchorShape.Rectangle:
        //            g.DrawImage(Image, pos);
        //            break;
        //        case AnchorShape.Round:
        //            Image temp = new Bitmap(Image.Width, Image.Height, Image.PixelFormat);
        //            using (Graphics g2 = Graphics.FromImage(temp))
        //            {
        //                GraphicsPath path = new GraphicsPath();
        //                path.AddEllipse(new Rectangle(Point.Empty, Size));
        //                g2.SetClip(path);
        //                g2.DrawImage(Image, 0, 0);
        //            }
        //            g.DrawImage(temp, pos);
        //            break;
        //    }
        //}

        public Rectangle GetBounds(Shape shape)
        {
            var bounds = shape.Bounds;
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

        public void Draw(Graphics g, Shape shape, bool fill = false)
        {
            var bounds = GetBounds(shape);
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
            return this.MemberwiseClone();
        }
    }
}
