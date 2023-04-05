using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Shapes
{
    public interface IDrawable
    {
        public void Draw(Graphics g);
        public void Draw(Graphics g, SizeF scale);

        public Rectangle GetBounds();
        public Rectangle GetBounds(SizeF scale);

        public bool IsHit(Point p);
        public bool IsHit(Point p, SizeF scale);

        public void SetPosition(Point p);
        public void SetSize(Size s);
        public void Bound(Rectangle r);
    }
}
