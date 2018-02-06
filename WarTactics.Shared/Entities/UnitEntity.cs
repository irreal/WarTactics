namespace WarTactics.Shared.Entities
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Nez;
    using Nez.Sprites;

    using WarTactics.Shared.Components;
    using WarTactics.Shared.Components.Units;

    public class UnitEntity : Entity
    {
        private static readonly Dictionary<Type, string> TypeToContentPath = new Dictionary<Type, string>()
                                                                        {
                                                                            { typeof(Footman), "footman" }
                                                                        };

        private readonly Unit unit;
        private Board board;

        public UnitEntity(Unit unit, string name) : base(name)
        {
            this.unit = unit;
        }

        public override void onAddedToScene()
        {
            this.board = this.scene.findComponentOfType<Board>();
            this.addComponent(this.unit);
            var texture = Core.content.Load<Texture2D>(TypeToContentPath[this.unit.GetType()]);
            var sprite = this.addComponent(new Sprite(texture));
            this.scale = (Vector2)this.board.HexLayout.size / new Vector2(51, 51);
            var field = this.board.FieldFromUnit(this.unit);
            this.position = this.board.HexPosition(field.Col, field.Row);

            if (this.unit?.Player != null)
            {
                sprite.setColor(this.unit.Player.Color);
            }

            base.onAddedToScene();
        }
    }
}
