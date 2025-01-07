using UnityEngine;

public abstract class SceneLoaderButton : MonoBehaviour
{
    protected SceneLoader SceneLoader { get; private set; }

    public void Initialize(SceneLoader sceneLoader)
    {
        SceneLoader = sceneLoader;
    }
}