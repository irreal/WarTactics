namespace WarTactics.Shared.Scenes.MainMenu
{
    using System;

    using Microsoft.Xna.Framework;

    using Nez;
    using Nez.UI;

    using WarTactics.Shared.Components;

    public class MainMenuUi : UICanvas
    {
        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            var skin = Skin.createDefaultSkin();

            var table = this.stage.addElement(new Table());
            table.defaults().pad(5f);
            table.setFillParent(true).center();
            table.add(new Label("WarTactics"));
            table.row();
            table.add(new Label("Jebo Igricu Bez Helta"));
            table.row();
            var button = new Button(ButtonStyle.create(Color.Black, Color.DarkGray, Color.Green));
            button.add(new Label("Map Editor"));
            button.onClicked += b => { WtGame.LoadMapEditor(); };
            table.add(button).setMinWidth(170).setMinHeight(70);
        }
    }
}
