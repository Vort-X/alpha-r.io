namespace r.io.model.Services.Abstract
{
    public delegate void RoundStartedEvent();

    public interface GameLoopManager
    {
        GameService gameService { get; }
        PlayerService playerService { get; }

        event RoundStartedEvent RoundStarted;

        void Start();
    }
}
