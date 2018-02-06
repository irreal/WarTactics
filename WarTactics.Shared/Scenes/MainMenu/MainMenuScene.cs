namespace WarTactics.Shared.Scenes.MainMenu
{
    using Nez;

    public class MainMenuScene : Scene
    {
        public override void initialize()
        {
            base.initialize();
            this.createEntity("UI").addComponent<MainMenuUi>();
            this.addRenderer(new DefaultRenderer());
        }
    }
}
