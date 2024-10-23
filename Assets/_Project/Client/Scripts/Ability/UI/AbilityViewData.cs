using UnityEngine;

namespace TurnBasedGame.Client
{
    [CreateAssetMenu(menuName = MenuNames.Abilities + "View")]
    public class AbilityViewData : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
