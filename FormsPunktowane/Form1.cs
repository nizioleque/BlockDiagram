using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Resources;

namespace FormsPunktowane
{
    public partial class Form1 : Form
    {
        private Bitmap? drawArea;
        Color bgColor;
        public List<DiagramElement> elements;
        bool hasStart;
        DiagramElement? selectedElement;
        bool moving;
        (int width, int height) canvasSize;
        ResourceManager rsm;

        (int x, int y) selectedElementOffset;

        Arrow? tmpArrow;

        public Form1()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            rsm = new ResourceManager("FormsPunktowane.Form1", typeof(Form1).Assembly);

            InitializeComponent();
            bgColor = Color.LightBlue;
            canvasSize = (1000, 1000);
            createBitmap();
            elements = new List<DiagramElement>();
        }



        private void createBitmap(bool clear = true)
        {
            int width = canvasSize.width;
            int height = canvasSize.height;

            if (clear)
            {
                deselectElement();
                if (elements != null) elements.Clear();
                hasStart = false;
                selectedElement = null;
            }
            else
            {
                if (selectedElement != null) selectElement(selectedElement);
            }
            moving = false;

            drawArea = new Bitmap(width, height);
            canvas.Image = drawArea;
            using (Graphics g = Graphics.FromImage(drawArea))
            {
                g.Clear(bgColor);
            }
            canvasParent.Width = width;
            canvasParent.Height = height;
            canvas.Width = width;
            canvas.Height = height;

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();

            if (f.DialogResult == DialogResult.OK)
            {
                canvasSize = (f.retWidth, f.retHeight);
                createBitmap();
            }
 
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        DiagramElement? findClosest(int x, int y)
        {
            List<DiagramElement> found = new List<DiagramElement>();
            foreach (DiagramElement d in elements)
            {
                if (d.isAt(x, y))
                {
                    found.Add(d);
                }
            }

            if (found.Count == 0)
            {
                // do nothing
                return null;
            }
            
            if (found.Count == 1)
            {
                return found[0];
            }
            else
            {
                DiagramElement closest = found[0];
                int minDist = (x - closest.x) * (x - closest.x) + (y - closest.y) * (y - closest.y);

                for (int i = 1; i < found.Count; i++)
                {
                    int tmpDist = (x - found[i].x) * (x - found[i].x) + (y - found[i].y) * (y - found[i].y);
                    if (tmpDist < minDist)
                    {
                        minDist = tmpDist;
                        closest = found[i];
                    }
                }
                return closest;
            }
        }

        Dot? findClosestDot(int x, int y, bool startDot)
        {
            List<Dot> found = new List<Dot>();
            foreach (DiagramElement d in elements)
            {
                foreach (Dot dd in d.dots)
                {
                    if (dd.isAt(x, y) && dd.startDot == startDot && !dd.occupied)
                    {
                        found.Add(dd);
                    }
                }
            }

            if (found.Count == 0)
            {
                // do nothing
                return null;
            }

            if (found.Count == 1)
            {
                return found[0];
            }
            else
            {
                Dot closest = found[0];
                int minDist = (x - closest.x) * (x - closest.x) + (y - closest.y) * (y - closest.y);

                for (int i = 1; i < found.Count; i++)
                {
                    int tmpDist = (x - found[i].x) * (x - found[i].x) + (y - found[i].y) * (y - found[i].y);
                    if (tmpDist < minDist)
                    {
                        minDist = tmpDist;
                        closest = found[i];
                    }
                }
                return closest;
            }

        }

        void selectElement(DiagramElement d)
        {
            if (selectedElement != null) deselectElement();

            d.selected = true;
            selectedElement = d;

            blockTextBox.Text = d.text;
            if (d.GetType() == typeof(DecisBlock) || d.GetType() == typeof(OperBlock))
            {
                blockTextBox.Enabled = true;
            }
        }

        void deselectElement()
        {
            if (selectedElement == null) return;
            selectedElement.selected = false;
            selectedElement = null;
            blockTextBox.Enabled = false;
            blockTextBox.Text = "";
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(bgColor);

            foreach (DiagramElement d in elements)
            {
                d.Draw(g);
            }
        }

        // inside Form class (for access to fields)
        [Serializable]
        public class DiagramElement
        {
            //protected Brush brush;
            //protected Pen pen;
            //protected Font font;
            public int x;
            public int y;
            public string text;
            protected RectangleF? textRect;
            public bool selected;

            public Dot[] dots;

            protected int w = 60;
            protected int h = 40;
            

            virtual public void Draw(Graphics g)
            {
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                Font font = new Font("Arial", 8);

                if (textRect == null)
                {
                    g.DrawString(text, font, Brushes.Black, new PointF(x, y), sf);
                }
                else
                {
                    g.DrawString(text, font, Brushes.Black, (RectangleF)textRect, sf);
                }
            }

            virtual public bool isAt(int x, int y)
            {
                return false;
            }

            public DiagramElement(int x, int y, string text)
            {
                this.x = x;
                this.y = y;
                this.text = text;
                this.selected = false;
                //brush = Brushes.White;
                //pen = new Pen(Color.Black, 2);
                //font = new Font("Arial", 8);
            }
        }

        [Serializable]
        public class Arrow : DiagramElement
        {
            public Dot startDot;
            public Dot endDot;
            (int x, int y) tmpEnd;

            public Arrow(Dot startDot) : base(0, 0, "")
            {
                this.startDot = startDot;
                this.endDot = null;
                dots = new Dot[0];
            }

            public void Update(int x, int y)
            {
                tmpEnd = (x, y);
            }

            public void SetEnd(Dot d)
            {

                this.endDot = d;

                startDot.occupied = true;
                endDot.occupied = true;

                startDot.arrow = this;
                endDot.arrow = this;
            }

            public override void Draw(Graphics g)
            {
                (int x, int y) end = tmpEnd;
                if (endDot != null)
                {
                    end = endDot.Coordinates();
                }

                Pen p = new Pen(Color.Black, 2);
                AdjustableArrowCap cap = new AdjustableArrowCap(5, 5);
                p.CustomEndCap = cap;

                var start = startDot.Coordinates();

                g.DrawLine(p, start.x, start.y, end.x, end.y);

                p.Dispose();
                cap.Dispose();
            }
        }

        [Serializable]
        class OperBlock : DiagramElement
        {
            protected PointF[] points;

            public OperBlock(int x, int y, string text) : base(x, y, text)
            {
                w = w * 3 / 4;
                h = w * 3 / 4;

                points = new PointF[4];
                calculatePoints();

                dots = new Dot[2];
                dots[0] = new Dot(this, 0, -h, false);
                dots[1] = new Dot(this, 0, +h, true);
            }

            void calculatePoints()
            {
                points[0] = new PointF(x - w, y - h);
                points[1] = new PointF(x + w, y - h);
                points[2] = new PointF(x + w, y + h);
                points[3] = new PointF(x - w, y + h);
                this.textRect = new RectangleF(points[0], new SizeF(2 * w, 2 * h));
            }

            public override void Draw(Graphics g)
            {
                calculatePoints();
                g.FillPolygon(Brushes.White, points);

                if (selected)
                {
                    Pen dashedPen = new Pen(Brushes.Black, 2);
                    dashedPen.DashPattern = new float[] { 3, 3 };
                    g.DrawPolygon(dashedPen, points);
                    dashedPen.Dispose();
                }
                else
                {
                    Pen pen = new Pen(Color.Black, 2);
                    g.DrawPolygon(pen, points);
                    pen.Dispose();
                }

                foreach (Dot dot in dots)
                {
                    dot.Draw(g);
                }

                base.Draw(g);
            }

            public override bool isAt(int x, int y)
            {
                if (x >= this.x - w 
                    && y >= this.y - h 
                    && x <= this.x + w 
                    && y <= this.y + h) 
                    return true;
                return false;
            }

        }

        [Serializable]
        class DecisBlock : DiagramElement
        {
            protected PointF[] points;

            public DecisBlock(int x, int y, string text) : base(x, y, text)
            {
                points = new PointF[4];
                calculatePoints();

                dots = new Dot[3];
                dots[0] = new Dot(this, -w, 0, true);
                dots[1] = new Dot(this, +w, 0, true);
                dots[2] = new Dot(this, 0, -h, false);
            }

            void calculatePoints()
            {
                points[0] = new PointF(x - w, y);
                points[1] = new PointF(x, y - h);
                points[2] = new PointF(x + w, y);
                points[3] = new PointF(x, y + h);

                this.textRect = new RectangleF(
                    x - w/2,
                    y - h/2,
                    w,
                    h
                );
            }


            public override void Draw(Graphics g)
            {
                calculatePoints();
                g.FillPolygon(Brushes.White, points);

                if (selected)
                {
                    Pen dashedPen = new Pen(Brushes.Black, 2);
                    dashedPen.DashPattern = new float[] { 3, 3 };
                    g.DrawPolygon(dashedPen, points);
                    dashedPen.Dispose();
                }
                else
                {
                    Pen pen = new Pen(Color.Black, 2);
                    g.DrawPolygon(pen, points);
                    pen.Dispose();
                }

                foreach (Dot dot in dots)
                {
                    dot.Draw(g);
                }

                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                Font font = new Font("Arial", 8);

                g.DrawString("T", font, Brushes.Black, new PointF(this.x - w, this.y - 14), sf);
                g.DrawString("F", font, Brushes.Black, new PointF(this.x + w, this.y - 14), sf);

                base.Draw(g);
            }

            public override bool isAt(int x, int y)
            {
                double a, b;

                a = 1.0 * -h / w;
                b = this.y - h - 1.0 * this.x * a;

                if (y < a * x + b) return false;

                b = this.y + h - 1.0 * this.x * a;

                if (y > a * x + b) return false;

                a = 1.0 * h / w;
                b = this.y - h - 1.0 *  this.x * a;

                if (y < a * x + b) return false;

                b = this.y + h - 1.0 * this.x * a;

                if (y > a * x + b) return false;

                return true;
            }
        }

        [Serializable]
        class StartBlock : DiagramElement
        {
            public StartBlock(int x, int y) : base(x, y, "START")
            {
                w = w * 3 / 4;
                h = h * 3 / 4;

                dots = new Dot[1];
                dots[0] = new Dot(this, 0, +h, true);
            }

            public override void Draw(Graphics g)
            {
                g.FillEllipse(Brushes.White, x - w, y - h, 2 * w, 2 * h);

                if (selected)
                {
                    Pen dashedPen = new Pen(Brushes.LimeGreen, 2);
                    dashedPen.DashPattern = new float[] { 3, 3 };
                    g.DrawEllipse(dashedPen, x - w, y - h, 2 * w, 2 * h);
                    dashedPen.Dispose();
                }
                else
                {
                    Pen pen = new Pen(Color.LimeGreen, 2);
                    g.DrawEllipse(pen, x - w, y - h, 2 * w, 2 * h);
                    pen.Dispose();
                }

                foreach (Dot dot in dots)
                {
                    dot.Draw(g);
                }


                base.Draw(g);
            }

            public override bool isAt(int x, int y)
            {
                double pos = 1.0 * (x - this.x) * (x - this.x) / w / w
                    + 1.0 * (y - this.y) * (y - this.y) / h / h;
                if (pos <= 1)
                {
                    return true; 
                }
                return false;
            }
        }

        [Serializable]
        class StopBlock: DiagramElement
        {
            public StopBlock(int x, int y) : base(x, y, "STOP")
            {
                w = w * 3 / 4;
                h = h * 3 / 4;

                dots = new Dot[1];
                dots[0] = new Dot(this, 0, -h, false);
            }

            public override void Draw(Graphics g)
            {
                g.FillEllipse(Brushes.White, x - w, y - h, 2 * w, 2 * h);

                if (selected)
                {
                    Pen dashedPen = new Pen(Brushes.Red, 2);
                    dashedPen.DashPattern = new float[] { 3, 3 };
                    g.DrawEllipse(dashedPen, x - w, y - h, 2 * w, 2 * h);
                    dashedPen.Dispose();
                }
                else
                {
                    Pen pen = new Pen(Color.Red, 2);
                    g.DrawEllipse(pen, x - w, y - h, 2 * w, 2 * h);
                    pen.Dispose();
                }

                foreach (Dot dot in dots)
                {
                    dot.Draw(g);
                }


                base.Draw(g);
            }

            public override bool isAt(int x, int y)
            {
                double pos = 1.0 * (x - this.x) * (x - this.x) / w / w
                    + 1.0 * (y - this.y) * (y - this.y) / h / h;
                if (pos <= 1) return true;
                return false;
            }

        }

        [Serializable]
        public class Dot
        {
            public DiagramElement parent;
            public Arrow arrow;
            public int x; // relative to parent's middle
            public int y;
            public bool occupied;
            public bool startDot;
            const int RADIUS = 5;
            //Pen pen = new Pen(Brushes.Black, 2);

            public Dot(DiagramElement parent, int x, int y, bool startDot)
            {
                this.parent = parent;
                this.x = x;
                this.y = y;
                this.occupied = false;
                this.startDot = startDot;
            }

            public void Draw(Graphics g)
            {
                if (this.occupied) return;

                Brush fillBrush = this.startDot ? Brushes.Black : Brushes.White;

                g.FillEllipse(
                    fillBrush,
                    parent.x + this.x - RADIUS,
                    parent.y + this.y - RADIUS,
                    2 * RADIUS,
                    2 * RADIUS
                );

                Pen pen = new Pen(Brushes.Black, 2);

                g.DrawEllipse(
                    pen,
                    parent.x + this.x - RADIUS,
                    parent.y + this.y - RADIUS,
                    2 * RADIUS,
                    2 * RADIUS
                );

                pen.Dispose();
            }

            public (int x, int y) Coordinates()
            {
                return (parent.x + this.x, parent.y + this.y);
            }

            public bool isAt(int x, int y)
            {
                int midX = parent.x + this.x;
                int midY = parent.y + this.y;

                double dist = 1.0 * (x - midX) * (x - midX) + 1.0 * (y - midY) * (y - midY);
                if (dist <= RADIUS * RADIUS) return true;
                return false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (moving) return;

                if (trash_button.Checked)
                {
                    DiagramElement closest = findClosest(e.X, e.Y);
                    if (closest != null)
                    {
                        if (closest.GetType() == typeof(StartBlock))
                            hasStart = false;

                        if (closest == selectedElement)
                            deselectElement();

                        foreach (Dot d in closest.dots)
                        {
                            if (d.arrow != null)
                            {
                                Dot otherEnd = (d == d.arrow.startDot)
                                    ? d.arrow.endDot : d.arrow.startDot;

                                otherEnd.arrow = null;
                                otherEnd.occupied = false;
                                elements.Remove(d.arrow);
                            }
                        }

                        elements.Remove(closest);
                    }
                }
                else if (link_button.Checked)
                {
                    Dot closest = findClosestDot(e.X, e.Y, true);
                    if (closest != null)
                    {
                        tmpArrow = new Arrow(closest);
                        tmpArrow.Update(e.X, e.Y);
                        elements.Add(tmpArrow);
                    }
                }
                else
                {
                    using (Graphics g = Graphics.FromImage(drawArea))
                    {
                        DiagramElement d;

                        if (blok_operacyjny.Checked)
                        {
                            d = new OperBlock(e.X, e.Y, this.rsm.GetString("OperBlock"));
                        }
                        else if (blok_decyzyjny.Checked)
                        {
                            d = new DecisBlock(e.X, e.Y, this.rsm.GetString("DecisBlock"));

                        }
                        else if (blok_start.Checked)
                        {
                            if (hasStart)
                            {
                                MessageBox.Show("Schemat posiada już blok startowy!");
                                return;
                            }

                            hasStart = true;
                            d = new StartBlock(e.X, e.Y);
                        }
                        else if (blok_stop.Checked)
                        {
                            d = new StopBlock(e.X, e.Y);
                        }
                        else
                        {
                            d = new DiagramElement(e.X, e.Y, "");
                        }

                        elements.Add(d);
                        d.Draw(g);
                    }
                }
                canvas.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (moving) return;

                DiagramElement closest = findClosest(e.X, e.Y);
                if (closest != null)
                {
                    selectElement(closest);
                }
                else
                {
                    deselectElement();
                }
                canvas.Invalidate();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if (selectedElement != null)
                {
                    moving = true;
                    selectedElementOffset = (e.X - selectedElement.x, e.Y - selectedElement.y);
                }
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (tmpArrow != null)
                {
                    tmpArrow.Update(e.X, e.Y);
                    canvas.Invalidate();
                }
            }
            if (e.Button == MouseButtons.Middle)
            {
                if (selectedElement != null)
                {
                    selectedElement.x = e.X - selectedElementOffset.x;
                    selectedElement.y = e.Y - selectedElementOffset.y;
                    canvas.Invalidate();
                }
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (tmpArrow != null)
                {
                    Dot closest = findClosestDot(e.X, e.Y, false);
                    if (closest != null && closest.parent != tmpArrow.startDot.parent)
                    {
                        tmpArrow.SetEnd(closest);
                    }
                    else
                    {
                        elements.Remove(tmpArrow);
                    }

                    tmpArrow = null;
                    canvas.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if (selectedElement != null)
                {
                    if (selectedElement.x < 0) selectedElement.x = 0;
                    if (selectedElement.y < 0) selectedElement.y = 0;
                    if (selectedElement.x >= canvas.Width) selectedElement.x = canvas.Width;
                    if (selectedElement.y >= canvas.Height) selectedElement.y = canvas.Height;
                    moving = false;
                    canvas.Invalidate();
                }
            }

        }

        private void blockTextBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedElement != null)
            {
                selectedElement.text = blockTextBox.Text;
                canvas.Invalidate();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Stream s;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Diagram files (*.diag)|*.diag";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((s = saveFileDialog.OpenFile()) != null) {

                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    DiagSize size = new DiagSize();
                    size.width = this.canvasSize.width;
                    size.height = this.canvasSize.height;

                    bformatter.Serialize(s, size);
                    bformatter.Serialize(s, this.elements);

                    s.Close();
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Diagram files (*.diag)|*.diag";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var fileStream = openFileDialog.OpenFile();

                        if (fileStream != null)
                        {
                            var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                            DiagSize size = (DiagSize)bformatter.Deserialize(fileStream);
                            this.elements = (List<DiagramElement>)bformatter.Deserialize(fileStream);
                            this.canvasSize = (size.width, size.height);

                            fileStream.Close();

                            this.hasStart = false;
                            this.selectedElement = null;

                            foreach (DiagramElement d in this.elements)
                            {
                                if (d.GetType() == typeof(StartBlock))
                                {
                                    if (this.hasStart)
                                    {
                                        throw new Exception("double start!");
                                    }
                                    this.hasStart = true;
                                }

                                if (d.selected)
                                {
                                    if (this.selectedElement != null)
                                    {
                                        throw new Exception("double selected element!");
                                    }
                                    this.selectedElement = d;
                                }
                            }

                            createBitmap(false);
                            canvas.Invalidate();

                        }
                    }
                    catch
                    {
                        MessageBox.Show(rsm.GetString("wrongFile"));
                    }
                }
            }
        }

        [Serializable]
        class DiagSize
        {
            public int width;
            public int height;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            reloadLanguage("pl-PL");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            reloadLanguage("en-US");

        }

        void reloadLanguage(string culture)
        {
            int scrollX = canvasParent.HorizontalScroll.Value;
            int scrollY = canvasParent.VerticalScroll.Value;
            Size size = this.Size;
            bool max = this.WindowState == FormWindowState.Maximized;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            rsm = new ResourceManager("FormsPunktowane.Form1", typeof(Form1).Assembly);

            this.Controls.Clear();

            InitializeComponent();

            canvas.Width = canvasSize.width;
            canvas.Height = canvasSize.height;
            canvasParent.Width = canvasSize.width;
            canvasParent.Height = canvasSize.height;
            this.Size = size;

            if (max)
            {
                this.WindowState = FormWindowState.Minimized;
                this.WindowState = FormWindowState.Maximized;
            }

            canvasParent.HorizontalScroll.Value = scrollX;
            canvasParent.VerticalScroll.Value = scrollY;
            canvasParent.ScrollControlIntoView(canvasParent);

            if (selectedElement != null)
            {
                blockTextBox.Text = selectedElement.text;
                if (selectedElement.GetType() == typeof(DecisBlock) || selectedElement.GetType() == typeof(OperBlock))
                {
                    blockTextBox.Enabled = true;
                }
            }
        }

        private void canvas_Click(object sender, EventArgs e)
        {

        }
    }


}
