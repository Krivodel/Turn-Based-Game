using System;
using System.Collections.Generic;
using System.Linq;

namespace TurnBasedGame
{
    public class AbilitiesUser
    {
        public event Action Changed;

        private readonly List<Ability> _abilities;

        public AbilitiesUser(IEnumerable<Ability> abilities)
        {
            if (abilities == null)
                throw new ArgumentNullException(nameof(abilities));

            _abilities = new(abilities);
        }

        public IReadOnlyList<Ability> GetAbilities()
        {
            return _abilities;
        }

        public bool CanUse(Ability ability)
        {
            return ability.CanUse();
        }

        public bool CanUse(string abilityId)
        {
            return CanUse(GetAbilityById(abilityId));
        }

        public void Use(Ability ability)
        {
            if (!CanUse(ability))
                throw new InvalidOperationException($"Cannot use ability '{ability.BaseData.Id}'.");

            ability.Use();

            OnChanged();
        }

        public void Use(string abilityId)
        {
            Use(GetAbilityById(abilityId));
        }

        public Ability GetAbilityById(string id)
        {
            return _abilities.First(v => v.BaseData.Id == id);
        }

        public Ability GetAbilityByIdOrNull(string id)
        {
            return _abilities.FirstOrDefault(v => v.BaseData.Id == id);
        }

        private void OnChanged()
        {
            Changed?.Invoke();
        }
    }
}
