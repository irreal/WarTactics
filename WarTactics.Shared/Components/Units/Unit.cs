namespace WarTactics.Shared.Components.Units
{
    using System;

    using Nez;

    using WarTactics.Shared.Components.Game;
    using WarTactics.Shared.Components.Units.Events;

    public abstract class Unit : Component
    {
        protected Unit(int speed, int attackRange, double attack, double armor, double health)
        {
            this.Speed = speed;
            this.AttackRange = attackRange;
            this.Attack = attack;
            this.Armor = armor;
            this.MaxHealth = health;
            this.InitialMaxHealth = health;
            this.Health = health;
        }

        public event EventHandler<UnitUpdatedEventArgs> UnitUpdated;

        public int Speed { get; set; }

        public int AttackRange { get; set; }

        public double Attack { get; set; }

        public double Armor { get; set; }

        public double Health { get; set; }

        public double MaxHealth { get; set; }

        public double InitialMaxHealth { get; set; }

        public Player Player { get; set; }

        public bool CanMove { get; set; }

        public bool CanAttack { get; set; }

        public virtual bool HasActions => this.CanAttack || this.CanMove;

        public virtual void ExecuteAttackUnit(Unit unit)
        {
            var dmg = this.Attack - unit.Armor;
            unit.DealDamage(dmg);
            this.CanAttack = false;
            this.CanMove = false;
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
            this.CanMove = false;
            this.OnUpdated(new UnitEvent(UnitEventType.Moved));
        }

        public virtual void TurnEnded()
        {
        }

        public virtual void TurnStarted()
        {
            this.CanMove = true;
            this.CanAttack = true;
        }

        private void OnUpdated(UnitEvent unitEvent)
        {
            this.UnitUpdated?.Invoke(this, new UnitUpdatedEventArgs(unitEvent));
        }
    }
}
