using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rotator), typeof(Attacker), typeof(Animator))]
public class EnemyView : BattlerView<EnemyModel>
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private ParticleSystem _bloodSplatEffect;

    private bool _isLeftSide;

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

    public override void Initialize(bool isLeftSide, Level level, int damage, int maxHealth)
    {
        base.Initialize(level, damage, maxHealth);

        _isLeftSide = isLeftSide;
        _healthSlider.maxValue = Model.MaxHealth;
        _healthSlider.value = Model.CurrentHealth;
    }

    public override void ModelInitialize(Level level, int damage, int maxHealth)
        => Model = new(level, Attacker, transform, damage, maxHealth, _isLeftSide);

    public override void OnReceiveDamage()
    {
        base.OnReceiveDamage();

        _healthSlider.value = Model.CurrentHealth;
        _bloodSplatEffect.Play();
    }
}