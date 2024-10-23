namespace TurnBasedGame
{
    public class FireStatusEffect : StatusEffect
    {
        public ModifiableHealth ModifiableHealth { get; }
        public int DamagePerTick { get; }

        public FireStatusEffect(ModifiableHealth modifiableHealth, int damagePerTick)
        {
            ModifiableHealth = modifiableHealth;
            DamagePerTick = damagePerTick;
        }

        public void Tick()
        {
            ModifiableHealth.Damage(new DamageInfo(DamagePerTick));
        }
    }
}
