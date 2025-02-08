using System;
using System.Collections.Generic;
using System.Linq;

namespace gamelibrary
{
    public class Game
    {
        public List<Alien> Aliens { get; private set; }
        public Player Player { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        public List<Bullet> AlienBullets { get; private set; }
        public List<Block> Blocks { get; private set; }
        private bool movingRight;
        public bool IsGameOver { get; private set; }
        public int NumberLevel { get; private set; } = 1;
        private int alienSpeed = 0;
        private int bulletSpeed = 4;
        private int randomShot = 4;
        private const int FormWidth = 450;
        private const int FormHeight = 700;
        private Random rnd;

        public Game()
        {
            rnd = new Random();
            InitializeGame();
        }

        private void InitializeGame()
        {
            Aliens = new List<Alien>();
            Player = new Player(FormWidth / 2 - 10, 551);
            Bullets = new List<Bullet>();
            AlienBullets = new List<Bullet>();
            Blocks = new List<Block>();
            movingRight = true;
            IsGameOver = false;
            alienSpeed++;
            bulletSpeed++;
            randomShot++;

            InitializeAliens();
            InitializeBlocks();
        }

        private void InitializeAliens()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 9; j++) 
                {
                    Aliens.Add(new Alien(30 + j * 30, 80 + i * 30));
                }
            }
        }

        private void InitializeBlocks()
        {
            int blockY = 500;
            for (int i = 0; i < 3; i++)
            {
                Blocks.Add(new Block(50 + i * 150, blockY, 50, 20, 5));
            }
        }

        public void Update()
        {
            if (IsGameOver) return;

            MoveBullets();
            bool changeDirection = MoveAliens();
            if (changeDirection) ChangeAliensDirection();

            RandomAlienShoot();

            CheckCollisions();

            RemoveInactiveBullets();

            CheckLevelCompletion();
        }

        private void MoveBullets()
        {
            foreach (var bullet in Bullets) bullet.Move();
            foreach (var bullet in AlienBullets) bullet.Move();
        }

        private bool MoveAliens()
        {
            bool changeDirection = false;
            foreach (var alien in Aliens)
            {
                if (alien.IsAlive)
                {
                    alien.Move(alienSpeed * (movingRight ? 1 : -1));
                    if ((movingRight && alien.X > FormWidth - 20) || (!movingRight && alien.X < 0))
                    {
                        changeDirection = true;
                    }

                    if (CheckCollision(alien, Player))
                    {
                        IsGameOver = true;
                        return false;
                    }

                    foreach (var block in Blocks)
                    {
                        if (block.IsActive && CheckCollision(alien, block))
                        {
                            IsGameOver = true;
                            return false;
                        }
                    }
                }
            }
            return changeDirection;
        }

        private void ChangeAliensDirection()
        {
            foreach (var alien in Aliens) alien.ChangeDirection();
            movingRight = !movingRight;
        }

        private void RandomAlienShoot()
        {
            if (rnd.Next(100) < randomShot)
            {
                var shootingAlien = Aliens.Where(a => a.IsAlive).OrderBy(a => rnd.Next()).FirstOrDefault();
                if (shootingAlien != null)
                {
                    AlienBullets.Add(shootingAlien.Shoot(bulletSpeed));
                }
            }
        }

        private void CheckCollisions()
        {
            CheckBulletCollisionsWithAliens();
            CheckBulletCollisionsWithBlocks();
            CheckBulletCollisionsWithPlayer();
        }

        private void CheckBulletCollisionsWithAliens()
        {
            foreach (var alien in Aliens)
            {
                if (alien.IsAlive)
                {
                    foreach (var bullet in Bullets)
                    {
                        if (bullet.IsActive && CheckCollision(alien, bullet))
                        {
                            alien.IsAlive = false;
                            bullet.IsActive = false;
                        }
                    }
                }
            }
        }

        private void CheckBulletCollisionsWithBlocks()
        {
            foreach (var block in Blocks)
            {
                if (block.IsActive)
                {
                    foreach (var bullet in Bullets)
                    {
                        if (bullet.IsActive && CheckCollision(block, bullet))
                        {
                            block.TakeDamage(1);
                            bullet.IsActive = false;
                        }
                    }

                    foreach (var bullet in AlienBullets)
                    {
                        if (bullet.IsActive && CheckCollision(block, bullet))
                        {
                            block.TakeDamage(1);
                            bullet.IsActive = false;
                        }
                    }
                }
            }
        }

        private void CheckBulletCollisionsWithPlayer()
        {
            foreach (var bullet in AlienBullets)
            {
                if (bullet.IsActive && CheckCollision(Player, bullet))
                {
                    IsGameOver = true;
                    return;
                }
            }
        }

        private void RemoveInactiveBullets()
        {
            Bullets.RemoveAll(b => !b.IsActive || b.Y < 0);
            AlienBullets.RemoveAll(b => !b.IsActive || b.Y > FormHeight);
        }

        private void CheckLevelCompletion()
        {
            if (Aliens.All(a => !a.IsAlive))
            {
                NumberLevel++;
                InitializeGame();
            }
        }

        private static bool CheckCollision(GameObject obj1, GameObject obj2)
        {
            return obj1.X < obj2.X + obj2.Width && obj1.X + obj1.Width > obj2.X && obj1.Y < obj2.Y + obj2.Height && obj1.Y + obj1.Height > obj2.Y;
        }
    }
}