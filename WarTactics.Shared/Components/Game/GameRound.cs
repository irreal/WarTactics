namespace WarTactics.Shared.Components.Game
{
    using System.Collections.Generic;

    using Nez;

    public class GameRound : Component
    {
        public List<Player> Players { get; } = new List<Player>();
    }
}
