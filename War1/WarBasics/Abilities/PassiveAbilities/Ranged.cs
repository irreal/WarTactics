using System;
using System.Collections.Generic;
using System.Text;

namespace War1.WarBasics.Abilities.PassiveAbilities
{
    public class Ranged : PassiveAbility
    {
        public int Range;
        public Ranged(Unit unit, int range) : base(unit)
        {
            Range = range;
        }

        public override string Description()
        {
            return "Can attack at range of "+Range;
        }
    }
}
