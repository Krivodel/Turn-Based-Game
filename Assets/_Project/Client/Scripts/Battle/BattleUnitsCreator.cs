using UnityEngine;

namespace TurnBasedGame.Client
{
    public sealed class BattleUnitsCreator : MonoBehaviour
    {
        [SerializeField] private GameObject _clientUnitPrefab;
        [SerializeField] private Transform _clientUnitSpawnPoint;
        [SerializeField] private GameObject _botUnitPrefab;
        [SerializeField] private Transform _botUnitSpawnPoint;

        [SerializeField] private UnitViewSetup _unitViewSetupPrefab;
        [SerializeField] private RectTransform _unitViewSetupsContainer;
        
        private IBattleNetwork _battleNetwork;
        private IStatusEffectViewsDistributor _statusEffectViewsDistributor;
        private IAbilityViewsDistributor _abilityViewsDistributor;

        private Transform _clientUnit;
        private UnitViewSetup _clientUnitViewSetup;
        private Transform _botUnit;
        private UnitViewSetup _botUnitViewSetup;

        public void Construct(IBattleNetwork battleNetwork, IStatusEffectViewsDistributor statusEffectViewsDistributor, IAbilityViewsDistributor abilityViewsDistributor)
        {
            _battleNetwork = battleNetwork;
            _statusEffectViewsDistributor = statusEffectViewsDistributor;
            _abilityViewsDistributor = abilityViewsDistributor;

            _battleNetwork.BattleCreated += OnBattleCreated;
            _battleNetwork.BattleStopped += OnBattleStopped;
        }

        private void OnDestroy()
        {
            _battleNetwork.BattleCreated -= OnBattleCreated;
            _battleNetwork.BattleStopped -= OnBattleStopped;
        }

        private void OnBattleCreated()
        {
            _clientUnit = CreateUnit(_clientUnitPrefab, _clientUnitSpawnPoint);
            _clientUnitViewSetup = CreateUnitViewSetup(_battleNetwork.ClientBattlePlayer.Unit, _clientUnit);
            _botUnit = CreateUnit(_botUnitPrefab, _botUnitSpawnPoint);
            _botUnitViewSetup = CreateUnitViewSetup(_battleNetwork.BotBattlePlayer.Unit, _botUnit);
        }

        private void OnBattleStopped()
        {
            Destroy(_clientUnit.gameObject);
            Destroy(_clientUnitViewSetup.gameObject);
            Destroy(_botUnit.gameObject);
            Destroy(_botUnitViewSetup.gameObject);
        }

        private Transform CreateUnit(GameObject prefab, Transform spawnPoint)
        {
            return Instantiate(prefab, spawnPoint.position, spawnPoint.rotation).transform;
        }

        private UnitViewSetup CreateUnitViewSetup(Unit unit, Transform unitTransform)
        {
            var unitViewSetup = Instantiate(_unitViewSetupPrefab, _unitViewSetupsContainer);

            unitViewSetup.Construct(_statusEffectViewsDistributor, unit, unitTransform);
            unitViewSetup.Create();

            return unitViewSetup;
        }
    }
}
