namespace WarTactics.Shared.Scenes.MapEditor
{
    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using Nez;

    using WarTactics.Shared.Components;
    using WarTactics.Shared.Entities;

    public class MapEditorScene : Scene
    {
        private BoardEntity mapEntity;

        private HexagonEntity hexagonEntity;

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
            var board = new Board(fields);
            this.mapEntity.SetupBoard(board);

            this.ReCreateHexagonEntity();
            this.hexagonEntity.setEnabled(false);

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
            if (Input.isKeyPressed(Keys.D2))
            {
                var component = this.getSceneComponent<MapEditorSceneComponent>();
                component.CurrentFieldType = (BoardFieldType)(((int)component.CurrentFieldType + 1) % Enum.GetValues(typeof(BoardFieldType)).Length);
                this.findComponentOfType<MapEditorUi>().UpdateSelectedSubTexture(component.CurrentFieldType);
                this.ReCreateHexagonEntity();
            }

            if (Input.isKeyPressed(Keys.D1))
            {
                var component = this.getSceneComponent<MapEditorSceneComponent>();
                var currentInt = (int)component.CurrentFieldType - 1;
                if (currentInt < 0)
                {
                    currentInt = Enum.GetValues(typeof(BoardFieldType)).Length - 1;
                }

                component.CurrentFieldType = (BoardFieldType)currentInt;
                this.findComponentOfType<MapEditorUi>().UpdateSelectedSubTexture(component.CurrentFieldType);
                this.ReCreateHexagonEntity();
            }

            base.update();
        }

        public static BoardField[,] GetFieldsFromMap(BoardFieldType[,] mapInfo)
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
                this.hexagonEntity.setEnabled(false);
                hex.HighlightColor = null;
            }
        }

        private void MapHoverEntered(object sender, Helpers.HexCoordsEventArgs e)
        {
            var hex = this.mapEntity.HexAtCoords(e.Coords);
            if (hex != null)
            {
                this.hexagonEntity.setEnabled(true);
                this.hexagonEntity.HighlightColor = Color.LightGray;
                this.hexagonEntity.position = hex.position;
                hex.HighlightColor = Color.LightBlue;
                if (Input.leftMouseButtonDown)
                {
                    var board = this.findComponentOfType<Board>();
                    board.Fields[e.Coords.X, e.Coords.Y].BoardFieldType = this.getOrCreateSceneComponent<MapEditorSceneComponent>().CurrentFieldType;
                    this.mapEntity.UpdateMapInfo();
                }
            }
        }

        private void ReCreateHexagonEntity()
        {
            var position = this.hexagonEntity?.position;
            this.hexagonEntity?.destroy();
            this.hexagonEntity = new HexagonEntity(this.getOrCreateSceneComponent<MapEditorSceneComponent>().CurrentFieldType, "PaintToolHexagon", 0);
            if (position.HasValue)
            {
                this.hexagonEntity.position = position.Value;
            }
            this.addEntity(this.hexagonEntity);
        }
    }
}
