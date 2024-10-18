public interface ISellable
{
    public int Cost { get; }
    public bool IsPurchased { get; }

    public void Buy();
}
