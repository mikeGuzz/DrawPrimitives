using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Security;
using System.Xml.Schema;
using System.Windows.Forms;
using System.Xml;
using DrawPrimitives.Helpers;
using DrawPrimitives.Shapes;
using DrawPrimitives.Dialog.SetupDialogs;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;

namespace DrawPrimitives
{
    public enum ToolType { Pointer, DrawShape };
    public enum ShapeType { Rectangle, Ellipse, TextBox, Diamond, Pentagon, Hexagone, Parallelepiped };

    public partial class Form1 : Form
    {
        private enum TransformState { None, Move, Rotate, Scale };

        //file
        private string filePath = string.Empty;
        private string fileName => string.IsNullOrEmpty(filePath) ? "Untitled" : (File.Exists(filePath) ? Path.GetFileNameWithoutExtension(filePath) : "Untitled");

        //pens
        private readonly Pen selectedPen;
        private readonly Pen anchorPen;
        private readonly Pen selectedBoundsPen;

        //brushes
        private readonly SolidBrush anchorBrush;
        private readonly SolidBrush selectedBoundsBrush;

        //select
        private Rectangle realSelectedBounds
        {
            get
            {
                var wN = selectedBounds;//.WithoutNegative();
                return new Rectangle((int)(wN.X / canvas.Zoom) - canvas.OffsetX, (int)(wN.Y / canvas.Zoom) - canvas.OffsetY, (int)(wN.Width / canvas.Zoom), (int)(wN.Height / canvas.Zoom));
            }
        }
        private Rectangle selectedBounds;

        //transform
        private TransformState transformState = TransformState.None;
        private AnchorPosition selectedAnchorPos = AnchorPosition.None;
        private TransformHelper transformHelper;
        private ShapeAnchor[] anchors;
        private readonly Size shapeMinimumSize = new Size(9, 9);

        //control
        private bool mouseDown;

        //shapes
        private Canvas canvas;
        private List<Shape> shapes = new List<Shape>();

        //menu strip
        private ShapeType selectedShapeType = ShapeType.Ellipse;
        private ToolType selectedToolType = ToolType.Pointer;

        //buffer
        private List<Shape> buffer = new List<Shape>();

        //offset
        //private Size clientBoundsSize => new Size(ClientRectangle.X + ClientRectangle.Width - vScrollBar1.Width, ClientRectangle.Height - menuStrip1.Height - hScrollBar1.Height - statusStrip1.Height);

        //undo/redo
        private UndoRedoHelper<string> undoRedoHelper;

        //json
        private JsonSerializerOptions jsonProperty;

