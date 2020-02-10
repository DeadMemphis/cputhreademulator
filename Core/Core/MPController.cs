using System;
using System.Threading;
using Drawer;

namespace Core // todo
{
   
    public class MPController : BaseController
    {
        public CacheController own_cache;
        private Thread cache_thread;

        public MPController(CONTROLLER_MODE mode) : base(CONTROLLER_TYPE.MPController, mode )
        {
            own_cache = new CacheController(mode);
            cache_thread = new Thread(own_cache.Simulator);
            own_cache.Name = own_cache.Name = base.Name + "[CC]";
            own_cache.CachingEvent += OnExecuted;
            cache_thread.Start();
            //CCThread = new Thread(own_cache.OnState) { IsBackground = true };
            //CCThread.Name = own_cache.Name = base.Name + "[CC]";
            //CCThread.Start();
        }

        public override void Execute()
        {
            Console.WriteLine("Call override Execute() in MPController.");
            switch (base.currient.TYPE)
            {
                case COMMAND_TYPE.CACHE:
                    base.Decode();
                    base.Execute();
                    base.Remove();
                    base.state = CONTROLLER_STATE.READY;
                    break;
                case COMMAND_TYPE.CACHE_CTRL:
                    base.Decode();
                    if (!SystemBus.IsBusy())
                    {
                        SystemBus.TakeBus();
                        Console.WriteLine(Name + " take SystemBus.");
                        base.Execute();
                        SystemBus.FreeBus();
                        Console.WriteLine(Name + " free SystemBus.");
                        base.Remove();
                        base.state = CONTROLLER_STATE.READY;
                    }
                    else
                    {
                        base.OnSetRequest();
                        //base.currient.DEFERRED = true;
                        Console.WriteLine(Name + " go to INTERRUPT case.");
                        base.state = CONTROLLER_STATE.INTERRUPT;
                    }
                    break;
                case COMMAND_TYPE.NON_CACHE:
                    own_cache.Caching(base.currient);
                    if (own_cache.state == CONTROLLER_STATE.WAITING)
                    {
                        cache_thread.Interrupt();
                        Thread.Sleep(100);
                    }
                    //_own_cache.Simulator();
                    if (own_cache.state == CONTROLLER_STATE.EXECUTED)
                    {
                        Console.WriteLine("Get execution in " + Name);
                        base.Decode();
                        base.Execute();
                        base.currient.COMPLITE = true;
                        own_cache.state = CONTROLLER_STATE.END;
                        base.Remove();
                        base.state = CONTROLLER_STATE.READY;
                    }
                    break;
                case COMMAND_TYPE.NON_CACHE_CTRL:
                    own_cache.Caching(base.currient);
                    if (own_cache.state == CONTROLLER_STATE.WAITING) cache_thread.Interrupt();
                    //_own_cache.Simulator();
                    if (own_cache.state == CONTROLLER_STATE.EXECUTED)
                    {
                        Console.WriteLine("Get execution in " + Name);
                        base.Decode();
                        base.currient.COMPLITE = true;
                        base.Remove();
                        base.state = CONTROLLER_STATE.READY;
                    }
                   
                    break;
            }
        }

        public override void Simulator()
        {
            Console.WriteLine("START POINT");
            base.state = CONTROLLER_STATE.STARTING;
            base.FeatTask();
            base.state = CONTROLLER_STATE.READY;
            base.Simulator();

        }

        public void OnExecuted(object sender, COMMAND cmnd)
        {
            base.Decode();
            base.currient.COMPLITE = true;
        }
    }
}
