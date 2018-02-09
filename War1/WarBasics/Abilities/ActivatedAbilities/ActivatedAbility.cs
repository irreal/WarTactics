using System;
using System.Collections.Generic;
using System.Text;

namespace War1.WarBasics.Abilities
{
    public abstract class ActivatedAbility : Ability
    {
        //protected Unit _myUnit;
        //public ActivatedAbility(Unit unit, int cost) { _myUnit = unit; Cost = cost; }
        //abstract public string Description();
        public int Cost;

        public ActivatedAbility(Unit unit, int cost) : base(unit)
        {
            Cost = cost;
        }

        abstract public void Activate();
    }
}
