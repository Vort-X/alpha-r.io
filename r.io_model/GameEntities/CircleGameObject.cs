using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io_model.GameEntities
{
    public abstract class CircleGameObject
    {
        internal int objectId { get; set; }
        internal int x { get; set; }
        internal int y { get; set; }
        internal double radius { get; set; }

        protected CircleGameObject(int x, int y, double radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
    }
}