        public Form1()
        {
            InitializeComponent();

            jsonProperty = new JsonSerializerOptions()
            {
                IncludeFields = true,
            };
            UpdateScrollBars();
            UpdatePictureBoxLocation();

            canvas = new Canvas(new Size(400, 180));
            canvas.OffsetX = 30;
            canvas.OffsetY = 15;

            //check on save
            Shape.DefaultPen = new Pen(Color.Black, 4);
            Shape.DefaultBrush = new SolidBrush(Color.Black);

            selectedPen = new Pen(Color.Black, 1.5f);
            selectedPen.Alignment = PenAlignment.Outset;
            selectedPen.DashStyle = DashStyle.Dash;
            selectedPen.DashPattern = new float[] { 3.5f, 2f };

            selectedBoundsPen = new Pen(Color.FromArgb(51, 120, 232), 1.5f);
            selectedBoundsBrush = new SolidBrush(Color.FromArgb(75, 51, 120, 232));

            anchorPen = new Pen(Color.DimGray, 2f);
            anchorBrush = new SolidBrush(Color.White);

            var anchorSize = new Size(8, 8);
            anchors = new ShapeAnchor[]
            {
                    new ShapeAnchor(anchorSize, AnchorPosition.Left, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.Top, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.Right, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.Bottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.LeftTop, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.LeftBottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.RightTop, anchorPen, anchorBrush, AnchorShape.Rectangle),
                    new ShapeAnchor(anchorSize, AnchorPosition.RightBottom, anchorPen, anchorBrush, AnchorShape.Rectangle),
            };
            transformHelper = new TransformHelper(selectedPen, null);
            undoRedoHelper = new UndoRedoHelper<string>();

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
            var docR = canvas.GetFileBounds(pictureBox1.ClientRectangle, true, true);
            var zoom = new SizeF((float)canvas.Zoom, (float)canvas.Zoom);

            //fill document bounds
            g.FillRectangle(new SolidBrush(canvas.BackColor), docR);
            using (var t = new Matrix())
            {
                t.Scale((float)canvas.Zoom, (float)canvas.Zoom);
                t.Translate((float)(canvas.OffsetX / canvas.Zoom), (float)(canvas.OffsetY / canvas.Zoom));
                g.Transform = t;
                foreach (var ob in shapes)
                {
                    ob.DrawFill(g);//, new Point(-canvas.OffsetX, -canvas.OffsetY), new SizeF(1, 1));
                    ob.DrawStroke(g);//, canvas.Offset, new SizeF(1, 1));
                }
                g.ResetTransform();
            }

            //draw document bounds
            g.DrawRectangle(Pens.Black, docR);

            shapes.ForEach(i =>
            {
                if (i.IsSelected)
                    g.DrawRectangle(selectedPen, i.GetBounds(canvas.Offset, zoom).WithoutNegative());
            });

            if (transformHelper.Any())
            {
                transformHelper.DrawStroke(g, canvas.Offset, zoom);
                var r = transformHelper.GetBounds(canvas.Offset, zoom);
                foreach (var ob in anchors)
                    ob.Draw(g, r, true);
            }

            if (mouseDown && (Math.Abs(selectedBounds.Width) > 1 || Math.Abs(selectedBounds.Height) > 1) && selectedToolType == ToolType.Pointer && transformState == TransformState.None)
            {
                var bounds = selectedBounds.WithoutNegative();
                g.FillRectangle(selectedBoundsBrush, bounds);
                g.DrawRectangle(selectedBoundsPen, bounds);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            mouseDown = true;
            selectedBounds.Location = e.Location;
            selectedBounds.Size = Size.Empty;
            var controlKeyState = ModifierKeys.HasFlag(Keys.Control);

            if (!(controlKeyState && selectedToolType == ToolType.Pointer) && transformHelper.Any())
            {
                if (IsHitAnyAnchor(e.Location, out var pos))
                {
                    transformHelper.StartTransform();
                    transformState = TransformState.Scale;
                    selectedAnchorPos = pos;
                    return;
                }
                if (transformHelper.IsHit(e.Location, canvas.Offset, canvas.SZoom))
                {
                    transformHelper.StartTransform();
                    transformState = TransformState.Move;
                    return;
                }
            }

            //tools logic
            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    transformHelper.Clear();
                    Shape shape;
                    var rect = new Rectangle(new Point(e.Location.X - canvas.OffsetX, e.Location.Y - canvas.OffsetY), Size.Empty);
                    switch (selectedShapeType)
                    {
                        case ShapeType.Rectangle:
                            shape = new RectangleShape(rect, drawOutlineToolStripMenuItem.Checked ? Shape.DefaultPen : null, drawFillToolStripMenuItem.Checked ? Shape.DefaultBrush : null);
                            break;
                        //case ShapeType.Ellipse:
                        //    shape = new EllipseShape(rect, drawOutlineToolStripMenuItem.Checked ? Shape.DefaultPen : null, drawFillToolStripMenuItem.Checked ? Shape.DefaultBrush : null);
                        //    break;
                        //case ShapeType.TextBox:
                        //    shape = new TextBoxShape(rect, Shape.DefaultBrush == null ? new SolidBrush(Color.Black) : Shape.DefaultBrush);
                        //    break;
                        //case ShapeType.Diamond:
                        //    shape = new DiamondShape(rect, drawOutlineToolStripMenuItem.Checked ? Shape.DefaultPen : null, drawFillToolStripMenuItem.Checked ? Shape.DefaultBrush : null);
                        //    break;
                        //case ShapeType.Pentagon:
                        //    shape = new PentagonShape(rect, drawOutlineToolStripMenuItem.Checked ? Shape.DefaultPen : null, drawFillToolStripMenuItem.Checked ? Shape.DefaultBrush : null);
                        //    break;
                        //case ShapeType.Hexagone:
                        //    shape = new HexagonShape(rect, drawOutlineToolStripMenuItem.Checked ? Shape.DefaultPen : null, drawFillToolStripMenuItem.Checked ? Shape.DefaultBrush : null);
                        //    break;
                        //case ShapeType.Parallelepiped:
                        //    shape = new ParallelepipedShape(rect, drawOutlineToolStripMenuItem.Checked ? Shape.DefaultPen : null, drawFillToolStripMenuItem.Checked ? Shape.DefaultBrush : null);
                        //    break;
                        default:
                            return;
                    }
                    shape.IsSelected = true;
                    shapes.Add(shape);
                    break;
                case ToolType.Pointer:
                    if (!controlKeyState)
                        transformHelper.Clear();
                    foreach (var ob in shapes.Reverse<Shape>())
                    {
                        if(ob.IsHit(e.Location, canvas.Offset, canvas.SZoom))
                        {
                            if (controlKeyState)
                            {
                                if (transformHelper.Contains(ob))
                                    transformHelper.Remove(ob);
                                else
                                    transformHelper.Add(ob);
                            }
                            else
                            {
                                transformState = TransformState.Move;
                                transformHelper.Add(ob);
                                transformHelper.StartTransform();
                            }
                            pictureBox1.Invalidate();
                            return;
                        }
                    }
                    transformHelper.Clear();
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos_toolStripStatusLabel.Text = $"{e.X}, {e.Y}px";

            if (transformHelper.Any() && transformState == TransformState.None)
            {
                if (IsHitAnyAnchor(e.Location, out var pos))
                {
                    switch (pos)
                    {
                        case AnchorPosition.RightBottom:
                            Cursor = Cursors.SizeNWSE;
                            break;
                        case AnchorPosition.LeftBottom:
                            Cursor = Cursors.SizeNESW;
                            break;
                        case AnchorPosition.LeftTop:
                            Cursor = Cursors.SizeNWSE;
                            break;
                        case AnchorPosition.Left:
                            Cursor = Cursors.SizeWE;
                            break;
                        case AnchorPosition.RightTop:
                            Cursor = Cursors.SizeNESW;
                            break;
                        case AnchorPosition.Right:
                            Cursor = Cursors.SizeWE;
                            break;
                        case AnchorPosition.Top:
                            Cursor = Cursors.SizeNS;
                            break;
                        case AnchorPosition.Bottom:
                            Cursor = Cursors.SizeNS;
                            break;
                    }
                }
                else if (transformHelper.IsHit(e.Location, new Point(canvas.OffsetX, canvas.OffsetY), new SizeF((float)canvas.Zoom, (float)canvas.Zoom)))
                    Cursor = Cursors.SizeAll;
                else
                    ResetCursor();
            }

            if (!mouseDown)
                return;

            selectedBounds.Size = new Size(e.X - selectedBounds.X, e.Y - selectedBounds.Y);

            if (transformState != TransformState.None && transformHelper.Any())
            {
                var realR = realSelectedBounds;
                switch (transformState)
                {
                    case TransformState.Move:
                        transformHelper.SetPosition(new Point(realR.Size));
                        break;
                    case TransformState.Scale:
                        Rectangle increase = Rectangle.Empty;
                        switch (selectedAnchorPos)
                        {
                            case AnchorPosition.RightBottom:
                                increase.Size = new Size(realR.Width, realR.Height);
                                break;
                            case AnchorPosition.LeftBottom:
                                increase.Size = new Size(-realR.Width, realR.Height);
                                increase.Location = new Point(realR.Width, 0);
                                break;
                            case AnchorPosition.LeftTop:
                                increase.Size = new Size(-realR.Width, -realR.Height);
                                increase.Location = new Point(realR.Width, realR.Height);
                                break;
                            case AnchorPosition.Left:
                                increase.Size = new Size(-realR.Width, 0);
                                increase.Location = new Point(realR.Width, 0);
                                break;
                            case AnchorPosition.RightTop:
                                increase.Size = new Size(realR.Width, -realR.Height);
                                increase.Location = new Point(0, realR.Height);
                                break;
                            case AnchorPosition.Right:
                                increase.Size = new Size(realR.Width, 0);
                                break;
                            case AnchorPosition.Top:
                                increase.Size = new Size(0, -realR.Height);
                                increase.Location = new Point(0, realR.Height);
                                break;
                            case AnchorPosition.Bottom:
                                increase.Size = new Size(0, realR.Height);
                                break;
                        }
                        var tempBounds = new Rectangle(new Point(increase.Location.X + transformHelper.StartTransformBounds.Location.X, increase.Location.Y + transformHelper.StartTransformBounds.Location.Y), increase.Size + transformHelper.StartTransformBounds.Size);

                        if (shapeMinimumSize.Width > tempBounds.Width)
                            tempBounds.Width = shapeMinimumSize.Width;
                        if (shapeMinimumSize.Height > tempBounds.Height)
                            tempBounds.Height = shapeMinimumSize.Height;

                        var minX = (transformHelper.StartTransformBounds.X + transformHelper.StartTransformBounds.Width - shapeMinimumSize.Width);
                        var minY = (transformHelper.StartTransformBounds.Y + transformHelper.StartTransformBounds.Height - shapeMinimumSize.Height);

                        if (minX < tempBounds.X)
                            tempBounds.X = minX;
                        if (minY < tempBounds.Y)
                            tempBounds.Y = minY;

                        transformHelper.Bound(tempBounds);
                        break;
                }
                pictureBox1.Invalidate();
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    shapes.Last().SetSize(realSelectedBounds.Size);
                    this.Text = shapes.Last().GetBounds().ToString();
                    break;
                case ToolType.Pointer:
                    var normalized = selectedBounds.WithoutNegative();
                    if (normalized.Width > 1 || normalized.Height > 1)
                    {
                        foreach (var ob in shapes)
                        {
                            var r = ob.GetBounds(canvas.Offset, canvas.SZoom);
                            ob.IsSelected = r.X > normalized.X && r.Y > normalized.Y && (r.X + r.Width) < (normalized.X + normalized.Width) && (r.Y + r.Height) < (normalized.Y + normalized.Height);
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
                if(TryJsonSerializeData(out var json))
                    undoRedoHelper.AddToUndoStack(json);
                return;
            }

            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    if (!(Math.Abs(selectedBounds.Width) > shapeMinimumSize.Width && Math.Abs(selectedBounds.Height) > shapeMinimumSize.Height))
                        shapes.RemoveAt(shapes.Count - 1);
                    else
                    {
                        shapes.Last().IsSelected = false;
                        shapes.Last().Bound(realSelectedBounds.WithoutNegative());
                        transformHelper.Add(shapes.Last());

                        if (shapes.Last() is TextBoxShape)
                            OpenTextShapeDialog(sender, e);

                        if (TryJsonSerializeData(out var json))
                            undoRedoHelper.AddToUndoStack(json);
                    }
                    break;
                case ToolType.Pointer:
                    var normalized = selectedBounds.WithoutNegative();
                    if (normalized.Width > 1 || normalized.Height > 1)
                    {
                        transformHelper.Clear();
                        transformHelper.AddRange(shapes.Where(i =>
                        {
                            if (i.IsSelected)
                            {
                                i.IsSelected = false;
                                return true;
                            }
                            return false;
                        }));
                    }
                    break;
            }
            pictureBox1.Invalidate();
        }

        private bool IsHitAnyAnchor(Point mousePos, out AnchorPosition pos)
        {
            pos = AnchorPosition.None;
            if (!transformHelper.Any())
                return false;
            foreach (var ob in anchors)
            {
                var anchorBounds = ob.GetBounds(transformHelper.GetBounds(new Point(canvas.OffsetX, canvas.OffsetY), new SizeF((float)canvas.Zoom, (float)canvas.Zoom)));
                if (mousePos.X >= anchorBounds.X && mousePos.Y >= anchorBounds.Y &&
                    mousePos.X <= (anchorBounds.X + anchorBounds.Width) && mousePos.Y <= (anchorBounds.Y + anchorBounds.Height))
                {
                    pos = ob.Position;
                    return true;
                }
            }
            return false;
        }

        public override void ResetCursor()
        {
            switch (selectedToolType)
            {
                case ToolType.DrawShape:
                    Cursor = Cursors.Cross;
                    break;
                case ToolType.Pointer:
                    Cursor = Cursors.Default;
                    break;
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
                    ResetCursor();
                }
            }
        }

        private void penSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PenDialog dialog;
            if (Shape.DefaultPen != null)
                dialog = new PenDialog("Pen setting", Shape.DefaultPen);
            else
                dialog = new PenDialog("Pen setting");
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Shape.DefaultPen = dialog.GetValue();
                foreach (var ob in transformHelper)
                {
                    ob.Pen = Shape.DefaultPen;
                }
                pictureBox1.Invalidate();
            }
        }//save state for undo/redo

