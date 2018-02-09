using System;
using System.Collections.Generic;
using System.Text;
using War1.WarBasics.Map;
namespace War1.WarBasics
{
    public static class CombatResolver
    {
        public static void ResolveFight(Unit attacker, Unit defender)
        {
            #region Ranged
            if (Map.Map.CalculateDistance(attacker.Position, defender.Position) > 1) { ResolveRangedFight(attacker, defender); return; }
            #endregion
            #region Melee
            ResolveMeleeFight(attacker, defender); return;
            #endregion
        }
        public static void ApplyAttackEffects(Unit attacker, Unit defender)
        {
            //Apply additional effects of the attack (poison, deathtouch, whatever)
        }

        public static void ResolveMeleeFight(Unit attacker, Unit defender)
        {
            #region FirstStrike
            if (attacker.FirstStrike()) { defender.TakeDamage(attacker.CurrentAttack, attacker.IgnoresArmour()); } 
            if (defender.FirstStrike()) { attacker.TakeDamage(defender.CurrentAttack, defender.IgnoresArmour()); }
            if (attacker.FirstStrike()) { ApplyAttackEffects(attacker, defender); }
            if (defender.FirstStrike()) { ApplyAttackEffects(defender, attacker); }
            #endregion

            #region SecondStrike

            if (attacker.CurrentHealth < 1 || defender.CurrentHealth < 1) return; //end of combat, one of them is dead

            if (!attacker.FirstStrike()) { defender.TakeDamage(attacker.CurrentAttack, attacker.IgnoresArmour()); }
            if (!defender.FirstStrike()) { attacker.TakeDamage(defender.CurrentAttack, defender.IgnoresArmour()); }
            if (!attacker.FirstStrike()) { ApplyAttackEffects(attacker, defender); }
            if (!defender.FirstStrike()) { ApplyAttackEffects(defender, attacker); }
            #endregion
        }

        public static void ResolveRangedFight(Unit attacker, Unit defender)
        {
            defender.TakeDamage(attacker.CurrentAttack, attacker.IgnoresArmour());
        }
    }
}
