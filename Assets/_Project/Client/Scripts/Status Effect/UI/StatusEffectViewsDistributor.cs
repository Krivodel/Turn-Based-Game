using UnityEngine;

namespace TurnBasedGame.Client
{
    [CreateAssetMenu(menuName = MenuNames.StatusEffects + "Views Distributor")]
    public sealed class StatusEffectViewsDistributor : ViewsDistributor<StatusEffect, StatusEffectViewData>, IStatusEffectViewsDistributor
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
