namespace WarTactics.Shared.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Nez;
    using Nez.Sprites;

    using WarTactics.Shared.Components;

    public class HexagonEntity : Entity
    {
        private readonly Color defaultColor = Color.White;
        private readonly Sprite sprite;

        private Color? highlightColor;
        private BoardFieldType type;

        public HexagonEntity(BoardFieldType type, string name, float layerDepth)
            : base(name)
        {
            this.type = type;

            this.sprite = new Sprite(WtGame.HexagonSubtextures[(int)type]) { color = this.defaultColor, layerDepth = layerDepth };
            this.addComponent(this.sprite);
        }

        public Color? HighlightColor
        {
            get => this.highlightColor;
            set
            {
                this.highlightColor = value;
                this.sprite.setColor(this.highlightColor ?? this.defaultColor);
            }
        }

        public void SetType(BoardFieldType newType)
        {
            if (this.type != newType)
            {
                this.type = newType;
                this.sprite.setSubtexture(WtGame.HexagonSubtextures[(int)newType]);
            }
        }

        public override void onAddedToScene()
        {
            var board = this.scene.findComponentOfType<Board>();
            Vector2 hexagonSize = board.HexLayout.size;
            this.scale = hexagonSize / new Vector2(68f, 68f);
            base.onAddedToScene();
        }

        public override void debugRender(Graphics graphics)
        {
            var board = this.scene.findComponentOfType<Board>();
            var coords = board.IntPointFromPosition(this.position);
            graphics.batcher.drawString(graphics.bitmapFont, $"{coords.X} - {coords.Y}", this.position, Color.Purple, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);

            base.debugRender(graphics);
        }
    }
}
