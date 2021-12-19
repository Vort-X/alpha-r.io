namespace r.io_model.GameEntities
{
    public class GameArea
    {
        public  int maxX { get; internal set; }
        public int maxY { get; internal set; }
        public int partSide { get; internal set; }

        public GameArea(int maxX, int maxY, int partSide)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.partSide = partSide;
        }
    }
}
