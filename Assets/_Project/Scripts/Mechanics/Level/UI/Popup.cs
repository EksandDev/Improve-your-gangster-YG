using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(LevelStatsCounterUI))]
public class Popup : MonoBehaviour
{
    [SerializeField] private LevelStatsCounterUI _levelStatsCounterUI;
    [SerializeField] private Transform _endAnimationPoint;

    #region Validate
    private void OnValidate()
    {
        _levelStatsCounterUI = GetComponent<LevelStatsCounterUI>();
    }
    #endregion 

    public void Show()
    {
        gameObject.SetActive(true);
        _levelStatsCounterUI.WriteStats();
        DOTween.Sequence()
            .Append(transform.DOMoveY(_endAnimationPoint.position.y + 10, 1))
            .Append(transform.DOMoveY(_endAnimationPoint.position.y, 0.2f))
            .SetLink(gameObject);
    }
}