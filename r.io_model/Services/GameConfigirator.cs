using r.io_model.GameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io_model.Services
{
    public class GameConfigirator
    {
        public List<CircleGameObject> killableObjects { get; internal set; }
        public GameArea gameArea { get; internal set; }

        protected GameConfigirator(List<CircleGameObject> killableObjects, GameArea gameArea)
        {
            this.killableObjects = killableObjects;
            this.gameArea = gameArea;
        }
    }
}
