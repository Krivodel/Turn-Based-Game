namespace TurnBasedGame
{
    public class RegenerationAbility : Ability
    {
        public override AbilityData BaseData => Data;
        public RegenerationAbilityData Data { get; } = new();

        private RegenerationStatusEffect _regenerationStatusEffect;

        public RegenerationAbility(Unit ownerUnit)
            : base(ownerUnit)
        {

        }

        protected override void OnUse()
        {
            _regenerationStatusEffect = new(OwnerUnit.ModifiableHealth, Data.HealthRegenerationPerMove);

            OwnerUnit.StatusEffectsApplicator.Apply(_regenerationStatusEffect);
        }

        protected override void OnDurationCompleted()
        {
            _regenerationStatusEffect.TryRemove();

            _regenerationStatusEffect = null;
        }

        protected override void OnDurationTicked()
        {
            if (_regenerationStatusEffect.HasApplicator())
                _regenerationStatusEffect.Tick();
        }
    }
}
