using System;
using System.Collections.Generic;
using System.Text;

namespace War1.WarBasics.Abilities
{
    public abstract class Ability
    {
        protected Unit _myUnit;
        public Ability(Unit unit) { _myUnit = unit; }
        abstract public string Description();
    }
}
