using System;
using System.Collections.Generic;
using System.Text;

namespace War1.WarBasics.Abilities.PassiveAbilities
{
    public abstract class PassiveAbility : Ability
    {
        public PassiveAbility(Unit unit) : base(unit)
        {
            _myUnit.PassiveAbilities.Add(this);
        }
    }
}
