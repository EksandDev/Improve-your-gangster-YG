using UnityEngine;

public class EnemyModel : BattlerModel
{
    public EnemyModel(Level level, Attacker attacker, Transform currentTransform, float damage, 
        float maxHealth, float firingRate) : base(level, attacker, currentTransform, damage, maxHealth, firingRate)
    {
    }

    public override void Die()
    {
        base.Die();
    }
}