using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Snake
{
    class FoodGenerator
    {
        public static List<MyPoint> GetAllCells()
        {
            List<MyPoint> allCells = new List<MyPoint>();
            for (int i = 0; i < 400; i+=20)
            {
                for (int j = 20; j < 400; j += 20)
                {
                    allCells.Add(new MyPoint(i, j));
                }
                    
            }                                        
            return allCells;
        }

        static List<MyPoint> GetFreeCells(List<MyPoint> allCells, List<MyPoint> snakeBody, List<MyPoint> wall)
        {          
            return allCells.
                Except(snakeBody, new PointComparer())
                .Except(wall, new PointComparer())               
                .ToList();
        }

        public static MyPoint GetFreeCell(List<MyPoint> allCells, Snake snake, Wall wall)
        {
            Random rnd = new Random();
            var freeCells = GetFreeCells(allCells, snake.Body, wall.Blocks);                   
            return freeCells[rnd.Next(0, freeCells.Count - 1)];           
        }
       
    }
}
