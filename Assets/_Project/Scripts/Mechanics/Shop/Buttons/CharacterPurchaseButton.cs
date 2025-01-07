using TMPro;
using UnityEngine;

public class CharacterPurchaseButton : PurchaseButton
{
    [SerializeField] private TMP_Text _costText;

    private int _cost;

    public int Cost
    {
        get => _cost;
        set
        {
            if (value <= 0)
                return;

            _cost = value;
            _costText.text = _cost.ToString();
        }
    }

    public void SetActive(bool value)
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(value);

        if (value)
            _costText.text = _cost.ToString();
    }
}