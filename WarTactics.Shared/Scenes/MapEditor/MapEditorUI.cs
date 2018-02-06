namespace WarTactics.Shared.Scenes.MapEditor
{
    using System;

    using Microsoft.Xna.Framework;

    using Nez;
    using Nez.UI;

    using WarTactics.Shared.Components;

    public class MapEditorUi : UICanvas
    {
        private SubtextureDrawable stDrawable;

        private MapEditorSceneComponent mapEditorSceneComponent;

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            this.mapEditorSceneComponent = this.entity.scene.getOrCreateSceneComponent<MapEditorSceneComponent>();

            // setup a Skin and a Table for our UI
            var skin = Skin.createDefaultSkin();

            var window = new Window("Tiles", new WindowStyle(Graphics.instance.bitmapFont, Color.White, new PrimitiveDrawable(250, 250, Color.DimGray)));
            window.setPosition(20, 20);
            window.pad(20);
            window.setSize(200,200);
            window.setMovable(true);

            var table = window.addElement(new Table());
            table.defaults().pad(5f);
            table.setFillParent(true).center();
            this.stDrawable = new SubtextureDrawable(WtGame.HexagonSubtextures[(int)this.mapEditorSceneComponent.CurrentFieldType]);
            table.add(new Label("Selected tile:"));
            table.add(new ImageButton(this.stDrawable)).getElement<ImageButton>().onClicked += b =>
                    {
                        this.mapEditorSceneComponent.CurrentFieldType = (BoardFieldType)(((int)this.mapEditorSceneComponent.CurrentFieldType + 1) % Enum.GetValues(typeof(BoardFieldType)).Length);
                        this.stDrawable.subtexture = WtGame.HexagonSubtextures[(int)this.mapEditorSceneComponent.CurrentFieldType];
                    };
            table.row();
            var button = new Button(ButtonStyle.create(Color.Black, Color.DarkGray, Color.Green));
            button.add(new Label("Clear map"));
            button.onClicked += b => { this.mapEditorSceneComponent.ClearMap(); };
            table.add(button).setMinWidth(110).setMinHeight(30);

            table.row();

            button = new Button(ButtonStyle.create(Color.Black, Color.DarkGray, Color.Green));
            button.add(new Label("Main Menu"));
            button.onClicked += b => { WtGame.LoadMainMenu(); };
            table.add(button).setMinWidth(110).setMinHeight(30);

            this.stage.addElement(window);

        }
    }
}
