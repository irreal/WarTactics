namespace WarTactics.Shared
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Nez;
    using Nez.Sprites;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class WtGame : Nez.Core
    {
        public WtGame() : base()
        {
            Core.defaultSamplerState = SamplerState.LinearClamp;
        }

        protected override void Initialize()
        {
            WtGame.scene = new GameScene();
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
