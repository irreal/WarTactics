using System;
using System.Collections.Generic;
using System.Text;

namespace War1.WarBasics.Abilities.PassiveAbilities
{
    public class FirstStrike : PassiveAbility
    {
        public FirstStrike(Unit unit) : base(unit)
        {
            _myUnit.PassiveAbilities.Add(this);
        }

        public override string Description()
        {
            return "Strikes before units without first strike"; 
        }
    }
}
