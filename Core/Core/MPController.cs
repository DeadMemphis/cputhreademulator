using System;
using System.Collections.Generic;
using System.Threading;

namespace Core // todo
{
    public delegate void StatusChange(object sender, CONTROLLER_STATES state);
    public delegate void Request(object sender, int TASK);
    public delegate void Execute(object sender, TASK Task);
    public class MPController
    {
        public string Name;
        public CONTROLLER_STATES state;
        private CacheController own_cache = new CacheController();
        private Thread CCThread;
        public event StatusChange ChangeEvent;
        public event Request RequestEvent;
        public event Execute ExecuteEvent;

        public Queue<TASK> TaskList = new Queue<TASK>();
        public TASK currient;
        public void Init(string threadName)
        {
            SetCC(threadName);
        }

        public void SetTask(TASK task)
        {
            currient = task;
            state = CONTROLLER_STATES.BUSY;
        }

        public void FeatTask()
        {
            TaskList.Enqueue(new TASK(2));
            TaskList.Enqueue(new TASK(1));
        }

        public void Remove()
        {
            //TaskList.Remove(currient);
        }

        public void Execute()
        {
            for (int i = 0; i < currient.DURATION; i++)
            {
                Console.WriteLine("Start MPController (" + Name + ").");
                Console.WriteLine("MPController  (" + Name + ") calling Drawer...");
                Thread.Sleep(1000);
                Console.WriteLine("MPController  (" + Name + ") finished task. Task: " + currient.ToString());
            }
            OnExecuted();
            //OnSetRequest();
        }

        public void DoTask()
        {
            state = CONTROLLER_STATES.STARTING;
            for(;state != CONTROLLER_STATES.END;)
            {              
                switch (state)
                {
                    case CONTROLLER_STATES.STARTING:                       
                        state = CONTROLLER_STATES.READY;
                        break;
                    case CONTROLLER_STATES.WAITING:                       
                        //Wait();
                        break;
                    case CONTROLLER_STATES.READY:
                        break;
                    case CONTROLLER_STATES.BUSY:
                        Execute();
                        break;
                    case CONTROLLER_STATES.INTERRUPT:                        
                        //state = CONTROLLER_STATES.WAITING;
                        break;
                    case CONTROLLER_STATES.END:
                        break;
                }
                OnStatusChange();
            }
        }

        public void Wait()
        {
            try
            {
                Thread.Sleep(Timeout.Infinite);
            }
            catch(ThreadInterruptedException)
            {
                Console.WriteLine(Name + " woke the fuck up");
            }
        }

        public void OnStatusChange()
        {
            ChangeEvent?.Invoke(this, state);
        }

        public void OnSetRequest()
        {
            RequestEvent?.Invoke(this, currient.DURATION);
        }

        public void OnExecuted()
        {
            ExecuteEvent?.Invoke(this, currient);
            state = CONTROLLER_STATES.READY;
        }

        private void SetCC(string name)
        {
            CCThread = new Thread(own_cache.DoTask) { IsBackground = true };
            CCThread.Name = own_cache.Name = name;
            CCThread.Start();
        }
    }
}
