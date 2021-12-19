namespace r.io_model.GameEntities
{
    public class PlayerCircle : CircleGameObject
    {
        public string name { get; internal set; }
        public float velocity { get; internal set; }
        public PlayerCircle(string name, double x, double y, double radius, float velocity) : base(x, y, radius)
        {
            this.name = name;
            this.velocity = velocity;       
        }

    }
}
