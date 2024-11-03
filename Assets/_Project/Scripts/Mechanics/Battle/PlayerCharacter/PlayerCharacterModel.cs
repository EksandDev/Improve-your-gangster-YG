using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterModel : BattlerModel
{
    private EnemyTrigger[] _enemyTriggers;
    private bool _currentTargetOnLeftSide;

    public bool CurrentTargetOnLeftSide => _currentTargetOnLeftSide;

    public PlayerCharacterModel(Level level, Attacker attacker, Transform currentTransform, float damage, 
        float maxHealth, float firingRate, EnemyTrigger[] enemyTriggers) : 
        base(level, attacker, currentTransform, damage, maxHealth, firingRate) 
    {
        _enemyTriggers = enemyTriggers;
    }

    public override void StartBattle(BattlerModel target)
    {
        Attacker.Attack(target);

        foreach (var enemyTrigger in _enemyTriggers)
        {
            if (enemyTrigger.IsTriggered == false)
                continue;

            _currentTargetOnLeftSide = enemyTrigger.IsLeftSide;
            InvokeBattleStarted(target);
            return;
        }

        Debug.LogError("Start battle with not triggered enemy trigger!");
    }

    public override void Die()
    {
        base.Die();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}