﻿using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private Image[] _levelImages;
    [SerializeField] private Color _purchasedLevelImageColor;
    [SerializeField] private Color _notPurchasedLevelImageColor;

    private Upgrade _currentUpgrade;

    public Upgrade CurrentUpgrade
    {
        get => _currentUpgrade;
        set
        {
            if (_currentUpgrade != null)
                _currentUpgrade.CurrentLevelChanged -= ChangeLevelImagesColor;

            _currentUpgrade = value;
            _currentUpgrade.CurrentLevelChanged += ChangeLevelImagesColor;
            ChangeLevelImagesColor();
        }
    }

    public void ChangeLevelImagesColor()
    {
        if (_currentUpgrade.CurrentLevel == 0)
        {
            foreach (var levelImage in _levelImages)
                levelImage.color = _notPurchasedLevelImageColor;

            return;
        }

        for (int i = 0; i <= _currentUpgrade.CurrentLevel - 1; i++)
        {
            if (i == _currentUpgrade.MaxLevel)
                return;

            _levelImages[i].color = _purchasedLevelImageColor;
        }

        for (int i = _currentUpgrade.CurrentLevel; i < _currentUpgrade.MaxLevel; i++)
            _levelImages[i].color = _notPurchasedLevelImageColor;
    }
}