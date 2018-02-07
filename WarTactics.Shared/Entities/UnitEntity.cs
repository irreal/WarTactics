namespace WarTactics.Shared.Entities
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Nez;
    using Nez.BitmapFonts;
    using Nez.Sprites;

    using WarTactics.Shared.Components;
    using WarTactics.Shared.Components.Units;

    public class UnitEntity : Entity
    {
        private static readonly Dictionary<Type, string> TypeToContentPath = new Dictionary<Type, string>
                                                                        {
                                                                            { typeof(Footman), "footman" }
                                                                        };

        private readonly Unit unit;

        private Text healthText;
        private Board board;

        public UnitEntity(Unit unit, string name) : base(name)
        {
            this.unit = unit;
            this.unit.UnitUpdated += (s, e) => { this.Update(); };
        }

        public override void onAddedToScene()
        {
            this.board = this.scene.findComponentOfType<Board>();
            this.addComponent(this.unit);

            var texture = Core.content.Load<Texture2D>(TypeToContentPath[this.unit.GetType()]);
            var sprite = this.addComponent(new Sprite(texture));
            this.scale = (Vector2)this.board.HexLayout.size / new Vector2(71, 71);
            var field = this.board.FieldFromUnit(this.unit);
            this.position = this.board.HexPosition(field.Col, field.Row);
            if (this.unit?.Player != null)
            {
                sprite.setColor(this.unit.Player.Color);
            }

            var font = Core.content.Load<BitmapFont>("MainFont");
            this.healthText = new Text(font, this.unit?.Health.ToString() ?? string.Empty, new Vector2(-20, 20), Color.Red);
            this.addComponent(this.healthText);

            base.onAddedToScene();
        }

        public void Update()
        {
            var field = this.board.FieldFromUnit(this.unit);
            this.healthText.text = this.unit.Health.ToString();
            this.healthText.text = "IgoreeeE";
            if (field != null)
            {
                this.localPosition = this.board.HexPosition(field.Col, field.Row);
            }

            if (this.unit.Health <= 0)
            {
                this.destroy();
            }
        }
    }
}
