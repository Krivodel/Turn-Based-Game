using System;

namespace TurnBasedGame
{
    public readonly struct RegenerationInfo
    {
        public int Regeneration { get; }

        public RegenerationInfo(int regeneration)
        {
            if (regeneration < 0)
                throw new ArgumentOutOfRangeException("Regeneration cannot be less than 0.");

            Regeneration = regeneration;
        }
    }
}
