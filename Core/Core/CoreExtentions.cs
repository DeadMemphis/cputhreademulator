using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class CoreExtentions
    {
        public static void WAKETHEFUCKUPSAMURAI(this Thread thrd)
        {
            thrd.Interrupt();
            Console.WriteLine("Thread " + thrd.Name + " interrupted.");
        }

        public static void Log(this Controller ctrl, string msg)
        {

        }
    }
}
