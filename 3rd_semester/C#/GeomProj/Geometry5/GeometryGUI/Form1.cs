using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geometry5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private int max(int a, int b)
        {
            return a > b ? a : b;
        }

        private int min(int a, int b)
        {
            return a < b ? a : b;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            int res = Geometry5.GeometrySolver.checkIsAPointInsideOfATriangle(
                        new Tuple<int, int>(Convert.ToInt32(Point0x.Text), Convert.ToInt32(Point0y.Text)),
                        new Tuple<Tuple<int,int>, Tuple<int,int>, Tuple<int,int>>(
                            new Tuple<int,int>(Convert.ToInt32(Point1x.Text), Convert.ToInt32(Point1y.Text)),
                            new Tuple<int,int>(Convert.ToInt32(Point2x.Text), Convert.ToInt32(Point2y.Text)),
                            new Tuple<int,int>(Convert.ToInt32(Point3x.Text), Convert.ToInt32(Point3y.Text))));

            drawingPanel.Refresh();
            if (res == 0)
            {
                MessageBox.Show("The point lies on an edge");
            }
            else if (res == 1)
            {
                MessageBox.Show("The point lies inside of an triangle");
            }
            else
            {
                MessageBox.Show("The point lies outside of an triangle");
            }
        }

        private void drawingPanel_Paint(object sender, PaintEventArgs e)
        {
            int p1x = Convert.ToInt32(Point1x.Text);
            int p1y = Convert.ToInt32(Point1y.Text);
            int p2x = Convert.ToInt32(Point2x.Text);
            int p2y = Convert.ToInt32(Point2y.Text);
            int p3x = Convert.ToInt32(Point3x.Text);
            int p3y = Convert.ToInt32(Point3y.Text);

            int p0x = Convert.ToInt32(Point0x.Text);
            int p0y = Convert.ToInt32(Point0y.Text);

            Graphics g = drawingPanel.CreateGraphics();
            g.Clear(MainForm.DefaultBackColor);

            int maxX = max(max(p1x, p2x), max(p3x, p0x));
            int minX = min(min(p1x, p2x), min(p3x, p0x));
            int maxY = max(max(p1y, p2y), max(p3y, p0y));
            int minY = min(min(p1y, p2y), min(p3y, p0y));
            const int border = 15;
            double coefX = (double)(Math.Abs(maxX - minX)) / (drawingPanel.Width - 2 * border);
            double coefY = (double)(Math.Abs(maxY - minY)) / (drawingPanel.Height - 2 * border);

            Pen drawingTrianglePen = new Pen(Color.Red);
            Point p1 = new Point((int)((p1x - minX) / coefX) + border, (int)((maxY - p1y) / coefY) + border);
            Point p2 = new Point((int)((p2x - minX) / coefX) + border, (int)((maxY - p2y) / coefY) + border);
            Point p3 = new Point((int)((p3x - minX) / coefX) + border, (int)((maxY - p3y) / coefY) + border);
            g.DrawLine(drawingTrianglePen, p1, p2);
            g.DrawLine(drawingTrianglePen, p2, p3);
            g.DrawLine(drawingTrianglePen, p3, p1);

            Brush pointBrush = (Brush)Brushes.Green;
            Point p0 = new Point((int)((p0x - minX) / coefX) + border, (int)((maxY - p0y) / coefY) + border);
            g.FillRectangle(pointBrush, p0.X - 2, p0.Y - 2, 2, 2);
        }
    }
}
