using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class Snake
    {
        public List<MyPoint> Body { get; set; }

        public delegate void EatHandler();
        public delegate void CollisionHandler();

        public event EatHandler OnEat;
        public event CollisionHandler OnCollision;

        public Snake(int x, int y)
        {
            Body = new List<MyPoint>() { new MyPoint(x, y) };
        }

        public Snake() : this(0, 0)
        {
        }

        public void AddNexus()
        {           
            MyPoint nexus = new MyPoint
            {
                X = Body[Body.Count - 1].X,
                Y = Body[Body.Count - 1].Y
            };
            Body.Add(nexus);
        }

        public void Move(Direction direction, MyPoint food, int maxHeight, int maxWidth, Wall wall)
        {
            // Начиная с конца тела
            for (int i = Body.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    // Передвинуть голову относительно direction
                    DoStep(direction);

                    // Определить коллизию с едой (food)                   
                    HandleCollisionWithFood(food);

                    // Определить коллизию с собственным телом (свойство Body)                 
                    HandleCollisionWithBody();

                    // Определить коллизию со стенами (wall)                       
                    HandleCollisionWithWall(wall);

                    // Определить коллизию с границами поля (0, 20, maxWidth, maxHeight)                 
                    HandleCollisionWithBorders(maxHeight, maxWidth);
                }
                else
                {
                    // Движение хвоста
                    Body[i].X = Body[i - 1].X;
                    Body[i].Y = Body[i - 1].Y;
                }
            }
        }

        void DoStep(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    Body[0].X += 20;
                    break;
                case Direction.Left:
                    Body[0].X -= 20;
                    break;
                case Direction.Up:
                    Body[0].Y -= 20;
                    break;
                case Direction.Down:
                    Body[0].Y += 20;
                    break;
            }
        }

        void HandleCollisionWithFood(MyPoint food)
        {
            if (Body[0].X == food.X && Body[0].Y == food.Y)
            {
                OnEat();
            }
        }

        void HandleCollisionWithBody()
        {
            for (int j = 2; j < Body.Count; j++)
            {
                if (Body[0].X == Body[j].X && Body[0].Y == Body[j].Y)
                {
                    OnCollision();
                }
            }
        }

        void HandleCollisionWithWall(Wall wall)
        {
            for (int j = 0; j < wall.Blocks.Count; j++)
            {
                if (wall.Blocks[j].X == Body[0].X && wall.Blocks[j].Y == Body[0].Y)
                {
                    OnCollision();
                }                  
            }
        }

        void HandleCollisionWithBorders(int maxHeight, int maxWidth)
        {
            if (Body[0].X < 0 || Body[0].Y < 20
                        || Body[0].X >= maxWidth || Body[0].Y >= maxHeight)
            {
                OnCollision();
            }
        }

    }
}
