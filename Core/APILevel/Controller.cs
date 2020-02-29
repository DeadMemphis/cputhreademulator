using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core;


namespace Core.APILevel
{
    public delegate void ExecuteTask(object sender, COMMAND Task);
    
    public class Controller //wocher
    {
        static List<Thread> ThreadPool = new List<Thread>();
        static List<MPController> MPPool = new List<MPController>();
        static Queue<COMMAND> CommandList = new Queue<COMMAND>();

        public event ExecuteTask ExecuteTaksEvent;

        private static bool ModulesIsReady = false;

        public void Init(short count)
        {
            for (int i = 0; i < count; i++)
            {
                MPController mp = new MPController(CONTROLLER_MODE.FIFO);
                Thread thr = new Thread(mp.Simulator)
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
            }
            CommandList.Enqueue(new COMMAND(1, COMMAND_TYPE.CACHE));
            CommandList.Enqueue(new COMMAND(1, COMMAND_TYPE.NON_CACHE));
            ModulesIsReady = true;
        }

        static void OnStatusChanged(object sender, CONTROLLER_STATE state)
        {
            MPController mp;
            if (sender is MPController && ModulesIsReady)
            {
                mp = sender as MPController;
                Console.WriteLine(mp.Name + " on changed state: " + state.ToString());
                if (CommandList.Count == 0) mp.state = CONTROLLER_STATE.END;
                else if (state == CONTROLLER_STATE.READY)
                {
                    mp.SetTask(CommandList.Dequeue());
                }               
            }
            else Console.WriteLine("wrong params");
        }

        static void OnRequest(object sender, COMMAND TASK)
        {
            if (sender is MPController && ModulesIsReady)
            {
                Console.WriteLine("Get Request from " + (sender as MPController).Name);
            }
            else Console.WriteLine("wrong params");
        }

        static void OnExecuted(object sender, COMMAND task)
        {
            if (sender is MPController)
            {
                Console.WriteLine("Executed task: " + task.TYPE.ToString() + " on " + (sender as MPController).Name);
            }
            else Console.WriteLine("wrong params");
        }
        
        public void OnExecutedTaks(COMMAND task)
        {
            ExecuteTaksEvent?.Invoke(this, task);
        }
    }
}
