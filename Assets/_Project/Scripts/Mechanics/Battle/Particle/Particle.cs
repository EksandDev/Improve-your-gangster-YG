using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(MovableObject))]
public class Particle : MonoBehaviour, IProduct
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _timeToDisable = 5;

    private bool _isInitialized;

    public bool IsInitialized => _isInitialized;

    #region Validate
    private void OnValidate()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    #endregion

    public void Inititalize(LevelMover levelMover)
    {
        levelMover.AddMovingObject(GetComponent<MovableObject>());
        _isInitialized = true;
    }

    private void OnEnable()
    {
        _particleSystem.Play();
        StartCoroutine(Disable());
    }

    private void OnDisable()
    {
        _particleSystem.Stop();
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(_timeToDisable);

        gameObject.SetActive(false);
    }
}