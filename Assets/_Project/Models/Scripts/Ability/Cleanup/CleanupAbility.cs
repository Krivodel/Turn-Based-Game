namespace TurnBasedGame
{
    public class CleanupAbility : Ability
    {
        public override AbilityData BaseData => Data;
        public CleanupAbilityData Data { get; } = new();

        public CleanupAbility(Unit ownerUnit)
            : base(ownerUnit)
        {

        }

        protected override void OnUse()
        {
            OwnerUnit.StatusEffectsApplicator.RemoveAllByType<FireStatusEffect>();
        }
    }
}
