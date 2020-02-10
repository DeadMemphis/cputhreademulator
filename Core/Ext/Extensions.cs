using System;
using System.Threading;

namespace Core
{
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

        public static string ToString(this COMMAND_TYPE value)
        {
            string _type = null;
            switch (value)
            {
                case COMMAND_TYPE.CACHE:
                    _type = "CACHE";
                    break;
                case COMMAND_TYPE.CACHE_CTRL:
                    _type = "CACHE OBJECT CONTROL";
                    break;
                case COMMAND_TYPE.NON_CACHE:
                    _type = "NON CACHE";
                    break;
                case COMMAND_TYPE.NON_CACHE_CTRL:
                    _type = "NON CACHE OBJECT CONTROL";
                    break;
                default:
                    _type = "NOT SUPPORTED { value : " + value + " }";
                    break;
            }
            return _type;
        }

        public static void WAKETHEFUCKUPSAMURAI(this Thread thrd)
        {
            thrd.Interrupt();
            Console.WriteLine("Thread " + thrd.Name + " interrupted.");
        }

    }
}
