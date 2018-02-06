namespace WarTactics.Shared
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Nez;
    using Nez.Textures;

    using WarTactics.Shared.Scenes.GameScene;
    using WarTactics.Shared.Scenes.MainMenu;
    using WarTactics.Shared.Scenes.MapEditor;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class WtGame : Core
    {
        public WtGame()
        {
            Core.defaultSamplerState = SamplerState.LinearClamp;
        }

        public static Texture2D HexagonesTexture { get; private set; }

        public static Subtexture[] HexagonSubtextures { get; private set; }

        public static void LoadMapEditor()
        {
            WtGame.scene = new MapEditorScene();
        }

        public static void LoadMainMenu()
        {
            WtGame.scene = new MainMenuScene();
        }

        public static void LoadGame()
        {
            WtGame.scene = new GameScene();
        }

        protected override void LoadContent()
        {
            HexagonesTexture = Core.content.Load<Texture2D>("hexagonImages");
            var hexagonLines = System.IO.File.ReadAllLines(@"Content\hexagonImagesMap.txt");
            HexagonSubtextures = new Subtexture[hexagonLines.Length];
            for (int i = 0; i < hexagonLines.Length; i++)
            {
                var args = hexagonLines[i].Split(' ');
                var subT = new Subtexture(HexagonesTexture, new Rectangle(int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]), int.Parse(args[5])));
                HexagonSubtextures[i] = subT;
            }

            WtGame.scene = new MainMenuScene();

            base.LoadContent();
        }
    }
}
