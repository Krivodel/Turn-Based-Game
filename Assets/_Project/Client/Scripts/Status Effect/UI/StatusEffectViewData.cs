using UnityEngine;

namespace TurnBasedGame.Client
{
    [CreateAssetMenu(menuName = MenuNames.StatusEffects + "View")]
    public class StatusEffectViewData : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
