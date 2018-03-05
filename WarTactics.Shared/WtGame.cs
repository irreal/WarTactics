namespace WarTactics.Shared
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Nez;
    using Nez.BitmapFonts;
    using Nez.Textures;

    using WarTactics.Shared.Components;
    using WarTactics.Shared.Scenes.GameScene;
    using WarTactics.Shared.Scenes.MainMenu;
    using WarTactics.Shared.Scenes.MapEditor;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class WtGame : Core
    {
        public WtGame() : base(1280, 768, false)
        {
            Core.defaultSamplerState = SamplerState.LinearClamp;
            Map = GetMap();
        }

        public static BoardFieldType[,] Map { get; set; }

        public static Texture2D HexagonesTexture { get; private set; }

        public static Subtexture[] HexagonSubtextures { get; private set; }

        public static BitmapFont MainFont { get; private set; }

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
            MainFont = Core.content.Load<BitmapFont>("MainFont");

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

        private static BoardFieldType[,] GetMap()
        {
            if (System.IO.File.Exists(@"MainMap.txt"))
            {
                List<int[]> rows = new List<int[]>();
                var reader = new System.IO.StreamReader(System.IO.File.OpenRead(@"MainMap.txt"));
                int maxWidth = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var tokens = line.Split(',');
                        var intTokens = Array.ConvertAll(tokens, int.Parse);
                        if (intTokens.Length > maxWidth)
                        {
                            maxWidth = intTokens.Length;
                        }

                        rows.Add(intTokens);
                    }
                }

                reader.Close();
                var map = new BoardFieldType[maxWidth, rows.Count];
                for (int row = 0; row < map.GetLength(1); row++)
                {
                    var rowItems = rows[row];
                    for (int col = 0; col < maxWidth; col++)
                    {
                        if (col < rowItems.Length)
                        {
                            map[col, row] = (BoardFieldType)rowItems[col];
                        }
                        else
                        {
                            map[col, row] = 0;
                        }
                    }
                }

                return map;
            }
            else
            {
                var map = new[,]
                              {
                                  { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 18, 18, 18, 18, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 18, 18, 18, 18, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 18, 18, 15, 15, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 18, 18, 18, 18, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 18, 18, 18, 18, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 18, 18, 15, 15, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 },
                                  { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 }
                              };
                BoardFieldType[,] finalMap = new BoardFieldType[map.GetLength(0), map.GetLength(1)];
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int u = 0; u < map.GetLength(1); u++)
                    {
                        finalMap[i, u] = (BoardFieldType)map[i, u];
                    }
                }

                return finalMap;
            }
        }
    }
}
