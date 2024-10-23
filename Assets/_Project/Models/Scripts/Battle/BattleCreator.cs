using System.Collections.Generic;

namespace TurnBasedGame
{
    public class BattleCreator
    {
        public Battle Create(IEnumerable<IBattlePlayer> players)
        {
            Battle battle = new();
            int id = 0;

            foreach (var player in players)
            {
                player.Init(id, CreateDefaultUnit(battle));

                id++;
            }

            battle.Init(players);

            return battle;
        }

        private Unit CreateDefaultUnit(Battle battle)
        {
            Unit unit = new();
            ModifiableHealth modifiableHealth = new(new Health(100), new DamageModificator());
            AbilitiesUser abilitiesUser = new(CreateDefaultAbilities(unit));
            StatusEffectsApplicator statusEffectsApplicator = new();

            unit.Init(modifiableHealth, abilitiesUser, statusEffectsApplicator, battle);

            return unit;
        }

        private Ability[] CreateDefaultAbilities(Unit ownerUnit)
        {
            return new Ability[]
            {
                new AttackAbility(ownerUnit),
                new BarrierAbility(ownerUnit),
                new RegenerationAbility(ownerUnit),
                new FireballAbility(ownerUnit),
                new CleanupAbility(ownerUnit)
            };
        }
    }
}
