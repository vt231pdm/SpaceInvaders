namespace gamelibrary
{
    public abstract class GameObject
    {
        protected int x;
        protected int y;
        protected int width;
        protected int height;

        public int X { get => x; }
        public int Y { get => y; }
        public int Width { get => width; }
        public int Height { get => height; }

        public GameObject(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public abstract void Move();
    }
}