using UnityEngine;

public class RestartButton : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    public void Initialize(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void GoToShop()
    {
        _sceneLoader.Load(_sceneLoader.LevelScene);
    }
}
