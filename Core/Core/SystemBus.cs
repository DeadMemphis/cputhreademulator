using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SystemBus
    {
        static object locker = new object();
        private static bool busy = false;
        public static bool IsBusy()
        {
            lock (locker)
            {
                if (busy) return true;
                else return false;
            }
        }
        public static void FreeBus()
        {
            lock (locker)
            {
                busy = false;
            }
        }
        public static void TakeBus()
        {
            lock (locker)
            {
                busy = true;
            }
        }
    }
}
