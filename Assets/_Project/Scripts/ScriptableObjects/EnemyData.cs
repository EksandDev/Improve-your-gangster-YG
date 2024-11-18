using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _firingRate;
    [SerializeField] private int _costForSpawn;
    [SerializeField] private int _levelToOpen;
    [SerializeField] private EnemyObjectPool _objectPool;

    public float MaxHealth => _maxHealth;
    public float Damage => _damage;
    public float FiringRate => _firingRate;
    public int CostForSpawn => _costForSpawn;
    public int LevelToOpen => _levelToOpen;
    public EnemyObjectPool ObjectPool => _objectPool;
}