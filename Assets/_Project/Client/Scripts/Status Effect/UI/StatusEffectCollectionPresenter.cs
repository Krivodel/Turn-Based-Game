using System.Collections.Generic;

namespace TurnBasedGame.Client
{
    public class StatusEffectCollectionPresenter
    {
        private readonly StatusEffectCollectionView _view;
        private readonly StatusEffectsApplicator _statusEffectsApplicator;
        private readonly IStatusEffectViewsDistributor _statusEffectViewsDistributor;

        public StatusEffectCollectionPresenter(StatusEffectCollectionView view, StatusEffectsApplicator statusEffectsApplicator, IStatusEffectViewsDistributor statusEffectViewsDistributor)
        {
            _view = view;
            _statusEffectsApplicator = statusEffectsApplicator;
            _statusEffectViewsDistributor = statusEffectViewsDistributor;
        }

        public void Enable()
        {
            _statusEffectsApplicator.Changed += OnStatusEffectsChanged;
        }

        public void Disable()
        {
            _statusEffectsApplicator.Changed -= OnStatusEffectsChanged;
        }

        private void OnStatusEffectsChanged()
        {
            var statusEffects = _statusEffectsApplicator.GetAll();
            List<StatusEffectViewData> viewDatas = new();

            foreach (var statusEffect in statusEffects)
                viewDatas.Add(_statusEffectViewsDistributor[statusEffect]);

            _view.Set(viewDatas);
        }
    }
}
