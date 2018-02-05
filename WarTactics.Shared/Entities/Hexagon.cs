using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace WarTactics.Shared.Entities
{
    public class Hexagon : Entity
    {
        private Color defaultColor = Color.CornflowerBlue;
        private Color? highlightColor;
        private Sprite sprite;
        public Hexagon(Texture2D hexagonTexture, string name) : base(name)
        {
            this.sprite = new Sprite(hexagonTexture);
            sprite.color = defaultColor;
            sprite.material = new Material(BlendState.AlphaBlend);

            this.addComponent(sprite);
        }

        public Color? HighlightColor
        {
            get => this.highlightColor;
            set
            {
                this.highlightColor = value;
                if (this.highlightColor == null)
                {
                    sprite.setColor(this.defaultColor);
                }
                else
                {
                    sprite.setColor(this.highlightColor.Value);
                }
            }
        }
    }
}
