namespace TurnBasedGame
{
    public class AttackAbility : Ability
    {
        public override AbilityData BaseData => Data;
        public AttackAbilityData Data { get; } = new();

        public AttackAbility(Unit ownerUnit)
            : base(ownerUnit)
        {

        }

        protected override void OnUse()
        {
            var targetUnits = OwnerUnit.Battle.GetOpponentUnitsFor(OwnerUnit);
            DamageInfo damageInfo = new(Data.Damage);

            foreach (var targetUnit in targetUnits)
                targetUnit.ModifiableHealth.Damage(damageInfo);
        }
    }
}
