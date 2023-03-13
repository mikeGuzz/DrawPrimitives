using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;

namespace DrawPrimitives
{
    public enum ToolType { Pointer = 0, DrawShape = 1 };

    public partial class Form1 : Form
    {
        private enum TransformState { None, Move, Rotate, Scale };

        //file
        private string fileName = "Untitled";
        private string fileFullName = string.Empty;

        //pens
        private Pen mainPen;
        private readonly Pen selectedPen;
        private readonly Pen anchorPen;
        private readonly Pen selectedBoundsPen;

        //brushes
        private Brush mainBrush;
        private readonly SolidBrush anchorBrush;
        private readonly SolidBrush selectedBoundsBrush;

        //select
        private Rectangle selectedBounds; 
        private readonly ShapeAnchor[] anchors;

        //transform
        private TransformState transformState = TransformState.None;
        private AnchorPosition selectedAnchorPos = AnchorPosition.None;
        private readonly Size minimumSize = new Size(9, 9);

        //control
        private bool mouseDown;

        //shapes
        private List<Shape> shapes = new List<Shape>();

        //menu strip
        private ShapeType selectedShapeType = ShapeType.Ellipse;
        private ToolType selectedToolType = ToolType.Pointer;

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

            this.Text = GetTitle();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //fill Shape menu
            int index = 0;
            foreach (var ob in Enum.GetNames(typeof(ShapeType)))
            {
                var item = shapeToolStripMenuItem.DropDownItems.Add(ob.SplitCamelCase());
                item.Click += ShapeMenuItemClick;
                var type = (ShapeType)index++;
                item.Tag = type;
            }
            //fill Tools menu
            index = 0;
            foreach (var ob in Enum.GetNames(typeof(ToolType)))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(ob.SplitCamelCase());
                toolsToolStripMenuItem.DropDownItems.Insert(index, item);
                item.Click += ToolsMenuItemClick;
                var type = (ToolType)index++;
                item.Tag = type;

                switch (type)
                {
                    case ToolType.Pointer:
                        item.ShortcutKeys = Keys.Control | Keys.P;
                        break;
                    case ToolType.DrawShape:
                        item.ShortcutKeys = Keys.Control | Keys.U;
                        break;
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            foreach (var ob in shapes)
            {
                ob.DrawFill(g);
                ob.DrawStroke(g);
                ob.DrawBounds(g, selectedPen, null);
            }

            //if(mouseDown && selectedToolType == ToolType.DrawShape && transformState == TransformState.None)
            //{
            //    var ob = selectedItems.Single();
            //    ob.DrawFill(g);
            //    ob.DrawStroke(g);
            //    ob.DrawBounds(g, selectedPen, null);
            //}
            //else
            //{
            //    foreach (var ob in selectedItems)
            //    {
            //        ob.DrawBounds(g, selectedPen, null);
            //        ob.DrawAnchors(g, true);
            //    }
            //}

            if(mouseDown && (Math.Abs(selectedBounds.Width) > 1 || Math.Abs(selectedBounds.Height) > 1) && selectedToolType == ToolType.Pointer && transformState == TransformState.None)
            {
                g.FillRectangle(selectedBoundsBrush, selectedBounds.WithoutNegative());
                g.DrawRectangle(selectedBoundsPen, selectedBounds.WithoutNegative());
            }
        }

        private void ResetSelection()
        {
            shapes.ForEach(i => i.SelectionState = ShapeSelectionState.None);
        }

        private IEnumerable<Shape> GetSelectedItems()
        {
            return shapes.Where(i => i.SelectionState.HasFlag(ShapeSelectionState.BoundsAndAnchors));
        }

        private void SetSelectedItemsDefaultBounds()
        {
            shapes.ForEach(i =>
            {
                if (i.SelectionState.HasFlag(ShapeSelectionState.BoundsAndAnchors))
                    i.DefaultBounds = i.Bounds;
            });
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button != MouseButtons.Left)
                return;
            mouseDown = true;
            selectedBounds.Location = e.Location;
            selectedBounds.Size = Size.Empty;
            var controlKeyState = ModifierKeys.HasFlag(Keys.Control);

