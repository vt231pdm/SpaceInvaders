namespace gamelibrary
{
    public class Player : GameObject
    {
        public Player(int x, int y) : base(x, y, 20, 20) { }

        public override void Move() { }

        public void Move(int dx)
        {
            x += dx;
            if (x < 0) x = 0;
            if (x > 430) x = 430;
        }

        public Bullet Shoot(int speed)
        {
            return new Bullet(x + width / 2, y, 5, 10, -speed);
        }
    }
}