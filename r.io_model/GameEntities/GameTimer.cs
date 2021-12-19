using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace r.io_model.GameEntities
{
    public class GameTimer
    {
        public int RoundsUntilFinish { get; internal set; }
        public Timer RoundTimer { get; internal set; }
        
        public event EventHandler RoundEnds;

        public event EventHandler GameEnd;

        public GameTimer(int roundsUntilFinish, Timer roundTimer)
        {
            RoundsUntilFinish = roundsUntilFinish;
            RoundTimer = roundTimer;
        }
    }
}
