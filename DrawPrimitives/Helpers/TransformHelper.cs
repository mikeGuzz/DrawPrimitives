using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using DrawPrimitives.Shapes;

namespace DrawPrimitives.Helpers
{
    public class TransformHelper : IList<Shape>, IDrawable
    {
        private List<Rectangle> startBounds = new List<Rectangle>();
        private List<Shape> shapes = new List<Shape>();

        public Rectangle StartTransformBounds { get; private set; }
        public Pen? Pen { get; set; }
        public Brush? Brush { get; set; }

        public int Count => shapes.Count;

        public bool IsReadOnly => false;

        public Shape this[int index]
        {
            get => shapes[index];
            set => shapes[index] = value;
        }

        public TransformHelper(Pen pen)
        {
            Pen = pen;
        }

        public TransformHelper(Brush brush)
        {
            Brush = brush;
        }

        public TransformHelper(Pen? pen, Brush? brush)
        {
            Pen = pen;
            Brush = brush;
        }

        public TransformHelper(params Shape[] shapes)
        {
            this.shapes.AddRange(shapes);
        }

        public void StartTransform()
        {
            startBounds.Clear();
            startBounds.AddRange(shapes.Select(i => i.GetBounds()));
            StartTransformBounds = GetBounds();
        }

        public void StartTransform(Point offset, SizeF scale)
        {
            startBounds.Clear();
            startBounds.AddRange(shapes.Select(i => i.GetBounds(offset, scale)));
            StartTransformBounds = GetBounds(offset, scale);
        }

        public void DrawStroke(Graphics g)
        {
            if (!shapes.Any() || Pen == null)
                return;
            if (shapes.Count == 1)
            {
                g.DrawRectangle(Pen, shapes.Single().GetBounds().WithoutNegative());
                return;
            }
            g.DrawRectangle(Pen, GetBounds().WithoutNegative());
        }

        public void DrawStroke(Graphics g, Point offset, SizeF scale)
        {
            if (!shapes.Any() || Pen == null)
                return;
            if (shapes.Count == 1)
            {
                g.DrawRectangle(Pen, shapes.Single().GetBounds(offset, scale).WithoutNegative());
                return;
            }
            g.DrawRectangle(Pen, GetBounds(offset, scale).WithoutNegative());
        }

        public void DrawFill(Graphics g)
        {
            if (!shapes.Any() || Brush == null)
                return;
            if (shapes.Count == 1)
            {
                g.FillRectangle(Brush, shapes.Single().GetBounds().WithoutNegative());
                return;
            }
            g.FillRectangle(Brush, GetBounds().WithoutNegative());
        }

        public void DrawFill(Graphics g, Point offset, SizeF scale)
        {
            if (!shapes.Any() || Brush == null)
                return;
            if (shapes.Count == 1)
            {
                g.FillRectangle(Brush, shapes.Single().GetBounds(offset, scale).WithoutNegative());
                return;
            }
            g.FillRectangle(Brush, GetBounds(offset, scale).WithoutNegative());
        }

        public Rectangle GetBounds()
        {
            if (shapes.Count == 1)
                return shapes.Single().GetBounds();
            else if (!shapes.Any())
                return Rectangle.Empty;
            else
                return Rectangle.FromLTRB(shapes.Min(i => i.GetBounds().Left), shapes.Min(i => i.GetBounds().Top), shapes.Max(i => i.GetBounds().Right), shapes.Max(i => i.GetBounds().Bottom));
        }

        public Rectangle GetBounds(Point offset, SizeF scale)
        {
            if (shapes.Count == 1)
                return shapes.Single().GetBounds(offset, scale);
            else if (!shapes.Any())
                return Rectangle.Empty;
            else
                return Rectangle.FromLTRB(shapes.Min(i => i.GetBounds(offset, scale).Left), shapes.Min(i => i.GetBounds(offset, scale).Top), shapes.Max(i => i.GetBounds(offset, scale).Right), shapes.Max(i => i.GetBounds(offset, scale).Bottom));
        }

