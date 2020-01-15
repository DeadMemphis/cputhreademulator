using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public struct COMMAND
    {
        public int DURATION;
        public bool COMPLITE;
        public bool DEFFERED;
        public bool DECODED;
        public COMMAND(int duration = 1, bool complite = false, bool deffered = false, bool decoded = false) // default 1 tact 
        {
            DURATION = duration;
            COMPLITE = complite;
            DEFFERED = deffered;
            DECODED = decoded;
        }
    }
}
