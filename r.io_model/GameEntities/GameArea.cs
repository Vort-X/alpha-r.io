using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io_model.GameEntities
{
    public class GameArea
    {
        internal int maxX { get; set; }
        internal int maxY { get; set; }
        internal int partSide { get; set; }

        public GameArea(int maxX, int maxY, int partSide)
        {
            this.maxX = maxX;
            this.maxY = maxY;
            this.partSide = partSide;
        }
    }
}
