using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    [Serializable]
    public class Wall
    {
        public List<MyPoint> Blocks { get; set; } = new List<MyPoint>();

        public Wall()
        {           
        }

        public Wall(List<MyPoint> blocks)
        {
            Blocks = blocks;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var item in Blocks)
            {
                sb.Append($"Block #{i}, X: {item.X}, Y: {item.Y}");
                i++;
            }
            return sb.ToString();
        }
    }
}
