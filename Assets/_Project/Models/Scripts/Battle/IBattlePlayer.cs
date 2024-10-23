namespace TurnBasedGame
{
    public interface IBattlePlayer
    {
        int Id { get; }
        Unit Unit { get; }

        void Init(int id, Unit unit);
        bool CanMove();
        void MakeMove();
    }
}
