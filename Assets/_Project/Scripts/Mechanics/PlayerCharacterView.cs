using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rotator), typeof(Attacker), typeof(Animator))]
public class PlayerCharacterView : BattlerView<PlayerCharacterModel>
{
    [SerializeField] private Transform _runDirectionPoint;

    #region Animations
    private const string IS_RUNNING = "IsRunning";
    private const string IS_STRAFING_LEFT = "IsStrafingLeft";
    private const string IS_STRAFING_RIGHT = "IsStrafingRight";

    public bool IsRunning
    {
        get => Animator.GetBool(IS_RUNNING);
        set
        {
            Animator.SetBool(IS_RUNNING, value);
        }
    }

    public bool IsStrafingLeft
    {
        get => Animator.GetBool(IS_STRAFING_LEFT);
        set
        {
            if (IsStrafingRight && value == true)
                IsStrafingRight = false;

            Animator.SetBool(IS_STRAFING_LEFT, value);
        }
    }

    public bool IsStrafingRight
    {
        get => Animator.GetBool(IS_STRAFING_RIGHT);
        set
        {
            if (IsStrafingLeft && value == true)
                IsStrafingLeft = false;

            Animator.SetBool(IS_STRAFING_RIGHT, value);
        }
    }
    #endregion

    public override void Initialize(EnemyTrigger[] enemyTriggers, Level level, int damage, int maxHealth)
    {
        base.Initialize(level);

        Model = new(level, Attacker, transform, damage, maxHealth, enemyTriggers);

        Model.BattleStarted += OnStartBattle;
        Model.BattleStopped += OnStopBattle;
        Model.DamageReceived += OnReceiveDamage;
        Model.Died += OnDie;

        IsRunning = true;
    }

    public override void OnStartBattle(BattlerModel target)
    {
        Rotator.IsActive = true;
        Rotator.StartCoroutine(Rotator.Follow(target.CurrentTransform, () =>
        {
            transform.DORotateQuaternion(_runDirectionPoint.rotation, 0.5f).SetLink(gameObject);
        }));

        IsShooting = true;

        if (Model.CurrentTargetOnLeftSide)
        {
            IsStrafingRight = true;
            return;
        }

        IsStrafingLeft = true;
    }

    public override void OnStopBattle()
    {
        Rotator.IsActive = false;
        IsShooting = false;
        IsStrafingRight = false;
        IsStrafingLeft = false;
    }
}