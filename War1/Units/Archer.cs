using System;
using System.Collections.Generic;
using System.Text;
using War1.WarBasics;
using War1.WarBasics.Abilities;
using War1.WarBasics.Abilities.PassiveAbilities;

namespace War1.Units
{
    public class Archer : Unit
    {
        public Archer(int health = 2, int attack=3, int armour=0, int movement = 1) 
            : base(health, attack, armour, movement)
        {
            PassiveAbilities.Add(new Ranged(this, 3));
        }
    }
}
