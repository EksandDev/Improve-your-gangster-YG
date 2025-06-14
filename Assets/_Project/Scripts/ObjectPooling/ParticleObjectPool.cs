using System.Linq;
using UnityEngine;

public class ParticleObjectPool : MonoBehaviour
{
    [SerializeField] private Particle[] _prefabs;
    [SerializeField] private int _count;

    private ObjectPool<Particle> _currentPool;

    public void Initialize(Transform parent = null)
        => _currentPool = new ObjectPool<Particle>(_prefabs.ToList(), _count, parent);

    public Particle Create(Vector3 position) => _currentPool.GetObject(position);
}