using System;
using UnityEngine;

public class EnemyModel : BattlerModel
{
    public event Action<EnemyModel> OverrideDied;

    public int MoneyForKill { get; }

    public EnemyModel(Level level, Attacker attacker, Transform currentTransform, float damage, float maxHealth, 
        float firingRate, int moneyForKill) : base(level, attacker, currentTransform, damage, maxHealth, firingRate)
    {
        MoneyForKill = moneyForKill;
    }

    public override void Die()
    {
        base.Die();
        OverrideDied?.Invoke(this);
    }
}