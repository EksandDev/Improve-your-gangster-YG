using UnityEngine;
using Zenject;

public class BootstrapEntryPoint : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;

    private SceneLoader _sceneLoader;

    #region Zenject initialization
    [Inject]
    private void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    #endregion

    private void Start()
    {
        _sceneContext.Run();
        _sceneLoader.Load("Menu");
    }
}
