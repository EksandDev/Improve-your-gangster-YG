using UnityEngine;

public class LevelEnd
{
    private CameraController _cameraController;
    private PlayerStats _playerStats;
    private Popup _finishPopup;
    private Popup _failurePopup;

    public LevelEnd(PlayerCharacterModel model, CameraController cameraController, PlayerStats playerStats,
        Popup finishPopup, Popup failurePopup)
    {
        model.Died += Failure;
        _cameraController = cameraController;
        _playerStats = playerStats;
        _finishPopup = finishPopup;
        _failurePopup = failurePopup;
    }

    public void Failure()
    {
        _cameraController.Deactivate();
        _failurePopup.Show();
    }

    public void Finish()
    {
        _cameraController.Deactivate();
        _playerStats.CurrentLevel++;
        _finishPopup.Show();
        Debug.Log($"Current level: {_playerStats.CurrentLevel}");
    }
}