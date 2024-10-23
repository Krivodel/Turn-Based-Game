using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.Client
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _fillerImage;
        [SerializeField] private float _fillDuration = 0.4f;
        [SerializeField] private AnimationCurve _fillCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private float _targetFillAmount;
        private Coroutine _fillerCoroutine;

        public void SetHealth(int currentHealth, int maxHealth)
        {
            if (_fillerCoroutine != null)
                StopCoroutine(_fillerCoroutine);

            _targetFillAmount = (float)currentHealth / maxHealth;

            _fillerCoroutine = StartCoroutine(FillerCoroutine());
        }

        private IEnumerator FillerCoroutine()
        {
            float startFillAmount = _fillerImage.fillAmount;
            float elapsedTime = 0f;

            while (elapsedTime < _fillDuration)
            {
                elapsedTime += Time.deltaTime;

                float curveTime = elapsedTime / _fillDuration;
                float curveValue = _fillCurve.Evaluate(curveTime);

                _fillerImage.fillAmount = ConvertRange(curveValue, 0f, 1f, startFillAmount, _targetFillAmount);

                yield return null;
            }

            _fillerCoroutine = null;
        }

        private float ConvertRange(float value, float originalMin, float originalMax, float newMin, float newMax)
        {
            return ((value - originalMin) * (newMax - newMin) / (originalMax - originalMin)) + newMin;
        }
    }
}
