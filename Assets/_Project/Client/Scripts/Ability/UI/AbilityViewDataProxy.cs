using UnityEngine;

namespace TurnBasedGame.Client
{
    public class AbilityViewDataProxy
    {
        public string Id { get; }
        public Sprite Icon => _viewData.Icon;
        public bool CanUse => _abilitiesUser.CanUse(Id);

        private readonly AbilityViewData _viewData;
        private readonly AbilitiesUser _abilitiesUser;

        public AbilityViewDataProxy(string id, AbilityViewData viewData, AbilitiesUser abilitiesUser)
        {
            Id = id;
            _viewData = viewData;
            _abilitiesUser = abilitiesUser;
        }
    }
}
