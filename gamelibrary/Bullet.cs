namespace gamelibrary
{
    public class Bullet : GameObject
    {
        public int Speed { get; private set; }
        public bool IsActive { get; set; } = true;

        public Bullet(int x, int y, int width, int height, int speed) : base(x, y, width, height)
        {
            this.Speed = speed;
        }

        public override void Move()
        {
            y += Speed;
            if (y < 0 || y > 700) IsActive = false;
        }
    }
}