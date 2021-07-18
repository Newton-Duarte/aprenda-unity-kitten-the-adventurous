using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    Shop _shop;
    [SerializeField] internal int idItem;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        _shop = FindObjectOfType(typeof(Shop)) as Shop;
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = _shop.spritesItems[idItem];
    }

    internal void openShop()
    {
        _shop.openShop(idItem);
    }
}
