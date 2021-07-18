using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    GameController _gameController;
    Player _player;
    [SerializeField] GameObject shopPanel;
    [SerializeField] internal Sprite[] spritesItems;
    [SerializeField] string[] itemsTitles;
    [SerializeField] string[] itemsText;
    [SerializeField] int[] itemsPrices;
    [SerializeField] Text txtItemTitle;
    [SerializeField] Text txtItemPrice;
    [SerializeField] Text txtItemText;
    [SerializeField] Image itemImage;
    int idItem;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _player = FindObjectOfType(typeof(Player)) as Player;
    }

    internal void openShop(int idShopItem)
    {
        idItem = idShopItem;
        itemImage.sprite = spritesItems[idShopItem];
        txtItemTitle.text = itemsTitles[idShopItem];
        txtItemPrice.text = itemsPrices[idShopItem].ToString();
        txtItemText.text = itemsText[idShopItem];
        Time.timeScale = 0;
        shopPanel.SetActive(true);
    }

    public void closeShop()
    {
        Time.timeScale = 1;
        shopPanel.SetActive(false);
    }

    public void buyItem()
    {
        int itemPrice = itemsPrices[idItem];

        if (_gameController.coins < itemPrice) { return; }

        _gameController.addCoins(-itemPrice);

        _player.updateItems(idItem);

        var foundShopItems = FindObjectsOfType(typeof(ShopItem));

        foreach(ShopItem shopItem in foundShopItems)
        {
            if (shopItem.idItem == idItem) { Destroy(shopItem.gameObject); }
        }
        closeShop();
    }
}
