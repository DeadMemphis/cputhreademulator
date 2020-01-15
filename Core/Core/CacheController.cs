using System;
using System.Collections.Generic;


namespace Core
{
    public delegate void Caching(object sender, COMMAND task);
    public class CacheController : BaseController 
    {
        public event Caching CachingEvent;
        public COMMAND caching;
        public CacheController(CONTROLLER_MODE mode) : base(CONTROLLER_TYPE.CacheController, mode)
        {

        }

        public void Caching(COMMAND cache)
        {
            caching = cache; 
        }

        public override void Execute()
        {
            
        }

        public override void OnState()
        {
            
        }
        public void OnCahed()
        {
            CachingEvent?.Invoke(this, caching); 
        }
    }
}
