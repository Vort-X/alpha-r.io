using r.io_model.GameEntities;
using System.Collections.Generic;

namespace r.io_model.Services.Abstract
{
    public interface GameService
    {
        List<PlayerCircle> getTopPlayers(int topAmount);
        void RegisterPlayer(string name);

    }
}
