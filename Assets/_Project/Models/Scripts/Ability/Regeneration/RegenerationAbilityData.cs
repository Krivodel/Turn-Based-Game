namespace TurnBasedGame
{
    public class RegenerationAbilityData : AbilityData
    {
        public override string Id { get; } = "regeneration";
        public override int Duration { get; } = 3;
        public override int Cooldown { get; } = 5;
        public int HealthRegenerationPerMove { get; } = 2;
    }
}
