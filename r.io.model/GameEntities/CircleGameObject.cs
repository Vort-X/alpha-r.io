namespace r.io.model.GameEntities
{
    public abstract class CircleGameObject
    {
        public double x { get; internal set; }
        public double y { get; internal set; }
        public double radius { get; internal set; }

        protected CircleGameObject(double x, double y, double radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
    }
}
