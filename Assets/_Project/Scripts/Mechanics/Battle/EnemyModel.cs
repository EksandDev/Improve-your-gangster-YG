using UnityEngine;

public class EnemyModel : BattlerModel
{
    public bool IsLeftSide { get; }

    public EnemyModel(Level level, Attacker attacker, Transform currentTransform, int damage, 
        int maxHealth, bool isLeftSide) : base(level, attacker, currentTransform, damage, maxHealth)
    {
        IsLeftSide = isLeftSide;
    }

    public override void Die()
    {
        base.Die();
    }
}