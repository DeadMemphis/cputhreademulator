using System;
using System.Collections.Generic;
using System.Threading;

namespace Core // todo
{
   
    public class MPController : Core.BaseController
    {     
        private CacheController own_cache = new CacheController();
        private Thread CCThread;      
       
        public MPController() : base(Core.CONTROLLER_TYPE.MPController)
        {

        }
        
        public void Init(string threadName)
        {
            SetCC(threadName);
        }

        public override void FeatTask()
        {
            TaskList.Enqueue(new TASK(2));
            TaskList.Enqueue(new TASK(1));
        }

        public override void Remove()
        {
            //TaskList.Remove(currient);
        }

        public override void Execute()
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

        public override void OnState()
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

        private void SetCC(string name)
        {
            CCThread = new Thread(own_cache.DoTask) { IsBackground = true };
            CCThread.Name = own_cache.Name = name;
            CCThread.Start();
        }
    }
}
