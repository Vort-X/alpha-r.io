using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io_model.GameEntities
{
    public class PlayerCircle : CircleGameObject
    {
        internal float velocity { get; set; }
        public PlayerCircle(int x, int y, double radius, float velocity) : base(x, y, radius)
        {
            this.velocity = velocity;       
        }

    }
}
