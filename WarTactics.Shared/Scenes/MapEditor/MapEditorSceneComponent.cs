namespace WarTactics.Shared.Scenes.MapEditor
{
    using Nez;

    using WarTactics.Shared.Components;

    public class MapEditorSceneComponent : SceneComponent
    {
        public BoardFieldType CurrentFieldType { get; set; }

        public void ClearMap()
        {
            ((MapEditorScene)this.scene).ClearMap();
        }
    }
}
