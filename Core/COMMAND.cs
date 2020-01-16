using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drawer;

namespace Core
{
    public struct COMMAND
    {
        public int DURATIONCOMMAND;
        public COMMAND_TYPE TYPE;
        public bool COMPLITE;
        public bool DECODED;
        public bool DEFERRED;
        public COMMAND(int duration, COMMAND_TYPE type, bool complite = false, bool decoded = false, bool deferred = false)
        {
            DURATIONCOMMAND = duration;
            TYPE = type;
            COMPLITE = complite;
            DECODED = decoded;
            DEFERRED = deferred;
        }
        public bool IsPriority // only for FIFO
        {
            get
            {
                if (this.TYPE == COMMAND_TYPE.CACHE || this.TYPE == COMMAND_TYPE.CACHE_CTRL) return true;
                else return false;
            }
        }
    }
}
