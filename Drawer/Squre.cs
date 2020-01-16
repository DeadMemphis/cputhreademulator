using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawer
{
    class Squre
    {
        static Point A = new Point(0, 0);
        static Point B = new Point(0, 0);
        static Point C = new Point(0, 0);
        static Point D = new Point(0, 0);
        static int FirstPointX;
        static int FirstPointY;
        static int PXStep;
        static int LineStep;
        public Squre(int pxstep, int linestep)
        {
            PXStep = pxstep;
            LineStep = linestep;
        }
        public PointF[] GetPoints()
        {
            return new PointF[] { A, B, C, D };
        }
        public PointF[] ReSetPoints(int pos, int line, SQURE_TYPE type)
        {
            SetPoints(pos, line, type);
            return new PointF[] { A, B, C, D };
        }
        private void SetPoints(int pos, int line, SQURE_TYPE type)
        {
            switch (type)
            {
                case SQURE_TYPE.UP:
                    {
                        FirstPointX = pos * PXStep;
                        FirstPointY = line * LineStep;
                        A = new Point(FirstPointX, FirstPointY); // 20, 60
                        B = new Point(FirstPointX, (FirstPointY -= PXStep)); // 20, 40
                        C = new Point((FirstPointX += PXStep), FirstPointY); // 40, 40
                        D = new Point(FirstPointX, (FirstPointY += PXStep)); // 40, 60
                        break;
                    }
                case SQURE_TYPE.DOWN:
                    {
                        FirstPointX = pos * PXStep;
                        FirstPointY = line * LineStep;
                        A = new Point(FirstPointX, FirstPointY); // 40, 60
                        B = new Point(FirstPointX, (FirstPointY += PXStep)); // 40, 80
                        C = new Point((FirstPointX += PXStep), FirstPointY); // 60, 80
                        D = new Point(FirstPointX, (FirstPointY -= PXStep)); // 60, 60
                        break;
                    }
                default:
                    throw new Exception();
            }
        }
    }
}
