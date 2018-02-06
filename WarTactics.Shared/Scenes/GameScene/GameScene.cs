namespace WarTactics.Shared.Scenes.GameScene
{
    using Microsoft.Xna.Framework;

    using Nez;

    using WarTactics.Shared.Components;
    using WarTactics.Shared.Entities;
    using WarTactics.Shared.Helpers;

    public class GameScene : Scene
    {
        public override void initialize()
        {
            base.initialize();
            var mapEntity = new BoardEntity();
            mapEntity.HoverEntered += this.MapHoverEntered;
            mapEntity.HoverLeft += this.MapHoverLeft;
            mapEntity.HexagonSelected += this.MapEntityHexagonSelected;
            mapEntity.setPosition(new Vector2(150, 150));
            this.addEntity(mapEntity);

            var mapInfo = this.GetMap();
            mapEntity.SetupBoard(mapInfo);

            this.addRenderer(new RenderLayerRenderer(0, 0));
            this.addRenderer(new ScreenSpaceRenderer(1, 1));

            // ToDo add game UI. this.createEntity("UI").addComponent<MapEditorUi>().renderLayer = 1;
            this.clearColor = Color.Wheat;
        }

        private void MapEntityHexagonSelected(object sender, HexCoordsEventArgs e)
        {
        }

        private void MapHoverLeft(object sender, HexCoordsEventArgs e)
        {
        }

        private void MapHoverEntered(object sender, HexCoordsEventArgs e)
        {
        }

        private BoardFieldType[,] GetMap()
        {
            var map = new[,]
                          {
                              {16,16,16,16,16,16,16,16,16,16,16,16,16,1 },
                              {16,16,15,15,15,18,18,18,18,16,16,16,16,1 },
                              {16,16,15,15,15,18,18,18,18,16,16,16,16,1 },
                              {16,16,15,15,15,18,18,15,15,16,16,16,16,1 },
                              {16,16,15,15,15,15,15,15,15,16,16,16,16,1 },
                              {16,16,15,15,15,15,15,15,16,16,16,16,16,1 },
                              {16,16,15,15,15,15,15,15,16,16,16,16,16,1 },
                              {16,16,15,15,15,15,15,15,16,16,16,16,16,1 },
                              {16,16,16,16,16,16,16,16,16,16,16,16,16,1 },
                              {16,16,16,16,16,16,16,16,16,16,16,16,16,1 },
                              {16,16,16,16,16,16,16,16,16,16,16,16,16,1 },
                              {16,16,15,15,15,18,18,18,18,16,16,16,16,1 },
                              {16,16,15,15,15,18,18,18,18,16,16,16,16,1 },
                              {16,16,15,15,15,18,18,15,15,16,16,16,16,1 },
                              {16,16,15,15,15,15,15,15,15,16,16,16,16,1 },
                              {16,16,15,15,15,15,15,15,16,16,16,16,16,1 },
                              {16,16,15,15,15,15,15,15,16,16,16,16,16,1 },
                              {16,16,15,15,15,15,15,15,16,16,16,16,16,1 },
                              {16,16,16,16,16,16,16,16,16,16,16,16,16,1 },
                              {16,16,16,16,16,16,16,16,16,16,16,16,16,1 }
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
