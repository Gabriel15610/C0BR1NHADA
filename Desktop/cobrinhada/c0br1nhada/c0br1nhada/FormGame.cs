using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace c0br1nhada
{
    public partial class FormGame : Form
    {
        private Timer gameTimer = new Timer();
        private List<Point> snake = new List<Point>();
        private Point food;
        private int size = 20;
        private string direction = "right";
        private Random rnd = new Random();
        private bool isPaused = false;

        private Panel pausePanel;
        private Button btnResume;
        private Button btnExitToMenu;

        public FormGame()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += FormGame_KeyDown;

            SetupPauseMenu();

            snake.Add(new Point(100, 100));
            GenerateFood();

            gameTimer.Interval = 100;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }

        private void SetupPauseMenu()
        {
            pausePanel = new Panel()
            {
                Size = new Size(300, 200),
                BackColor = Color.FromArgb(200, 0, 0, 0),
                Visible = false
            };
            CenterControl(pausePanel);

            btnResume = new Button()
            {
                Text = "Continuar",
                Size = new Size(250, 50),
                Location = new Point(25, 30),
                BackColor = Color.LimeGreen,
                Font = new Font("Consolas", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnResume.Click += (s, e) => ResumeGame();

            btnExitToMenu = new Button()
            {
                Text = "Sair para o menu",
                Size = new Size(250, 50),
                Location = new Point(25, 100),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Consolas", 14, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnExitToMenu.Click += (s, e) => ExitToMenu();

            pausePanel.Controls.Add(btnResume);
            pausePanel.Controls.Add(btnExitToMenu);
            this.Controls.Add(pausePanel);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (isPaused) return;

            MoveSnake();

            if (snake[0] == food)
            {
                snake.Add(new Point(-100, -100));
                GenerateFood();
            }

            for (int i = 1; i < snake.Count; i++)
                if (snake[0] == snake[i]) GameOver();

            if (snake[0].X < 0 || snake[0].X >= this.ClientSize.Width ||
                snake[0].Y < 0 || snake[0].Y >= this.ClientSize.Height)
                GameOver();

            Invalidate();
        }

        private void MoveSnake()
        {
            for (int i = snake.Count - 1; i > 0; i--)
                snake[i] = snake[i - 1];

            Point head = snake[0];
            switch (direction)
            {
                case "up": head.Y -= size; break;
                case "down": head.Y += size; break;
                case "left": head.X -= size; break;
                case "right": head.X += size; break;
            }
            snake[0] = head;
        }

        private void GenerateFood()
        {
            int maxX = this.ClientSize.Width / size;
            int maxY = this.ClientSize.Height / size;

            Point newFood;
            do
            {
                newFood = new Point(rnd.Next(0, maxX) * size, rnd.Next(0, maxY) * size);
            } while (snake.Contains(newFood));

            food = newFood;
        }

        private void GameOver()
        {
            gameTimer.Stop();
            MessageBox.Show($"🔥 Game Over! Pontuação: {snake.Count - 1} Clique OK para voltar ao menu inicial! Criado por PLASMA STUDIOS/TEAM no Visual Studio.", "Fim de Jogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ExitToMenu();
        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (isPaused)
            {
                if (e.KeyCode == Keys.Escape) ResumeGame();
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    if (direction != "down") direction = "up";
                    break;
                case Keys.S:
                case Keys.Down:
                    if (direction != "up") direction = "down";
                    break;
                case Keys.A:
                case Keys.Left:
                    if (direction != "right") direction = "left";
                    break;
                case Keys.D:
                case Keys.Right:
                    if (direction != "left") direction = "right";
                    break;
                case Keys.Escape:
                    PauseGame();
                    break;
            }
        }

        private void PauseGame()
        {
            isPaused = true;
            gameTimer.Stop();
            pausePanel.Visible = true;
            pausePanel.BringToFront();
            CenterControl(pausePanel);
        }

        private void ResumeGame()
        {
            isPaused = false;
            pausePanel.Visible = false;
            gameTimer.Start();
        }

        private void ExitToMenu()
        {
            this.Close(); // Fecha o FormGame e volta para o menu
        }

        private void CenterControl(Control ctrl)
        {
            ctrl.Left = (this.ClientSize.Width - ctrl.Width) / 2;
            ctrl.Top = (this.ClientSize.Height - ctrl.Height) / 2;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            SolidBrush headBrush = new SolidBrush(Color.Cyan);
            SolidBrush bodyBrush = new SolidBrush(Color.DeepSkyBlue);
            SolidBrush flameBrush = new SolidBrush(Color.OrangeRed);
            SolidBrush foodBrush = new SolidBrush(Color.Red);

            // Snake
            for (int i = 0; i < snake.Count; i++)
            {
                Brush brush = i == 0 ? headBrush : bodyBrush;
                e.Graphics.FillRectangle(brush, new Rectangle(snake[i].X, snake[i].Y, size, size));
                if (i != 0)
                    e.Graphics.DrawRectangle(new Pen(flameBrush, 2), new Rectangle(snake[i].X, snake[i].Y, size, size));
            }

            // Food
            e.Graphics.FillEllipse(foodBrush, new Rectangle(food.X, food.Y, size, size));
        }
    }
}
