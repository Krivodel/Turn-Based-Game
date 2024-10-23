namespace TurnBasedGame
{
    public class Unit
    {
        public ModifiableHealth ModifiableHealth { get; private set; }
        public AbilitiesUser AbilitiesUser { get; private set; }
        public StatusEffectsApplicator StatusEffectsApplicator { get; private set; }
        public Battle Battle { get; private set; }

        public void Init(ModifiableHealth modifiableHealth, AbilitiesUser abilitiesUser, StatusEffectsApplicator statusEffectsApplicator, Battle battle)
        {
            ModifiableHealth = modifiableHealth;
            AbilitiesUser = abilitiesUser;
            StatusEffectsApplicator = statusEffectsApplicator;
            Battle = battle;
        }
    }
}
