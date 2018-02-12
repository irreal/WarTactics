namespace WarTactics.Shared.Entities
{
    using System;

    using Microsoft.Xna.Framework;

    using Nez;
    using Nez.Tweens;

    public class TextEventEntity : Entity
    {
        public TextEventEntity(string text, Color color, Vector2 position, bool screenspace, float length = 3f)
        {
            if (length < 0.5f)
            {
                throw new ArgumentException("Length must be at least 0.5", nameof(length));
            }

            this.Text = text;
            this.Color = color;
            this.Length = length;
            this.position = position;
            this.tweenPositionTo(this.position + new Vector2(0, -30), length).setEaseType(EaseType.QuadOut).start();
            var textComponent = new Text(WtGame.MainFont, this.Text, Vector2.Zero, this.Color) { renderLayer = screenspace ? 1 : 0 };
            this.addComponent(textComponent);
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
