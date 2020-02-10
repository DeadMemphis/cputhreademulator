using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Drawer
{
    public class Drawer : IDraw
    {
        Squre squreUp = new Squre(20, 60), squreDown = new Squre(20, 60);
        public void Hatching(Graphics graph, Bitmap bitmap, PointF[] points, HatchStyle hatchStyle, Color foreColor)
        {
            HatchBrush hBrush = new HatchBrush(
                    hatchStyle,
                    foreColor,
                    Color.FromArgb(0, 0, 0, 0));
            graph.FillPolygon(hBrush, points);
        }
        public Bitmap DrawBrickUp(Graphics graph, Bitmap bitmap, int pos, int line, COMMAND_TYPE_COLOR ctype, bool hatch = false)
        {
            graph = Graphics.FromImage(bitmap);
            squreUp.ReSetPoints(pos, line, SQURE_TYPE.UP);
            if (hatch) Hatching(graph, bitmap, squreUp.GetPoints(), HatchStyle.BackwardDiagonal, Color.FromKnownColor((KnownColor)ctype));
            graph.DrawCurve(new Pen(Color.FromKnownColor((KnownColor)ctype), 3), squreUp.GetPoints(), 0.01f);
            return bitmap;
        }
        public Bitmap DrawBrickDown(Graphics graph, Bitmap bitmap, int pos, int line, COMMAND_TYPE_COLOR ctype, bool hatch = false)
        {
            graph = Graphics.FromImage(bitmap);
            squreDown.ReSetPoints(pos, line, SQURE_TYPE.DOWN);
            if (hatch) Hatching(graph, bitmap, squreDown.GetPoints(), HatchStyle.BackwardDiagonal, Color.FromKnownColor((KnownColor)ctype));
            graph.DrawCurve(new Pen(Color.FromKnownColor((KnownColor)ctype), 3), squreDown.GetPoints(), 0.01f);
            return bitmap;
        }
        public Bitmap DrawField(Graphics graph, Bitmap bitmap, PictureBox targetBox)
        {
            graph = Graphics.FromImage(bitmap);
            for (int i = 0; i < targetBox.Width; i += 20)
                graph.DrawLine(new Pen(Color.Black, 2), new Point(i, targetBox.Height), new Point(i, 0));
            for (int i = 0; i < targetBox.Height; i += 20)
                graph.DrawLine(new Pen(Color.Black, 2), new Point(targetBox.Width, i), new Point(0, i));
            return bitmap;
        }
        public Bitmap DrawMPLine(Graphics graph, Bitmap bitmap, PictureBox targetBox, int countMP)
        {
            graph = Graphics.FromImage(bitmap);
            countMP *= 2; // mp + cc
            graph.DrawLine(new Pen(Color.Black, 4), new Point(20, 0), new Point(20, targetBox.Height));
            for (int i = 60, step = 0; step < countMP; i += 60, step++)
                graph.DrawLine(new Pen(Color.Black, 4), new Point(targetBox.Width, i), new Point(20, i));
            return bitmap;
            //Graph.Image = mainBitMap;
            //Graph.Refresh();
            //GC.Collect();
        }
    }
    interface IDraw
    {
        Bitmap DrawBrickUp(Graphics graph, Bitmap bitmap, int pos, int line, COMMAND_TYPE_COLOR ctype, bool hatch);
        Bitmap DrawBrickDown(Graphics graph, Bitmap bitmap, int pos, int line, COMMAND_TYPE_COLOR ctype, bool hatch);        
        Bitmap DrawField(Graphics graph, Bitmap bitmap, PictureBox targetBox);
        Bitmap DrawMPLine(Graphics graph, Bitmap bitmap, PictureBox targetBox, int countMP);
        void Hatching(Graphics graph, Bitmap bitmap, PointF[] points, HatchStyle hatchStyle, Color foreColor);
    }

}
