using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private CameraController _cameraController;
    private PlayerStats _playerStats;
    private Transform _finishPopup;

    public void Initialize(CameraController cameraController, PlayerStats playerStats, Transform finishPopup)
    {
        _cameraController = cameraController;
        _playerStats = playerStats;
        _finishPopup = finishPopup;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacterView player))
        {
            _cameraController.Deactivate();
            _playerStats.CurrentLevel++;
            _finishPopup.gameObject.SetActive(true);
            Debug.Log($"Current level: {_playerStats.CurrentLevel}");
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