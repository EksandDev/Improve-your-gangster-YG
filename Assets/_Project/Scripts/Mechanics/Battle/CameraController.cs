using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _runCameraPoint;
    private Transform _battleCameraPoint;

    public void Initialize(Transform runCameraPoint, Transform battleCameraPoint)
    {
        _runCameraPoint = runCameraPoint;
        _battleCameraPoint = battleCameraPoint;
        GoToIdleCameraPoint();
    }

    public void GoToIdleCameraPoint() => StartCoroutine(GoToCameraPoint(_runCameraPoint));
    public void GoToBattleCameraPoint() => StartCoroutine(GoToCameraPoint(_battleCameraPoint));

    private IEnumerator GoToCameraPoint(Transform cameraPoint)
    {
        transform.DORotateQuaternion(cameraPoint.rotation, 1).SetLink(gameObject);

        yield return transform.DOMove(cameraPoint.position, 1).SetLink(gameObject);

        transform.parent = cameraPoint;
    }
}
