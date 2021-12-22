using r.io.model.GameEntities;
using r.io.model.Services.Abstract;
using System;
using System.Timers;

namespace r.io.model.Services
{
    public class GameLoopManagerImpl : GameLoopManager
    {
        private readonly GameFactory factory;
        public GameService gameService { get; private set; }
        public PlayerService playerService { get; private set; }

        public GameLoopManagerImpl(GameFactory factory)
        {
            this.factory = factory;
        }

        public void StartNewGame()
        {
            Game game = factory.createGame();
            this.playerService = new PlayerServiceImpl(game);
            this.gameService = new GameServiceImpl(game, factory, playerService);
            game.timerFromRoundStart.Elapsed += RestartGameOnTimer;
        }
        public void RestartGameOnTimer(object sender, EventArgs e)
        {
            Timer resetGameTimer = new Timer(1000);
            resetGameTimer.Elapsed += EndResetingGame;
        }

        public void EndResetingGame(object sender, EventArgs e)
        {
            StartNewGame();
        }
    }
}
