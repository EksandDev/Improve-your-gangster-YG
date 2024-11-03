using System;
using UnityEngine;

public class ChangeCharacterButton : MonoBehaviour
{
    [SerializeField] private bool _toNext;

    private Shop _shop;

    public void Inititalize(Shop shop)
    {
        _shop = shop;
    }

    public void OnClick()
    {
        if (_toNext)
        {
            if (_shop.SellableCharacters.Count - 1 > _shop.CurrentCharacterIndex)
            {
                _shop.CurrentCharacterIndex += 1;
                return;
            }

            _shop.CurrentCharacterIndex = 0;
        }

        else
        {
            if (_shop.SellableCharacters.Count - 1 <= _shop.CurrentCharacterIndex)
            {
                _shop.CurrentCharacterIndex -= 1;
                return;
            }

            _shop.CurrentCharacterIndex = _shop.SellableCharacters.Count - 1;
        }
    }
}
