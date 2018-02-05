using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Text;
using WarTactics.Shared.Components;

namespace WarTactics.Shared.Entities
{
    public class HexagonMapEntity : Entity
    {
        private Texture2D[] hexagonTextures;
        private HexMap hexMap;
        private bool hoverHighlightEnabled = true;
        private Helpers.IntPoint size;

        private Hexagon[,] hexagons;
        private List<Hexagon> hexagonList;
        public HexagonMapEntity(Texture2D[] hexagonTextures, int[,] mapInfo) : base("HexagonMap")
        {
            this.hexagonTextures = hexagonTextures;
            this.size = new Helpers.IntPoint(mapInfo.GetLength(0), mapInfo.GetLength(1));
            var map = new HexMap(mapInfo);
            this.hexMap = map;
            this.addComponent(map);
        }
        public Helpers.IntPoint Size => this.size;

        public override void onAddedToScene()
        {
            this.hexagons = new Hexagon[size.X, size.Y];
            this.hexagonList = new List<Hexagon>();
            for (int col = 0; col < size.X; col++)
            {
                for (int row = 0; row < size.Y; row++)
                {
                    var field = this.hexMap.Fields[col, row];
                    Hexagon hg = new Hexagon(hexagonTextures[(int)field.BoardFieldType], $"Hex{col}{row}");
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
            if (this.hoverHighlightEnabled)
            {
                foreach (var hex in this.hexagonList)
                {
                    hex.HighlightColor = null;
                }

                var pos = this.hexMap.IntPointFromPosition(this.scene.camera.mouseToWorldPoint() - this.position);
                if (pos.X >= 0 && pos.X < this.size.X && pos.Y >= 0 && pos.Y < this.size.Y)
                {
                    Hexagon hoverHex = this.hexagons[pos.X, pos.Y];

                    hoverHex.HighlightColor = Color.Blue;
                }
            }
            base.update();
        }
    }
}
