using UnityEngine;

public class EnemyModel : BattlerModel
{
    public EnemyModel(Level level, Attacker attacker, Transform currentTransform, int damage, 
        int maxHealth) : base(level, attacker, currentTransform, damage, maxHealth)
    {
    }

    public override void Die()
    {
        base.Die();
    }
}