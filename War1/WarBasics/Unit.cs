using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using War1.WarBasics.Abilities;
using War1.WarBasics.Abilities.PassiveAbilities;

namespace War1.WarBasics
{
    public class Unit
    {
        #region Basic Stats
        
        #region Health
        public int MaxHealth;
        private int StartingHealth;
        private int _currentHealth;
        public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; CheckForDeath(); } }
        
        public void HealFully() { CurrentHealth = MaxHealth; }
        public void HealByAmount(int n) { CurrentHealth = Math.Min(MaxHealth, CurrentHealth + n); }
        public void ChangeMaxHealthByAmount(int n)
        {
            MaxHealth += n;
            if (CurrentHealth > MaxHealth) { SetCurrenthHealthTo(MaxHealth); }
        }

        private void SetCurrenthHealthTo(int health)  { CurrentHealth = health; }

        public void IncreaseTotalHealthByAmount(int n) { ChangeMaxHealthByAmount(n); ChangeCurrenthHealthByAmount(n); }

        public void ChangeCurrenthHealthByAmount(int n) { CurrentHealth = Math.Min(MaxHealth, CurrentHealth + n); }

        public void TakeDamage(int damage, bool ignoresAmour = false)
        {
            if (ignoresAmour) { ChangeCurrenthHealthByAmount(-damage); return; }
            ChangeCurrenthHealthByAmount(Math.Min(-1, -damage + CurrentArmour));
        }

        public void ResetHealth()
        {
            MaxHealth = StartingHealth;
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        }
        #endregion
        #region Attack
        public int CurrentAttack;
        private int StartingAttack;
        public void ChangeAttackByAmount(int n) { CurrentAttack = Math.Max(CurrentAttack + n, 0); }
        public void SetCurrentAttackTo(int att) { CurrentAttack = att; }
        public void RefreshAttack() { CurrentAttack = StartingAttack; }
        #endregion
        #region Armour
        public int CurrentArmour;
        private int StartingArmour;
        public void RefreshArmour() { CurrentArmour = StartingArmour; }
        #endregion
        #region Movement
        private int NormalMovement;
        public int RemainingMovement;
        public void ChangeMovementByAmount(int n) { NormalMovement = Math.Max(NormalMovement + n, 0); }
        public void RefreshMovement() { RemainingMovement = NormalMovement; }
        #endregion

        #endregion

        public MapPosition Position;

        #region Activated Abilities
        public ObservableCollection<ActivatedAbility> ActivatedAbilities { get; set; }
        #endregion

        #region Passive Abilities
        public ObservableCollection<PassiveAbility> PassiveAbilities { get; set; }
        public bool FirstStrike()
        {
            return PassiveAbilities.Any(ab => ab is FirstStrike);
        }
        public bool IsRanged()
        {
            return PassiveAbilities.Any(ab => ab is Ranged);
        }
        public int Range
        {
            get
            {
                if (IsRanged()) { return ((Ranged)PassiveAbilities.Where(ab => ab is Ranged).ToList().First()).Range; }
                return 1;
            }
        }

        public bool IgnoresArmour()
        {
            return PassiveAbilities.Any(ab => ab is ArmourPiercing);
        }
        #endregion

        public Unit(int health, int attack, int armour, int movement=1) //basic attributes
        {
            #region Assign Attributes
            StartingHealth = health;
            MaxHealth = StartingHealth;
            CurrentHealth = MaxHealth;

            StartingAttack = attack;
            CurrentAttack = StartingAttack;

            StartingArmour = armour;
            CurrentArmour = StartingArmour;

            NormalMovement = movement;
            RemainingMovement = NormalMovement;
            #endregion

            #region Assign Passive Abilities
            PassiveAbilities = new ObservableCollection<PassiveAbility>();

            #endregion

            #region Assign Active Abilities //basic Unit has none
            ActivatedAbilities = new ObservableCollection<ActivatedAbility>();

            #endregion

            Position = new MapPosition();
            #warning needs work
        }

        #region Death
        private void CheckForDeath()
        {
            if (CurrentHealth <= 0) Death();
        }

        private void Death() { Debug.WriteLine("I AM DEAD! " + this.GetType().ToString()); }

        #endregion

        public void Attack(Unit enemy)
        {
            if (Map.Map.CalculateDistance(Position, enemy.Position) > Range) { Debug.WriteLine("Out of range!"); return; } //out of range
            CombatResolver.ResolveFight(this, enemy);
        }
    }
}
