namespace r.io_model.GameEntities
{
    public abstract class CircleGameObject
    {
        public  int objectId { get; internal set; }
        public int x { get; internal set; }
        public int y { get; internal set; }
        public double radius { get; internal set; }

        protected CircleGameObject(int objectId, int x, int y, double radius)
        {
            this.objectId = objectId;
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
    }
}
