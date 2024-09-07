using System.Linq;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] private EnemyView[] _prefabs;
    [SerializeField] private int _count;

    private ObjectPool<EnemyView> _currentPool;

    public void Initialize(Transform parent = null)
        => _currentPool = new ObjectPool<EnemyView>(_prefabs.ToList(), _count, parent);

    public EnemyView Create(Vector3 position) => _currentPool.GetObject(position);

}
