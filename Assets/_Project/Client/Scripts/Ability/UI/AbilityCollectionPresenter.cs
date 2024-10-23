using System.Collections.Generic;

namespace TurnBasedGame.Client
{
    public class AbilityCollectionPresenter
    {
        private readonly AbilityCollectionView _view;
        private readonly AbilitiesUser _abilitiesUser;
        private readonly IAbilityViewsDistributor _abilityViewsDistributor;
        private readonly IBattleNetwork _battleNetwork;

        public AbilityCollectionPresenter(AbilityCollectionView view, AbilitiesUser abilitiesUser, IAbilityViewsDistributor abilityViewsDistributor, IBattleNetwork battleNetwork)
        {
            _view = view;
            _abilitiesUser = abilitiesUser;
            _abilityViewsDistributor = abilityViewsDistributor;
            _battleNetwork = battleNetwork;
        }

        public void Enable()
        {
            _view.AbilitySelected += OnAbilitySelected;
            _abilitiesUser.Changed += OnAbilitiesChanged;

            OnAbilitiesChanged();
        }

        public void Disable()
        {
            _view.AbilitySelected -= OnAbilitySelected;
            _abilitiesUser.Changed -= OnAbilitiesChanged;
        }

        private void OnAbilitiesChanged()
        {
            var abilities = _abilitiesUser.GetAbilities();
            List<AbilityViewDataProxy> viewDatas = new();

            foreach (var ability in abilities)
            {
                viewDatas.Add(
                    new AbilityViewDataProxy(
                    ability.BaseData.Id,
                    _abilityViewsDistributor[ability],
                    _abilitiesUser)
                    );
            }

            _view.Set(viewDatas);
        }

        private void OnAbilitySelected(string id)
        {
            _battleNetwork.ClientBattlePlayer.SelectAbility(id);
        }
    }
}
