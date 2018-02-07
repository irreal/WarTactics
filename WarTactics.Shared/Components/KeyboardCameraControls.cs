namespace WarTactics.Shared.Components
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using Nez;

    public class KeyboardCameraControls : SceneComponent
    {
        public void Update()
        {
            if (this.scene == null)
            {
                return;
            }

            Vector2 cameraMove = Vector2.Zero;

            if (Input.isKeyDown(Keys.A))
            {
                cameraMove.X = -5f;
            }

            if (Input.isKeyDown(Keys.D))
            {
                cameraMove.X = 5f;
            }

            if (Input.isKeyDown(Keys.W))
            {
                cameraMove.Y = -5f;
            }

            if (Input.isKeyDown(Keys.S))
            {
                cameraMove.Y = 5f;
            }

            if (Input.isKeyDown(Keys.Q))
            {
                this.scene.camera.zoom -= 0.01f;
            }
            else if (Input.isKeyDown(Keys.E))
            {
                this.scene.camera.zoom += 0.01f;
            }


            this.scene.camera.position += cameraMove;
        }
    }
}
