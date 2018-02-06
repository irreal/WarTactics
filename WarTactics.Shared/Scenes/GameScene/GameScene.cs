namespace WarTactics.Shared.Scenes.GameScene
{
    using Microsoft.Xna.Framework;

    using Nez;

    using WarTactics.Shared.Components;
    using WarTactics.Shared.Components.Game;
    using WarTactics.Shared.Components.Units;
    using WarTactics.Shared.Entities;
    using WarTactics.Shared.Helpers;

    public class GameScene : Scene
    {
        public override void initialize()
        {
            base.initialize();
            var boardEntity = new BoardEntity();
            boardEntity.HoverEntered += this.BoardHoverEntered;
            boardEntity.HoverLeft += this.BoardHoverLeft;
            boardEntity.HexagonSelected += this.BoardHexagonSelected;
            boardEntity.setPosition(new Vector2(150, 150));
            this.addEntity(boardEntity);

            var mapInfo = this.GetMap();

            var fields = GetFieldsFromMap(mapInfo);

            boardEntity.SetupBoard(fields);

            var gr = this.addEntity(new GameEntity()).getComponent<GameRound>();
            int count = 0;
            foreach (var player in gr.Players)
            {
                for (int i = 0; i < 3; i++)
                {
                    count++;
                    BoardField field = null;

                    while (field == null || field.Unit != null)
                    {
                        field = fields[Random.nextInt(mapInfo.GetLength(0)), Random.nextInt(mapInfo.GetLength(1))];
                    }

                    var footman = new Footman { Player = player };
                    field.SetUnit(footman);
                    this.addEntity(new UnitEntity(footman, $"footman{count}")).setParent(boardEntity);
                }
            }

            this.addRenderer(new RenderLayerRenderer(0, 0));
            this.addRenderer(new ScreenSpaceRenderer(1, 1));

            this.createEntity("UI").addComponent<GameSceneUi>().renderLayer = 1;
            this.clearColor = Color.Wheat;
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

        private void BoardHexagonSelected(object sender, HexCoordsEventArgs e)
        {
        }

        private void BoardHoverLeft(object sender, HexCoordsEventArgs e)
        {
            var boardEntity = this.findEntity("Board") as BoardEntity;
            if (boardEntity == null)
            {
                return;
            }

            foreach (var hexagon in boardEntity.HexagonList)
            {
                hexagon.HighlightColor = null;
            }
        }

        private void BoardHoverEntered(object sender, HexCoordsEventArgs e)
        {
            var board = this.findComponentOfType<Board>();
            var boardEntity = this.findEntity("Board") as BoardEntity;
            var gameRound = this.findComponentOfType<GameRound>();
            if (board == null || boardEntity == null)
            {
                return;
            }

            var field = board.Fields[e.Coords.X, e.Coords.Y];
            if (field.Unit != null && field.Unit.Player == gameRound.CurrentPlayer && field.Unit.CanMove)
            {
                var hex = boardEntity.HexAtCoords(e.Coords);
                hex.HighlightColor = new Color(Color.White, 200);
                var moveRange = Hex.Range(field.Hex, field.Unit.Speed);
                foreach (var rangeHex in moveRange)
                {
                    var ofc = OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, rangeHex);
                    if (!board.CoordInMap(ofc))
                    {
                        continue;
                    }

                    var targetField = board.Fields[ofc.col, ofc.row];
                    if (targetField.Unit == null)
                    {
                        var targetHexagon = boardEntity.HexAtCoords(ofc);
                        targetHexagon.HighlightColor = new Color(Color.White, 200);
                    }
                }
            }

            if (field.Unit != null && field.Unit.Player == gameRound.CurrentPlayer && field.Unit.CanAttack)
            {
                var hex = boardEntity.HexAtCoords(e.Coords);
                hex.HighlightColor = new Color(Color.White, 200);
                var attackRange = Hex.Range(field.Hex, field.Unit.AttackRange);
                foreach (var rangeHex in attackRange)
                {
                    var ofc = OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, rangeHex);
                    if (!board.CoordInMap(ofc))
                    {
                        continue;
                    }

                    var targetField = board.Fields[ofc.col, ofc.row];
                    if (targetField.Unit != null && targetField.Unit.Player != gameRound.CurrentPlayer)
                    {
                        var targetHexagon = boardEntity.HexAtCoords(ofc);
                        targetHexagon.HighlightColor = new Color(Color.Red, 150);
                    }
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
