using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;

    public int MaxHealth => _maxHealth;
    public int Damage => _damage;
}