namespace TurnBasedGame
{
    public class RegenerationStatusEffect : StatusEffect
    {
        public ModifiableHealth ModifiableHealth { get; }
        public int Regeneration { get; }

        public RegenerationStatusEffect(ModifiableHealth modifiableHealth, int regeneration)
        {
            ModifiableHealth = modifiableHealth;
            Regeneration = regeneration;
        }

        public void Tick()
        {
            ModifiableHealth.Regenerate(new RegenerationInfo(Regeneration));
        }
    }
}
