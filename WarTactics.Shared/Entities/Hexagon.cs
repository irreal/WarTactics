using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace WarTactics.Shared.Entities
{
    public class Hexagon : Entity
    {
        private Color defaultColor = Color.White;
        private Color? highlightColor;
        private Sprite sprite;
        private Texture2D[] textures;
        private int type;
        public Hexagon(Texture2D[] hexagonTexture, int type, string name) : base(name)
        {
            this.sprite = new Sprite(hexagonTexture[type]);
            this.type = type;
            this.textures = hexagonTexture;
            sprite.color = defaultColor;

            this.addComponent(sprite);
        }

        public void SetType (int type)
        {
            if (this.type != type)
            {
            this.type = type;
                this.removeComponent(this.sprite);
                this.sprite = new Sprite(this.textures[type]);
                this.addComponent(sprite);
            }
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
