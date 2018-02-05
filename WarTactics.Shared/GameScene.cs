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
        public GameScene() : base()
        {
            
        }
        public override void initialize()
        {
            var text = new Texture2D[3];
            text[0] = this.content.Load<Texture2D>("waterHex");
            text[1] = this.content.Load<Texture2D>("sheepHex");
            text[2] = this.content.Load<Texture2D>("woodHex");

            var mapInfo = GetMap();
            var map = new HexagonMapEntity(text, mapInfo);
            map.setPosition(new Microsoft.Xna.Framework.Vector2(150, 150));
            this.addEntity(map);

            this.addRenderer(new DefaultRenderer());
            this.clearColor = Color.Wheat;
            base.initialize();
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
                {0,0,0,0,0,0,0,0,0,0 },
                {0,0,1,1,1,2,2,2,2,0 },
                {0,0,1,1,1,2,2,2,2,0 },
                {0,0,1,1,1,2,2,1,1,0 },
                {0,0,1,1,1,1,1,1,1,0 },
                {0,0,1,1,1,1,1,1,0,0 },
                {0,0,1,1,1,1,1,1,0,0 },
                {0,0,1,1,1,1,1,1,0,0 },
                {0,0,0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0,0,0 }
            };
        }
    }
}
