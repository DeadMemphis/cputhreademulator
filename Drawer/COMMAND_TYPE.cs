using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawer
{
    public enum COMMAND_TYPE 
    {
        CACHE = KnownColor.Maroon,
        NON_CACHE = KnownColor.Indigo,
        CACHE_CTRL = KnownColor.Salmon,
        NON_CACHE_CTRL = KnownColor.Teal
    
    }
    public static class COMMAND_TYPE_Extensions
    {
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
    }
}
