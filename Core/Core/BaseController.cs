using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Core
{
    public delegate void StatusChange(object sender, CONTROLLER_STATES state);
    public delegate void Request(object sender, int TASK);
    public delegate void Execute(object sender, COMMAND Task);
    public abstract class BaseController
    {
        public string Name;
        public CONTROLLER_STATES state;
        public readonly CONTROLLER_MODE mode;
        public readonly CONTROLLER_TYPE type;
        public event StatusChange ChangeEvent;
        public event Request RequestEvent;
        public event Execute ExecuteEvent;
        public Queue<COMMAND> TaskList = new Queue<COMMAND>();
        public COMMAND currient;

        public BaseController(CONTROLLER_TYPE Type, CONTROLLER_MODE Mode)
        {
            type = Type;
            mode = Mode;
        }

        public virtual void SetTask(COMMAND task)
        {
            currient = task;
            state = CONTROLLER_STATES.BUSY;
        }

        public virtual void FeatTask()
        {
            TaskList.Enqueue(new COMMAND(2));
            TaskList.Enqueue(new COMMAND(1));
        }

        public virtual void Remove()
        {
            //TaskList.Remove(currient);
        }

        public virtual void Decode()
        {
            currient.DECODED = true; 
        }
        public virtual void Execute()
        {
            for (int i = 0; i < currient.DURATION; i++)
            {
                Console.WriteLine("Start MPController (" + Name + ").");
                Console.WriteLine("MPController  (" + Name + ") calling Drawer...");
                //Thread.Sleep(1000);
                Console.WriteLine("MPController  (" + Name + ") finished task. Task: " + currient.ToString());
                currient.COMPLITE = true; 
            }
            OnExecuted();
            //OnSetRequest();
        }

        public virtual void OnState()
        {
            state = CONTROLLER_STATES.STARTING;
            for (; state != CONTROLLER_STATES.END;)
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
            catch (ThreadInterruptedException)
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

    }
}
