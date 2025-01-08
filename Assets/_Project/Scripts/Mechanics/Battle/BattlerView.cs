using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rotator), typeof(Attacker), typeof(Animator))]
public abstract class BattlerView<T> : MonoBehaviour where T : BattlerModel
{
    public T Model { get; protected set; }
    public bool IsInitialized { get; private set; }
    protected Animator Animator { get; private set; }
    protected Attacker Attacker { get; private set; }
    protected Rotator Rotator { get; private set; }

    #region Animations
    private const string IS_SHOOTING = "IsShooting";
    private const string IS_DYING = "IsDying";

    public bool IsDying
    {
        get => Animator.GetBool(IS_DYING);
        set
        {
            Animator.SetBool(IS_DYING, value);
        }
    }

    public bool IsShooting
    {
        get => Animator.GetBool(IS_SHOOTING);
        set
        {
            Animator.SetBool(IS_SHOOTING, value);
        }
    }
    #endregion

    public virtual void Initialize(Level level)
    {
        IsInitialized = true;

        Animator = GetComponent<Animator>();
        Attacker = GetComponent<Attacker>();
        Rotator = GetComponent<Rotator>();
    }

    public virtual void Initialize(Level level, float damage, float maxHealth, float firingRate) 
        => Initialize(level);

    public virtual void Initialize(Level level, float damage, float maxHealth, float firingRate, int moneyForKill)
        => Initialize(level, damage, maxHealth, firingRate);

    public virtual void Initialize(EnemyTrigger[] enemyTriggers, Level level, float damage, float maxHealth, 
        float firingRate, Slider healthSlider) => Initialize(level, damage, maxHealth, firingRate);

    public virtual void OnStartBattle(BattlerModel target)
    {
        Rotator.IsActive = true;
        Rotator.StartCoroutine(Rotator.Follow(target.CurrentTransform));
    } 

    public virtual void OnStopBattle()
    {
        Rotator.IsActive = false;
        IsShooting = false;
    }

    public virtual void OnHealthRecovered() => IsDying = false;
    public virtual void OnDie() => IsDying = true;
    public virtual void OnReceiveDamage() { }
}