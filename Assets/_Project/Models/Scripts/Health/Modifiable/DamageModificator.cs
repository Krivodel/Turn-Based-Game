using System.Collections.Generic;

namespace TurnBasedGame
{
    public class DamageModificator
    {
        private readonly List<DamageModifier> _modifiers = new();

        public IEnumerable<DamageModifier> GetModifiers()
        {
            return _modifiers;
        }

        public void Add(DamageModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void Remove(DamageModifier modifier)
        {
            _modifiers.Remove(modifier);
        }

        public DamageInfo ModifyDamage(DamageInfo damageInfo)
        {
            int modifiedDamage = damageInfo.Damage;

            foreach (var modifier in _modifiers)
                modifiedDamage = modifier.ModifyDamage(modifiedDamage);

            return new(modifiedDamage);
        }
    }
}
