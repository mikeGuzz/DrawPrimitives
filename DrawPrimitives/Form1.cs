using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DrawPrimitives
{
    public enum ShapeType { Line = 0, Rectangle = 1, Ellipse = 2 };
    public enum ToolType { Pointer = 0, DrawShape = 1, FillShape = 2 };

    public partial class Form1 : Form
    {
        private enum TransformState { None, Move, Rotate, Scale };

        //pens
        private Pen mainPen;
        private readonly Pen selectedPen;
        private readonly Pen anchorPen;
        private readonly Pen selectedBoundsPen;

        //brushes
        private Brush mainBrush;
        private readonly SolidBrush anchorBrush;
        private readonly SolidBrush selectedBoundsBrush;

        //select variables
        private Point startSelectionPoint;
        private Rectangle selectedBounds;
        private List<Shape> selectedItems = new List<Shape>();

        //transform
        private List<Rectangle> transformBuffer = new List<Rectangle>();
        private TransformState transformState = TransformState.None;
        private AnchorPosition selectedAnchorPos = AnchorPosition.None;
        private readonly Size minimumSize = new Size(9, 9);

        //control
        private bool mouseDown;

        //shapes
        private List<Shape> shapes = new List<Shape>();
        private readonly ShapeAnchor[] anchors;

        //menu strip
        private ShapeType selectedShapeType = ShapeType.Ellipse;
        private ToolType selectedToolType = ToolType.DrawShape;

        //buffer
        private List<Shape> buffer = new List<Shape>();

        public Form1()
        {
            InitializeComponent();

            //check on save
            mainPen = new Pen(Color.Black, 2);
            mainBrush = new SolidBrush(Color.White);

            selectedPen = new Pen(Color.Black, 1.5f);
            selectedPen.Alignment = PenAlignment.Outset;
            selectedPen.DashStyle = DashStyle.Dash;
            selectedPen.DashPattern = new float[] { 3.5f, 2f };
            selectedBoundsPen = new Pen(Color.FromArgb(51, 120, 232), 1.5f);
            selectedBoundsBrush = new SolidBrush(Color.FromArgb(75, 51, 120, 232));

            anchorPen = new Pen(Color.DimGray, 2f);
            anchorBrush = new SolidBrush(Color.White);

            var size = new Size(8, 8);
            anchors = new ShapeAnchor[]
            {
                    new ShapeAnchor(size, AnchorPosition.Left, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.Top, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.Right, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.Bottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.LeftTop, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.LeftBottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.RightTop, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(size, AnchorPosition.RightBottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //fill Shape menu
            int index = 0;
            foreach (var ob in Enum.GetNames(typeof(ShapeType)))
            {
                var item = shapeToolStripMenuItem.DropDownItems.Add(ob.SplitCamelCase());
                item.Click += ShapeMenuItemClick;
                item.Tag = (ShapeType)index++;
            }
            //fill Tools menu
            index = 0;
            foreach (var ob in Enum.GetNames(typeof(ToolType)))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(ob.SplitCamelCase());
                toolsToolStripMenuItem.DropDownItems.Insert(index, item);
                item.Click += ToolsMenuItemClick;
                item.Tag = (ToolType)index++;
            }
        }

        //private void TempSave()
        //{
        //    var dialog = new SaveFileDialog();
        //    if (dialog.ShowDialog() == DialogResult.OK)
        //    {
        //        using (Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height))
        //        {
        //            pictureBox1.DrawToBitmap(map, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
        //            map.Save(dialog.FileName, ImageFormat.Png);
        //        }
        //    }
        //}

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            foreach (var ob in shapes)
            {
                ob.DrawStroke(g);
                ob.DrawFill(g);
                if (ob.Tag != null && (bool)ob.Tag)
                    ob.DrawBounds(g, selectedPen, null);
            }

            if(mouseDown && selectedToolType == ToolType.DrawShape && transformState == TransformState.None)
            {
                var ob = selectedItems.First();
                ob.DrawStroke(g);
                ob.DrawFill(g);
                ob.DrawBounds(g, selectedPen, null);
            }
            else if (selectedItems.Count != 0)
            {
                foreach (var ob in selectedItems)
                {
                    ob.DrawBounds(g, selectedPen, null);
                    ob.DrawAnchors(g, true);
                }
            }

            if(mouseDown && (Math.Abs(selectedBounds.Width) > 1 || Math.Abs(selectedBounds.Height) > 1) && selectedToolType == ToolType.Pointer && transformState == TransformState.None)
            {
                g.FillRectangle(selectedBoundsBrush, selectedBounds.Normalized());
                g.DrawRectangle(selectedBoundsPen, selectedBounds.Normalized());
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            startSelectionPoint = e.Location;
            selectedBounds.Location = e.Location;
            selectedBounds.Size = Size.Empty;

            foreach (var shape in selectedItems)
            {
                foreach (var anchor in shape.GetAnchors())
                {
                    //check anchors
                    var anchorBounds = anchor.GetBounds(shape);
                    if (e.Location.X >= anchorBounds.X && e.Location.Y >= anchorBounds.Y &&
                        e.Location.X <= (anchorBounds.X + anchorBounds.Width) && e.Location.Y <= (anchorBounds.Y + anchorBounds.Height))
                    {
                        if (anchor.Position == AnchorPosition.OverShape)
                        {
                            transformState = TransformState.Rotate;
                        }
                        else
                        {
                            transformState = TransformState.Scale;
                            selectedAnchorPos = anchor.Position;
                        }
                        transformBuffer.Clear();
                        transformBuffer.AddRange(selectedItems.Select(i => i.Bounds));
                        return;
                    }

                }
                //check bounds
                var rBounds = shape.GetNormalizedBounds();
                if (e.Location.X >= rBounds.X && e.Location.Y >= rBounds.Y &&
                        e.Location.X <= (rBounds.X + rBounds.Width) && e.Location.Y <= (rBounds.Y + rBounds.Height))
                {
                    transformState = TransformState.Move;
                    transformBuffer.Clear();
                    transformBuffer.AddRange(selectedItems.Select(i => i.Bounds));
                    return;
                }
            }

            //tools logic
            selectedPen.DashOffset = 0;
            selectedItems.Clear();

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    Shape tempShape;
                    switch (selectedShapeType)
                    {
                        case ShapeType.Line:
                            tempShape = new LineShape(new Rectangle(e.Location, Size.Empty), mainPen, null);//temp brush
                            selectedItems.Add(tempShape);
                            break;
                        case ShapeType.Rectangle:
                            tempShape = new RectangleShape(new Rectangle(e.Location, Size.Empty), mainPen, null);//temp brush
                            selectedItems.Add(tempShape);
                            break;
                        case ShapeType.Ellipse:
                            tempShape = new EllipseShape(new Rectangle(e.Location, Size.Empty), mainPen, null);//temp brush
                            selectedItems.Add(tempShape);
                            break;
                        default:
                            return;
                    }
                    tempShape.AddRangeAnchors(anchors);
                    break;
                case ToolType.Pointer:
                    if (selectedItems.Count != 0)
                        selectedItems.Clear();
                    for (int i = shapes.Count - 1; i >= 0; i--)
                    {
                        var p = e.Location;
                        if (p.X > shapes[i].Bounds.X && p.Y > shapes[i].Bounds.Y && p.X < (shapes[i].Bounds.X + shapes[i].Bounds.Width) && p.Y < (shapes[i].Bounds.Y + shapes[i].Bounds.Height))
                        {
                            transformState = TransformState.Move;
                            selectedItems.Add(shapes[i]);
                            transformBuffer.Clear();
                            transformBuffer.AddRange(selectedItems.Select(i => i.Bounds));
                            break;
                        }
                    }
                    pictureBox1.Invalidate();
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
                return;

            selectedBounds.Size = new Size(e.X - startSelectionPoint.X, e.Y - startSelectionPoint.Y);

            if (transformState != TransformState.None)//transform logic
            {
                switch (transformState)
                {
                    case TransformState.Scale:
                        Rectangle increase = Rectangle.Empty;
                        switch (selectedAnchorPos)
                        {
                            case AnchorPosition.RightBottom:
                                increase.Size = new Size(selectedBounds.Width, selectedBounds.Height);
                                break;
                            case AnchorPosition.LeftBottom:
                                increase.Size = new Size(-selectedBounds.Width, selectedBounds.Height);
                                increase.Location = new Point(selectedBounds.Width, 0);
                                break;
                            case AnchorPosition.LeftTop:
                                increase.Size = new Size(-selectedBounds.Width, -selectedBounds.Height);
                                increase.Location = new Point(selectedBounds.Width, selectedBounds.Height);
                                break;
                            case AnchorPosition.Left:
                                increase.Size = new Size(-selectedBounds.Width, 0);
                                increase.Location = new Point(selectedBounds.Width, 0);
                                break;
                            case AnchorPosition.RightTop:
                                increase.Size = new Size(selectedBounds.Width, -selectedBounds.Height);
                                increase.Location = new Point(0, selectedBounds.Height);
                                break;
                            case AnchorPosition.Right:
                                increase.Size = new Size(selectedBounds.Width, 0);
                                break;
                            case AnchorPosition.Top:
                                increase.Size = new Size(0, -selectedBounds.Height);
                                increase.Location = new Point(0, selectedBounds.Height);
                                break;
                            case AnchorPosition.Bottom:
                                increase.Size = new Size(0, selectedBounds.Height);
                                break;
                        }
                        for (int i = 0; i < transformBuffer.Count; i++)
                        {
                            var tempBounds = new Rectangle(new Point(increase.Location.X + transformBuffer[i].Location.X, increase.Location.Y + transformBuffer[i].Location.Y), increase.Size + transformBuffer[i].Size);

                            if (minimumSize.Width > tempBounds.Width)
                                tempBounds.Width = minimumSize.Width;
                            if (minimumSize.Height > tempBounds.Height)
                                tempBounds.Height = minimumSize.Height;
                            var minX = (transformBuffer[i].X + transformBuffer[i].Width - minimumSize.Width);
                            var minY = (transformBuffer[i].Y + transformBuffer[i].Height - minimumSize.Height);
                            if (minX < tempBounds.X)
                                tempBounds.X = minX;
                            if (minY < tempBounds.Y)
                                tempBounds.Y = minY;

                            selectedItems[i].Bounds = tempBounds;
                        }
                        break;
                    case TransformState.Move:
                        for (int i = 0; i < selectedItems.Count; i++)
                        {
                            selectedItems[i].Bounds.Location = new Point(transformBuffer[i].X + selectedBounds.Width, transformBuffer[i].Y + selectedBounds.Height);
                        }
                        break;
                }
                pictureBox1.Invalidate();
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    selectedItems.Single().Bounds.Size = selectedBounds.Size;
                    break;
                case ToolType.Pointer:
                    var normalized = selectedBounds.Normalized();
                    if (normalized.Width > 1 || normalized.Height > 1)
                    {
                        if(selectedItems.Count != 0)
                            selectedItems.Clear();
                        foreach (var ob in shapes)
                        {
                            var r = ob.Bounds;
                            
                            if (r.X > normalized.X && r.Y > normalized.Y && (r.X + r.Width) < (normalized.X + normalized.Width) && (r.Y + r.Height) < (normalized.Y + normalized.Height))
                                ob.Tag = true;
                            else
                                ob.Tag = false;
                        }
                    }
                    break;
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            if (transformState != TransformState.None)
            {
                transformState = TransformState.None;
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    if (Math.Abs(selectedBounds.Width) > minimumSize.Width && Math.Abs(selectedBounds.Height) > minimumSize.Height)
                    {
                        var item = selectedItems.First();
                        item.NormalizeBounds();
                        shapes.Add(item);
                    }
                    else
                    {
                        selectedItems.Clear();
                    }
                    break;
                case ToolType.Pointer:
                    var normalized = selectedBounds.Normalized();
                    if (normalized.Width > 1 || normalized.Height > 1)
                    {
                        foreach (var ob in shapes)
                        {
                            if (ob.Tag != null && (bool)ob.Tag)
                                selectedItems.Add(ob);
                            ob.Tag = false;
                        }
                    }
                    break;
            }
            pictureBox1.Invalidate();
        }

        private void ShapeMenuItemClick(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                if (item.Tag is ShapeType type)
                {
                    selectedShapeType = type;
                }
            }
        }

        private void ToolsMenuItemClick(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                if (item.Tag is ToolType type)
                {
                    selectedToolType = type;
                }
            }
        }

        private void penSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new PenSetupDialog("Pen setting", mainPen);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mainPen = dialog.GetValue();
                if(selectedItems.Count > 0)
                {
                    foreach (var ob in selectedItems)
                    {
                        ob.Pen = mainPen;
                    }
                    pictureBox1.Invalidate();
                }
            }
        }

        private void brushSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new BrushSetupDialog("Brush setting", mainBrush);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mainBrush = dialog.GetValue();
                if (selectedItems.Count > 0)
                {
                    foreach (var ob in selectedItems)
                    {
                        ob.Brush = mainBrush;
                    }
                    pictureBox1.Invalidate();
                }
            }
        }

        private void DeleteSelectedItems()
        {
            for (int i = selectedItems.Count - 1; i >= 0; i--)
            {
                if (shapes.Contains(selectedItems[i]))
                {
                    shapes.Remove(selectedItems[i]);
                }
            }
            selectedItems.Clear();
        }

        private void Cut()
        {
            if (selectedItems.Count == 0)
                return;
            buffer.Clear();
            buffer.AddRange(selectedItems);
            DeleteSelectedItems();
            pictureBox1.Invalidate();
        }

        private void Paste()
        {
            if (buffer.Count == 0)
                return;
            var coll = buffer.Select(i => (Shape)i.Clone());
            shapes.AddRange(coll);
            selectedItems.AddRange(coll);
            pictureBox1.Invalidate();
        }

        private void Copy()
        {
            if (selectedItems.Count == 0)
                return;
            buffer.Clear();
            buffer.AddRange(selectedItems.Select(i => (Shape)i.Clone()));
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Copy();
        }
    }
}