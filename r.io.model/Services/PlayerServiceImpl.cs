using Microsoft.CodeAnalysis;
using r.io.model.GameEntities;
using r.io.model.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace r.io.model.Services
{
    public class PlayerServiceImpl : PlayerService
    {
        private readonly Game game;
        
        public PlayerServiceImpl(Game game)
        {
            this.game = game;
        }

        public void Move(PlayerCircle player, double x, double y)
        {
            if (x == 0 && y == 0) return;

            var newCoords = GetNewPlayerCoords(player, x, y);
            double newX = newCoords.Item1;
            double newY = newCoords.Item2;
            
            if (newX > 0 && newX < game.gameArea.maxX && newY > 0 && newY < game.gameArea.maxY)
            {
                AreaPart partBeforeMove = getAreaPart(player.x, player.y);
                player.x = newX;
                player.y = newY;

                AreaPart newAreaPart = getAreaPart(player.x, player.y);

                if (!newAreaPart.Equals(partBeforeMove))
                {
                    changeArea(player, partBeforeMove, newAreaPart);
                }
            }
            
        }

        public void TryEat(CircleGameObject player)
        {
            game.gameArea.parts.SelectMany(i => i.killableObjects)
                               .Where(i => isEatable(player, i))
                               .ToList()
                               .ForEach(i => Eat(player, i));
        }

        public List<AreaPart> getGameAreasAround(double x, double y)
        {
            AreaPart part = getAreaPart(x, y);
            int sideRange = game.gameArea.side;
            int[] xCoords = { part.xLeft - sideRange, part.xLeft, part.xLeft + sideRange };
            int[] yCoords = { part.yTop - sideRange, part.yTop, part.yTop + sideRange };
            return game.gameArea.parts
                .Where(i => xCoords.Contains(i.xLeft) && yCoords.Contains(i.yTop))
                .ToList();
        }

        public AreaPart getAreaPart(double x, double y)
        {
            int sideRange = game.gameArea.side;
            return game.gameArea.parts.Find(i => (x >= i.xLeft && x < i.xLeft + sideRange) && (y >= i.yTop && y < i.yTop + sideRange));
        }

        public void Kill(CircleGameObject player)
        {
            getParts().Find(i => i.killableObjects.Contains(player)).killableObjects.Remove(player);
            player.isAlive = false;
        }

        private (double, double) GetNewPlayerCoords(PlayerCircle player, double x, double y)
        {
            double normalizedX = Normalize(x, y);
            double normalizedY = Normalize(y, x);
            return (player.x + normalizedX * player.velocity / player.radius, 
                player.y + normalizedY * player.velocity / player.radius);
        }

        private double Normalize(double toNorm, double secondCoord)
        {
            return toNorm / Math.Sqrt(toNorm * toNorm + secondCoord * secondCoord);
        }

        private bool isEatable(CircleGameObject player, CircleGameObject food)
        {
            double rangeToFood = (Math.Sqrt(Math.Pow(food.x - player.x, 2) + Math.Pow(food.y - player.y, 2)));
            return rangeToFood == 0 ? false : rangeToFood < player.radius;
        }

        private void changeArea(CircleGameObject player, AreaPart oldArea, AreaPart newArea)
        {
            oldArea.killableObjects.Remove(player);
            newArea.killableObjects.Add(player);
        }

        private void Eat(CircleGameObject killer, CircleGameObject food)
        {
            killer.radius += Math.Sqrt(food.radius) / 3;
            Kill(food);
        }

        private List<AreaPart> getParts()
        {
            return game.gameArea.parts;
        }
    }
}
