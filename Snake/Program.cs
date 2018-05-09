using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;

namespace Snake
{
    class Program
    {
        static System.ComponentModel.IContainer components = null;
        static System.Windows.Forms.Timer timer;
        static Form form;
        static PictureBox gameCanvas;
        static Snake snake;
        static Direction direction;
        static Direction currentDirection;
        static MyPoint food;
        static Wall wall;
        static List<MyPoint> allCells;
        static Button button;
        static int score = 0;
        static int bestScore = 0;
        static bool isGameOver = false;
        static bool isPaused = false;

        [STAThread]
        static void Main(string[] args)
        {
            Initialize();
            Application.Run(form);
        }

        static void Initialize()
        {
            FormInitialize();
            GameCanvasInitialize();
            StopButtonInitialize();

            form.Controls.Add(button);
            form.Controls.Add(gameCanvas);

            components = new System.ComponentModel.Container();
        }

        static void FormInitialize()
        {
            form = new Form();
            form.Text = "Snake";
            form.ClientSize = new Size(400, 400);
            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.KeyPreview = true;
            form.Load += (sender, e) => Form1_Load((Form)sender, e);
            form.KeyDown += Form_KeyDown;
        }

        static void GameCanvasInitialize()
        {
            gameCanvas = new PictureBox();
            gameCanvas.Dock = DockStyle.Fill;
            gameCanvas.BackColor = Color.White;
            gameCanvas.Paint += new PaintEventHandler(pictureBox1_Paint);
        }

        static void StopButtonInitialize()
        {
            button = new Button();
            button.Location = new Point(370, 0);
            button.Height = 19;
            button.Width = 30;
            button.Text = "⏸";
            button.Font = new Font(new FontFamily("Segoe UI"), 12, FontStyle.Bold);
            button.ForeColor = Color.FromArgb(0xc0, 0xda, 0xff);
            button.BackColor = Color.FromArgb(0x54, 0x78, 0xB2);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.PreviewKeyDown += Button_PreviewKeyDown;
            button.Click += Button_Click;
        }

        private static void Button_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private static void Button_Click(object sender, EventArgs e)
        {
            // Поскольку одна и та же кнопка останавливает/запускает игру
            StopOrPlay();

            // Сохранение направления текущего движения
            currentDirection = direction;
        }

        private static void Form1_Load(Form sender, System.EventArgs e)
        {
            StartGame();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (direction == Direction.Up || direction == Direction.Down)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        direction = Direction.Left;
                        break;
                    case Keys.Right:
                        direction = Direction.Right;
                        break;
                    case Keys.Enter:
                        button.PerformClick();
                        break;
                }
            }
            else if (direction == Direction.Right || direction == Direction.Left)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        direction = Direction.Up;
                        break;
                    case Keys.Down:
                        direction = Direction.Down;
                        break;
                    case Keys.Enter:
                        button.PerformClick();
                        break;
                }
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            gameCanvas.Invalidate();           
            snake.Move(direction, food, gameCanvas.Size, wall);
        }

        private static void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var size = form.ClientSize;
            var background = new SolidBrush(Color.FromArgb(0x1e, 0x39, 0x64)); // #002d3c #1E3964
            graphics.FillRectangle(background, 0, 0, form.ClientSize.Width, form.ClientSize.Height);
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            Visualizer.VisualizeWalls(graphics, wall);
            Visualizer.VisualizeGameField(graphics, size, snake, food);
            Visualizer.VisualizeStatsField(graphics, score, bestScore, gameCanvas.Size.Width);
            if (isGameOver)
                Visualizer.VisualizeGameOverInfo(graphics);
        }

        static void GenerateFood()
        {
            food = FoodGenerator.GetFreeCell(allCells, snake, wall);
        }

        static void GenerateWalls()
        {
            wall = WallSerializer.GetWall();
        }

        static void Eat()
        {
            snake.AddNexus();
            GenerateFood();
            score += 10;
            if (score > bestScore)
                bestScore = score;
        }

        static void StartGame()
        {
            isGameOver = false;
            bestScore = ScoreManager.GetBestScoreFromBinary();
            snake = new Snake(40, 40);
            snake.OnEat += Eat;
            snake.OnCollision += Die;
            direction = Direction.Right;
            allCells = FoodGenerator.GetAllCells();
            GenerateWalls();
            GenerateFood();
            timer = new System.Windows.Forms.Timer(components);
            timer.Tick += Timer_Tick;
            timer.Interval = 200;
            timer.Enabled = true;
            timer.Start();
        }

        static void Die()
        {
            isGameOver = true;
            gameCanvas.Update();
            Thread.Sleep(1500);
            timer.Stop();
            timer.Enabled = false;
            ScoreManager.SetBestScore(bestScore);
            score = 0;
            StartGame();
        }

        static void StopOrPlay()
        {
            if (!isPaused)
            {
                timer.Stop();
                isPaused = true;
                button.Text = "▶";
            }
            else
            {
                // для восстановления направления движения, 
                // вне зависимости от нажатых клавиш во время паузы
                direction = currentDirection;

                timer.Start();
                isPaused = false;
                button.Text = "⏸";
            }
        }
    }
}
