using UnityEngine;

namespace TurnBasedGame.Client
{
    [CreateAssetMenu(menuName = MenuNames.Abilities + "Views Distributor")]
    public sealed class AbilityViewsDistributor : ViewsDistributor<Ability, AbilityViewData>, IAbilityViewsDistributor
    {
#if UNITY_EDITOR
        // Unity bug.
        [ContextMenu("Init Descriptors")]
        protected override void EditorInitDescriptors()
        {
            base.EditorInitDescriptors();
        }
#endif
    }
}
