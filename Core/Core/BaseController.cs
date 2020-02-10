using System;
using System.Collections.Generic;
using System.Threading;

namespace Core
{
    
    #region EventCallBack
    public delegate void StatusChange(object sender, CONTROLLER_STATE state);
    public delegate void Request(object sender, COMMAND cmnd);
    public delegate void Execute(object sender, COMMAND cmnd);
    #endregion

    public abstract class BaseController
    {
        public string Name;
        public CONTROLLER_STATE state;
        public readonly CONTROLLER_MODE mode;
        public readonly CONTROLLER_TYPE type;

        #region EventCallBack
        public event StatusChange ChangeEvent;
        public event Request RequestEvent;
        public event Execute ExecuteEvent;
        #endregion

        public List<COMMAND> CommandList = new List<COMMAND>();
        public COMMAND currient;

        public BaseController(CONTROLLER_TYPE Type, CONTROLLER_MODE Mode)
        {
            type = Type;
            mode = Mode;
        }

        public virtual void SetTask(COMMAND task)
        {
            currient = task;
            state = CONTROLLER_STATE.BUSY;
        }

        public virtual void FeatTask()
        {
            CommandList.Add(new COMMAND(1, COMMAND_TYPE.CACHE));
            CommandList.Add(new COMMAND(1, COMMAND_TYPE.NON_CACHE));
            //CommandList.Add(new COMMAND(2, COMMAND_TYPE.CACHE_CTRL));
            ////tasks.Add(new TASK(2, COMMAND_TYPE.CACHE_CTRL, false, false));
            ////currient = new TASK(2, COMMAND_TYPE.CACHE_CTRL, false, false);
            ////tasks.Add(new TASK(2, COMMAND_TYPE.CACHE_CTRL, false, false));
            ////tasks.Add(new TASK(2, COMMAND_TYPE.NON_CACHE_CTRL, false, false));
            Console.WriteLine("Set tasks for " + Name);
        }

        public bool PickTask()
        {
            //if (currient.TYPE != 0)
            //{
            //    if (currient.COMPLITE)
            //    {
            //        currient = tasks[0];
            //        RemoveTask();
            //        _state = CONTROLLER_STATE.BUSY;
            //        return true;
            //    }
            //    else if (currient.DEFERRED)
            //    {
            //        currient = tasks[1];
            //        _state = CONTROLLER_STATE.BUSY;
            //        return true;
            //    }
            //}
            //else
            //{
            //    currient = tasks[0];
            //    RemoveTask();
            //    _state = CONTROLLER_STATE.BUSY;
            //    return true;
            //}
            return false;         
        }

        public virtual bool PickTask(COMMAND cmnd)
        {
            //if (currient.TYPE != 0)
            //{
            //    if (currient.COMPLITE)
            //    {
            //        currient = tasks[0];
            //        RemoveTask();
            //        return true;
            //    }
            //    else if (currient.DEFERRED)
            //    {
            //        currient = tasks[1];
            //        return true;
            //    }
            //}
            //else
            //{
            //    currient = tasks[0];
            //    RemoveTask();
            //    return true;
            //}
            //return false;
            currient = cmnd;
            state = CONTROLLER_STATE.BUSY;
            return true;
        }

        #region BaseStructure
        public virtual void Remove()
        {
            CommandList.Remove(currient);
        }
        public virtual void Decode()
        {
            //Console.WriteLine("Decoding");
            //if (!currient.DECODED)
            //{
            //    FormDrawer.BrickUP(currient.TYPE);
            //    currient.DECODED = true;
            //}
            currient.DECODED = true; 
        }
        public virtual void Execute()
        {
            //Console.WriteLine("Call base Execute()");
            //for (int i = 0; i < currient.DURATIONCOMMAND; i++)
            //{
            //    Console.WriteLine("Call BrickDOWN(), i = " + i);
            //    FormDrawer.BrickDOWN(currient.TYPE);
            //}
            currient.COMPLITE = true;
        }
        public virtual void Simulator()
        {
            for (; ;)
            {
                OnState();
                if (state == CONTROLLER_STATE.END)
                {
                    Console.WriteLine(Name + " END");
                    return;
                }
            }
        }
        public virtual void OnState()
        {
            switch (state)
            {
                case CONTROLLER_STATE.READY:
                    break;
                case CONTROLLER_STATE.STARTING:
                    Console.WriteLine("<" + Name + "> STARTING");
                    //if (PickTask())
                    //{
                    //    Console.WriteLine("Pick on " + Name + "task: " + currient.TYPE.ToString());
                    //    _state = CONTROLLER_STATE.BUSY;
                    //}
                    //else if (!currient.COMPLITE && (currient.TYPE == COMMAND_TYPE.NON_CACHE || currient.TYPE == COMMAND_TYPE.NON_CACHE_CTRL))
                    //    _state = CONTROLLER_STATE.BUSY;
                    //else Console.WriteLine("Currient task on " + Name + ": " + currient.TYPE.ToString());
                    break;
                case CONTROLLER_STATE.WAITING:
                    Wait();
                    state = CONTROLLER_STATE.READY;
                    break;
                case CONTROLLER_STATE.BUSY:
                    Execute();
                    if (CommandList.Count == 0)
                    {
                        Console.WriteLine(Name + " go break.");
                        state = CONTROLLER_STATE.END;
                    }
                    break;
                case CONTROLLER_STATE.INTERRUPT:
                    state = CONTROLLER_STATE.WAITING;
                    break;
                case CONTROLLER_STATE.END:
                    Console.WriteLine(Name + " state.END.");
                    break;
            }
            OnStatusChange();
        }
        #endregion

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

        #region EventCallBack     
        public void OnStatusChange()
        {
            ChangeEvent?.Invoke(this, state);
        }

        public void OnSetRequest()
        {
            RequestEvent?.Invoke(this, currient);
        }

        public void OnExecuted()
        {
            ExecuteEvent?.Invoke(this, currient);
            state = CONTROLLER_STATE.READY;
        }
        #endregion
    }
}
