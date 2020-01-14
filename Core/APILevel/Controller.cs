using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Core.APILevel
{
    public delegate void ExecuteTask(object sender, TASK Task);
    public class Controller
    {
        static List<Thread> ThreadPool = new List<Thread>();
        static List<MPController> MPPool = new List<MPController>();
        public event ExecuteTask ExecuteTaksEvent;
        public void Init(short count)
        {
            for (int i = 0; i < count; i++)
            {
                MPController mp = new MPController();
                Thread thr = new Thread(mp.OnState)
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
        static void OnStatusChanged(object sender, CONTROLLER_STATES state)
        {
            MPController mp;
            if (sender is MPController)
            {
                mp = sender as MPController;
                Console.WriteLine(mp.Name + " on changed state: " + state.ToString());
                if (state == CONTROLLER_STATES.READY) mp.SetTask(mp.TaskList.Dequeue());
                if (mp.TaskList.Count == 0) mp.state = CONTROLLER_STATES.END;
            }
            else Console.WriteLine("wrong params");
        }

        static void OnRequest(object sender, int TASK)
        {
            if (sender is MPController)
            {
                Console.WriteLine("Get Request from " + (sender as MPController).Name);
            }
            else Console.WriteLine("wrong params");
        }

        static void OnExecuted(object sender, TASK task)
        {
            if (sender is MPController)
            {
                Console.WriteLine("Executed task: " + task.ToString() + " on " + (sender as MPController).Name);
            }
            else Console.WriteLine("wrong params");
        }
        public void OnExecutedTaks(TASK task)
        {
            ExecuteTaksEvent?.Invoke(this, task);
        }
    }
}
