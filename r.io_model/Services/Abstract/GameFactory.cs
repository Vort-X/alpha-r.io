using r.io_model.GameEntities;

namespace r.io_model.Services.Abstract
{
    public interface GameFactory
    {
        Game createGame();
        PlayerCircle createPlayer(Game game, string name);
    }
}
