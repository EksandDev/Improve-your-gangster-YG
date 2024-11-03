using System;
using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isActive = true;

    public bool IsActive { get => _isActive; set => _isActive = value; }

    public IEnumerator Follow(Transform target, Action ended = null)
    {
        while (_isActive)
        {
            RotateTo(target.position);

            yield return new WaitForFixedUpdate();
        }

        ended?.Invoke();
    }

    private void RotateTo(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }
}
