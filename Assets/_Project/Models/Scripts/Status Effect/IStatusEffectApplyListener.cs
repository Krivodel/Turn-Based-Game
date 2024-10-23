namespace TurnBasedGame
{
    public interface IStatusEffectApplyListener
    {
        void OnApply(StatusEffectsApplicator applicator);
    }
}
