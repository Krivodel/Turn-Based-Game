namespace TurnBasedGame
{
    public class CleanupAbilityData : AbilityData
    {
        public override string Id { get; } = "cleanup";
        public override int Duration { get; } = 0;
        public override int Cooldown { get; } = 5;
    }
}
