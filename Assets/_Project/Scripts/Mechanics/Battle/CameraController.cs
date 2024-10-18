using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _runCameraPoint;
    [SerializeField] private Transform _battleCameraPoint;

    private void Start() => GoToIdleCameraPoint();
    public void GoToIdleCameraPoint() => StartCoroutine(GoToCameraPoint(_runCameraPoint));
    public void GoToBattleCameraPoint() => StartCoroutine(GoToCameraPoint(_battleCameraPoint));

    private IEnumerator GoToCameraPoint(Transform cameraPoint)
    {
        transform.DORotateQuaternion(cameraPoint.rotation, 1).SetLink(gameObject);

        yield return transform.DOMove(cameraPoint.position, 1).SetLink(gameObject);

        transform.parent = cameraPoint;
    }
}
