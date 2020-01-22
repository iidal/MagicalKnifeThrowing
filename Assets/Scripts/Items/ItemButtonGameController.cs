using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonGameController : MonoBehaviour
{
    string itemName;
    Image icon;
    Image buttonBG;
    public Button thisItemButton;
    bool itemUsed = false;  //one certain item per match so this goes to true when item has been used
    public bool buttonUsed = false;    //is the button populated with an item
    



    void Start()
    {
        thisItemButton = GetComponent<Button>();
        thisItemButton.interactable = true;

        buttonBG = GetComponent<Image>();

        GameObject temp = transform.Find("IconImage").gameObject;
        icon = temp.GetComponent<Image>();
    }

    public void PopulateButton(SpellItem spell){
        gameObject.SetActive(true);
        thisItemButton.interactable = true;
        itemName = spell.itemName;
        icon.sprite = spell.itemIcon;
        buttonBG.color = spell.itemBackgroundColor;
        buttonUsed = true;
    }
    public void UseItem(){
        thisItemButton.interactable = false;
        PlayerManager.instance.UseItem(itemName);
        buttonUsed = false;
    }

 
}