            if (!(controlKeyState && selectedToolType == ToolType.Pointer))
            {
                var coll = GetSelectedItems();
                foreach (var shape in coll)
                {
                    foreach (var anchor in shape.GetAnchors())
                    {
                        var anchorBounds = shape.GetAnchorBounds(anchor.Position);
                        if (e.Location.X >= anchorBounds.X && e.Location.Y >= anchorBounds.Y &&
                            e.Location.X <= (anchorBounds.X + anchorBounds.Width) && e.Location.Y <= (anchorBounds.Y + anchorBounds.Height))
                        {
                            transformState = TransformState.Scale;
                            selectedAnchorPos = anchor.Position;
                            SetSelectedItemsDefaultBounds();
                            return;
                        }
                    }
                }
                //check bounds
                
                foreach(var ob in coll.Reverse())
                {
                    var rBounds = ob.GetWithotNegative();
                    if (e.Location.X >= rBounds.X && e.Location.Y >= rBounds.Y &&
                            e.Location.X <= (rBounds.X + rBounds.Width) && e.Location.Y <= (rBounds.Y + rBounds.Height))
                    {
                        transformState = TransformState.Move;
                        SetSelectedItemsDefaultBounds();
                        return;
                    }
                }
            }
            
            //tools logic
            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    ResetSelection();
                    Shape shape;
                    switch (selectedShapeType)
                    {
                        case ShapeType.Line:
                            shape = new LineShape(new Rectangle(e.Location, Size.Empty), drawOutlineToolStripMenuItem.Checked ? mainPen : null, drawFillToolStripMenuItem.Checked ? mainBrush : null);
                            break;
                        case ShapeType.Rectangle:
                            shape = new RectangleShape(new Rectangle(e.Location, Size.Empty), drawOutlineToolStripMenuItem.Checked ? mainPen : null, drawFillToolStripMenuItem.Checked ? mainBrush : null);
                            break;
                        case ShapeType.Ellipse:
                            shape = new EllipseShape(new Rectangle(e.Location, Size.Empty), drawOutlineToolStripMenuItem.Checked ? mainPen : null, drawFillToolStripMenuItem.Checked ? mainBrush : null);
                            break;
                        default:
                            return;
                    }
                    shape.AddAnchors(anchors);
                    shape.SelectionState = ShapeSelectionState.Bounds;
                    shapes.Add(shape);
                    break;
                case ToolType.Pointer:
                    if (!controlKeyState)
                        ResetSelection();
                    foreach(var ob in shapes.Reverse<Shape>())
                    {
                        var p = e.Location;
                        if (p.X > ob.Bounds.X && p.Y > ob.Bounds.Y && p.X < (ob.Bounds.X + ob.Bounds.Width) && p.Y < (ob.Bounds.Y + ob.Bounds.Height))
                        {
                            if (controlKeyState)
                            {
                                ob.SelectionState = ob.SelectionState.HasFlag(ShapeSelectionState.BoundsAndAnchors) ? ShapeSelectionState.None : ShapeSelectionState.BoundsAndAnchors;
                            }
                            else
                            {
                                transformState = TransformState.Move;
                                ob.SelectionState = ShapeSelectionState.BoundsAndAnchors;
                            }
                            SetSelectedItemsDefaultBounds();
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

            selectedBounds.Size = new Size(e.X - selectedBounds.X, e.Y - selectedBounds.Y);

            //transform logic
            if (transformState != TransformState.None)
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
                        foreach (var ob in GetSelectedItems())
                        {
                            var tempBounds = new Rectangle(new Point(increase.Location.X + ob.DefaultBounds.Location.X, increase.Location.Y + ob.DefaultBounds.Location.Y), increase.Size + ob.DefaultBounds.Size);

                            if (minimumSize.Width > tempBounds.Width)
                                tempBounds.Width = minimumSize.Width;
                            if (minimumSize.Height > tempBounds.Height)
                                tempBounds.Height = minimumSize.Height;
                            var minX = (ob.DefaultBounds.X + ob.DefaultBounds.Width - minimumSize.Width);
                            var minY = (ob.DefaultBounds.Y + ob.DefaultBounds.Height - minimumSize.Height);
                            if (minX < tempBounds.X)
                                tempBounds.X = minX;
                            if (minY < tempBounds.Y)
                                tempBounds.Y = minY;

                            ob.Bounds = tempBounds;
                        }
                        break;
                    case TransformState.Move:
                        foreach(var ob in GetSelectedItems())
                        {
                            ob.Bounds.Location = new Point(ob.DefaultBounds.X + selectedBounds.Width, ob.DefaultBounds.Y + selectedBounds.Height);
                        }
                        break;
                }
                pictureBox1.Invalidate();
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    shapes.Last().Bounds = selectedBounds.WithoutNegative();
                    break;
                case ToolType.Pointer:
                    var normalized = selectedBounds.WithoutNegative();
                    if (normalized.Width > 1 || normalized.Height > 1)
                    {
                        var selectedColl = GetSelectedItems();
                        if (!selectedColl.Any())
                            ResetSelection();
                        foreach (var ob in shapes)
                        {
                            var r = ob.Bounds;
                            if (r.X > normalized.X && r.Y > normalized.Y && (r.X + r.Width) < (normalized.X + normalized.Width) && (r.Y + r.Height) < (normalized.Y + normalized.Height))
                                ob.SelectionState = ShapeSelectionState.Bounds;
                            else
                                ob.SelectionState = ShapeSelectionState.None;
                        }
                    }
                    break;
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
                return;
            mouseDown = false;
            if (transformState != TransformState.None)
            {
                transformState = TransformState.None;
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    if (!(Math.Abs(selectedBounds.Width) > minimumSize.Width && Math.Abs(selectedBounds.Height) > minimumSize.Height))
                        shapes.RemoveAt(shapes.Count - 1);
                    else
                        shapes.Last().SelectionState = ShapeSelectionState.BoundsAndAnchors;
                    break;
                case ToolType.Pointer:
                    var normalized = selectedBounds.WithoutNegative();
                    var validArea = (normalized.Width > 1 || normalized.Height > 1);
                    shapes.ForEach(i =>
                    {
                        if (i.SelectionState.HasFlag(ShapeSelectionState.Bounds))
                            i.SelectionState = validArea ? ShapeSelectionState.BoundsAndAnchors : ShapeSelectionState.None;
                    });
                    break;
            }
            pictureBox1.Invalidate();
        }

