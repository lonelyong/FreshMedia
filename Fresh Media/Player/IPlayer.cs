namespace FreshMedia.Player
{
    interface IPlayer : IPlayerBase
    {
        string currentPositionString { get; }
        string currentURL { get; }
        bool IsInterimPlay { get; }
    }
}
