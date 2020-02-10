using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Drawer
{
    public partial class FormDrawer : Form
    {
        public static Bitmap mainBitMap;
        public static Graphics graph;
        static Drawer draw = new Drawer();
        public static int MPCount = 2, pxstep = 20, squrepos = 1, linepos = 0, shift = 0, monoline = 0, position = 0;
        static Dictionary<string, int> casesline = new Dictionary<string, int>();    
        static Dictionary<string, int> casesqure = new Dictionary<string, int>(); // name MP/CC, last position squre 
        static Action action = new Action(() => { Graph.Refresh(); });
        public static object threadLock = new object();
        public static bool DrawerInterfaceIsReady = false;
        
        public FormDrawer()
        {
            InitializeComponent();
            mainBitMap = new Bitmap(Graph.Width, Graph.Height);
        }

        public static void SetGlobalShift(int commandcount)
        {  
            lock (threadLock)
            {
                shift += commandcount;                
            }
                         
        }
        
     
        
        private void FormDrawer_Load(object sender, EventArgs e) //клетчатое поле
        {
            Graph.Image = draw.DrawField(graph, mainBitMap, Graph);
            Graph.Refresh();
            Graph.Image = draw.DrawMPLine(graph, mainBitMap, Graph, MPCount);
            Graph.Refresh();
            DrawerInterfaceIsReady = true;
        }

        public static void BrickUP(COMMAND_TYPE_COLOR ctype, bool hatch = false)
        {
            int pos;
            lock (threadLock)
            {               
                Console.WriteLine("{0} thread using BrickUP()", Thread.CurrentThread.Name);
                if (!casesqure.ContainsKey(Thread.CurrentThread.Name))
                {
                    casesqure.Add(Thread.CurrentThread.Name, 0);
                    if (!casesline.ContainsKey(Thread.CurrentThread.Name)) casesline.Add(Thread.CurrentThread.Name, linepos += 1);
                }
                pos = casesqure[Thread.CurrentThread.Name] += squrepos;               
                if (ctype == COMMAND_TYPE_COLOR.NON_CACHE || ctype == COMMAND_TYPE_COLOR.NON_CACHE_CTRL)
                {
                    hatch = true;                   
                }                
                if (ctype != COMMAND_TYPE_COLOR.CACHE && ctype != COMMAND_TYPE_COLOR.CACHE_CTRL)
                {
                    if (monoline == 0) monoline = pos;
                    Graph.Image = draw.DrawBrickUp(graph, mainBitMap, monoline++, casesline[Thread.CurrentThread.Name], ctype, hatch);
                }
                else
                {
                    Graph.Image = draw.DrawBrickUp(graph, mainBitMap, pos, casesline[Thread.CurrentThread.Name], ctype, hatch);
                }
                Graph.Invoke(action);
            }                      
        }

        public static void BrickDOWN(COMMAND_TYPE_COLOR ctype, bool hatch = false)
        {
            int pos;
            lock (threadLock)
            {
                Console.WriteLine("{0} thread using BrickDOWN()", Thread.CurrentThread.Name);
                if (!casesqure.ContainsKey(Thread.CurrentThread.Name))
                {
                    casesqure.Add(Thread.CurrentThread.Name, 0);
                    if (!casesline.ContainsKey(Thread.CurrentThread.Name)) casesline.Add(Thread.CurrentThread.Name, linepos += 1);
                }
                pos = casesqure[Thread.CurrentThread.Name] += squrepos;              
                if (ctype != COMMAND_TYPE_COLOR.CACHE) 
                {
                    hatch = true;
                    if (monoline == 0) monoline = pos;
                    Graph.Image = draw.DrawBrickDown(graph, mainBitMap, monoline++, casesline[Thread.CurrentThread.Name], ctype, hatch);
                }
                else
                {
                    Graph.Image = draw.DrawBrickDown(graph, mainBitMap, pos, casesline[Thread.CurrentThread.Name], ctype, hatch);
                }
                          
                Graph.Invoke(action);
            }             
        }

        public static void BrickUP(int pos, COMMAND_TYPE_COLOR ctype, bool hatch = false)
        {
            lock (threadLock)
            {
                Console.WriteLine("{0} thread using BrickDOWN(overload)", Thread.CurrentThread.Name);
                if (!casesqure.ContainsKey(Thread.CurrentThread.Name))
                {
                    casesqure.Add(Thread.CurrentThread.Name, 0);
                    if (!casesline.ContainsKey(Thread.CurrentThread.Name)) casesline.Add(Thread.CurrentThread.Name, linepos += 1);
                }
                if (ctype != COMMAND_TYPE_COLOR.CACHE)
                {
                    hatch = true;
                    Graph.Image = draw.DrawBrickUp(graph, mainBitMap, casesqure[Thread.CurrentThread.Name] + (pos + shift), casesline[Thread.CurrentThread.Name], ctype, hatch);
                }
                else Graph.Image = draw.DrawBrickUp(graph, mainBitMap, casesqure[Thread.CurrentThread.Name] + pos, casesline[Thread.CurrentThread.Name], ctype, hatch);
                Graph.Invoke(action);
            }
        }

        public static void BrickDOWN(int pos, COMMAND_TYPE_COLOR ctype, bool hatch = false)
        {
            lock (threadLock)
            {
                Console.WriteLine("{0} thread using BrickDOWN(overload)", Thread.CurrentThread.Name);
                if (!casesqure.ContainsKey(Thread.CurrentThread.Name))
                {
                    casesqure.Add(Thread.CurrentThread.Name, 0);
                    if (!casesline.ContainsKey(Thread.CurrentThread.Name)) casesline.Add(Thread.CurrentThread.Name, linepos += 1);
                }
                if (ctype != COMMAND_TYPE_COLOR.CACHE)
                {
                    hatch = true;
                    Graph.Image = draw.DrawBrickDown(graph, mainBitMap, casesqure[Thread.CurrentThread.Name] + (pos + shift), casesline[Thread.CurrentThread.Name], ctype, hatch);
                }
                else Graph.Image = draw.DrawBrickDown(graph, mainBitMap, casesqure[Thread.CurrentThread.Name] + pos, casesline[Thread.CurrentThread.Name], ctype, hatch);
                Graph.Invoke(action);
            }
                
        }

        public static void BricksUP(int count, COMMAND_TYPE_COLOR ctype, bool enableSkipping = false, bool hatch = false)
        {
            lock (threadLock)
            {
                for (int bricks = 1, pos = squrepos; bricks <= count; bricks++, pos++)
                {
                    BrickUP(pos, ctype);
                }
                if (enableSkipping)
                {
                    SetGlobalShift(count);
                }
            }
        }

        public static void BricksDOWN(int count, COMMAND_TYPE_COLOR ctype, bool hatch = false)
        {
            lock (threadLock)
            {
                for (int bricks = 1, pos = squrepos; bricks <= count; bricks++, pos++)
                {
                    BrickDOWN(pos, ctype);

                }
            }  
        }
        public void SetControllers(int num)
        {
            for (int i = 1; i <= num; i++)
            {

            }
        }
        public int MonoLinePosition
        {
            get
            {
                return monoline;
            }
            set
            {
                monoline = value;
            }
        }
        public void SetMonoLinePos(COMMAND_TYPE_COLOR ctype)
        {
            switch (ctype)
            {
                case COMMAND_TYPE_COLOR.CACHE:
                    break;
                case COMMAND_TYPE_COLOR.CACHE_CTRL:
                    monoline++;
                    break;
                case COMMAND_TYPE_COLOR.NON_CACHE:
                    monoline++;
                    break;
                case COMMAND_TYPE_COLOR.NON_CACHE_CTRL:
                    monoline++;
                    break;
            }

        }
    }  
}
