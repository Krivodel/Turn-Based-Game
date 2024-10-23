using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.Client
{
    public class StatusEffectView : MonoBehaviour
    {
        private Image IconImage => _iconImage ??= GetComponent<Image>();

        private Image _iconImage;

        public void Set(StatusEffectViewData viewData)
        {
            IconImage.sprite = viewData.Icon;

            gameObject.SetActive(true);
        }

        public void Clear()
        {
            gameObject.SetActive(false);
        }

        public bool IsOccupied()
        {
            return gameObject.activeInHierarchy;
        }
    }
}
