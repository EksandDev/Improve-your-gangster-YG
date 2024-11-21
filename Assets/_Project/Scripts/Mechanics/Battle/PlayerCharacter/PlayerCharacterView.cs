using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;

[RequireComponent(typeof(Rotator), typeof(Attacker), typeof(Animator))]
public class PlayerCharacterView : BattlerView<PlayerCharacterModel>
{
    [SerializeField] private Transform _battleCameraPoint;
    [SerializeField] private Transform _battleGunPoint;
    [SerializeField] private Transform _idleGunPoint;
    [SerializeField] private Transform _gun;
    [SerializeField] private ParticleSystem _moneyEffect;

    public Transform RunDirectionPoint { get; set; }
    public Transform BattleCameraPoint => _battleCameraPoint;
    public ParticleSystem MoneyEffect => _moneyEffect;

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

    public override void Initialize(EnemyTrigger[] enemyTriggers, Level level, float damage, float maxHealth, 
        float firingRate)
    {
        base.Initialize(level);

        Model = new(level, Attacker, transform, damage, maxHealth, firingRate, enemyTriggers);

        Model.BattleStarted += OnStartBattle;
        Model.BattleStopped += OnStopBattle;
        Model.DamageReceived += OnReceiveDamage;
        Model.HealthRecovered += OnHealthRecovered;
        Model.Died += OnDie;

        IsRunning = true;
    }

    public override void OnStartBattle(BattlerModel target)
    {
        _gun.parent = _battleGunPoint;
        _gun.position = _battleGunPoint.position;
        _gun.rotation = _battleGunPoint.rotation;
        Rotator.IsActive = true;
        Rotator.StartCoroutine(Rotator.Follow(target.CurrentTransform, () =>
        {
            transform.DORotateQuaternion(RunDirectionPoint.rotation, 0.5f).SetLink(gameObject);
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
        _gun.parent = _idleGunPoint;
        _gun.position = _idleGunPoint.position;
        _gun.rotation = _idleGunPoint.rotation;
        Rotator.IsActive = false;
        IsShooting = false;
        IsStrafingRight = false;
        IsStrafingLeft = false;
    }
}