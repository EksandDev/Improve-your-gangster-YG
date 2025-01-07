using UnityEngine;

[RequireComponent(typeof(MovableObject))]
public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private bool _isLeftSide;

    private Level _level;

    public bool IsTriggered { get; private set; }
    public bool IsLeftSide => _isLeftSide;

    public void Initialize(Level level) => _level = level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyView enemy))
        {
            IsTriggered = true;

            if (_level.CurrentBattle == null)
                _level.CurrentBattle = new(_level.PlayerView.Model, enemy.Model);

            _level.CurrentBattle.Stopped += OnStopBattle;
            _level.StateMachine.SetState<BattleLevelState>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyView enemy))
            _level.StateMachine.SetState<IdleLevelState>();
    }

    private void OnStopBattle()
    {
        IsTriggered = false;
        _level.CurrentBattle.Stopped -= OnStopBattle;
    }
}