using UnityEngine;

public class RestartButton : SceneLoaderButton
{
    public void Restart()
    {
        SceneLoader.Load(SceneLoader.LevelScene);
    }
}
