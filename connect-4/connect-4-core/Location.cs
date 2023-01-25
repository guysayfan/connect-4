using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect_4_core
{
    public class Location
    {
        public uint Col { get; set;  }
        public uint Row { get; set;  }

        public Location(uint col, uint row)
        {
            if (Col > 6)
            {
                throw new Exception("Col out of bounds");
            }
            if (Row > 5)
            {
                throw new Exception("Row out of bounds");
            }
            Col = col;
            Row = row;
        }
    }
}
