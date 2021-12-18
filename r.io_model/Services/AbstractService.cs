using r.io_model.GameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io_model.Services
{
    public abstract class AbstractService
    {
        protected List<CircleGameObject> killableObjects;
        protected GameArea gameArea;

        protected AbstractService(List<CircleGameObject> killableObjects, GameArea gameArea)
        {
            this.killableObjects = killableObjects;
            this.gameArea = gameArea;
        }
    }
}
