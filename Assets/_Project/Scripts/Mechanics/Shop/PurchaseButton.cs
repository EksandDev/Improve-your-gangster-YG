using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    private ISellable _item;

    public void Initialize(ISellable purchaseItem)
    {
        _item = purchaseItem;
    }

    public void OnClick()
    {
    }
}