namespace TurnBasedGame
{
    public class AttackAbilityData : AbilityData
    {
        public override string Id => "attack";
        public int Damage { get; } = 8;
    }
}
