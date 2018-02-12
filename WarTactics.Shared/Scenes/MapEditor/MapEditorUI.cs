namespace WarTactics.Shared.Scenes.MapEditor
{
    using System;

    using Microsoft.Xna.Framework;

    using Nez;
    using Nez.UI;

    using WarTactics.Shared.Components;
    using WarTactics.Shared.Entities;

    public class MapEditorUi : UICanvas
    {
        private SubtextureDrawable stDrawable;

        private MapEditorSceneComponent mapEditorSceneComponent;


        public void UpdateSelectedSubTexture(BoardFieldType fieldType)
        {
            this.stDrawable.subtexture = WtGame.HexagonSubtextures[(int)fieldType];
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            this.mapEditorSceneComponent = this.entity.scene.getOrCreateSceneComponent<MapEditorSceneComponent>();

            // setup a Skin and a Table for our UI
            var skin = Skin.createDefaultSkin();

            var window = new Window("Tiles", new WindowStyle(Graphics.instance.bitmapFont, Color.White, new PrimitiveDrawable(350, 350, Color.DimGray)));
            window.setPosition(20, 20);
            window.pad(20);
            window.setSize(300,300);
            window.setMovable(true);

            var table = window.addElement(new Table());
            table.defaults().pad(5f);
            table.setFillParent(true).center();
            this.stDrawable = new SubtextureDrawable(WtGame.HexagonSubtextures[(int)this.mapEditorSceneComponent.CurrentFieldType]);
            table.add(new Label("Selected tile:"));
            table.add(new ImageButton(this.stDrawable)).getElement<ImageButton>().onClicked += b =>
                    {
                        this.mapEditorSceneComponent.CurrentFieldType = (BoardFieldType)(((int)this.mapEditorSceneComponent.CurrentFieldType + 1) % Enum.GetValues(typeof(BoardFieldType)).Length);
                        this.UpdateSelectedSubTexture(this.mapEditorSceneComponent.CurrentFieldType);
                    };
            table.row();
            var button = new Button(ButtonStyle.create(Color.Black, Color.DarkGray, Color.Green));
            button.add(new Label("Clear map"));
            button.onClicked += b => { this.mapEditorSceneComponent.ClearMap(); };
            table.add(button).setMinWidth(110).setMinHeight(30);

            button = new Button(ButtonStyle.create(Color.Black, Color.DarkGray, Color.Green));
            button.add(new Label("Save map"));
            button.onClicked += b =>
                {
                    var writer = new System.IO.StreamWriter(System.IO.File.OpenWrite(@"MainMap.txt"));

                    var board = this.entity.scene.findComponentOfType<Board>();
                    for (int row = 0; row < board.Size.Y; row++)
                    {
                        for (int col = 0; col < board.Size.X; col++)
                        {
                            writer.Write((int)board.Fields[col, row].BoardFieldType);
                            if (col < board.Size.X - 1)
                            {
                                writer.Write(",");
                            }
                        }

                        writer.Write(Environment.NewLine);
                    }
                    writer.Close();
                    this.entity.scene.addEntity(new TextEventEntity("Map saved to MainMap.txt", Color.White, Screen.center, true));
                };
            table.add(button).setMinWidth(110).setMinHeight(30);


            table.row();

            button = new Button(ButtonStyle.create(Color.Black, Color.DarkGray, Color.Green));
            button.add(new Label("Main Menu"));
            button.onClicked += b =>
                {
                    var board = this.entity.scene.findComponentOfType<Board>();
                    for (int col = 0; col < board.Size.X; col++)
                    {
                        for (int row = 0; row < board.Size.Y; row++)
                        {
                            WtGame.Map[col, row] = board.Fields[col, row].BoardFieldType;
                        }
                    }

                    WtGame.LoadMainMenu();
                };
            table.add(button).setMinWidth(110).setMinHeight(30);

            this.stage.addElement(window);

        }
    }
}
