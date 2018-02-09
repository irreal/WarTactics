using System;
using System.Collections.Generic;
using System.Text;

namespace War1.WarBasics.Abilities
{
    public class PowerAttack : ActivatedAbility
    {
        public int PowerAttackAmount;
        
        public PowerAttack(Unit unit, PowerAttack powerAttack) : base(unit, powerAttack.Cost)
        {
            PowerAttackAmount = powerAttack.PowerAttackAmount;
        }

        public PowerAttack(Unit unit, int cost, int amount) : base(unit, cost) { PowerAttackAmount = amount; }

        public override void Activate()
        {
            _myUnit.CurrentAttack += PowerAttackAmount;
        }

        public override string Description()
        {
            return Cost + ": Power Attack " + PowerAttackAmount;
        }
    }
}
