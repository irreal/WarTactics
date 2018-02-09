using System;
using System.Collections.Generic;
using System.Text;
using War1.WarBasics;
using War1.WarBasics.Abilities;

namespace War1.Units
{
    public class Footman : Unit
    {
        public Footman(int health = 3, int attack = 2, int armour = 1, int movement = 1) : base(health, attack, armour, movement)
        {
        }
    }
}
