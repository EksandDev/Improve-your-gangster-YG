public abstract class Upgrade : ISellable
{
    public int Cost => throw new System.NotImplementedException();

    public void OnBuyItem()
    {
        throw new System.NotImplementedException();
    }
}
