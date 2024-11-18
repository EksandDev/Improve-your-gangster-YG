using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private CameraController _cameraController;
    private Transform _finishPopup;

    public void Initialize(CameraController cameraController, Transform finishPopup)
    {
        _cameraController = cameraController;
        _finishPopup = finishPopup;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacterView player))
        {
            _cameraController.Deactivate();
            _finishPopup.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacterView player))
        {
            player.gameObject.SetActive(false);
        }
    }
}