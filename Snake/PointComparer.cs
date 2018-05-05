using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class PointComparer : IEqualityComparer<MyPoint>
    {
        public bool Equals(MyPoint a, MyPoint b)
        {
            if (Object.ReferenceEquals(a, b)) return true;            
            return a != null && b != null && a.X.Equals(b.X) && a.Y.Equals(b.Y);
        }

        public int GetHashCode(MyPoint obj)
        {           
            var hashPointX =  obj.X.GetHashCode();           
            var hashPointY = obj.Y.GetHashCode(); 
            // хэш - xor точки X и Y
            return hashPointX ^ hashPointY;
        }
    }
}
