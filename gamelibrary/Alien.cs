namespace gamelibrary
{
    public class Alien : GameObject
    {
        public bool IsAlive { get; set; } = true;

        public Alien(int x, int y) : base(x, y, 20, 20) { }

        public override void Move()
        {
            x += width;
        }

        public void Move(int dx)
        {
            x += dx;
        }

        public void ChangeDirection()
        {
            y += height;
        }

        public Bullet Shoot(int speed)
        {
            return new Bullet(x + width / 2, y + height, 5, 10, speed);
        }
    }
}