using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using WarTactics.Shared.Components;
using WarTactics.Shared.Helpers;

namespace WarTactics.Shared.Entities
{
    public class HexagonMapEntity : Entity
    {
        private Texture2D[] hexagonTextures;
        private HexMap hexMap;

        private Hexagon[,] hexagons;
        private List<Hexagon> hexagonList;

        private IntPoint? hoverHex = null;

        public HexagonMapEntity(Texture2D[] hexagonTextures) : base("HexagonMap")
        {
            this.hexagonTextures = hexagonTextures;
        }

        public event EventHandler<HexCoordsEventArgs> HoverEntered;
        public event EventHandler<HexCoordsEventArgs> HoverLeft;
        public event EventHandler<HexCoordsEventArgs> HexagonSelected;


        public IntPoint Size { get; private set; }

        public bool MouseControlled { get; set; } = true;

        public void CreateHexagons()
        {
            if (this.hexagonList != null)
            {
                foreach (var hexagon in hexagonList)
                {
                    hexagon.destroy();
                }
            }
            this.hexagons = new Hexagon[this.Size.X, this.Size.Y];
            this.hexagonList = new List<Hexagon>();
            for (int col = 0; col < this.Size.X; col++)
            {
                for (int row = 0; row < this.Size.Y; row++)
                {
                    var field = this.hexMap.Fields[col, row];
                    Hexagon hg = new Hexagon(hexagonTextures, (int)field.BoardFieldType, $"Hex{col}{row}");
                    hg.setParent(this);
                    hg.setPosition(hexMap.HexPosition(col, row));
                    this.scene.addEntity(hg);
                    this.hexagons[col, row] = hg;
                    this.hexagonList.addIfNotPresent(hg);
                }
            }

            base.onAddedToScene();
        }

        public override void update()
        {
            if (this.MouseControlled)
            {
                MouseControl();
            }
            base.update();
        }

        private void MouseControl()
        {
            var pos = this.hexMap.IntPointFromPosition(this.scene.camera.mouseToWorldPoint() - this.position);
            if (pos.X >= 0 && pos.X < this.Size.X && pos.Y >= 0 && pos.Y < this.Size.Y)
            {
                if (hoverHex != null)
                {
                    if (hoverHex.Value.X != pos.X || hoverHex.Value.Y != pos.Y)
                    {
                        HoverLeft?.Invoke(this, new HexCoordsEventArgs(hoverHex.Value));
                        hoverHex = pos;
                        HoverEntered?.Invoke(this, new HexCoordsEventArgs(hoverHex.Value));
                    }
                }
                else
                {
                    hoverHex = pos;
                    HoverEntered?.Invoke(this, new HexCoordsEventArgs(hoverHex.Value));
                }
            }
            else
            {
                if (hoverHex != null)
                {
                    HoverLeft?.Invoke(this, new HexCoordsEventArgs(hoverHex.Value));
                    hoverHex = null;
                }
            }

            if (hoverHex != null && Input.leftMouseButtonPressed)
            {
                HexagonSelected?.Invoke(this, new HexCoordsEventArgs(hoverHex.Value));
            }
        }

        public Hexagon HexAtCoords(IntPoint coords)
        {
            if (this.hexagons == null || coords.X < 0 || coords.X >= this.hexagons.GetLength(0) || coords.Y < 0 || coords.Y >= this.hexagons.GetLength(1))
            {
                return null;
            }

            return this.hexagons[coords.X, coords.Y];
        }

        public void SetMapInfo(int[,] mapInfo)
        {
            SetupBoard(mapInfo);
        }

        private void SetupBoard(int[,] mapInfo)
        {
            this.Size = new Helpers.IntPoint(mapInfo.GetLength(0), mapInfo.GetLength(1));
            this.hexMap = new HexMap(mapInfo);
            this.removeComponent<HexMap>();
            this.addComponent(this.hexMap);
            CreateHexagons();
        }
    }
}
