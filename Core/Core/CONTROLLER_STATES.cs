using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum CONTROLLER_STATES : byte
    {
        STARTING = 0,
        WAITING = 2,
        BUSY = 4,
        INTERRUPT = 8,
        END = 16,
        EXECUTED = 32, 
        READY = 64
    }

    public static class CoreExtensions
    {
        public static string ToString(this CONTROLLER_STATES state)
        {
            switch (state)
            {
                case CONTROLLER_STATES.BUSY:
                    return "BUSY";
                case CONTROLLER_STATES.END:
                    return "END";
                case CONTROLLER_STATES.EXECUTED:
                    return "EXECUTED";
                case CONTROLLER_STATES.INTERRUPT:
                    return "INTERRUPT";
                case CONTROLLER_STATES.STARTING:
                    return "STARTING";
                case CONTROLLER_STATES.WAITING:
                    return "WAITING";
                case CONTROLLER_STATES.READY:
                    return "READY";
                default:
                    return "NOT SUPPORTED";

            }
        }
    }
}
