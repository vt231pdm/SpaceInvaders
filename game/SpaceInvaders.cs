using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using gamelibrary;

namespace game
{
    public partial class SpaceInvaders : Form
    {
        private Label Level = new Label();
        private Game game;
        private Timer timer;
        private bool isPaused;
        private PictureBox playerPictureBox;
        private List<PictureBox> alienPictureBoxes;

        public SpaceInvaders()
        {
            InitializeComponent();
            this.ClientSize = new Size(450, 700); 
            game = new Game();

            timer = new Timer();
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
            timer.Start();

            this.DoubleBuffered = true;
            this.Paint += Form_Paint;
            this.KeyDown += GameControle_KeyDown;

            Level.Location = new Point(10, 10);
            Level.Size = new Size(60, 20);
            Level.ForeColor = Color.White;
            Level.Text = "Рівень: 1";
            Level.BackColor = Color.Transparent;
            this.Controls.Add(Level);

            playerPictureBox = new PictureBox();
            playerPictureBox.Size = new Size(game.Player.Width, game.Player.Height);
            playerPictureBox.Location = new Point(game.Player.X, game.Player.Y);
            playerPictureBox.BackColor = Color.DarkBlue;
            playerPictureBox.Image = Image.FromFile("C:\\Users\\Denis\\OneDrive\\Рабочий стол\\spaceship.png");
            playerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(playerPictureBox);

            alienPictureBoxes = new List<PictureBox>();
            foreach (var alien in game.Aliens)
            {
                var alienPictureBox = new PictureBox();
                alienPictureBox.Size = new Size(alien.Width, alien.Height);
                alienPictureBox.Location = new Point(alien.X, alien.Y);
                alienPictureBox.BackColor = Color.DarkBlue;
                alienPictureBox.Image = Image.FromFile("C:\\Users\\Denis\\OneDrive\\Рабочий стол\\alien_1.png");
                alienPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                alienPictureBoxes.Add(alienPictureBox);
                this.Controls.Add(alienPictureBox);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                game.Update();
                if (game.IsGameOver)
                {
                    timer.Stop();
                    MessageBox.Show("Гру завершено!");
                    this.Close();
                    Load load = new Load();
                    load.Show();
                }
                else
                {
                    Level.Text = $"Рівень: {game.NumberLevel}"; 
                    UpdateGameObjects();
                }
                this.Invalidate();
            }
        }

        private void GameControle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                game.Player.Move(-10);
            }
            else if (e.KeyCode == Keys.Right)
            {
                game.Player.Move(10);
            }
            else if (e.KeyCode == Keys.Space)
            {
                game.Bullets.Add(game.Player.Shoot(5));
            }
            else if (e.KeyCode == Keys.Escape)
            {
                PauseGame();
            }
        }

        private void PauseGame()
        {
            Pause();
            ShowPauseMessage();
            Resume();
        }

        private void Pause()
        {
            isPaused = true;
            timer.Stop();
        }

        private void ShowPauseMessage()
        {
            MessageBox.Show("Гра на паузі", "Пауза", MessageBoxButtons.OK);
        }

        private void Resume()
        {
            isPaused = false;
            timer.Start();
        }

        private void UpdateGameObjects()
        {
            playerPictureBox.Location = new Point(game.Player.X, game.Player.Y);

            for (int i = 0; i < game.Aliens.Count; i++)
            {
                var alien = game.Aliens[i];
                var alienPictureBox = alienPictureBoxes[i];
                if (alien.IsAlive)
                {
                    alienPictureBox.Visible = true;
                    alienPictureBox.Location = new Point(alien.X, alien.Y);
                }
                else
                {
                    alienPictureBox.Visible = false;
                }
            }
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var bullet in game.Bullets)
            {
                if (bullet.IsActive)
                {
                    g.FillRectangle(Brushes.Green, bullet.X, bullet.Y, bullet.Width, bullet.Height);
                }
            }

            foreach (var bullet in game.AlienBullets)
            {
                if (bullet.IsActive)
                {
                    g.FillRectangle(Brushes.Yellow, bullet.X, bullet.Y, bullet.Width, bullet.Height);
                }
            }

            foreach (var block in game.Blocks)
            {
                if (block.IsActive)
                {
                    g.FillRectangle(Brushes.Gray, block.X, block.Y, block.Width, block.Height);
                }
            }
        }
    }
}
