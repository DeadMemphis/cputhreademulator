using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    interface ISimulator
    {
        void SetTask(COMMAND task);
        void Decode();
        void Execute();
        void Simulator();
        void OnState();
    }
}
