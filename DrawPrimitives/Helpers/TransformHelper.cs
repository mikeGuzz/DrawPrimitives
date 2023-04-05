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
using DrawPrimitives.My;
using DrawPrimitives.Shapes;

namespace DrawPrimitives.Helpers
{
    public class TransformHelper : IList<Shape>, IDrawable
    {
        private List<Rectangle> startBounds = new List<Rectangle>();
        private List<Shape> shapes = new List<Shape>();

        public Rectangle StartTransformBounds { get; private set; }
        public Pen? Pen { get; set; }
        public BrushHolder? Brush { get; set; }

        public bool UsePen { get; set; } = true;
        public bool UseBrush { get; set; } = true;

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

        public TransformHelper(BrushHolder brush)
        {
            Brush = brush;
        }

        public TransformHelper(Pen? pen, BrushHolder? brush)
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

        public void StartTransform(SizeF scale)
        {
            startBounds.Clear();
            startBounds.AddRange(shapes.Select(i => i.GetBounds(scale)));
            StartTransformBounds = GetBounds(scale);
        }

        public void StartTransform(float scale)
        {
            StartTransform(new SizeF(scale, scale));
        }

        private void Draw(Graphics g, Rectangle rect)
        {
            if (!shapes.Any())
                return;
            rect = rect.WithoutNegative();
            if (UseBrush && Brush != null)
            {
                g.FillRectangle(Brush.GetBrush(rect), rect);
            }
            if (UsePen && Pen != null)
            {
                g.DrawRectangle(Pen, rect);
            }
        }

        public void Draw(Graphics g)
        {
            if (shapes.Count == 1)
                Draw(g, shapes.Single().GetBounds());
            else
                Draw(g, GetBounds());
        }

        public void Draw(Graphics g, SizeF scale)
        {
            if (shapes.Count == 1)
                Draw(g, shapes.Single().GetBounds(scale));
            else
                Draw(g, GetBounds(scale));
        }

        public void Draw(Graphics g, float scale)
        {
            Draw(g, new SizeF(scale, scale));
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

        public Rectangle GetBounds(SizeF scale)
        {
            if (shapes.Count == 1)
                return shapes.Single().GetBounds(scale);
            else if (!shapes.Any())
                return Rectangle.Empty;
            else
                return Rectangle.FromLTRB(shapes.Min(i => i.GetBounds(scale).Left), shapes.Min(i => i.GetBounds(scale).Top), shapes.Max(i => i.GetBounds(scale).Right), shapes.Max(i => i.GetBounds(scale).Bottom));
        }

        public Rectangle GetBounds(float scale)
        {
            return GetBounds(new SizeF(scale, scale));
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
                shapes[i].SetPosition((int)(r.X + dX * pW), (int)(r.Y + dY * pH));
                shapes[i].SetSize((int)(startBounds[i].Width * pW), (int)(startBounds[i].Height * pH));
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

        public bool IsHit(Point p, SizeF scale)
        {
            if (!shapes.Any())
                return false;
            if (shapes.Count == 1)
                return shapes.Single().IsHit(p, scale);
            else
            {
                var rBounds = GetBounds(scale).WithoutNegative();
                return p.X >= rBounds.X && p.Y >= rBounds.Y &&
                        p.X <= rBounds.X + rBounds.Width && p.Y <= rBounds.Y + rBounds.Height;
            }
        }

        public bool IsHit(Point p, float scale)
        {
            return IsHit(p, new SizeF(scale, scale));
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
