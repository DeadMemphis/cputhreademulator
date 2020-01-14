using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SystemBus
    {
        private bool busy = false;
        private readonly object lockObj = new object();

        public bool IsBusy()
        {
            if (busy) return true;
            else return false;
        }

        public void TakeSystemBus()
        {
            lock (lockObj)
            {
                busy = true;
            }
        }
        public void FreeSystemBus()
        {
            lock (lockObj)
            {
                busy = false;
            }
        }
    }
}
