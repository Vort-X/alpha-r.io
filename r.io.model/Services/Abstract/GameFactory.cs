using r.io.model.GameEntities;

namespace r.io.model.Services.Abstract
{
    public interface GameFactory
    {
        Game createGame();
        PlayerCircle createPlayer(Game game, string name);
    }
}
