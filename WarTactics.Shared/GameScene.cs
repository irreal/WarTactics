using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Text;
using WarTactics.Shared.Entities;

namespace WarTactics.Shared
{
    public class GameScene : Nez.Scene
    {
        private HexagonMapEntity mapEntity;
        private int[,] mapInfo;

        public GameScene() : base()
        {
            
        }
        public override void initialize()
        {
            var text = new Texture2D[6];
            text[0] = this.content.Load<Texture2D>("waterHex");
            text[1] = this.content.Load<Texture2D>("sheepHex");
            text[2] = this.content.Load<Texture2D>("woodHex");
            text[3] = this.content.Load<Texture2D>("desertHex");
            text[4] = this.content.Load<Texture2D>("oreHex");
            text[5] = this.content.Load<Texture2D>("wheatHex");

            this.mapInfo = GetMap();
            this.mapEntity = new HexagonMapEntity(text);
            mapEntity.HoverEntered += Map_HoverEntered;
            mapEntity.HoverLeft += Map_HoverLeft;
            mapEntity.HexagonSelected += MapEntity_HexagonSelected;
            mapEntity.setPosition(new Vector2(150, 150));
            this.addEntity(mapEntity);
            mapEntity.SetMapInfo(this.mapInfo);

            this.addRenderer(new DefaultRenderer());
            this.clearColor = Color.Wheat;
            base.initialize();
        }

        private void MapEntity_HexagonSelected(object sender, Helpers.HexCoordsEventArgs e)
        {
            this.mapInfo[e.Coords.X, e.Coords.Y] = (this.mapInfo[e.Coords.X, e.Coords.Y] + 1) % 6;
            this.mapEntity.SetMapInfo(this.mapInfo);
        }

        private void Map_HoverLeft(object sender, Helpers.HexCoordsEventArgs e)
        {
            var hex = mapEntity.HexAtCoords(e.Coords);
            if (hex != null)
            {
                hex.HighlightColor = null;
            }
        }

        private void Map_HoverEntered(object sender, Helpers.HexCoordsEventArgs e)
        {
            var hex = mapEntity.HexAtCoords(e.Coords);
            if (hex != null)
            {
                hex.HighlightColor = Color.DarkBlue;
            }
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
            if(Input.mousePosition.Y < 10)
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

        private int[,] GetMap()
        {
            return new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,1,1,1,2,2,2,2,0,0,0,0,0 },
                {0,0,1,1,1,2,2,2,2,0,0,0,0,0 },
                {0,0,1,1,1,2,2,1,1,0,0,0,0,0 },
                {0,0,1,1,1,1,1,1,1,0,0,0,0,0 },
                {0,0,1,1,1,1,1,1,0,0,0,0,0,0 },
                {0,0,1,1,1,1,1,1,0,0,0,0,0,0 },
                {0,0,1,1,1,1,1,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,1,1,1,2,2,2,2,0,0,0,0,0 },
                {0,0,1,1,1,2,2,2,2,0,0,0,0,0 },
                {0,0,1,1,1,2,2,1,1,0,0,0,0,0 },
                {0,0,1,1,1,1,1,1,1,0,0,0,0,0 },
                {0,0,1,1,1,1,1,1,0,0,0,0,0,0 },
                {0,0,1,1,1,1,1,1,0,0,0,0,0,0 },
                {0,0,1,1,1,1,1,1,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
            };
        }
    }
}
