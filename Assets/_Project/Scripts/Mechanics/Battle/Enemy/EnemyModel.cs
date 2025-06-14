using System;
using UnityEngine;

public class EnemyModel : BattlerModel
{
    public event Action<EnemyModel> OverrideDied;

    public int MoneyForKill { get; }

    public EnemyModel(Level level, Attacker attacker, ParticleController particleController, 
        Transform currentTransform, float damage, float maxHealth, float firingRate, int moneyForKill) : 
        base(level, attacker, particleController, currentTransform, damage, maxHealth, firingRate)
    {
        MoneyForKill = moneyForKill;
    }

    public override void Die()
    {
        base.Die();
        OverrideDied?.Invoke(this);
    }
}