using UnityEngine;

public class AudioStorage : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private AudioClip _pistolShotSound;
    [SerializeField] private AudioClip _rifleShotSound;
    [SerializeField] private AudioClip _shotgunShotSound;

    [Header("UI")]
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _rejectSound;
}
