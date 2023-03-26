using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public interface IDrawable
    {
        public void DrawStroke(Graphics g);
        public void DrawFill(Graphics g);
        public void DrawStroke(Graphics g, Point offset, SizeF scale);
        public void DrawFill(Graphics g, Point offset, SizeF scale);
        public Rectangle GetBounds();
        public Rectangle GetBounds(Point offset, SizeF scale);
        //{
        //    var r = GetBounds();
        //    return new Rectangle((int)(r.X * canv.Zoom) + canv.OffsetX, (int)(r.Y * canv.Zoom) + canv.OffsetY, (int)(r.Width * canv.Zoom), (int)(r.Height * canv.Zoom));
        //}
        public void SetPosition(Point p);
        public void SetPosition(int x, int y);
        public void SetSize(Size s);
        public void SetSize(int w, int h);
        public void Bound(Rectangle r);
        public void Bound(int x, int y, int w, int h);
        public bool IsHit(Point p);
        public bool IsHit(Point p, Point offset, SizeF scale);
    }
}
