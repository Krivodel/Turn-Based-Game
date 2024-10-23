using System;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.Client
{
    public class BattleView : MonoBehaviour
    {
        public event Action RestartClicked;

        [SerializeField] private Button _restartButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartClicked);
        }

        private void OnRestartClicked()
        {
            RestartClicked?.Invoke();
        }
    }
}
