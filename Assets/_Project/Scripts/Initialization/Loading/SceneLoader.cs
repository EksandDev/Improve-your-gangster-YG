using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LoadingScreen))]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;

    private AsyncOperation _currentAsyncOperation;
    private float _currentProgress;

    #region Validate
    private void OnValidate()
    {
        _loadingScreen = GetComponent<LoadingScreen>();
    }
    #endregion

    public void Load(string sceneName, Action sceneLoaded = null) 
        => StartCoroutine(LoadCoroutine(sceneName, sceneLoaded));

    private IEnumerator LoadCoroutine(string sceneName, Action sceneLoaded)
    {
        _loadingScreen.Show();

        if (_currentAsyncOperation == null)
            _currentAsyncOperation = SceneManager.LoadSceneAsync(sceneName);

        else
        {
            Debug.LogWarning("Scene loader attempts to load a scene when another scene didn't loaded");
            yield break;
        }

        while (!_currentAsyncOperation.isDone)
        {
            _currentProgress = _currentAsyncOperation.progress;
            yield return null;
        }

        _currentAsyncOperation = null;
        _loadingScreen.Hide();
        sceneLoaded?.Invoke();
    }
}