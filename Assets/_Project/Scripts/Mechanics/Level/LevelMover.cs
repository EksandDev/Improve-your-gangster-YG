using System.Collections.Generic;
using UnityEngine;

public class LevelMover : MonoBehaviour
{
    [SerializeField] private float _idleSpeedModificator;
    [SerializeField] private float _battleSpeedModificator;

    private List<MovingObject> _movingObjects;
    private float _speedModificator;

    public void Initialize()
    {
        SetIdleSpeedModificator();
        _movingObjects = new();
    }

    public void SetIdleSpeedModificator() => _speedModificator = _idleSpeedModificator;
    public void SetBattleSpeedModificator() => _speedModificator = _battleSpeedModificator;
    public void AddMovingObject(MovingObject movingObject) => _movingObjects.Add(movingObject);

    private void FixedUpdate()
    {
        if (_movingObjects == null)
            return;

        foreach (var levelPart in _movingObjects)
            levelPart.transform.position += Vector3.right * _speedModificator * Time.fixedDeltaTime;
    }
}