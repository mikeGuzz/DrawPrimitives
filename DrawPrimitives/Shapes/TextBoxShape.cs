using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawPrimitives.Shapes
{
    public abstract class TextBoxShape : Shape
    {
        //public static Font DefaultFont { get; set; } = SystemFonts.DefaultFont;

        //public string Text { get; set; }
        //public Font Font { get; set; } = DefaultFont;

        //public TextBoxShape() : base()
        //{
        //    Text = string.Empty;
        //    Brush = DefaultBrush;
        //}

        //public TextBoxShape(Brush? brush) : base()
        //{
        //    Brush = brush;
        //    Text = string.Empty;
        //}

        //public TextBoxShape(Rectangle bounds) : base()
        //{
        //    Bounds = bounds;
        //    Brush = DefaultBrush;
        //    Text = string.Empty;
        //}

        //public TextBoxShape(Rectangle bounds, Brush brush) : base()
        //{
        //    Bounds = bounds;
        //    Brush = brush;
        //    Text = string.Empty;
        //}

        //public TextBoxShape(Rectangle bounds, Brush brush, string text) : base()
        //{
        //    Bounds = bounds;
        //    Brush = brush;
        //    Text = text;
        //}

        //public TextBoxShape(Rectangle bounds, string text, Font font) : base()
        //{
        //    Bounds = bounds;
        //    Brush = DefaultBrush;
        //    Text = text;
        //    Font = font;
        //}

        //public TextBoxShape(Rectangle bounds, Brush brush, string text, Font font) : base()
        //{
        //    Bounds = bounds;
        //    Brush = brush;
        //    Text = text;
        //    Font = font;
        //}

        //public override void DrawBounds(Graphics g, Pen pen, Brush? brush)
        //{
        //    var path = new GraphicsPath();
        //    path.AddRectangle(Bounds);
        //    //path.GetBounds().ToString();
        //    if (path.PathData.Points != null && path.PathData.Points.Any())
        //        path.PathData.Points[0] = new Point();
        //    g.DrawPath(pen, path);

        //}

        //public override void DrawFill(Graphics g)
        //{
        //    base.DrawFill(g);
        //    if (Brush == null)
        //        return;
        //    using (var flipXMatrix = new Matrix(-1, 0, 0, 1, Bounds.Width, 0))
        //    using (var flipYMatrix = new Matrix(1, 0, 0, -1, 0, Bounds.Height))
        //    using (var transformMatrix = new Matrix())
        //    {
        //        if (FlipX)
        //        {
        //            transformMatrix.Multiply(flipXMatrix);
        //        }
        //        if (FlipY)
        //        {
        //            transformMatrix.Multiply(flipYMatrix);
        //        }
        //        //path.Transform(transformMatrix);
        //        //Or e.Graphics.Transform = TransformMatrix;
        //    }
        //    g.DrawString(Text, Font, Brush, Bounds);
        //}

        //public override void DrawStroke(Graphics g) { }
    }
}
