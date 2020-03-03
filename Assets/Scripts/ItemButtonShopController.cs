using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonShopController : MonoBehaviour
{
    [SerializeField] SpellItem spell;
    public Image itemIcon;
    public Button buyButton;
    string itemName;
    int itemPrice;
    [SerializeField] ItemButtonMenuController[] menuButtons;


    void Start()
    {
        GameObject tempOb = transform.Find("ItemIcon").gameObject;
        //item icon
        itemIcon = tempOb.GetComponent<Image>();
        tempOb = transform.GetChild(1).gameObject;  //idk why using find didnt workd
        buyButton =  tempOb.GetComponent<Button>();
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
        if (coins >= itemPrice)
        {
            foreach (ItemButtonMenuController itembutton in menuButtons)
            {
                if (itemName == itembutton.spellName)
                {
                    ItemManager.instance.AddingItems(itemName);
                    CoinManager.instance.UseCoins(itemPrice);
                    
                    //coins = CoinManager.instance.CheckCoinAmount();
                    //if(coins< itemPrice){
                    //    buyButton.interactable = false;
                    //}
                }

            }
        }
    }
    public void CoinUpdate(int coins){
        Debug.Log("coun update");
        if(coins>= itemPrice){
            buyButton.interactable = true;
        }
        else{
            buyButton.interactable = false;
        }
    }
}
