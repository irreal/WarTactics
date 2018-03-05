namespace WarTactics.Shared.Entities
{
    using System.Runtime.CompilerServices;

    using Microsoft.Xna.Framework;

    using Nez;

    using WarTactics.Shared.Components.Game;

    public class GameEntity : Entity
    {
        public GameEntity(GameRound gameRound)
        {

            this.addComponent(gameRound);
        }

        public override void onAddedToScene()
        {
            base.onAddedToScene();
        }
    }
}
