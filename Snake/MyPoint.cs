using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class MyPoint
    {
        public int X;
        public int Y;

        public MyPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public MyPoint() : this(0, 0)
        {

        }
    }
}
