using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    [SerializeField] AudioSource itemUsedAudio; // on item button parent obj
    [SerializeField] AudioClip timeItemClip, timeItemEndClip, destroyItemClip;
    [SerializeField] ItemButtonMenuController[] buttons;    //start menu buttons
    public SpellItem[] spellItems;

    [SerializeField] ItemButtonGameController[] gameButtons;
    public ItemButtonShopController[] shopButtons;

    SavedItems itemsInventory;


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
          LoadingItems();
        Invoke("PopulateButtons", 0.1f);

      

    }
    void PopulateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PopulateButton(spellItems[i], itemsInventory);

        }
    }
    public void OnGameStart()
    {
        foreach (ItemButtonGameController but in gameButtons)
        {
            but.buttonUsed = false;
        }
        SetUpGameButtons();


    }
    void SetUpGameButtons()
    {

        int i = 0;
        foreach (ItemButtonMenuController but in buttons)
        {
            //check waht items have been selected and use those in the game

            if (but.itemSelected)
            {
                gameButtons[i].PopulateButton(but.spell);
                UseItem(but.spellName, but);
                but.itemSelected = false;
            }
            i++;
        }
        foreach (ItemButtonGameController but in gameButtons)
        {
            if (!but.buttonUsed)
            {
                but.gameObject.SetActive(false);
            }

        }
    }

    void UseItem(string itemname, ItemButtonMenuController ibmc)
    {
        //using in game, not during game you know
        int newAmount = 0;
        // SavedItems tempItems = new SavedItems(0,0);
        if (itemname == "SlowTime")
        {
            newAmount = itemsInventory.timeAmount - 1;
            itemsInventory = new SavedItems(newAmount, itemsInventory.destroyAmount);
            ibmc.UpdateAmount(newAmount);
            
        }
        else if (itemname == "DestroyOrbs")
        {
            newAmount = itemsInventory.destroyAmount - 1;
            itemsInventory = new SavedItems(itemsInventory.timeAmount, newAmount);
            ibmc.UpdateAmount(newAmount);
            
        }

        //itemsInventory = new SavedItems(tempItems.timeAmount, tempItems.destroyAmount);
        SavingItems();

    }

    #region using items in game
    //using items in game in game
    public void UseItem(string itemName, Animator anim, GameObject go)
    {
        switch (itemName)
        {
            case "SlowTime":
                StartCoroutine(SlowDownTime(anim, go));
                break;
            case "DestroyOrbs":
                StartCoroutine(DestroyItem(anim, go));
                break;
            default:
                break;
        }

    }
    IEnumerator SlowDownTime(Animator anim, GameObject go)
    {
        Time.timeScale = 0.5f;
        anim.Play("circleExpand");
        itemUsedAudio.PlayOneShot(timeItemClip);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        anim.Play("circleShrink");
        itemUsedAudio.PlayOneShot(timeItemEndClip);
        yield return new WaitForSeconds(0.5f);
        go.SetActive(false);    //anim at some point with the same animator
    }
    IEnumerator DestroyItem(Animator anim, GameObject go)
    {
        anim.Play("circleExpand");
        itemUsedAudio.PlayOneShot(destroyItemClip);
        yield return new WaitForSeconds(0.2f);
        OrbManager.instance.DestroyOrbs();
        yield return new WaitForSeconds(0.2f);
        anim.Play("Default");
        
        go.SetActive(false);    //anim at some point with the same animator
    }

    #endregion

    public void AddingItems(string itemname)
    {
        int newAmount = 0;
        if (itemname == "SlowTime")
        {
            newAmount = itemsInventory.timeAmount + 1;
            itemsInventory = new SavedItems(newAmount, itemsInventory.destroyAmount);
            //ibmc.UpdateAmount(newAmount);
        }
        else if (itemname == "DestroyOrbs")
        {
            newAmount = itemsInventory.destroyAmount + 1;
            itemsInventory = new SavedItems(itemsInventory.timeAmount, newAmount);
            //ibmc.UpdateAmount(newAmount);
        }

        foreach(ItemButtonMenuController ib in buttons){
            if(ib.spellName == itemname){
                
                ib.UpdateAmount(newAmount);

            }
        }

        SavingItems();

    }

    void LoadingItems()
    {
        itemsInventory = SaveLoad.LoadItems();
    }
    void SavingItems()
    {
        SaveLoad.SaveItems(itemsInventory.timeAmount, itemsInventory.destroyAmount);
    }

}


[Serializable]
public class SavedItems
{
    public SavedItems(int time, int destroy)
    {
        timeAmount = time;
        destroyAmount = destroy;
    }
    public int timeAmount { get; }
    public int destroyAmount { get; }

}
