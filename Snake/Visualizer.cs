using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Visualizer
    {
        static Pen snakePen = new Pen(Color.FromArgb(0x41, 0x8d, 0xcb), 1); //#FF7C45 //#418dcb
        static Pen foodPen = new Pen(Color.FromArgb(0xff, 0x7c, 0x45), 1); //#ff7c45
        static Pen statsFieldPen = new Pen(Color.FromArgb(0x54, 0x78, 0xB2), 1); //#5478B2
        static Pen wallPen = new Pen(Color.FromArgb(0xff, 0xdd, 0x73), 1);   //#976D23 #FFDD73

        static SolidBrush rectangleColor = new SolidBrush(Color.FromArgb(0x54, 0x79, 0xB2));
        static SolidBrush textColor = new SolidBrush(Color.FromArgb(0xc0, 0xda, 0xff)); //#C6E6F5 #C0DAFF      

        static FontFamily fontFamily = new FontFamily("Arial");
        static FontFamily upperTextFont = new FontFamily("Segoe UI");
        
        public static void VisualizeGameField(Graphics graphics, Size size, Snake snake, MyPoint food)
        {
            foreach (var item in snake.Body)
            {
                graphics.DrawRectangle(snakePen, new Rectangle(new Point(item.X, item.Y), new Size(16, 16)));              
            }

            // еда
            graphics.DrawRectangle(foodPen, new Rectangle(new Point(food.X, food.Y), new Size(16, 16)));
        }

        public static void VisualizeStatsField(Graphics graphics, int score, int bestScore, int width)
        {
            Rectangle rectangle = new Rectangle(new Point(0, 0), new Size(width, 18));
            graphics.DrawRectangle(statsFieldPen, rectangle);
            graphics.FillRectangle(rectangleColor, rectangle);
            graphics.DrawString($"Score: {score}  |  Best Score: {bestScore}",
                    new Font(upperTextFont, 10, FontStyle.Bold),
                    textColor,
                    new Point(5, 0));
        }

        public static void VisualizeGameOverInfo(Graphics graphics)
        {
            Rectangle rectangle = new Rectangle(new Point(100, 175), new Size(200, 50));
            graphics.DrawRectangle(statsFieldPen, rectangle);
            graphics.FillRectangle(rectangleColor, rectangle);
            graphics.DrawString($"Game Over",
                   new Font(upperTextFont, 26, FontStyle.Bold),
                   textColor,
                   new Point(102, 175));
        }

        public static void VisualizeWalls(Graphics graphics, Wall wall)
        {
            foreach (var item in wall.Blocks)
            {
                graphics.DrawRectangle(wallPen, new Rectangle(new Point(item.X, item.Y), new Size(16, 16)));
            }            
        }
    }
}
