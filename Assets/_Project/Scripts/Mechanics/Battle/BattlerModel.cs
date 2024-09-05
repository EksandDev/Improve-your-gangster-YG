using System;
using UnityEngine;

public abstract class BattlerModel : IDamageable
{
    public Level Level { get; }
    public Attacker Attacker { get; }
    public Transform CurrentTransform { get; }

    public event Action<BattlerModel> BattleStarted;
    public event Action BattleStoped;
    public event Action DamageReceived;
    public event Action Died;

    private int _maxHealth;
    private int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    public BattlerModel(Level level, Attacker attacker, Transform currentTransform, int damage, int maxHealth)
    {
        Level = level;
        Attacker = attacker;
        CurrentTransform = currentTransform;
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;

        Attacker.Initialize(damage);
    }

    public virtual void StartBattle(BattlerModel target)
    {
        Attacker.Attack(target);
        BattleStarted?.Invoke(target);
    }

    public virtual void StopBattle()
    {
        Level.StateMachine.SetState<IdleLevelState>();
        Attacker.AttackStop();
        BattleStoped?.Invoke();
    }

    public virtual void ReceiveDamage(int value)
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
}