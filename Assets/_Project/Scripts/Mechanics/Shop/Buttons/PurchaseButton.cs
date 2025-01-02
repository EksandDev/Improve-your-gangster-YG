using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PurchaseButton : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _purchaseSound;
    [SerializeField] private AudioClip _clickSound;

    private Shop _shop;

    public ISellable Item { get; set; }

    #region Validate
    private void OnValidate()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }
    #endregion

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
            _audioSource.PlayOneShot(_purchaseSound);
        }

        else
        {
            _audioSource.PlayOneShot(_clickSound);
        }
    }
}