namespace WarTactics.Shared.Scenes.GameScene
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    using Nez;
    using Nez.UI;

    using WarTactics.Shared.Components.Game;

    public class GameSceneUi : UICanvas
    {
        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            var drawable = new SubtextureDrawable(Core.content.Load<Texture2D>("frame"));
            var image = new Image(drawable);
            image.setPosition(0, 0);
            this.stage.addElement(image);

            var btn = new Button(ButtonStyle.create(new Color(Color.Black, 80), Color.Black, new Color(Color.Black, 120)));
            btn.setWidth(60f);
            btn.setHeight(30f);
            btn.onClicked += b => { this.entity.scene.findComponentOfType<GameRound>().EndTurn(); };
            var lbl = new Label("End turn");
            btn.add(lbl);
            btn.setPosition(20, 210);
            this.stage.addElement(btn);

            btn = new Button(ButtonStyle.create(new Color(Color.Black, 80), Color.Black, new Color(Color.Black, 120)));
            btn.setWidth(60f);
            btn.setHeight(30f);
            btn.onClicked += b => { WtGame.LoadMainMenu(); };
            lbl = new Label("Exit to Menu");
            btn.add(lbl);
            btn.setPosition(20, 255);
            this.stage.addElement(btn);
        }
    }
}
