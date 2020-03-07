using System;
using System.Threading;
using Drawer; 


namespace Core
{
    
    #region EventCallBack
    public delegate void Caching(object sender, COMMAND task);
    #endregion

    public class CacheController : BaseController 
    {
        
        #region EventCallBack
        public event Caching CachingEvent;
        #endregion

        public COMMAND caching;
        public CacheController(CONTROLLER_MODE mode) : base(CONTROLLER_TYPE.CacheController, mode)
        {
            
        }

        #region BaseStructure
        public void Caching(COMMAND cache)
        {
            CoreExtensions.ConsoleLog(Thread.CurrentThread.Name, "Caching task: " + currient.TYPE);
            caching = cache;
            state = CONTROLLER_STATE.BUSY;
        }

        public override void Execute()
        {
            CoreExtensions.ConsoleLog(Thread.CurrentThread.Name, "Call override Execute().");
            switch (caching.TYPE)
            {
                case COMMAND_TYPE.NON_CACHE:
                    if (!SystemBus.IsBusy())
                    {
                        SystemBus.TakeBus();
                        CoreExtensions.ConsoleLog(Thread.CurrentThread.Name, "take SystemBus.");
                        //for (int i = 0; i < 6; i++)
                        //{
                        //    FormDrawer.BrickUP(caching.TYPE);
                        //}
                        SystemBus.FreeBus();
                        CoreExtensions.ConsoleLog(Thread.CurrentThread.Name, "free SystemBus.");                        
                        base.state = CONTROLLER_STATE.EXECUTED;
                        OnCahed();
                    }
                    else
                    {
                        base.OnSetRequest();
                        Console.WriteLine(Name + " go to INTERRUPT case.");
                        base.state = CONTROLLER_STATE.INTERRUPT;
                    }
                    break;
                case COMMAND_TYPE.NON_CACHE_CTRL:
                    if (!SystemBus.IsBusy())
                    {
                        SystemBus.TakeBus();
                        Console.WriteLine(Name + " take SystemBus.");
                        //for (int i = 0; i < 6; i++)
                        //{
                        //    FormDrawer.BrickUP(caching.TYPE);
                        //}
                        SystemBus.FreeBus();
                        Console.WriteLine(Name + " free SystemBus.");
                        base.state = CONTROLLER_STATE.EXECUTED;
                    }
                    else
                    {
                        base.OnSetRequest();
                        Console.WriteLine(Name + " go to INTERRUPT case.");
                        base.state = CONTROLLER_STATE.INTERRUPT;
                    }
                    break;
            }
        }

        public override void Simulator()
        {
            CoreExtensions.ConsoleLog(Thread.CurrentThread.Name, "START POINT");
            base.state = CONTROLLER_STATE.STARTING;
            //base.FeatTask();
            base.state = CONTROLLER_STATE.READY;
            base.Simulator();
        }
        #endregion

        #region EventCallBack
        
        public void OnCahed()
        {
            CachingEvent?.Invoke(this, caching);
            CoreExtensions.ConsoleLog(Thread.CurrentThread.Name, "[CC] cached: " + caching.TYPE);
        }
        #endregion
    }
}
