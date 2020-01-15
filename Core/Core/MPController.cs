using System;
using System.Collections.Generic;
using System.Threading;

namespace Core // todo
{
   
    public class MPController : BaseController
    {     
        private CacheController own_cache = new CacheController(CONTROLLER_MODE.FIFO);
        private Thread CCThread;      
       
        public MPController(CONTROLLER_MODE mode) : base(CONTROLLER_TYPE.MPController, mode )
        {
            CCThread = new Thread(own_cache.OnState) { IsBackground = true };
            CCThread.Name = own_cache.Name = base.Name + "[CC]";
            CCThread.Start();
        }

        public override void FeatTask()
        {
            TaskList.Enqueue(new COMMAND(2));
            TaskList.Enqueue(new COMMAND());
        }

        public override void Remove()
        {
        
        }

        public override void Decode()
        {
            
        }

        public override void Execute()
        {
            
            
            
            OnExecuted();
            OnSetRequest();
        }

        public override void OnState()
        {
            Console.WriteLine("START POINT");

        }               
    }
}
