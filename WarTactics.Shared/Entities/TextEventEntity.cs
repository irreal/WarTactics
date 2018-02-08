namespace WarTactics.Shared.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Nez;
    using Nez.BitmapFonts;
    using Nez.Sprites;

    public class TextEventEntity : Entity
    {
        public TextEventEntity(string text, Color color, float length = 1.5f)
        {
            this.Text = text;
            this.Color = color;
            this.Length = length;

            var sprite = new Sprite(Core.content.Load<Texture2D>("frame"));
            this.addComponent(sprite);
            var textComponent = new Nez.Text(WtGame.MainFont, this.Text, Vector2.Zero, this.Color);
            //this.addComponent(textComponent);
        }

        public string Text { get; set; }

        public Color Color { get; set; }

        public float Length { get; set; }

        public override void update()
        {
            this.Length -= Time.deltaTime;
            if (this.Length <= 0)
            {
                this.destroy();
            }

            base.update();
        }
    }
}
