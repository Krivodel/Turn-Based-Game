namespace TurnBasedGame
{
    public class ModifiableHealth
    {
        public Health Health { get; }
        public DamageModificator DamageModificator { get; }

        public ModifiableHealth(Health health, DamageModificator damageModificator)
        {
            Health = health;
            DamageModificator = damageModificator;
        }

        public void Regenerate(RegenerationInfo regenerationInfo)
        {
            Health.Regenerate(regenerationInfo);
        }

        public void Damage(DamageInfo damageInfo)
        {
            var modifiedDamageInfo = DamageModificator.ModifyDamage(damageInfo);

            Health.Damage(modifiedDamageInfo);
        }
    }
}
