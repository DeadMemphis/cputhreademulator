using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace Drawer
{
    interface IDraw
    {
        Bitmap DrawBrickUp(Graphics graph, Bitmap bitmap, int pos, int line, COMMAND_TYPE_COLOR ctype, bool hatch);
        Bitmap DrawBrickDown(Graphics graph, Bitmap bitmap, int pos, int line, COMMAND_TYPE_COLOR ctype, bool hatch);
        Bitmap DrawField(Graphics graph, Bitmap bitmap, PictureBox targetBox);
        Bitmap DrawMPLine(Graphics graph, Bitmap bitmap, PictureBox targetBox, int countMP);
        void Hatching(Graphics graph, Bitmap bitmap, PointF[] points, HatchStyle hatchStyle, Color foreColor);
    }
}
