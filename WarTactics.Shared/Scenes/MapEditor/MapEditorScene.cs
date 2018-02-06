namespace WarTactics.Shared.Scenes.MapEditor
{
    using Microsoft.Xna.Framework;

    using Nez;

    using WarTactics.Shared.Components;
    using WarTactics.Shared.Entities;

    public class MapEditorScene : Scene
    {
        private BoardEntity mapEntity;

        public override void initialize()
        {
            this.mapEntity = new BoardEntity();
            this.mapEntity.HoverEntered += this.MapHoverEntered;
            this.mapEntity.HoverLeft += this.MapHoverLeft;
            this.mapEntity.HexagonSelected += this.MapEntityHexagonSelected;
            this.mapEntity.setPosition(new Vector2(150, 150));
            this.addEntity(this.mapEntity);

            var mapInfo = this.GetMap();
            this.mapEntity.SetupBoard(mapInfo);

            this.addRenderer(new RenderLayerRenderer(0, 0));
            var spRenderer = new ScreenSpaceRenderer(1, 1);
            this.addRenderer(spRenderer);

            this.createEntity("UI").addComponent<MapEditorUi>().renderLayer = 1;

            this.clearColor = Color.Wheat;
            base.initialize();
        }

        public void ClearMap()
        {
            var board = this.findComponentOfType<Board>();
            
            var type = this.getOrCreateSceneComponent<MapEditorSceneComponent>().CurrentFieldType;

            for (int col = 0; col < board.Size.X; col++)
            {
                for (int row = 0; row < board.Size.Y; row++)
                {
                    board.Fields[col, row].BoardFieldType = type;
                }
            }

            this.mapEntity.UpdateMapInfo();
        }

        public override void update()
        {
            Vector2 cameraMove = Vector2.Zero;
            if (Input.mousePosition.X < 10)
            {
                cameraMove.X = -5f;
            }

            if (Input.mousePosition.X > 1270)
            {
                cameraMove.X = 5f;
            }

            if (Input.mousePosition.Y < 10)
            {
                cameraMove.Y = -5f;
            }

            if (Input.mousePosition.Y > 710)
            {
                cameraMove.Y = 5f;
            }

            if (Input.mouseWheelDelta > 0)
            {
                this.camera.zoom += 0.1f;
            }
            else if (Input.mouseWheelDelta < 0)
            {
                this.camera.zoom -= 0.1f;
            }

            if (Input.isKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                cameraMove.X = -5f;
            }

            if (Input.isKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                cameraMove.X = 5f;
            }

            if (Input.isKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                cameraMove.Y = -5f;
            }

            if (Input.isKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                cameraMove.Y = 5f;
            }

            this.camera.position += cameraMove;
            base.update();
        }

        private void MapEntityHexagonSelected(object sender, Helpers.HexCoordsEventArgs e)
        {
            var board = this.findComponentOfType<Board>();
            board.Fields[e.Coords.X, e.Coords.Y].BoardFieldType = this.getOrCreateSceneComponent<MapEditorSceneComponent>().CurrentFieldType;
            this.mapEntity.UpdateMapInfo();
        }

        private void MapHoverLeft(object sender, Helpers.HexCoordsEventArgs e)
        {
            var hex = this.mapEntity.HexAtCoords(e.Coords);
            if (hex != null)
            {
                hex.HighlightColor = null;
            }
        }

        private void MapHoverEntered(object sender, Helpers.HexCoordsEventArgs e)
        {
            var hex = this.mapEntity.HexAtCoords(e.Coords);
            if (hex != null)
            {
                hex.HighlightColor = Color.LightBlue;
                if (Input.leftMouseButtonDown)
                {
                    var board = this.findComponentOfType<Board>();
                    board.Fields[e.Coords.X, e.Coords.Y].BoardFieldType = this.getOrCreateSceneComponent<MapEditorSceneComponent>().CurrentFieldType;
                    this.mapEntity.UpdateMapInfo();
                }
            }
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
