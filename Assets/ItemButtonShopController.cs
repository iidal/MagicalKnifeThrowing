using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonShopController : MonoBehaviour
{
    [SerializeField] SpellItem spell;
    Image itemIcon;
    string itemName;
    int itemPrice;
    [SerializeField] ItemButtonMenuController[] menuButtons;

    void Start()
    {
        GameObject tempOb = transform.Find("ItemIcon").gameObject;
        //item icon
        itemIcon = tempOb.GetComponent<Image>();
        PopulateButton();
    }


    void PopulateButton()
    {
        itemIcon.sprite = spell.itemIcon;
        itemName = spell.itemName;
        itemPrice = spell.price;
    }
    public void BuyItem()
    {
        int coins = CoinManager.instance.CheckCoinAmount();
        if (coins > itemPrice)
        {
            foreach (ItemButtonMenuController itembutton in menuButtons)
            {
                if (itemName == itembutton.spellName)
                {
                    ItemManager.instance.AddingItems(itemName);
                    CoinManager.instance.UseCoins(itemPrice);
                }

            }
        }
    }
}
