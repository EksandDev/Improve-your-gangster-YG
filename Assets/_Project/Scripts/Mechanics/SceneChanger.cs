using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private AsyncOperation _currentAsyncOperation;
    private float _currentProgress;

    public void Change(string sceneName)
    {
        StartCoroutine(ChangeCoroutine(sceneName));
    }

    private IEnumerator ChangeCoroutine(string sceneName)
    {
        if (_currentAsyncOperation == null)
            _currentAsyncOperation = SceneManager.LoadSceneAsync(sceneName);

        if (_currentAsyncOperation.isDone)
        {
            _currentAsyncOperation = null;

            yield break;
        }

        _currentProgress = _currentAsyncOperation.progress;

        yield return null;
    }
}