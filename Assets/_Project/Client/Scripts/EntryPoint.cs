using TurnBasedGame.Networking;
using UnityEngine;

namespace TurnBasedGame.Client
{
    // «ависимости лучше прокидывать через Zenject/VContainer/etc, но дл€ маленького проекта € решил его не использовать.
    [RequireComponent(typeof(BattleUnitsCreator))]
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private AbilityViewsDistributor _abilityViewsDistributor;
        [SerializeField] private StatusEffectViewsDistributor _statusEffectViewsDistributor;
        [SerializeField] private BattleView _battleView;
        [SerializeField] private AbilityCollectionView _clientAbilitiesView;

        private IMessageTransport _messageTransport;
        private IBattleNetwork _battleNetwork;
        private BattleUnitsCreator _battleUnitsCreator;

        private void Start()
        {
            CreateServices();
            StartServices();
            CreatePresenters();
        }

        private void OnDestroy()
        {
            StopServices();
        }

        private void CreateServices()
        {
            _messageTransport = ConnectionEmulation.MessageTransport;
            _battleNetwork = new BattleNetwork(_messageTransport);
            _battleUnitsCreator = GetComponent<BattleUnitsCreator>();
            _battleUnitsCreator.Construct(_battleNetwork, _statusEffectViewsDistributor, _abilityViewsDistributor);
        }

        private void StartServices()
        {
            _battleNetwork.StartService();

            _battleNetwork.CreateBattle();
        }

        private void StopServices()
        {
            _battleNetwork.StopService();
        }

        private void CreatePresenters()
        {
            new BattlePresenter(_battleView, _battleNetwork).Enable();
            new AbilityCollectionPresenter(_clientAbilitiesView, _battleNetwork.ClientBattlePlayer.Unit.AbilitiesUser, _abilityViewsDistributor, _battleNetwork).Enable();
        }
    }
}
