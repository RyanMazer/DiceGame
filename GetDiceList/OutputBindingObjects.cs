using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    public class DiceList
    {
        public string DiceName { get; set; }
        public string DiceFace { get; set; }
    }

    public class Session
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
    }
}
