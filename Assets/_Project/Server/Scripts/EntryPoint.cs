using TurnBasedGame.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurnBasedGame.Server
{
    // ����������� ����� ����������� ����� Zenject, �� ��� ���������� ������� � ����� ��� �� ������������.
    public sealed class EntryPoint : MonoBehaviour
    {
        private const string SceneName = "Server";

        private IMessageTransport _messageTransport;
        private BattleNetwork _battleNetwork;

        [RuntimeInitializeOnLoadMethod]
        private static void Main()
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }

        private void Awake()
        {
            CreateServices();
            StartServices();
        }

        private void OnDestroy()
        {
            StopServices();
        }

        private void CreateServices()
        {
            _messageTransport = ConnectionEmulation.MessageTransport;
            _battleNetwork = new(_messageTransport);
        }

        private void StartServices()
        {
            _battleNetwork.Start();
        }

        private void StopServices()
        {
            _battleNetwork.Stop();
        }
    }
}