        public void SetPosition(Point p)
        {
            if (startBounds.Count != shapes.Count)
                StartTransform();
            for (int i = 0; i < startBounds.Count; i++)
            {
                shapes[i].SetPosition(new Point(startBounds[i].X + p.X, startBounds[i].Y + p.Y));
            }
        }

        public void SetPosition(int x, int y)
        {
            SetPosition(new Point(x, y));
        }

        public void SetSize(Size s)
        {
            if (startBounds.Count != shapes.Count)
                StartTransform();
            for (int i = 0; i < startBounds.Count; i++)
            {
                shapes[i].SetSize(startBounds[i].Size + s);
            }
        }

        public void SetSize(int w, int h)
        {
            SetSize(new Size(w, h));
        }

        public void Bound(Rectangle r)
        {
            if (startBounds.Count != shapes.Count)
                StartTransform();
            var pW = r.Width / (double)StartTransformBounds.Width;
            var pH = r.Height / (double)StartTransformBounds.Height;
            for (int i = 0; i < startBounds.Count; i++)
            {
                var dX = (double)startBounds[i].X - StartTransformBounds.X;
                var dY = (double)startBounds[i].Y - StartTransformBounds.Y;
                shapes[i].SetPosition(new Point((int)(r.X + dX * pW), (int)(r.Y + dY * pH)));
                shapes[i].SetSize(new Size((int)(startBounds[i].Width * pW), (int)(startBounds[i].Height * pH)));
            }
        }

        public void Bound(int x, int y, int w, int h)
        {
            Bound(new Rectangle(x, y, w, h));
        }

        public bool IsHit(Point p)
        {
            if (!shapes.Any())
                return false;
            if (shapes.Count == 1)
                return shapes.Single().IsHit(p);
            else
            {
                var rBounds = GetBounds().WithoutNegative();
                return p.X >= rBounds.X && p.Y >= rBounds.Y &&
                        p.X <= rBounds.X + rBounds.Width && p.Y <= rBounds.Y + rBounds.Height;
            }
        }

        public bool IsHit(Point p, Point offset, SizeF scale)
        {
            if (!shapes.Any())
                return false;
            if (shapes.Count == 1)
                return shapes.Single().IsHit(p, offset, scale);
            else
            {
                var rBounds = GetBounds(offset, scale).WithoutNegative();
                return p.X >= rBounds.X && p.Y >= rBounds.Y &&
                        p.X <= rBounds.X + rBounds.Width && p.Y <= rBounds.Y + rBounds.Height;
            }
        }

        public int IndexOf(Shape item)
        {
            return shapes.IndexOf(item);
        }

        public void Insert(int index, Shape item)
        {
            shapes.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            shapes.RemoveAt(index);
        }

        public void CopyTo(Shape[] array, int arrayIndex)
        {
            shapes.CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Shape> GetEnumerator()
        {
            return new TransformHelperEnumerator(shapes.ToArray());
        }

        public void Add(Shape item)
        {
            shapes.Add(item);
        }

        public void AddRange(IEnumerable<Shape> collection)
        {
            shapes.AddRange(collection);
        }

        public void Clear()
        {
            shapes.Clear();
        }

        public bool Contains(Shape item)
        {
            return shapes.Contains(item);
        }

        public bool Remove(Shape item)
        {
            return shapes.Remove(item);
        }
    }

    public class TransformHelperEnumerator : IEnumerator<Shape>
    {
        private Shape[] arr;
        private int index = -1;

        public TransformHelperEnumerator(Shape[] collection)
        {
            arr = collection;
        }

        object IEnumerator.Current => Current;

        public Shape Current
        {
            get
            {
                try
                {
                    return arr[index];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public bool MoveNext()
        {
            return ++index < arr.Length;
        }

        public void Reset()
        {
            index = -1;
        }

        public void Dispose() { }
    }
}
