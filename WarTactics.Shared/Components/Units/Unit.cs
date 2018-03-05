namespace WarTactics.Shared.Components.Units
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Nez;

    using WarTactics.Shared.Components.Game;
    using WarTactics.Shared.Components.Units.Abilities;
    using WarTactics.Shared.Components.Units.Abilities.Passive;
    using WarTactics.Shared.Components.Units.Attributes;
    using WarTactics.Shared.Components.Units.Events;

    [Health(20)]
    [Speed(5)]
    [Attack(20)]
    [Armor(10)]
    [AttackRange(1)]
    [AttackCount(1)]
    public abstract class Unit : Component
    {
        private readonly List<PassiveAbility> passiveAbilities = new List<PassiveAbility>();
        private readonly List<ActiveAbility> activeAbilities = new List<ActiveAbility>();

        protected Unit()
        {
            var healthAttribute = this.GetAttribute<HealthAttribute>();
            var speedAttribute = this.GetAttribute<SpeedAttribute>();
            var attackAttribute = this.GetAttribute<AttackAttribute>();
            var armorAttribute = this.GetAttribute<ArmorAttribute>();
            var attackRangeAttribute = this.GetAttribute<AttackRangeAttribute>();
            var attackCountAttribute = this.GetAttribute<AttackCountAttribute>();

            this.MaxHealth = healthAttribute.MaximumAmount;
            this.InitialMaxHealth = healthAttribute.MaximumAmount;
            this.Health = healthAttribute.Amount;
            this.Speed = speedAttribute.Amount;
            this.AttackRange = attackRangeAttribute.Amount;
            this.Attack = attackAttribute.Amount;
            this.Armor = armorAttribute.Amount;
            this.AttackCount = attackCountAttribute.Amount;

            var abilityAttributes = this.GetAttributes<Ability>();
            foreach (var ab in abilityAttributes)
            {
                if (ab is PassiveAbility pa)
                {
                    this.passiveAbilities.Add(pa);
                }
                else if (ab is ActiveAbility aa)
                {
                    this.activeAbilities.Add(aa);
                }
            }
        }

        public event EventHandler<UnitUpdatedEventArgs> UnitUpdated;

        public IReadOnlyCollection<PassiveAbility> PassiveAbilities => this.passiveAbilities;

        public IReadOnlyCollection<ActiveAbility> ActiveAbilities => this.activeAbilities;

        public int Speed { get; }

        public int SpeedRemaining { get; private set; }

        public int AttackRange { get; }

        public double Attack { get; }

        public double CurrentAttackValue => this.CalculateAttackValue();

        public int AttackCount { get; }

        public int AttacksRemaining { get; private set; }

        public double Armor { get; }

        public double CurrentArmorValue => this.CalculateArmorValue();

        public double Health { get; private set; }

        public double MaxHealth { get; set; }

        public double InitialMaxHealth { get; set; }

        public Player Player { get; set; }

        public bool CanMove => this.SpeedRemaining > 0;

        public bool CanAttack => this.AttacksRemaining > 0;

        public virtual bool HasActions => this.CanAttack || this.CanMove;

        public T GetAbility<T>() where T : Ability
        {
            return this.passiveAbilities.FirstOrDefault(pa => pa is T) as T ?? this.activeAbilities.FirstOrDefault(aa => aa is T) as T;
        }

        public virtual double AboutToAttack(Unit unit, bool selfInitiated)
        {
            return this.CalculateAttackValue();
        }

        public virtual void FinishedAttacking(Unit unit, bool selfInitiated)
        {
            if (selfInitiated)
            {
                this.AttacksRemaining -= 1;
                if (this.GetAbility<Mobility>() == null)
                {
                    this.SpeedRemaining = 0;
                }
            }
        }

        public virtual double AboutToBeAttacked(Unit unit, bool selfInitiated)
        {
            return this.CalculateArmorValue();
        }

        public virtual void FinishedBeingAttacked(Unit unit, bool selfInitiated)
        {
        }

        public virtual void DealDamage(double damage)
        {
            this.Health -= damage;
            this.OnUpdated(new UnitEvent(UnitEventType.TookDamage, damage));
            if (this.Health <= 0)
            {
                this.Die();
            }
        }

        public virtual void Die()
        {
            this.OnUpdated(new UnitEvent(UnitEventType.Died));
        }

        public virtual bool CanMoveToField(BoardField field)
        {
            return true;
        }

        public virtual bool CanAttackField(BoardField field)
        {
            return field.Unit != null && field.Unit.Player != this.Player;
        }

        public virtual void Moved(int distance)
        {
            this.SpeedRemaining -= distance;
            this.OnUpdated(new UnitEvent(UnitEventType.Moved));
        }

        public virtual void TurnEnded()
        {
        }

        public virtual void TurnStarted()
        {
            this.AttacksRemaining = this.AttackCount;
            this.SpeedRemaining = this.Speed;
        }

        private double CalculateArmorValue()
        {
            return this.Armor;
        }

        private double CalculateAttackValue()
        {
            return this.Attack;
        }

        private void OnUpdated(UnitEvent unitEvent)
        {
            this.UnitUpdated?.Invoke(this, new UnitUpdatedEventArgs(unitEvent));
        }

        private T GetAttribute<T>()
        {
            return (T)this.GetType().GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }

        private T[] GetAttributes<T>()
        {
            return Array.ConvertAll(this.GetType().GetCustomAttributes(typeof(T), true), item => (T)item);
        }
    }
}
