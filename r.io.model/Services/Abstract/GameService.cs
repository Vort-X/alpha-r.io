using r.io.model.GameEntities;
using System.Collections.Generic;

namespace r.io.model.Services.Abstract
{
    public interface GameService
    {
        List<PlayerCircle> getTopPlayers(int topAmount);
        PlayerCircle RegisterPlayer(string name);

    }
}
