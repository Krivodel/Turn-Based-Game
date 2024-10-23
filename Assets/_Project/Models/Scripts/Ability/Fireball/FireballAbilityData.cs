namespace TurnBasedGame
{
    public class FireballAbilityData : AbilityData
    {
        public override string Id { get; } = "fireball";
        public override int Duration { get; } = 5;
        public override int Cooldown { get; } = 6;
        public int DamagePerUse { get; } = 5;
        public int DamagePerMove { get; } = 1;
    }
}
