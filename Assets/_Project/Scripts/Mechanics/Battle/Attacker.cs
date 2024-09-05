using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ParticleSystem _gunshotEffect;

    private bool _isAttacking;
    private int _damage;

    public void Initialize(int damage)
    {
        _damage = damage;
    }

    public void Attack(IDamageable target)
    {
        _isAttacking = true;
        StartCoroutine(AttackCoroutine(target));
    }

    public void AttackStop()
    {
        _isAttacking = false;
    }

    private IEnumerator AttackCoroutine(IDamageable target)
    {
        yield return new WaitForSeconds(0.5f);

        while (_isAttacking)
        {
            _gunshotEffect.Play();
            target.ReceiveDamage(_damage);

            yield return new WaitForSeconds(1);
        }
    }
}