using UnityEngine;

public class LevelMover : MonoBehaviour
{
    [SerializeField] private float _idleSpeedModificator;
    [SerializeField] private float _battleSpeedModificator;

    private MovingObject[] _movingLevelParts;
    private float _speedModificator;

    public void SetIdleSpeedModificator() => _speedModificator = _idleSpeedModificator;
    public void SetBattleSpeedModificator() => _speedModificator = _battleSpeedModificator;

    public void Initialize()
    {
        _movingLevelParts = GetComponentsInChildren<MovingObject>();
        SetIdleSpeedModificator();
    }

    private void FixedUpdate()
    {
        if (_movingLevelParts == null)
            return;

        foreach (var levelPart in _movingLevelParts)
            levelPart.transform.position += Vector3.right * _speedModificator * Time.fixedDeltaTime;
    }
}