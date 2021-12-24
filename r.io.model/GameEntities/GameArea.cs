using System.Collections.Generic;

namespace r.io.model.GameEntities
{
    public class GameArea
    {
        public  int maxX { get; internal set; }
        public int maxY { get; internal set; }
        public int side { get; internal set; }
        public List<AreaPart> parts { get; internal set; }

        public GameArea(int maxX, int maxY, int side)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.side = side;
            this.parts = new List<AreaPart>();
        }
    }
}
