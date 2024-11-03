using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreenImage;

    public void Show()
    {
        _loadingScreenImage.SetActive(true);
    }

    public void Hide()
    {
        _loadingScreenImage.SetActive(false);
    }
}
