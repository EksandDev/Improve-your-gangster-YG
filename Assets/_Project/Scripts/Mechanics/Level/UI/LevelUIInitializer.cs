public class LevelUIInitializer
{
    public LevelUIInitializer(ToShopButton[] toShopButtons, SceneLoader sceneLoader)
    {
        foreach (var button in toShopButtons)
            button.Initialize(sceneLoader);
    }
}