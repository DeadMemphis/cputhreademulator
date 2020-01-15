using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    enum COMMAND_TYPE : byte
    {
        CACHE = 2,
        CACHE_OBJCTRL = 4,
        NON_CACHE = 8,
        NON_CACHE_OBJCTRL = 16
    }
}
