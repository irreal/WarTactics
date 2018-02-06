namespace WarTactics.Shared.Components
{
    using Microsoft.Xna.Framework;

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

            this.scene.camera.position += cameraMove;
        }
    }
}
