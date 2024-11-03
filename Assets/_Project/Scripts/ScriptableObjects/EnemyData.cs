using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _firingRate;

    public float MaxHealth => _maxHealth;
    public float Damage => _damage;
    public float FiringRate => _firingRate;
}