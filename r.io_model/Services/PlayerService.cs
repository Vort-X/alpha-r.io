using Microsoft.CodeAnalysis;
using r.io_model.GameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io_model.Services
{
    public class PlayerService : AbstractService
    {
        private readonly GameService gameService;
        private readonly CircleGameObject player;
        public PlayerService(List<CircleGameObject> killableObjects, GameArea gameArea, int Id) : base(killableObjects, gameArea)
        {
            player = killableObjects.Find(i => i.objectId == Id);
            killableObjects.Remove(player);
        }

        public void changeGameArea(int x, int y)
        {
            (gameArea, killableObjects) = gameService.updatePlayerVision(x, y);
        }

        public bool checkPositionChange()
        {
            return false;
        }

        public Optional<CircleGameObject> eatCheck()
        {
            Optional<CircleGameObject> circle = new Optional<CircleGameObject>(killableObjects.Find(i => isInsideCircle(i)));
            return circle;
        }

        public void Eat(CircleGameObject gameObject)
        {
            player.radius += gameObject.radius;
            gameService.Kill(gameObject);
        }

        private bool isInsideCircle(CircleGameObject circle)
        {
            return (Math.Sqrt(Math.Pow(circle.x - player.x, 2) + Math.Pow(circle.y - player.y, 2))) < player.radius;
        }
    }
}
