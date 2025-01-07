using UnityEngine;

[RequireComponent(typeof(LevelStatsCounterUI))]
public class Popup : MonoBehaviour
{
    [SerializeField] private LevelStatsCounterUI _levelStatsCounterUI;

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
    }
}