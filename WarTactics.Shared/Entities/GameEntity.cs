namespace WarTactics.Shared.Entities
{
    using System.Runtime.CompilerServices;

    using Microsoft.Xna.Framework;

    using Nez;

    using WarTactics.Shared.Components.Game;

    public class GameEntity : Entity
    {
        public GameEntity()
        {
            var game = new GameRound();
            var player1 = new Player("Irreal", Color.Blue);
            var player2 = new Player("Igor", Color.Red);
            game.Players.Add(player1);
            game.Players.Add(player2);

            this.addComponent(game);
        }

        public override void onAddedToScene()
        {
            base.onAddedToScene();
        }
    }
}
