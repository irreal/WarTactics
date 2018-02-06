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
    }
}