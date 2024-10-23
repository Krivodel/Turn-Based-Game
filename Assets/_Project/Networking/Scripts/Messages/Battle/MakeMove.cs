namespace TurnBasedGame.Networking.BattleMessages
{
    public readonly struct MakeMove
    {
        public readonly int PlayerId;
        public readonly string AbilityId;

        public MakeMove(int playerId, string abilityId)
        {
            PlayerId = playerId;
            AbilityId = abilityId;
        }
    }
}
