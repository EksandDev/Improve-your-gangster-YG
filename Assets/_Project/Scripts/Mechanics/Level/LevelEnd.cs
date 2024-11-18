using UnityEngine.SceneManagement;

public class LevelEnd
{
    public SceneLoader _sceneLoader;

    public LevelEnd(PlayerCharacterModel model, SceneLoader sceneLoader)
    {
        model.Died += OnFailure;
        _sceneLoader = sceneLoader;
    }

    public void OnFailure()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnFinish()
    {
        _sceneLoader.Load(_sceneLoader.LevelScene);
    }
}