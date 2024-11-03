using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ParticleSystem _gunshotEffect;
    [SerializeField] private float _timeToPerformFirstAttack = 0.5f;

    private bool _isAttacking;
    private float _damage;
    private float _firingRate;

    public void Initialize(float damage, float firingRate)
    {
        _damage = damage;
        _firingRate = firingRate;
    }

    public void Attack(IDamageable target)
    {
        _isAttacking = true;
        StartCoroutine(AttackCoroutine(target));
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

            yield return new WaitForSeconds(_firingRate);
        }
    }
}