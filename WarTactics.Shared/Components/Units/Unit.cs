namespace WarTactics.Shared.Components.Units
{
    using System;
    using System.Linq;

    using Nez;

    using WarTactics.Shared.Components.Game;
    using WarTactics.Shared.Components.Units.Attributes;
    using WarTactics.Shared.Components.Units.Events;

    [Health(20)]
    [Speed(2)]
    [Attack(10)]
    [Armor(0)]
    [AttackRange(1)]
    [AttackCount(2)]
    [MoveCount(2)]
    public abstract class Unit : Component
    {
        protected Unit()
        {
            var healthAttribute = this.GetAttribute<HealthAttribute>();
            var speedAttribute = this.GetAttribute<SpeedAttribute>();
            var attackAttribute = this.GetAttribute<AttackAttribute>();
            var armorAttribute = this.GetAttribute<ArmorAttribute>();
            var attackRangeAttribute = this.GetAttribute<AttackRangeAttribute>();
            var moveCountAttribute = this.GetAttribute<MoveCountAttribute>();
            var attackCountAttribute = this.GetAttribute<AttackCountAttribute>();

            this.MaxHealth = healthAttribute.MaximumAmount;
            this.InitialMaxHealth = healthAttribute.MaximumAmount;
            this.Health = healthAttribute.Amount;
            this.Speed = speedAttribute.Amount;
            this.AttackRange = attackRangeAttribute.Amount;
            this.Attack = attackAttribute.Amount;
            this.Armor = armorAttribute.Amount;
            this.MoveCount = moveCountAttribute.Amount;
            this.AttackCount = attackCountAttribute.Amount;
        }

        public event EventHandler<UnitUpdatedEventArgs> UnitUpdated;

        public int Speed { get; }

        public int MoveCount { get; }

        public int MovesRemaining { get; private set; }

        public int AttackRange { get; }

        public double Attack { get; }

        public int AttackCount { get; }

        public int AttacksRemaining { get; private set; }

        public double Armor { get;  }

        public double Health { get; private set; }

        public double MaxHealth { get; set; }

        public double InitialMaxHealth { get; set; }

        public Player Player { get; set; }

        public bool CanMove => this.MovesRemaining > 0;

        public bool CanAttack => this.AttacksRemaining > 0;

        public virtual bool HasActions => this.CanAttack || this.CanMove;

        public virtual double AboutToAttack(Unit unit, bool selfInitiated)
        {
            return this.Attack;
        }

        public virtual void FinishedAttacking(Unit unit, bool selfInitiated)
        {
            if (selfInitiated)
            {
                this.AttacksRemaining -= 1;
                this.MovesRemaining = 0;
            }
        }

        public virtual double AboutToBeAttacked(Unit unit, bool selfInitiated)
        {
            return this.Armor;
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

        public virtual void Moved()
        {
            this.MovesRemaining -= 1;
            this.OnUpdated(new UnitEvent(UnitEventType.Moved));
        }

        public virtual void TurnEnded()
        {
        }

        public virtual void TurnStarted()
        {
            this.AttacksRemaining = this.AttackCount;
            this.MovesRemaining = this.MoveCount;
        }

        private void OnUpdated(UnitEvent unitEvent)
        {
            this.UnitUpdated?.Invoke(this, new UnitUpdatedEventArgs(unitEvent));
        }

        private T GetAttribute<T>()
        {
            return (T)this.GetType().GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }
    }
}
