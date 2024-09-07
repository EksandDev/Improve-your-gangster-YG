using System.Linq;
using UnityEngine;

public class LevelPartObjectPool : MonoBehaviour
{
    [SerializeField] private LevelPart[] _prefabs;
    [SerializeField] private int _count;

    private ObjectPool<LevelPart> _currentPool;

    public void Initialize(Transform parent = null) 
        => _currentPool = new ObjectPool<LevelPart>(_prefabs.ToList(), _count, parent);

    public LevelPart Create(Vector3 position) => _currentPool.GetObject(position);

}