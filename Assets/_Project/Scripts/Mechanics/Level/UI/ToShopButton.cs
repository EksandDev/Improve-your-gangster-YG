public class ToShopButton : SceneLoaderButton
{
    public void GoToShop()
    {
        SceneLoader.Load(SceneLoader.MenuScene);
    }
}