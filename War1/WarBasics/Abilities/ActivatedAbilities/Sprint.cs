using System;
using System.Collections.Generic;
using System.Text;

namespace War1.WarBasics.Abilities
{
    public class Sprint : ActivatedAbility
    {
        public int SprintAmount;
            
        public Sprint(Unit unit, int cost, int amount) :base(unit, cost) { Cost = cost; SprintAmount = amount; }

        public Sprint(Unit unit, Sprint sprint) : base(unit, sprint.Cost)
        {
            SprintAmount = sprint.SprintAmount;
        }

        public override void Activate()
        {
            _myUnit.RemainingMovement += SprintAmount;
        }

        public override string Description()
        {
            return Cost + ": Sprint " + SprintAmount;
        }
    }
}
