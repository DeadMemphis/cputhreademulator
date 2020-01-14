using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Core
{
    public class CacheController
    {
        public int TaskCount = 2;
        public string Name;
        public CONTROLLER_STATES state;
        public void DoTask()
        {
            state = CONTROLLER_STATES.STARTING;
            for (;state != CONTROLLER_STATES.END;)
            {
                switch (state)
                {
                    case CONTROLLER_STATES.STARTING:
                        Console.WriteLine("(" + Name + "). STATE.STARTING ");
                        Console.WriteLine("(" + Name + "). go to STATE.WAITING... ");
                        state = CONTROLLER_STATES.WAITING;
                        break;
                    case CONTROLLER_STATES.WAITING:
                        Console.WriteLine("(" + Name + "). STATE.WAITING ");
                        Console.WriteLine("(" + Name + "). go to STATE.BUSY... ");
                        state = CONTROLLER_STATES.BUSY;
                        break;
                    case CONTROLLER_STATES.BUSY:
                        Console.WriteLine("Start MPController (" + Name + ").");
                        Console.WriteLine("MPController  (" + Name + ") calling Drawer...");
                        Thread.Sleep(1000);
                        Console.WriteLine("MPController  (" + Name + ") finished task. Task: " + TaskCount);
                        TaskCount++;
                        if (TaskCount == 2) state = CONTROLLER_STATES.END;
                        break;
                    case CONTROLLER_STATES.INTERRUPT:
                        Console.WriteLine("(" + Name + "). INTERRUPTED ");
                        //state = CONTROLLER_STATES.WAITING;
                        break;
                    case CONTROLLER_STATES.END:
                        Console.WriteLine("(" + Name + "). ENDED.");
                        break;
                }
            }
        }
    }
}
