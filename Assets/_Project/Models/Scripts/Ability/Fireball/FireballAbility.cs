using System.Collections.Generic;

namespace TurnBasedGame
{
    public class FireballAbility : Ability
    {
        public override AbilityData BaseData => Data;
        public FireballAbilityData Data { get; } = new();

        private List<FireStatusEffect> _fireStatusEffects = new();

        public FireballAbility(Unit ownerUnit)
            : base(ownerUnit)
        {

        }

        protected override void OnUse()
        {
            var targetUnits = OwnerUnit.Battle.GetOpponentUnitsFor(OwnerUnit);

            foreach (var targetUnit in targetUnits)
            {
                FireStatusEffect fireStatusEffect = new(OwnerUnit.ModifiableHealth, Data.DamagePerMove);

                targetUnit.ModifiableHealth.Damage(new DamageInfo(Data.DamagePerUse));
                targetUnit.StatusEffectsApplicator.Apply(fireStatusEffect);

                _fireStatusEffects.Add(fireStatusEffect);
            }
        }

        protected override void OnDurationCompleted()
        {
            foreach (var fireStatusEffect in _fireStatusEffects)
                fireStatusEffect.TryRemove();

            _fireStatusEffects.Clear();
        }

        protected override void OnDurationTicked()
        {
            foreach (var fireStatusEffect in _fireStatusEffects)
            {
                if (fireStatusEffect.HasApplicator())
                    fireStatusEffect.Tick();
            }
        }
    }
}
