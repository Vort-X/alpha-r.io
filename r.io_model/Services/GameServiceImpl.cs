﻿using r.io_model.GameEntities;
using r.io_model.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace r.io_model.Services
{
    public class GameServiceImpl : GameService
    {
        public Game game { get; internal set; }

        private readonly PlayerService playerService;
        private readonly GameFactory factory;

        public GameServiceImpl(Game game, GameFactory factory, PlayerService playerService)
        {
            this.game = game;
            this.factory = factory;
            this.playerService = playerService;
        }

        public List<PlayerCircle> getTopPlayers(int topAmount)
        {
            return game.gameArea.parts.SelectMany(i => i.killableObjects)
                             .Where(i => i is PlayerCircle)
                             .Cast<PlayerCircle>()
                             .OrderByDescending(i => i.radius)
                             .Take(topAmount)
                             .ToList();
        }

        public void RegisterPlayer(string name)
        {
            PlayerCircle player = factory.createPlayer(game, name);
            playerService.getAreaPart(player.x, player.y).killableObjects.Add(player);
        }
    }
}