        private bool IsSaved()
        {
            if (!File.Exists(fileFullName))
            {
                return !shapes.Any();
            }
            else
            {
                try
                {
                    return new Bitmap(fileFullName).CompareMemCmp(GetBitmap(false));
                }
                catch
                {
                    return false;
                }
            }
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
                foreach (var ob in GetSelectedItems())
                {
                    ob.Pen = mainPen;
                }
                pictureBox1.Invalidate();
            }
        }

        private void brushSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new BrushSetupDialog("Brush setting", mainBrush);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mainBrush = dialog.GetValue();
                foreach (var ob in GetSelectedItems())
                {
                    ob.Brush = mainBrush;
                }
                pictureBox1.Invalidate();
            }
        }

        private void RemoveItems()
        {
            foreach(var ob in GetSelectedItems().Reverse())
            {
                shapes.Remove(ob);
            }
        }

        private void CutItems()
        {
            var coll = GetSelectedItems();
            if (!coll.Any())
                return;
            buffer.Clear();
            buffer.AddRange(coll.Select(i => (Shape)i.Clone()));
            RemoveItems();
            pictureBox1.Invalidate();
        }

        private void PasteItems()
        {
            if (!buffer.Any())
                return;
            ResetSelection();
            shapes.AddRange(buffer.Select(i =>
            {
                i = (Shape)i.Clone();
                if (MousePosition.X > pictureBox1.Location.X && MousePosition.Y > pictureBox1.Location.Y && MousePosition.X < (pictureBox1.Location.X + pictureBox1.Width) && MousePosition.Y < (pictureBox1.Location.Y + pictureBox1.Height))
                {
                    i.Bounds.Location = new Point(MousePosition.X + 15, MousePosition.Y + 15);
                }
                else
                {
                    i.Bounds.X += 15;
                    i.Bounds.Y += 15;
                }
                i.SelectionState = ShapeSelectionState.BoundsAndAnchors;
                return i;
            }));
            pictureBox1.Invalidate();
        }

        private void CopyItems()
        {
            var coll = GetSelectedItems();
            if (!coll.Any())
                return;
            buffer.Clear();
            buffer.AddRange(coll.Select(i => (Shape)i.Clone()));
            pictureBox1.Invalidate();
        }

        private void BringToFrontItems()
        {
            //foreach(var ob in GetSelectedItems())
            //{
            //    int index = shapes.IndexOf(ob);
            //    int lastIndex = shapes.Count - 1;
            //    if (index != -1 && index < lastIndex)
            //    {
            //        shapes
            //        shapes.RemoveAt(index);
            //        shapes.Insert(index + 1, ob);
            //    }
            //}
            //pictureBox1.Invalidate();
        }

        private void SendToBackItems()
        {
            //foreach (var ob in selectedItems)
            //{
            //    int index = shapes.IndexOf(ob);
            //    if (index != -1 && index > 0)
            //    {
            //        shapes.RemoveAt(index);
            //        shapes.Insert(index - 1, ob);
            //    }
            //}
            //pictureBox1.Invalidate();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CutItems();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PasteItems();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CopyItems();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RemoveItems();
            pictureBox1.Invalidate();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shapes.ForEach(i => i.SelectionState = ShapeSelectionState.BoundsAndAnchors);
            pictureBox1.Invalidate();
        }

        private void resetSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shapes.ForEach(i => i.SelectionState = ShapeSelectionState.None);
            pictureBox1.Invalidate();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var count = GetSelectedItems().Count();
            bool oneOrMoreSelected = count >= 1;
            propertiesToolStripMenuItem.Enabled = count == 1;
            cutToolStripMenuItem.Enabled = oneOrMoreSelected;
            copyToolStripMenuItem.Enabled = oneOrMoreSelected;
            pasteToolStripMenuItem.Enabled = buffer.Any();
            deleteToolStripMenuItem.Enabled = oneOrMoreSelected;
            bringToFrontToolStripMenuItem.Enabled = oneOrMoreSelected;
            sendToBackToolStripMenuItem.Enabled = oneOrMoreSelected;
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var coll = GetSelectedItems();
            if (coll.Count() != 1)
                return;
            var dialog = new SetupShapePropertiesDialog("Shape setting", coll.Single());
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                //coll.Single().;
                //var index = shapes.IndexOf(coll.Single());
                //if (index == -1)
                //    return;
                //var selectState = shapes[index].SelectionState;
                //shapes[index] = dialog.GetValue();
                //shapes[index].SelectionState = selectState;
                //pictureBox1.Invalidate();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutItems();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyItems();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteItems();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveItems();
            pictureBox1.Invalidate();
        }

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BringToFrontItems();
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendToBackItems();
        }

        private string GetTitle()
        {
            return $"{fileName} - {this.ProductName.SplitCamelCase()}";
        }

        private void Export()
        {
            var dialog = new SaveFileDialog();

            dialog.Filter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|BMP files (*.bmp)|*.bmp|HEIC files (*.heic)|*.heic|TIFF files (*.tif)|*.tif|All files (*.*)|*.*";
            dialog.DefaultExt = ".png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileFullName = dialog.FileName;
                fileName = Path.GetFileNameWithoutExtension(fileFullName);
                try
                {
                    using (var map = GetBitmap(false))
                    {
                        map.Save(fileFullName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Text = GetTitle();
            }
        }

        private Bitmap GetBitmap(bool dispose = true)
        {
            Bitmap map = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (var g = Graphics.FromImage(map))
            {
                foreach (var ob in shapes)
                {
                    ob.DrawFill(g);
                    ob.DrawStroke(g);
                }
            }
            if (dispose)
                map.Dispose();
            return map;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void SaveFile()
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsSaved())
            {
                var res = NonSavedFileDialog.ShowNonSaveDialog();
                if (res == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if(res == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }
    }
}