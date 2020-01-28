using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemManager : MonoBehaviour
{
    
    [SerializeField] ItemButtonMenuController[] buttons;    //start menu buttons
    public SpellItem[] spellItems;

    [SerializeField] ItemButtonGameController[] gameButtons;

    SavedItems itemsInventory;


    void Start()
    {

        LoadingItems();
        Invoke("PopulateButtons", 0.1f);



    }
    void PopulateButtons(){
        for(int i = 0; i<buttons.Length; i++){
            buttons[i].PopulateButton(spellItems[i], itemsInventory);
            
        }
    }
    public void OnGameStart(){
         foreach(ItemButtonGameController but in gameButtons){
                but.buttonUsed = false;
        }
        SetUpGameButtons();
        

    }
    void SetUpGameButtons(){

        int i = 0;
        foreach(ItemButtonMenuController but in buttons){
            //check waht items have been selected and use those in the game
            
            if(but.itemSelected){
                gameButtons[i].PopulateButton(but.spell);
                UseItem(but.spellName, but);
            }
            i++;
        }
        foreach(ItemButtonGameController but in gameButtons){
            if(!but.buttonUsed){
                but.gameObject.SetActive(false);
            }

        }
    }

    void UseItem(string itemname, ItemButtonMenuController ibmc){
        //using in game, not during game
        int newAmount =0;
       // SavedItems tempItems = new SavedItems(0,0);
        if(itemname == "SlowTime"){
            newAmount = itemsInventory.timeAmount -1;
            itemsInventory= new SavedItems(newAmount, itemsInventory.destroyAmount);
            ibmc.UpdateAmount(newAmount);
        }
        else if(itemname == "DestroyOrbs"){
            newAmount = itemsInventory.destroyAmount -1;
            itemsInventory = new SavedItems(itemsInventory.timeAmount, newAmount);
            ibmc.UpdateAmount(newAmount);
        }
        
        //itemsInventory = new SavedItems(tempItems.timeAmount, tempItems.destroyAmount);
        SavingItems();
        
    }



    void LoadingItems(){
        itemsInventory = SaveLoad.LoadItems();
    }
    void SavingItems(){
        SaveLoad.SaveItems(itemsInventory.timeAmount, itemsInventory.destroyAmount);
    }

}
[Serializable]  
public class SavedItems{
    public SavedItems(int time, int destroy)
    {
        timeAmount = time;
        destroyAmount = destroy;
    }
    public int timeAmount {get;}
    public int destroyAmount {get;}

}
