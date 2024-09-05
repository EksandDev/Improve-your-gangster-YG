using UnityEngine;

[RequireComponent(typeof(Rotator), typeof(Attacker), typeof(Animator))]
public abstract class BattlerView<T> : MonoBehaviour where T : BattlerModel
{
    public T Model { get; protected set; }
    protected Animator Animator { get; private set; }
    protected Attacker Attacker { get; private set; }
    protected Rotator Rotator { get; private set; }

    #region Animations
    private const string IS_SHOOTING = "IsShooting";

    public bool IsShooting
    {
        get => Animator.GetBool(IS_SHOOTING);
        set
        {
            Animator.SetBool(IS_SHOOTING, value);
        }
    }
    #endregion

    public virtual void Initialize(Level level, int damage, int maxHealth)
    {
        Animator = GetComponent<Animator>();
        Attacker = GetComponent<Attacker>();
        Rotator = GetComponent<Rotator>();

        ModelInitialize(level, damage, maxHealth);

        Model.BattleStarted += OnStartBattle;
        Model.BattleStopped += OnStopBattle;
        Model.DamageReceived += OnReceiveDamage;
        Model.Died += OnDie;
    }

    public virtual void Initialize(EnemyTrigger[] enemyTriggers, Level level, int damage, int maxHealth) 
        => Initialize(level, damage, maxHealth);

    public abstract void ModelInitialize(Level level, int damage, int maxHealth);

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

    public virtual void OnReceiveDamage() { }

    public virtual void OnDie()
    {
        gameObject.SetActive(false);
    }
}