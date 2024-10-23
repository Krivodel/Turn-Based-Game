using System;

namespace TurnBasedGame.Client
{
    public interface IBattleNetwork
    {
        ClientBattlePlayer ClientBattlePlayer { get; }
        BotBattlePlayer BotBattlePlayer { get; }

        event Action BattleCreated;
        event Action BattleStarted;
        event Action BattleStopped;
        event Action BattleCompleted;

        void StartService();
        void StopService();
        bool CanCreate();
        void CreateBattle();
        void StopBattle();
    }
}
