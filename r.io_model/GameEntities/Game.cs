using System.Timers;

namespace r.io_model.GameEntities
{
    public class Game
    {
        public GameArea gameArea { get; internal set; }
        public Timer timerFromRoundStart { get; internal set; }

        public Game(GameArea gameArea, Timer roundTimer)
        {
            this.gameArea = gameArea;
            this.timerFromRoundStart = roundTimer;
        }
    }
}
