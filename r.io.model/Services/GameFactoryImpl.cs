using r.io.model.GameEntities;
using r.io.model.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Timers;

namespace r.io.model.Services
{
    public class GameFactoryImpl : GameFactory
    {
        public Game createGame()
        {
            double roundDurationInSeconds = 100;
            int gameAreaSide = 1000;
            int areaPartSide = 100;
            double foodRadius = 2;
            int foodAmount = 40*10;

            Timer roundTimer = new Timer(100 * roundDurationInSeconds);

            List<AreaPart> areaParts = new List<AreaPart>();

            int areaPartAmount = gameAreaSide / areaPartSide;

            for (int y = 0; y < areaPartAmount; y++)
            {
                for (int x = 0; x < areaPartAmount; x++)
                {
                    areaParts.Add(new AreaPart(0 + x * areaPartSide, 0 + y * areaPartSide));
                }
            }

            for(int i = 0; i < foodAmount; i++)
            {
                CircleGameObject food = new FoodCircle(generateRandomCoords(gameAreaSide), generateRandomCoords(gameAreaSide), foodRadius);
                getAreaPart(food.x, food.y, areaParts, areaPartSide).killableObjects.Add(food);
            }

            GameArea gameArea = new(gameAreaSide, gameAreaSide, areaPartSide);
            gameArea.parts = areaParts;

            return new Game(gameArea, roundTimer);
        }

        public PlayerCircle createPlayer(Game game, string name)
        {
            int minimalRadius = 5;
            float velocity = 3;


            double x = generateRandomCoords(game.gameArea.maxX);
            double y = generateRandomCoords(game.gameArea.maxY);
            return new PlayerCircle(name, x, y, minimalRadius, velocity);
        }

        private double generateRandomCoords(int max)
        {
            return new Random().NextDouble()*max;
        }

        private AreaPart getAreaPart(double x, double y, List<AreaPart> areaParts, int sideRange)
        {
            return areaParts.Find(i => (x >= i.xLeft && x < i.xLeft + sideRange) && (y >= i.yTop && y < i.yTop + sideRange));
        }
    }
}
