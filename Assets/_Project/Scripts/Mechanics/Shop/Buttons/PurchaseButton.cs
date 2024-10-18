using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    private Shop _shop;
    private ISellable _item;

    public ISellable Item => _item;

    public void Initialize(ISellable purchaseItem)
    {
        _item = purchaseItem;
    }

    public void OnClick()
    {
        if (_shop.TryBuyItem(_item))
        {

        }

        else
        {

        }
    }
}