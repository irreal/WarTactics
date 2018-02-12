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
    using WarTactics.Shared.Components.Units.Events;

    public class UnitEntity : Entity
    {
        private static readonly Dictionary<Type, string> TypeToContentPath = new Dictionary<Type, string>
                                                                        {
                                                                            { typeof(Footman), "footman" }
                                                                        };

        private readonly Unit unit;

        private Text healthText;
        private Text armorText;
        private Text attackText;
        private Text speedText;

        private Board board;

        public UnitEntity(Unit unit, string name) : base(name)
        {
            this.unit = unit;
            this.unit.UnitUpdated += (s, e) => { this.Update(e.UnitEvent); };
        }

        public override void onAddedToScene()
        {
            this.board = this.scene.findComponentOfType<Board>();
            this.addComponent(this.unit);

            var texture = Core.content.Load<Texture2D>(TypeToContentPath[this.unit.GetType()]);
            var sprite = this.addComponent(new Sprite(texture));
            sprite.layerDepth = 0.8f;
            this.scale = this.board.HexLayout.size / new Vector2(81, 81);
            var field = this.board.FieldFromUnit(this.unit);
            this.position = this.board.HexPosition(field.Col, field.Row);
            if (this.unit?.Player != null)
            {
                sprite.setColor(this.unit.Player.Color);
            }

            var font = Core.content.Load<BitmapFont>("MainFont");
            this.healthText = new Text(font, this.unit?.Health.ToString("G") ?? string.Empty, new Vector2(-25, 13), Color.Red);
            this.healthText.layerDepth = 0.6f;
            this.armorText = new Text(font, this.unit?.Armor.ToString("G") ?? string.Empty, new Vector2(15, 13), Color.Blue);
            this.armorText.layerDepth = 0.6f;
            this.attackText = new Text(font, this.unit?.Attack.ToString("G") ?? string.Empty, new Vector2(-25, -23), Color.Orange);
            this.attackText.layerDepth = 0.6f;
            this.speedText = new Text(font, this.unit?.Speed.ToString("G") ?? string.Empty, new Vector2(15, -23), Color.Green);
            this.speedText.layerDepth = 0.6f;
            this.addComponent(this.healthText);
            this.addComponent(this.armorText);
            this.addComponent(this.attackText);
            this.addComponent(this.speedText);

            base.onAddedToScene();
        }

        public void Update(UnitEvent unitEvent)
        {
            var field = this.board.FieldFromUnit(this.unit);

            if (unitEvent.EventType == UnitEventType.Moved && field != null)
            {
                this.localPosition = this.board.HexPosition(field.Col, field.Row);
            }

            if (unitEvent.EventType == UnitEventType.TookDamage)
            {
                this.UpdateStats();
                var ent = this.scene.addEntity(new TextEventEntity($"{unitEvent.Amount}", Color.Red, this.position + new Vector2(-20, -30), false));
            }

            if (unitEvent.EventType == UnitEventType.Healed)
            {
                this.UpdateStats();
            }

            if (unitEvent.EventType == UnitEventType.Died)
            {
                this.getComponent<Sprite>().enabled = false;
                this.destroy();
            }
        }

        private void UpdateStats()
        {
            this.healthText.text = this.unit?.Health.ToString("G");
            this.armorText.text = this.unit?.Armor.ToString("G");
            this.attackText.text = this.unit?.Attack.ToString("G");
            this.speedText.text = this.unit?.Speed.ToString("G");
        }
    }
}
