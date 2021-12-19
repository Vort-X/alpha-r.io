using Microsoft.CodeAnalysis;
using r.io_model.GameEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io_model.Services
{
    public class PlayerServiceImpl : AbstractService
    {
        private readonly GameServiceImpl gameService;
        public PlayerServiceImpl(List<CircleGameObject> killableObjects, GameArea gameArea) : base(killableObjects, gameArea)
        {
        }

        public void changeGameArea(int x, int y)
        {
            (gameArea, killableObjects) = gameService.UpdatePlayerVision(x, y);
        }

        public void Move(int id, int x, int y)
        {

        }

        public Optional<CircleGameObject> eatCheck(CircleGameObject player)
        {
            Optional<CircleGameObject> circle = new Optional<CircleGameObject>(killableObjects.Find(i => isEatable(player, i)));
            return circle;
        }

        public void Eat(CircleGameObject killer, CircleGameObject food)
        {
            killer.radius += food.radius;
            gameService.Kill(food);
        }

        private bool isEatable(CircleGameObject player, CircleGameObject food)
        {
            return (Math.Sqrt(Math.Pow(food.x - player.x, 2) + Math.Pow(food.y - player.y, 2))) < player.radius;
        }
    }
}
