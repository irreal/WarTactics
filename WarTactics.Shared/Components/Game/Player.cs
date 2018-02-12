namespace WarTactics.Shared.Components.Game
{
    using Microsoft.Xna.Framework;

    using Nez;

    public class Player : Component
    {
        public Player(string name, Color color)
        {
            this.Name = name;
            this.Color = color;
        }

        public string Name { get; set; }

        public Color Color { get; set; }

        public int StrategyPoints { get; set; }

        public int UsedStrategyPointsThisRound { get; set; }

        public int StrategyPointsPerRound { get; set; } = 3;

        public int StrategyPointsAllowedToCarryOver { get; set; } = 1;

        public virtual void EndTurn()
        {
            if (this.UsedStrategyPointsThisRound < this.StrategyPointsPerRound - this.StrategyPointsAllowedToCarryOver)
            {
                this.StrategyPoints -= this.StrategyPointsPerRound - this.StrategyPointsAllowedToCarryOver - this.UsedStrategyPointsThisRound;
            }
        }

        public virtual void StartTurn()
        {
            this.StrategyPoints += this.StrategyPointsPerRound;
        }
    }
}