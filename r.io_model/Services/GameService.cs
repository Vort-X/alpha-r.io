using r.io_model.GameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io_model.Services
{
    public class GameService : AbstractService
    {
        public GameService(List<CircleGameObject> killableObjects, GameArea gameArea) : base(killableObjects, gameArea)
        {
        }

        public void Kill(CircleGameObject gameObject)
        {
            killableObjects.Remove(gameObject);
        }

        public (GameArea, List<CircleGameObject>) updatePlayerVision(int x, int y)
        {
            return (new GameArea(x, y, y), new List<CircleGameObject>());
        }
    }
}
