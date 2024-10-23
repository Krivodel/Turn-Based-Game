namespace TurnBasedGame
{
    public class BarrierStatusEffect : StatusEffect
    {
        public ModifiableHealth ModifiableHealth { get; }
        public DamageModifier DamageModifier { get; }

        public BarrierStatusEffect(ModifiableHealth modifiableHealth, DamageModifier damageModifier)
        {
            ModifiableHealth = modifiableHealth;
            DamageModifier = damageModifier;
        }

        protected override void OnApply()
        {
            ModifiableHealth.DamageModificator.Add(DamageModifier);
        }

        protected override void OnRemove()
        {
            ModifiableHealth.DamageModificator.Remove(DamageModifier);
        }
    }
}
