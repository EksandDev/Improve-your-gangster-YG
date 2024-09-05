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

    public override void Initialize(Level level, int damage, int maxHealth)
    {
        base.Initialize(level, damage, maxHealth);

        IsRunning = true;
    }

    public override void ModelInitialize(Level level, int damage, int maxHealth) 
        => Model = new(level, Attacker, transform, damage, maxHealth);

    public override void OnStartBattle(BattlerModel target)
    {
        Rotator.StartCoroutine(Rotator.Follow(target.CurrentTransform, () =>
        {
            transform.DORotateQuaternion(_runDirectionPoint.rotation, 0.5f).SetLink(gameObject);
        }));
    }

    public override void OnStopBattle()
    {
        Rotator.IsActive = false;
        IsShooting = false;
        IsStrafingRight = false;
        IsStrafingLeft = false;
    }
}