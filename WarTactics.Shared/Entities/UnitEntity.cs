namespace WarTactics.Shared.Entities
{
    using System;
    using System.Collections.Generic;

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

        private Unit unit;
        private Board board;

        public UnitEntity(Unit unit)
        {
            this.unit = unit;
            
        }

        public override void onAddedToScene()
        {
            this.board = this.scene.findComponentOfType<Board>();
            this.addComponent(this.unit);
            var texture = Core.content.Load<Texture2D>(TypeToContentPath[this.unit.GetType()]);
            this.addComponent(new Sprite(texture));

            var field = this.board.FieldFromUnit(this.unit);
            this.position = this.board.HexPosition(field.Col, field.Row);

            base.onAddedToScene();
        }
    }
}
