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
            this.Health = health;
        }

        public int Speed { get; set; }

        public double Attack { get; set; }

        public double Armor { get; set; }

        public double Health { get; set; }

        public Player Player { get; set; }
    }
}