        private void brushSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BrushDialog dialog;
            if (Shape.DefaultBrush is SolidBrush solid)
                dialog = new BrushDialog("Brush setting", solid);
            else if (Shape.DefaultBrush is HatchBrush hatch)
                dialog = new BrushDialog("Brush setting", hatch);
            else if (Shape.DefaultBrush is TextureBrush texture)
                dialog = new BrushDialog("Brush setting", texture);
            else return;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Shape.DefaultBrush = dialog.GetValue();
                foreach (var ob in transformHelper)
                {
                    ob.Brush = Shape.DefaultBrush;
                }
                pictureBox1.Invalidate();
            }
        }//save state for undo/redo

        //private void brushSettingToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    string title = "Brush setting";
        //    BrushSetupDialog dialog;
        //    if (!transformHelper.Any())
        //    {
        //        dialog = new BrushSetupDialog(title);
        //    }
        //    else if (transformHelper.Count == 1)
        //    {
        //        title += " (1 item)";
        //        if (mainBrush is SolidBrush solid)
        //            dialog = new BrushSetupDialog(title, solid);
        //        else if (mainBrush is HatchBrush hatch)
        //            dialog = new BrushSetupDialog(title, hatch);
        //        else if (mainBrush is TextureBrush texture)
        //            dialog = new BrushSetupDialog(title, texture);
        //        else return;
        //    }
        //    else
        //    {
        //        title += $" ({transformHelper.Count} items)";
        //        dialog = new BrushSetupDialog(title);
        //    }
        //    if (dialog.ShowDialog() == DialogResult.OK)
        //    {
        //        mainBrush = dialog.GetValue();
        //        foreach (var ob in transformHelper)
        //        {
        //            ob.Brush = mainBrush;
        //        }
        //        pictureBox1.Invalidate();
        //    }
        //}//save state for undo/redo

        private void RemoveItems()
        {
            foreach (var ob in transformHelper)
            {
                shapes.Remove(ob);
            }
            transformHelper.Clear();
            if (TryJsonSerializeData(out var json))
                undoRedoHelper.AddToUndoStack(json);
        }

        private void CutItems()
        {
            if (!transformHelper.Any())
                return;
            buffer.Clear();
            buffer.AddRange(transformHelper.Select(i => (Shape)i.Clone()));
            RemoveItems();
            pictureBox1.Invalidate();
            if (TryJsonSerializeData(out var json))
                undoRedoHelper.AddToUndoStack(json);
        }

        private void PasteItems()
        {
            if (!buffer.Any())
                return;
            transformHelper.Clear();
            shapes.AddRange(buffer.Select(i =>
            {
                i = (Shape)i.Clone();
                //if (MousePosition.X > pictureBox1.Location.X && MousePosition.Y > pictureBox1.Location.Y && MousePosition.X < (pictureBox1.Location.X + pictureBox1.Width) && MousePosition.Y < (pictureBox1.Location.Y + pictureBox1.Height))
                //{
                //    i.SetPosition(new Point(MousePosition.X + 15, MousePosition.Y + 15));//temp
                //}
                //else
                //{
                //    var r = i.GetBounds();
                //    i.SetPosition(r.X + 15, r.Y + 15);
                //}
                transformHelper.Add(i);
                return i;
            }));
            pictureBox1.Invalidate();
            if (TryJsonSerializeData(out var json))
                undoRedoHelper.AddToUndoStack(json);
        }

        private void CopyItems()
        {
            if (!transformHelper.Any())
                return;
            buffer.Clear();
            buffer.AddRange(transformHelper.Select(i => (Shape)i.Clone()));
        }

        private void BringToFrontItems()
        {
            foreach (var ob in transformHelper)
            {
                int index = shapes.IndexOf(ob);
                int lastIndex = shapes.Count - 1;
                if (index != -1 && index < lastIndex)
                {
                    shapes.RemoveAt(index);
                    shapes.Insert(index + 1, ob);
                }
            }
            pictureBox1.Invalidate();
            if (TryJsonSerializeData(out var json))
                undoRedoHelper.AddToUndoStack(json);
        }

        private void SendToBackItems()
        {
            foreach (var ob in transformHelper)
            {
                int index = shapes.IndexOf(ob);
                if (index != -1 && index > 0)
                {
                    shapes.RemoveAt(index);
                    shapes.Insert(index - 1, ob);
                }
            }
            pictureBox1.Invalidate();
            if (TryJsonSerializeData(out var json))
                undoRedoHelper.AddToUndoStack(json);
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
            transformHelper.Clear();
            transformHelper.AddRange(shapes);
            pictureBox1.Invalidate();
        }

        private void resetSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transformHelper.Clear();
            pictureBox1.Invalidate();
        }

        private void OpenTextShapeDialog(object? sender, EventArgs e)
        {
            if (transformHelper.Count != 1)
                return;
            if (transformHelper.First() is not TextBoxShape)
                return;
            var shape = (TextBoxShape)transformHelper.Single();
            var dialog = new TextShapeDialog("Text setting", shape);
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                //shape.Text = dialog.SelectedText;
                //shape.Font = dialog.SelectedFont;
                //TextBoxShape.DefaultFont = dialog.SelectedFont;
            }
            pictureBox1.Invalidate();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool oneOrMoreSelected = transformHelper.Count >= 1;
            propertiesToolStripMenuItem.Enabled = transformHelper.Any();
            cutToolStripMenuItem.Enabled = oneOrMoreSelected;
            copyToolStripMenuItem.Enabled = oneOrMoreSelected;
            pasteToolStripMenuItem.Enabled = buffer.Any();
            deleteToolStripMenuItem.Enabled = oneOrMoreSelected;
            bringToFrontToolStripMenuItem.Enabled = oneOrMoreSelected;
            sendToBackToolStripMenuItem.Enabled = oneOrMoreSelected;

            if(transformHelper.Count == 1 && transformHelper.Single() is TextBoxShape)
            {
                contextMenuStrip1.Items.Insert(1, new ToolStripMenuItem("Edit Text", null, OpenTextShapeDialog));
                contextMenuStrip1.Items[1].Tag = true;
            }
        }

        private void contextMenuStrip1_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (contextMenuStrip1.Items[1].Tag is not null)
                contextMenuStrip1.Items.RemoveAt(1);
        }

        private void ShowSelectedShapesProperties()
        {
            if (!transformHelper.Any())
                return;
            var title = $"Shape setting ({transformHelper.Count} item)";
            ShapeDialog dialog;
            if (transformHelper.Count == 1)
                dialog = new ShapeDialog(title, transformHelper.Single());
            else
                dialog = new ShapeDialog(title, transformHelper);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foreach(var ob in transformHelper)
                {
                    if (dialog.IsEditedSize)
                        ob.SetSize(dialog.ShapeSize);
                    if (dialog.IsEditedLocation)
                        ob.SetPosition(dialog.ShapeLocation);
                    if (dialog.IsEditedBrush)
                        ob.Brush = dialog.ShapeBrush;
                    if (dialog.IsEditedPen)
                        ob.Pen = dialog.ShapePen;
                    if (dialog.IsEditedFlipX)
                        ob.FlipX = dialog.ShapeFlipX;
                    if (dialog.IsEditedFlipY)
                        ob.FlipY = dialog.ShapeFlipY;
                }
                pictureBox1.Invalidate();
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSelectedShapesProperties();
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

        private bool TryXmlDeseializeData(string filePath, out MyOrder order)
        {
            order = new MyOrder();
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(MyOrder));
            try
            {
                using (Stream s = File.OpenRead(filePath))
                {
                    if (serializer.Deserialize(s) is MyOrder ob)
                    {
                        order = ob;
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool TryJsonSerializeData(out string json)
        {
            json = string.Empty;
            try
            {
                json = JsonSerializer.Serialize(new MyOrder(canvas, shapes.ToArray()), jsonProperty);
                using(Stream s = File.Create("myjson.txt"))
                {
                    using (var t = new StreamWriter(s))
                    {
                        t.Write(json);
                    }
                }
                return true;
            }
            catch 
            { 
                return false; 
            }
        }

        private bool TryJsonDeserializeData(string json, out MyOrder order)
        {
            order = new MyOrder();
            try
            {
                var ob = JsonSerializer.Deserialize<MyOrder>(json, jsonProperty);
                if (ob != null)
                {
                    order = ob;
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private bool IsSaved()
        {
            if (!File.Exists(filePath))
            {
                return !shapes.Any();
            }
            else
            {
                if(TryXmlDeseializeData(filePath, out var order))
                {
                    if (order.Shapes == null)
                        return !(shapes.Count > 0);
                    if(shapes.Except(order.Shapes).Count() != 0)
                        return false;
                    if (order.Canvas == null)
                        return false;
                    return order.Canvas == canvas;
                }
            }
            return false;
        }

        private string GetTitle()
        {
            return $"{fileName} - {ProductName.SplitCamelCase()}";
        }

        private Bitmap GetBitmap(bool dispose)
        {
            Bitmap map = new Bitmap(10, 10);//(pictureBox1.Width, pictureBox1.Height);
            //using (var g = Graphics.FromImage(map))
            //{
            //    g.Clear(pictureBox1.BackColor);
            //    if(pictureBox1.Image != null)
            //        g.DrawImage(pictureBox1.Image, Point.Empty);
            //    foreach (var ob in shapes)
            //    {
            //        ob.DrawFill(g);
            //        ob.DrawStroke(g);
            //    }
            //}
            if (dispose)
                map.Dispose();
            return map;
        }

        public bool Export(string fileName)
        {
            try
            {
                using (var map = GetBitmap(false))
                {
                    map.Save(fileName);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void Export()
        {
            var dialog = new SaveFileDialog();

            dialog.Filter = "PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|BMP files (*.bmp)|*.bmp|HEIC files (*.heic)|*.heic|TIFF files (*.tif)|*.tif|All files (*.*)|*.*";
            dialog.DefaultExt = ".png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Export(dialog.FileName);
            }
        }

        public bool Save(string fileName)
        {
            try
            {
                using (Stream s = File.Create(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(MyOrder));
                    serializer.Serialize(s, new MyOrder(canvas, shapes.ToArray()));

                    filePath = fileName;
                    Text = GetTitle();                    
                }

                //using(Stream s = File.Create("xmlText.txt"))
                //{
                //    using (StreamWriter w = new StreamWriter(s))
                //    {
                //        XmlSerializer serializer = new XmlSerializer(typeof(MyOrder));
                //        serializer.Serialize(w, new MyOrder(canvas, shapes.ToArray()));
                //    }
                //}
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void Save()
        {
            if (File.Exists(filePath))
                Save(filePath);
            else
                SaveAsNew();
        }

        private void SaveAsNew()
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
                Save(dialog.FileName);
        }

        public void New()
        {
            if (!IsSaved())
            {
                var res = new NonSavedFileDialog(string.IsNullOrEmpty(filePath) ? "Untitled" : filePath).ShowDialog();
                if (res == DialogResult.Yes)
                    Save();
                else if (res == DialogResult.Cancel)
                    return;
            }
            transformHelper.Clear();
            shapes.Clear();
            filePath = string.Empty;
            Text = GetTitle();

            pictureBox1.Invalidate();
        }

        private void LoadFromOrder(MyOrder order)
        {
            transformHelper.Clear();
            shapes.Clear();

            if (order.Shapes != null)
                shapes.AddRange(order.Shapes);
            if (order.Canvas != null)
                canvas = order.Canvas;
            //canvas.SetupPictureBox(pictureBox1);

            pictureBox1.Invalidate();
        }

        public bool OpenXmlFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException();
            if (!IsSaved())
            {
                var res = new NonSavedFileDialog(string.IsNullOrEmpty(filePath) ? "Untitled" : filePath).ShowDialog();
                if (res == DialogResult.Yes)
                    Save();
                else if (res == DialogResult.Cancel)
                    return false;
            }
            try
            {
                if (TryXmlDeseializeData(fileName, out var order))
                {
                    LoadFromOrder(order);
                    filePath = fileName;
                    Text = GetTitle();
                    return true;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show($"Error: {e.Message}", ProductName.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void Open()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                OpenXmlFile(dialog.FileName);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsSaved())
            {
                var res = new NonSavedFileDialog(string.IsNullOrEmpty(filePath) ? "Untitled" : filePath).ShowDialog();
                if (res == DialogResult.Yes)
                    Save();
                else if (res == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsNew();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            UpdatePictureBoxLocation();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            UpdatePictureBoxLocation();
        }

        private void UpdatePictureBoxLocation()
        {
            //pictureBox1.Location = new Point(startOffsetPoint.X - hScrollBar1.Value, startOffsetPoint.Y - vScrollBar1.Value);
        }

        private void UpdateScrollBars()
        {
            //var size = clientBoundsSize;
            //var diffX = (pictureBox1.Location.X + pictureBox1.Size.Width) - (size.Width + startOffsetPoint.X) + vScrollBar1.Width;
            //var diffY = (pictureBox1.Location.Y + pictureBox1.Size.Height) - (size.Height + startOffsetPoint.Y) + hScrollBar1.Height;

            //hScrollBar1.Enabled = !(pictureBox1.Location.X >= startOffsetPoint.X && diffX <= 0);
            //if(hScrollBar1.Enabled)
            //{
            //    hScrollBar1.Maximum = diffX + (hScrollBar1.LargeChange - 1);
            //    hScrollBar1.Minimum = 0;
            //}
            //vScrollBar1.Enabled = !(pictureBox1.Location.Y >= startOffsetPoint.Y && diffY <= 0);
            //if(vScrollBar1.Enabled)
            //{
            //    vScrollBar1.Maximum = diffY + (vScrollBar1.LargeChange - 1);
            //    vScrollBar1.Minimum = 0;
            //}
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            UpdateScrollBars();
            UpdatePictureBoxLocation();
        }

        private void canvasSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasDialog dialog;
            dialog = new CanvasDialog("Canvas setting", canvas);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                canvas = dialog.GetValue();
                //canvas.SetupPictureBox(pictureBox1);
                UpdateScrollBars();//temp
                UpdatePictureBoxLocation();
                if (TryJsonSerializeData(out var json))
                    undoRedoHelper.AddToUndoStack(json);
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoRedoHelper.CanUndo())
            {
                if (TryJsonDeserializeData(undoRedoHelper.GetFromUndoStack(), out var res))
                    LoadFromOrder(res);
                else
                    undoRedoHelper.Clear();
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoRedoHelper.CanRedo())
            {
                if (TryJsonDeserializeData(undoRedoHelper.GetFromRedoStack(), out var res))
                    LoadFromOrder(res);
                else
                    undoRedoHelper.Clear();
            }
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = undoRedoHelper.CanUndo();
            redoToolStripMenuItem.Enabled = undoRedoHelper.CanRedo();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
    }
}