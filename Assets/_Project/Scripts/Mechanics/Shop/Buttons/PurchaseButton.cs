using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    private Shop _shop;

    public ISellable Item { get; set; }

    public void Initialize(Shop shop)
    {
        _shop = shop;
    }

    public void Initialize(Shop shop, ISellable sellableItem)
    {
        Initialize(shop);
        Item = sellableItem;
    }

    public void OnClick()
    {
        if (_shop.TryBuyItem(Item))
        {

        }

        else
        {

        }
    }
}