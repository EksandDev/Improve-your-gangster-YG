using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Attacker : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ParticleSystem _gunshotEffect;
    [SerializeField] private float _timeToPerformFirstAttack = 0.5f;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _shotSound;

    private Coroutine _attackCoroutine;
    private bool _isAttacking;
    private float _damage;
    private float _firingRate;

    #region Validate
    public void OnValidate()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _shotSound;
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 1;
    }
    #endregion

    public void Initialize(float damage, float firingRate)
    {
        _damage = damage;
        _firingRate = firingRate;
    }

    public void Attack(IDamageable target)
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);

        _isAttacking = true;
        _attackCoroutine = StartCoroutine(AttackCoroutine(target));
    }

    public void StopAttack()
    {
        _isAttacking = false;
    }

    private IEnumerator AttackCoroutine(IDamageable target)
    {
        yield return new WaitForSeconds(_timeToPerformFirstAttack);

        while (_isAttacking)
        {
            _gunshotEffect.Play();
            target.ReceiveDamage(_damage);
            _audioSource.Play();

            yield return new WaitForSeconds(_firingRate);
        }
    }
}