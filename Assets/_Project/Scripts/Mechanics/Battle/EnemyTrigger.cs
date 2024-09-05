using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private Level _level;

    public void Initialize(Level level) => _level = level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyView enemy))
        {
            if (_level.CurrentBattle == null)
                _level.CurrentBattle = new(_level.PlayerView.Model, enemy.Model);

            _level.StateMachine.SetState<BattleLevelState>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EnemyView enemy))
            _level.StateMachine.SetState<IdleLevelState>();
    }
}