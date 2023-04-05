using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrawPrimitives.My
{
    public static class MyExtensions
    {
        public static Rectangle ToRectangle(this Point p1, Point p2)
        {
            return new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }

        public static Rectangle WithoutNegative(this Rectangle r)
        {
            if (r.Width >= 0 && r.Height >= 0)
                return r;
            else
            {
                var newBounds = r;
                if (r.Width < 0)
                {
                    newBounds.X += r.Width;
                    newBounds.Width -= r.Width * 2;
                }
                if (r.Height < 0)
                {
                    newBounds.Y += r.Height;
                    newBounds.Height -= r.Height * 2;
                }
                return newBounds;
            }
        }

        public static bool OutOfBounds(this Rectangle r, Rectangle bounds)
        {
            if(r.X > bounds.Right || r.Y > bounds.Bottom) 
                return true;
            if((r.X + r.Width) < bounds.Left || (r.Y + r.Height) < bounds.Top) 
                return true;
            return false;
        }

        public static bool OutOfBounds(this Rectangle r, Size size)
        {
            if (r.X > size.Width || r.Y > size.Height)
                return true;
            if ((r.X + r.Width) < 0 || (r.Y + r.Height) < 0)
                return true;
            return false;
        }

        public static bool EqualsPen(this Pen a, Pen b)
        {
            return a.Color == b.Color
                && a.Alignment == b.Alignment
                && a.Width == b.Width
                && a.DashCap == b.DashCap
                && a.DashStyle == b.DashStyle;
        }

        public static int GetPenHashCode(this Pen a)
        {
            var hash = a.Color.GetHashCode();
            hash ^= a.Alignment.GetHashCode();
            hash ^= a.Width.GetHashCode();
            hash ^= a.DashCap.GetHashCode();
            hash ^= a.DashStyle.GetHashCode();
            return hash;
        }

        public static bool IsHit(this Rectangle rect, Point p)
        {
            var rBounds = rect.WithoutNegative();
            return p.X >= rBounds.X && p.Y >= rBounds.Y && p.X <= rBounds.X + rBounds.Width && p.Y <= rBounds.Y + rBounds.Height;
        }

        public static bool IsIntersect(this Rectangle rect1, Rectangle rect2)
        {
            return rect1.WithoutNegative().IntersectsWith(rect2.WithoutNegative());
        }

        public static string SplitCamelCase(this string s)
        {
            return Regex.Replace(s, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }

        public static bool TryParsePoint(this string str, out Point res)
        {
            res = Point.Empty;
            var arr = str.Split(',');
            if (str.Length < 2)
                return false;
            res = new Point(int.Parse(arr[0]), int.Parse(arr[1]));
            return true;
        }

        public static bool TryParseSize(this string str, out Size res)
        {
            res = Size.Empty;
            var arr = str.Split(',');
            if (str.Length < 2)
                return false;
            res = new Size(int.Parse(arr[0]), int.Parse(arr[1]));
            if (res.Width < 0 || res.Height < 0)
                return false;
            return true;
        }
    }
}
