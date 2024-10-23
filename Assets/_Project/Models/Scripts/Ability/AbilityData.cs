namespace TurnBasedGame
{
    public abstract class AbilityData
    {
        public abstract string Id { get; }
        public virtual int Duration { get; } = 0;
        public virtual int Cooldown { get; } = 0;
    }
}
