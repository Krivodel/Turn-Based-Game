using UnityEngine;

namespace TurnBasedGame.Client
{
    [RequireComponent(typeof(RectTransform))]
    public class UnitViewSetup : MonoBehaviour
    {
        [SerializeField] private HealthBarView _healthBarView;
        [SerializeField] private StatusEffectCollectionView _statusEffectCollectionView;
        [SerializeField] private Vector2 _offset;

        private IStatusEffectViewsDistributor _statusEffectViewsDistributor;
        private RectTransform _rectTransform;
        private Unit _unit;
        private Transform _unitTransform;
        private Camera _camera;

        public void Construct(IStatusEffectViewsDistributor statusEffectViewsDistributor, Unit unit, Transform unitTransform)
        {
            _statusEffectViewsDistributor = statusEffectViewsDistributor;
            _unit = unit;
            _unitTransform = unitTransform;
        }

        public void Create()
        {
            new HealthBarPresenter(_healthBarView, _unit.ModifiableHealth.Health).Enable();
            new StatusEffectCollectionPresenter(_statusEffectCollectionView, _unit.StatusEffectsApplicator, _statusEffectViewsDistributor).Enable();

            _camera = Camera.main;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            Vector3 targetScreenPosition = _camera.WorldToScreenPoint(_unitTransform.position);
            
            _rectTransform.position = targetScreenPosition;
            _rectTransform.anchoredPosition += _offset / targetScreenPosition.z;
        }
    }
}
