using System;

public abstract class Upgrade : ISellable
{
    public event Action CurrentLevelChanged; 

    public bool IsPurchased { get; private set; }
    public int CurrentLevel { get; private set; }
    public int MaxLevel { get; private set; }
    public int Cost { get; private set; }

    public Upgrade(int cost, int maxLevel)
    {
        ChangeCost(cost);
        ChangeMaxLevel(maxLevel);
    }

    public abstract void Buy();

    public void LoadData(UpgradeSaves saves)
    {
        ChangeCost(saves.Cost);
        ChangePurchaseStatus(saves.IsPurchased);
        ChangeCurrentLevel(saves.CurrentLevel);
    }

    protected void ChangePurchaseStatus(bool value) => IsPurchased = value;
    protected void ChangeCurrentLevel(int value)
    {
        CurrentLevel = Math.Clamp(value, 0, MaxLevel);
        CurrentLevelChanged?.Invoke();
    }
    protected void ChangeMaxLevel(int value) => MaxLevel = Math.Clamp(value, 1, 10);
    protected void ChangeCost(int value) => Cost = value;
}