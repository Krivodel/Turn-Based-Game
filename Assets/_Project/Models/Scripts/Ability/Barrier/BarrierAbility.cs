namespace TurnBasedGame
{
    public class BarrierAbility : Ability
    {
        public override AbilityData BaseData => Data;
        public BarrierAbilityData Data { get; } = new();

        private BarrierStatusEffect _barrierStatusEffect;

        public BarrierAbility(Unit ownerUnit)
            : base(ownerUnit)
        {

        }

        protected override void OnUse()
        {
            DamageModifier damageModifier = new(Data.BlockableDamageMode, Data.BlockableDamage);

            _barrierStatusEffect = new(OwnerUnit.ModifiableHealth, damageModifier);

            OwnerUnit.StatusEffectsApplicator.Apply(_barrierStatusEffect);
        }

        protected override void OnDurationCompleted()
        {
            _barrierStatusEffect.TryRemove();

            _barrierStatusEffect = null;
        }
    }
}
