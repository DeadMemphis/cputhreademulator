using System;
using System.Collections.Generic;
using System.Threading;
using Core;

namespace Threading_Test
{
    class Program
    {
        static List<Thread> ThreadPool = new List<Thread>();
        static List<ThreadMP> MPPool = new List<ThreadMP>(); // <busy>
        static void Main(string[] args)
        {
            Console.WriteLine("Core v2.0 .");
            InitPool(2);
            //for (int task = 0; task < 2; task++)
            //{                
            //    for (int mp = 0; mp < MPPool.Count; mp++)
            //    {                    
            //        if (MPPool[mp].state == CONTROLLER_STATES.WAITING)
            //        {
            //            Console.WriteLine("Thread " + mp + " set WAITING to mainThread");
            //            ThreadPool[mp].WAKETHEFUCKUPSAMURAI();
            //        }
                    
            //        if (MPPool[mp].state == CONTROLLER_STATES.END)
            //        {                        
            //            ThreadPool[mp].Abort();
            //            Console.WriteLine("Thread " + mp + " aborted from mainThread");
            //        }
            //    }
            //}
            //ShutDown();
            Console.ReadKey(); 
        }
       
        static void ShutDown()
        {
            bool end = false;
            for (int thr = 0; thr < ThreadPool.Count; thr++)
            {
                if (MPPool[thr].state == CONTROLLER_STATES.END)
                {
                    ThreadPool[thr].Abort();
                    Console.WriteLine("Thread " + thr + " aborted from mainThread");
                }
                else if (!ThreadPool[thr].IsAlive)
                {
                    end = true;
                }
            }
            if (!IsThreadsAlive()) Console.WriteLine("ShutDown.");
        }

        static bool IsThreadsAlive()
        {
            for (int thr = 0; thr < ThreadPool.Count; thr++)
            {
                if (ThreadPool[thr].IsAlive) return true;
            }
            return false;
        }

        static void InitPool(int count = 2)
        {
            for (int i = 0; i < count; i++)
            {
                ThreadMP mp = new ThreadMP();
                Thread thr = new Thread(mp.DoTask)
                {
                    IsBackground = true,
                    Name = "MP" + i.ToString()
                };
                mp.Name = thr.Name;
                mp.RequestEvent += OnRequest;
                mp.ChangeEvent += OnStatusChanged;
                mp.ExecuteEvent += OnExecuted;
                MPPool.Add(mp);
                ThreadPool.Add(thr);
                thr.Start();
                mp.FeatTask();
            }
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Optimized);
        }       
    }


    public static class Extentions
    {
        public static void WAKETHEFUCKUPSAMURAI(this Thread thrd)
        {
            thrd.Interrupt();
            Console.WriteLine("Thread " + thrd.Name + " interrupted.");
        }
    }
}
