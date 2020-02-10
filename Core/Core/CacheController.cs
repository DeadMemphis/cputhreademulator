using System;
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
            caching = cache; 
        }

        public override void Execute()
        {
            Console.WriteLine("Call override Execute() in CCController.");
            switch (caching.TYPE)
            {
                case COMMAND_TYPE.NON_CACHE:
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
        #endregion

        #region EventCallBack
        public override void OnState()
        {
            
        }
        
        public void OnCahed()
        {
            CachingEvent?.Invoke(this, caching); 
        }
        #endregion
    }
}
