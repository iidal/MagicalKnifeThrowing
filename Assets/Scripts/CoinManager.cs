using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [SerializeField] ItemButtonShopController[] shopButtons;
    int coins = 0;

    [SerializeField] TextMeshProUGUI menuCoinsText, storeCoinsText;


    
    // void OnEnable(){
        
    //     //Invoke("UpdateShopStatus", .1f);
    //     UpdateShopStatus();
    // }

    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

    }

    public void LoadCoins()
    {
        coins = SaveLoad.LoadCoins();
        menuCoinsText.text = coins.ToString();
        storeCoinsText.text = coins.ToString();
        UpdateShopStatus();

    }
    public void AddCoins(int co)
    {
        coins += co;
        SaveLoad.SaveCoins(coins);
        menuCoinsText.text = coins.ToString();
        storeCoinsText.text = coins.ToString();

       UpdateShopStatus();

    }
    public void UseCoins(int price)
    {
        coins -= price;
        SaveLoad.SaveCoins(coins);
        menuCoinsText.text = coins.ToString();
        storeCoinsText.text = coins.ToString();
        UpdateShopStatus();
    }

    public void UpdateShopStatus(){
        //should the buttons be interactable or not based on coins and prices
         foreach (ItemButtonShopController but in shopButtons)
        {
            but.CoinUpdate(coins);
        }
    }

    public int CheckCoinAmount()
    {
        return coins;
    }
}
