using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum CONTROLLER_STATE : byte
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
        public static string ToString(this CONTROLLER_STATE state)
        {
            switch (state)
            {
                case CONTROLLER_STATE.BUSY:
                    return "BUSY";
                case CONTROLLER_STATE.END:
                    return "END";
                case CONTROLLER_STATE.EXECUTED:
                    return "EXECUTED";
                case CONTROLLER_STATE.INTERRUPT:
                    return "INTERRUPT";
                case CONTROLLER_STATE.STARTING:
                    return "STARTING";
                case CONTROLLER_STATE.WAITING:
                    return "WAITING";
                case CONTROLLER_STATE.READY:
                    return "READY";
                default:
                    return "NOT SUPPORTED";

            }
        }
    }
}
