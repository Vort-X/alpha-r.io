using System.Collections.Generic;

namespace r.io_model.GameEntities
{
    public class AreaPart
    {
        public int xLeft { get; internal set; }
        public int yTop { get; internal set; }
        public List<CircleGameObject> killableObjects { get; internal set; }

        public AreaPart(int xLeft, int yTop)
        {
            this.xLeft = xLeft;
            this.yTop = yTop;
            this.killableObjects = new List<CircleGameObject>();
        }
    }
}
