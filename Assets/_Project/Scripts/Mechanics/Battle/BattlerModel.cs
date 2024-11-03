using System;
using UnityEngine;

public abstract class BattlerModel : IDamageable
{
    public Level Level { get; }
    public Attacker Attacker { get; }
    public Transform CurrentTransform { get; }

    public event Action<BattlerModel> BattleStarted;
    public event Action BattleStopped;
    public event Action DamageReceived;
    public event Action HealthRecovered;
    public event Action Died;

    private float _maxHealth;
    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public BattlerModel(Level level, Attacker attacker, Transform currentTransform, float damage, 
        float maxHealth, float firingRate)
    {
        Level = level;
        Attacker = attacker;
        CurrentTransform = currentTransform;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;

        Attacker.Initialize(damage, firingRate);
    }

    public virtual void StartBattle(BattlerModel target)
    {
        Attacker.Attack(target);
        BattleStarted?.Invoke(target);
    }

    public virtual void StopBattle()
    {
        Level.StateMachine.SetState<IdleLevelState>();
        Attacker.StopAttack();
        BattleStopped?.Invoke();
    }

    public virtual void ReceiveDamage(float value)
    {
        if (CurrentHealth <= 0 || value <= 0)
            return;

        _currentHealth -= value;
        DamageReceived?.Invoke();

        if (CurrentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        StopBattle();
        Died?.Invoke();
    }

    public virtual void RecoverHealth()
    {
        _currentHealth = MaxHealth;
        HealthRecovered?.Invoke();
    }

    protected void InvokeBattleStarted(BattlerModel target) => BattleStarted?.Invoke(target);
}