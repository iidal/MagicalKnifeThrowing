using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    int coins = 0;

    [SerializeField]
    TextMeshProUGUI menuCoinsText;
    void Start()
    {
        if(instance != null){
            Destroy(this);
        }
        else{
            instance = this;
        }
    }

    public void LoadCoins(){
        coins = SaveLoad.LoadCoins();
        menuCoinsText.text = coins.ToString();

    }
    public void AddCoins(int co){
        coins += co;
        SaveLoad.SaveCoins(coins);
        menuCoinsText.text = coins.ToString();
    }
    public void UseCoins(int price){
        coins -= price;
        SaveLoad.SaveCoins(coins);
        menuCoinsText.text = coins.ToString();

    }
    public int CheckCoinAmount(){
        return coins;
    }
}
