using System;
using System.Collections.Generic;
using System.Text;
using War1.WarBasics;
using War1.WarBasics.Abilities;

namespace War1.Units
{
    public class Berzerker : Unit
    {
        public Berzerker(int health = 3, int attack = 2, int armour = 1, int movement = 1,
                         int dodge = 0, bool firstStrike = false, int range = 0, bool taunt = false                         )
            : base(health, attack, armour, movement)
        {
            this.ActivatedAbilities.Add( new PowerAttack(this, 1, 2));
        }// = new System.Collections.ObjectModel.ObservableCollection<ActivatedAbility>();
    }
}
