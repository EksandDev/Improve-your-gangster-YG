using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rotator), typeof(Attacker), typeof(Animator))]
public class EnemyView : BattlerView<EnemyModel>, IProduct
{
    [SerializeField] private EnemyData _data;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private ParticleSystem _bloodSplatEffect;

    #region Animations
    private const string IS_RUNNING = "IsRunning";

    public bool IsRunning
    {
        get => Animator.GetBool(IS_RUNNING);
        set
        {
            Animator.SetBool(IS_RUNNING, value);
        }
    }
    #endregion

    public override void Initialize(Level level)
    {
        base.Initialize(level);

        Model = new(level, Attacker, transform, _data.Damage, _data.MaxHealth);

        Model.BattleStarted += OnStartBattle;
        Model.BattleStopped += OnStopBattle;
        Model.DamageReceived += OnReceiveDamage;
        Model.HealthRecovered += OnHealthRecovered;
        Model.Died += OnDie;

        _healthSlider.maxValue = Model.MaxHealth;
        _healthSlider.value = Model.CurrentHealth;
    }

    public override void OnReceiveDamage()
    {
        base.OnReceiveDamage();

        _healthSlider.value = Model.CurrentHealth;
        _bloodSplatEffect.Play();
    }

    public override void OnHealthRecovered()
    {
        base.OnHealthRecovered();

        _healthSlider.value = Model.CurrentHealth;
    }
}