namespace r.io_model.GameEntities
{
    public class PlayerCircle : CircleGameObject
    {
        public float velocity { get; internal set; }
        public PlayerCircle(int ObjectId, int x, int y, double radius, float velocity) : base(ObjectId, x, y, radius)
        {
            this.velocity = velocity;       
        }

    }
}
