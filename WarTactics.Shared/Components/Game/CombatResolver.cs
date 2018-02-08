namespace WarTactics.Shared.Components.Game
{
    using WarTactics.Shared.Components.Units;

    public static class CombatResolver
    {
        public static void UnitAttackingUnit(Unit attacker, Unit defender)
        {
            var attack1 = attacker.AboutToAttack(defender, true);
            var armor1 = defender.AboutToBeAttacked(attacker, false);

            var attack2 = defender.AboutToAttack(attacker, false);
            var armor2 = attacker.AboutToBeAttacked(defender, true);

            var dmg1 = attack1 - armor1;
            var dmg2 = attack2 - armor2;

            defender.DealDamage(dmg1);
            attacker.DealDamage(dmg2);

            defender.FinishedBeingAttacked(attacker, false);
            attacker.FinishedAttacking(defender, true);
            attacker.FinishedBeingAttacked(defender, true);
            defender.FinishedAttacking(attacker, false);
        }
    }
}
