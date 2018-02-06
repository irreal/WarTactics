namespace WarTactics.Shared.Components.Units
{
    using Nez;

    using WarTactics.Shared.Components.Game;

    public abstract class Unit : Component
    {
        protected Unit(int speed, double attack, double armor, double health)
        {
            this.Speed = speed;
            this.Attack = attack;
            this.Armor = armor;
            this.MaxHealth = health;
            this.InitialMaxHealth = health;
            this.Health = health;
        }

        public int Speed { get; set; }

        public double Attack { get; set; }

        public double Armor { get; set; }

        public double Health { get; set; }

        public double MaxHealth { get; set; }

        public double InitialMaxHealth { get; set; }

        public Player Player { get; set; }

        public bool CanMove { get; set; }

        public bool CanAttack { get; set; }

        public virtual void TurnEnded(Player currentPlayer)
        {
        }

        public virtual void TurnStarted(Player currentPlayer)
        {
            this.CanMove = true;
            this.CanAttack = true;
        }
    }
}
