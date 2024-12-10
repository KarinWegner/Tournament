namespace Services.Contracts.Services
{
    public interface IServiceManager
    {
        IGameService GameService { get; }
        ITournamentService TournamentService { get; }
    }
}
