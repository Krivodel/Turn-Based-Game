using System;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.Client
{
    public class AbilityView : MonoBehaviour
    {
        public string Id { get; private set; }

        public event Action<AbilityView> Clicked;

        private Button Button => _button ??= GetComponent<Button>();

        [SerializeField] private Image _iconImage;

        private Button _button;

        public void Set(AbilityViewDataProxy viewData)
        {
            Id = viewData.Id;
            _iconImage.sprite = viewData.Icon;
            Button.interactable = viewData.CanUse;

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

        private void Awake()
        {
            Button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            Clicked?.Invoke(this);
        }
    }
}
