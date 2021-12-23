using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r.io.model.Constants
{
    //TODO: Move to config file
    static class Round
    {
        private static readonly int Second = 1000;

        public static readonly long Duration = 100 * Second;
        public static readonly long PauseBeforeNext = 10 * Second;
    }
}
