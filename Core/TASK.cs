using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public struct TASK
    {
        public int DURATION;
        public bool COMPLITE;
        public TASK(int duration, bool complite = false)
        {
            DURATION = duration;
            COMPLITE = complite;
        }
    }
}
