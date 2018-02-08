namespace WarTactics.Shared.Entities
{
    using System;
    using System.Collections.Generic;

    using Nez;

    using WarTactics.Shared.Components;
    using WarTactics.Shared.Components.Units;
    using WarTactics.Shared.Helpers;

    public class BoardEntity : Entity
    {
        private Board board;

        private HexagonEntity[,] hexagons;
        private List<HexagonEntity> hexagonList;

        private IntPoint? hoverHex;

        public BoardEntity() : base("Board")
        {
        }

        public event EventHandler<HexCoordsEventArgs> HoverEntered;

        public event EventHandler<HexCoordsEventArgs> HoverLeft;

        public event EventHandler<HexCoordsEventArgs> HexagonSelected;

        public HexagonEntity[,] Hexagons => this.hexagons;

        public IReadOnlyCollection<HexagonEntity> HexagonList => this.hexagonList;

        public bool MouseControlled { get; set; } = true;

        public void CreateHexagons()
        {
            this.hexagons = new HexagonEntity[this.board.Size.X, this.board.Size.Y];
            this.hexagonList = new List<HexagonEntity>();
            for (int col = 0; col < this.board.Size.X; col++)
            {
                for (int row = 0; row < this.board.Size.Y; row++)
                {
                    var index = (col * this.board.Size.X) + row;
                    float depthOffset = (float)index * 0.00001f;
                    var field = this.board.Fields[col, row];
                    HexagonEntity hg = new HexagonEntity(field.BoardFieldType, $"Hex{col}{row}", 1 - depthOffset);
                    hg.setParent(this);
                    hg.setPosition(this.board.HexPosition(col, row));
                    this.scene.addEntity(hg);
                    this.hexagons[col, row] = hg;
                    this.hexagonList.Add(hg);
                }
            }

            this.onAddedToScene();
        }

        public override void update()
        {
            if (this.MouseControlled)
            {
                this.MouseControl();
            }

            base.update();
        }

        public void RefreshHover()
        {
            if (this.hoverHex.HasValue)
            {
                this.HoverLeft?.Invoke(this, new HexCoordsEventArgs(hoverHex.Value));
                this.HoverEntered?.Invoke(this, new HexCoordsEventArgs(hoverHex.Value));
            }
        }

        public HexagonEntity HexAtCoords(IntPoint coords)
        {
            if (this.hexagons == null || coords.X < 0 || coords.X >= this.hexagons.GetLength(0) || coords.Y < 0 || coords.Y >= this.hexagons.GetLength(1))
            {
                return null;
            }

            return this.hexagons[coords.X, coords.Y];
        }

        public void UpdateMapInfo()
        {
            for (int col = 0; col < this.board.Size.X; col++)
            {
                for (int row = 0; row < this.board.Size.Y; row++)
                {
                    this.hexagons[col, row].SetType(this.board.Fields[col, row].BoardFieldType);
                }
            }
        }

        public void SetupBoard(BoardField[,] mapInfo)
        {
            this.board = new Board(mapInfo);
            this.removeComponent<Board>();
            this.addComponent(this.board);
            this.CreateHexagons();
        }

        private void MouseControl()
        {
            var pos = this.board.IntPointFromPosition(this.scene.camera.mouseToWorldPoint() - this.position);
            if (pos.X >= 0 && pos.X < this.board.Size.X && pos.Y >= 0 && pos.Y < this.board.Size.Y)
            {
                if (this.hoverHex != null)
                {
                    if (this.hoverHex.Value.X != pos.X || this.hoverHex.Value.Y != pos.Y)
                    {
                        this.HoverLeft?.Invoke(this, new HexCoordsEventArgs(this.hoverHex.Value));
                        this.hoverHex = pos;
                        this.HoverEntered?.Invoke(this, new HexCoordsEventArgs(this.hoverHex.Value));
                    }
                }
                else
                {
                    this.hoverHex = pos;
                    this.HoverEntered?.Invoke(this, new HexCoordsEventArgs(this.hoverHex.Value));
                }
            }
            else
            {
                if (this.hoverHex != null)
                {
                    this.HoverLeft?.Invoke(this, new HexCoordsEventArgs(this.hoverHex.Value));
                    this.hoverHex = null;
                }
            }

            if (this.hoverHex != null && Input.leftMouseButtonPressed)
            {
                this.HexagonSelected?.Invoke(this, new HexCoordsEventArgs(this.hoverHex.Value));
            }
        }
    }
}
