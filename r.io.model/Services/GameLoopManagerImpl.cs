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
        private CoolerTimer timer;

        //Invoke when round time expires
        //i.e.: game.timerFromRoundStart.Elapsed += (s, e) => RoundEnded?.Invoke();
        public event RoundStartedEvent RoundStarted;

        public GameService gameService { get; private set; }
        public PlayerService playerService { get; private set; }

        public GameLoopManagerImpl(GameFactory factory)
        {
            this.factory = factory;
        }

        public void Start()
        {
            //Method StartNewGame calls every Round.Duration + Round.PauseBeforeNext milliseconds
            //Delay before first execution - 1000 milliseconds
            timer = new(StartNewGame, new object(), 1000, Round.Duration);
        }

        private void StartNewGame(object state)
        {
            Game game = factory.createGame();
            this.playerService = new PlayerServiceImpl(game);
            this.gameService = new GameServiceImpl(game, factory, playerService);
            game.timerFromRoundStart.Interval = Round.Duration;
            game.timerFromRoundStart.AutoReset = false;
            game.timerFromRoundStart.Start();
            RoundStarted?.Invoke();
            Console.WriteLine("New Round Started");
        }
    }
}
