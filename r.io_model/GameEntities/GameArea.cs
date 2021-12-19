using System.Collections.Generic;

namespace r.io_model.GameEntities
{
    public class GameArea
    {
        public  int maxX { get; internal set; }
        public int maxY { get; internal set; }
        public int side { get; internal set; }
        public List<AreaPart> parts { get; internal set; }

        public GameArea(int maxX, int maxY)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.parts = new List<AreaPart>();
        }
    }
}
