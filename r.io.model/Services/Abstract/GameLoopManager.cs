namespace r.io.model.Services.Abstract
{
    public delegate void RoundEndedEvent();

    public interface GameLoopManager
    {
        GameService gameService { get; }
        PlayerService playerService { get; }

        event RoundEndedEvent RoundEnded;

        void Start();
    }
}
