namespace WarTactics.Shared.Scenes.GameScene
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using Nez;

    using WarTactics.Shared.Components;
    using WarTactics.Shared.Components.Game;
    using WarTactics.Shared.Components.Units;
    using WarTactics.Shared.Entities;
    using WarTactics.Shared.Helpers;

    public class GameScene : Scene
    {
        private BoardField selectedField;

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
            // this.getOrCreateSceneComponent<MouseCameraControls>().Update();
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
            var board = this.findComponentOfType<Board>();
            var game = this.findComponentOfType<GameRound>();
            var field = board.Fields[e.Coords.X, e.Coords.Y];

            if (this.selectedField == null)
            {
                if (field.Unit != null && field.Unit.Player == game.CurrentPlayer
                                       && (this.FieldsUnitCanAttack(field).Count > 0 || this.FieldsUnitCanMoveTo(field).Count > 0))
                {
                    this.selectedField = field;
                }
            }
            else
            {
                if (field == this.selectedField)
                {
                    this.selectedField = null;
                    this.BoardHoverLeft(this, e);
                    this.BoardHoverEntered(this, e);
                }
                else
                {
                    if (field.Unit == null)
                    {
                        var moveFields = this.FieldsUnitCanMoveTo(this.selectedField);
                        if (moveFields.Contains(field))
                        {
                            this.ExecuteMove(this.selectedField, field);
                            this.selectedField = null;
                            this.BoardHoverLeft(this, e);
                            this.BoardHoverEntered(this, e);
                        }
                        else
                        {
                            // ToDo notify invalid move target
                            this.selectedField = null;
                            this.BoardHoverLeft(this, e);
                            this.BoardHoverEntered(this, e);
                            this.BoardHexagonSelected(this, e);
                        }
                    }
                    else
                    {
                        var attackFields = this.FieldsUnitCanAttack(this.selectedField);
                        if (attackFields.Contains(field))
                        {
                            this.ExecuteAttack(this.selectedField, field);
                            this.selectedField = null;
                            this.BoardHoverLeft(this, e);
                            this.BoardHoverEntered(this, e);
                        }
                        else
                        {
                            // ToDo notify invalid attack target
                            this.selectedField = null;
                            this.BoardHoverLeft(this, e);
                            this.BoardHoverEntered(this, e);
                            this.BoardHexagonSelected(this, e);
                        }
                    }
                }
            }
        }

        private void ExecuteMove(BoardField sourceField, BoardField targetField)
        {
            var unit = sourceField.RemoveUnit();
            targetField.TakeUnit(unit);
        }

        private void ExecuteAttack(BoardField sourceField, BoardField targetField)
        {
            sourceField.Unit.ExecuteAttackUnit(targetField.Unit);
        }

        private void BoardHoverLeft(object sender, HexCoordsEventArgs e)
        {
            if (this.selectedField != null)
            {
                return;
            }

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
            if (this.selectedField != null)
            {
                return;
            }

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
                var moveFields = this.FieldsUnitCanMoveTo(field);
                if (moveFields.Count > 0)
                {
                    var hex = boardEntity.HexAtCoords(e.Coords);
                    hex.HighlightColor = new Color(Color.White, 200);
                    foreach (var targetField in moveFields)
                    {
                        var targetHexagon = boardEntity.HexAtCoords(targetField.Coords);
                        targetHexagon.HighlightColor = new Color(Color.White, 200);
                    }
                }
            }

            if (field.Unit != null && field.Unit.Player == gameRound.CurrentPlayer && field.Unit.CanAttack)
            {
                var attackFields = this.FieldsUnitCanAttack(field);
                if (attackFields.Count > 0)
                {
                    var hex = boardEntity.HexAtCoords(e.Coords);
                    hex.HighlightColor = new Color(Color.White, 200);
                    foreach (var targetField in attackFields)
                    {
                        var targetHexagon = boardEntity.HexAtCoords(targetField.Coords);
                        targetHexagon.HighlightColor = new Color(Color.Red, 150);
                    }
                }
            }
        }

        private List<BoardField> FieldsUnitCanAttack(BoardField sourceField)
        {
            var board = this.findComponentOfType<Board>();
            List<BoardField> fields = new List<BoardField>();
            if (sourceField.Unit == null || !sourceField.Unit.CanAttack)
            {
                return fields;
            }

            var attackRange = Hex.Range(sourceField.Hex, sourceField.Unit.AttackRange);
            foreach (var rangeHex in attackRange)
            {
                var ofc = OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, rangeHex);
                if (!board.CoordInMap(ofc))
                {
                    continue;
                }

                var targetField = board.Fields[ofc.col, ofc.row];
                if (targetField.CanBeAttackedByUnit(sourceField.Unit) && sourceField.Unit.CanAttackField(targetField))
                {
                    fields.Add(targetField);
                }
            }

            return fields;
        }

        private List<BoardField> FieldsUnitCanMoveTo(BoardField sourceField)
        {
            var board = this.findComponentOfType<Board>();
            List<BoardField> fields = new List<BoardField>();
            if (sourceField.Unit == null || !sourceField.Unit.CanMove)
            {
                return fields;
            }

            var moveRange = Hex.Range(sourceField.Hex, sourceField.Unit.Speed);
            foreach (var rangeHex in moveRange)
            {
                var ofc = OffsetCoord.QoffsetFromCube(OffsetCoord.EVEN, rangeHex);
                if (!board.CoordInMap(ofc))
                {
                    continue;
                }

                var targetField = board.Fields[ofc.col, ofc.row];
                if (targetField.CanTakeUnit(sourceField.Unit) && sourceField.Unit.CanMoveToField(targetField))
                {
                    fields.Add(targetField);
                }
            }

            return fields;
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
