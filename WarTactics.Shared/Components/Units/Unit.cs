namespace WarTactics.Shared.Components.Units
{
    using Nez;

    using WarTactics.Shared.Components.Game;

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

        public virtual void TurnEnded()
        {
        }

        public virtual void TurnStarted()
        {
            this.CanMove = true;
            this.CanAttack = true;
        }
    }
}
