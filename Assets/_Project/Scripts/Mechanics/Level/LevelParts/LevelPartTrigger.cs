using UnityEngine;

public class LevelPartTrigger : MonoBehaviour
{
    [SerializeField] private LevelPart _parentLevelPart;

    private LevelPartCreator _levelPartCreator => _parentLevelPart.LevelPartCreator;
    private Transform _nextLevelPartSpawnPoint => _parentLevelPart.NextLevelPartSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacterView player))
            _levelPartCreator.Create(_nextLevelPartSpawnPoint.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacterView player))
            _parentLevelPart.Deactivate();
    }
}