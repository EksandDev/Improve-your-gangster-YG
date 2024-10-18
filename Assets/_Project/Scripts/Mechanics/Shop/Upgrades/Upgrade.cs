using System;

public abstract class Upgrade : ISellable
{
    private bool _isPurchased;
    private int _currentLevel;
    private int _cost;

    public bool IsPurchased => _isPurchased;
    public int CurrentLevel => _currentLevel;
    public int Cost => _cost;

    public Upgrade(int cost)
    {
        _cost = cost;
    }

    public abstract void Buy();

    protected void ChangePurchaseStatus(bool value) => _isPurchased = value;
    protected void ChangeCurrentLevel(int value, int maxValue) => _currentLevel = Math.Clamp(value, 0, maxValue);
    protected void ChangeCost(int value) => _cost = value;
}
