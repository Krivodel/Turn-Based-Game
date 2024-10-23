namespace TurnBasedGame
{
    public class BarrierAbilityData : AbilityData
    {
        public override string Id { get; } = "barrier";
        public override int Duration { get; } = 2;
        public override int Cooldown { get; } = 4;
        public DamageModifierMode BlockableDamageMode { get; } = DamageModifierMode.Absolute;
        public int BlockableDamage { get; } = 5;
    }
}
