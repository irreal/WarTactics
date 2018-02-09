using System;
using System.Collections.Generic;
using System.Text;

namespace War1.WarBasics.Abilities.PassiveAbilities
{
    public class ArmourPiercing : PassiveAbility
    {
        public ArmourPiercing(Unit unit) : base(unit)
        {

        }

        public override string Description()
        {
            return "Ignores armour when attacking";
        }
    }
}
