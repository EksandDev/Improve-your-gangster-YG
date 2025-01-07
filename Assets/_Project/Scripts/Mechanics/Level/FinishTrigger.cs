using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private LevelEnd _levelEnd;

    public void Initialize(LevelEnd levelEnd)
    {
        _levelEnd = levelEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacterView player))
        {
            _levelEnd.Finish();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerCharacterView player))
        {
            player.gameObject.SetActive(false);
        }
    }
}