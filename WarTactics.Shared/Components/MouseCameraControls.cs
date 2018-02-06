namespace WarTactics.Shared.Components
{
    using Microsoft.Xna.Framework;

    using Nez;

    public class MouseCameraControls : SceneComponent
    {
        public void Update()
        {
            if (this.scene == null)
            {
                return;
            }

            Vector2 cameraMove = Vector2.Zero;
            if (Input.mousePosition.X < 10)
            {
                cameraMove.X = -5f;
            }

            if (Input.mousePosition.X > 1270)
            {
                cameraMove.X = 5f;
            }

            if (Input.mousePosition.Y < 10)
            {
                cameraMove.Y = -5f;
            }

            if (Input.mousePosition.Y > 710)
            {
                cameraMove.Y = 5f;
            }

            if (Input.mouseWheelDelta > 0)
            {
                this.scene.camera.zoom += 0.1f;
            }
            else if (Input.mouseWheelDelta < 0)
            {
                this.scene.camera.zoom -= 0.1f;
            }

            this.scene.camera.position += cameraMove;
        }
    }
}
