using System;

public abstract class Upgrade : ISellable
{
    private bool _isPurchased;
    private int _currentLevel;
    private int _maxLevel;
    private int _cost;

    public event Action CurrentLevelChanged; 

    public bool IsPurchased => _isPurchased;
    public int CurrentLevel => _currentLevel;
    public int MaxLevel => _maxLevel;   
    public int Cost => _cost;

    public Upgrade(int cost, int maxLevel)
    {
        ChangeCost(cost);
        ChangeMaxLevel(maxLevel);
    }

    public abstract void Buy();

    protected void ChangePurchaseStatus(bool value) => _isPurchased = value;
    protected void ChangeCurrentLevel(int value)
    {
        _currentLevel = Math.Clamp(value, 0, MaxLevel);
        CurrentLevelChanged?.Invoke();
    }
    protected void ChangeMaxLevel(int value) => _maxLevel = Math.Clamp(value, 1, 10);
    protected void ChangeCost(int value) => _cost = value;
}