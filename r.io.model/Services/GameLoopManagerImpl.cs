using r.io.model.Constants;
using r.io.model.GameEntities;
using r.io.model.Services.Abstract;
using System;
using System.Timers;
using CoolerTimer = System.Threading.Timer;

namespace r.io.model.Services
{
    public class GameLoopManagerImpl : GameLoopManager
    {
        private readonly GameFactory factory;

        //Invoke when round time expires
        //i.e.: game.timerFromRoundStart.Elapsed += (s, e) => RoundEnded?.Invoke();
        public event RoundEndedEvent RoundEnded;

        public GameService gameService { get; private set; }
        public PlayerService playerService { get; private set; }

        public GameLoopManagerImpl(GameFactory factory)
        {
            this.factory = factory;
        }

        public void Start()
        {
            //Method StartNewGame calls every Round.Duration + Round.PauseBeforeNext milliseconds
            //Delay before first execution - 3000 milliseconds
            CoolerTimer t = new(StartNewGame, new object(), 3000, Round.Duration + Round.PauseBeforeNext);
        }

        private void StartNewGame(object state)
        {
            Game game = factory.createGame();
            this.playerService = new PlayerServiceImpl(game);
            this.gameService = new GameServiceImpl(game, factory, playerService);
            game.timerFromRoundStart.Interval = Round.Duration;
            game.timerFromRoundStart.AutoReset = false;
            game.timerFromRoundStart.Elapsed += (s, e) => RoundEnded?.Invoke();
            game.timerFromRoundStart.Start();
        }
    }
}
