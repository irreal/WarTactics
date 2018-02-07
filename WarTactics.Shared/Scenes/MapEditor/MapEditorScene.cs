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

            var mapInfo = WtGame.Map;
            var fields = GetFieldsFromMap(mapInfo);
            this.mapEntity.SetupBoard(fields);

            this.addRenderer(new RenderLayerRenderer(0, 0));
            var screenSpaceRenderer = new ScreenSpaceRenderer(1, 1);
            this.addRenderer(screenSpaceRenderer);

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
            this.getOrCreateSceneComponent<MouseCameraControls>().Update();
            this.getOrCreateSceneComponent<KeyboardCameraControls>().Update();
            base.update();
        }

        private static BoardField[,] GetFieldsFromMap(BoardFieldType[,] mapInfo)
        {
            var fields = new BoardField[mapInfo.GetLength(0), mapInfo.GetLength(1)];
            for (int col = 0; col < mapInfo.GetLength(0); col++)
            {
                for (int row = 0; row < mapInfo.GetLength(1); row++)
                {
                    fields[col, row] = new BoardField(col, row, mapInfo[col, row]);
                }
            }

            return fields;
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
    }
}
