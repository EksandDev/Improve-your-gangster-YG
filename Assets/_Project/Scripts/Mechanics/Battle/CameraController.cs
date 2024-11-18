using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MovableObject))]
public class CameraController : MonoBehaviour
{
    private Transform _runCameraPoint;
    private Transform _battleCameraPoint;
    private LevelMover _levelMover;
    private bool _isActive = true;

    public void Initialize(Transform runCameraPoint, Transform battleCameraPoint, LevelMover levelMover)
    {
        _runCameraPoint = runCameraPoint;
        _battleCameraPoint = battleCameraPoint;
        _levelMover = levelMover;
        GoToIdleCameraPoint();
    }

    public void Deactivate()
    {
        _isActive = false;
        transform.parent = null;
        _levelMover.AddMovingObject(gameObject.GetComponent<MovableObject>());
    }

    public void GoToIdleCameraPoint() => StartCoroutine(GoToCameraPoint(_runCameraPoint));
    public void GoToBattleCameraPoint() => StartCoroutine(GoToCameraPoint(_battleCameraPoint));

    private IEnumerator GoToCameraPoint(Transform cameraPoint)
    {
        if (_isActive == false)
            yield break;

        transform.DORotateQuaternion(cameraPoint.rotation, 1).SetLink(gameObject);

        yield return transform.DOMove(cameraPoint.position, 1).SetLink(gameObject);

        transform.parent = cameraPoint;
    }
}
