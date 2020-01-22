using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButtonMenuController : MonoBehaviour
{
    
    public bool itemSelected = false;
    public Image itemIcon;
    public TextMeshProUGUI amountText;
    public GameObject selectionMark;
    public Image buttonBase;
    public SpellItem spell;
  

    void Start()
    {
        GameObject tempOb = transform.Find("ItemIcon").gameObject;
        //item icon
        itemIcon = tempOb.GetComponent<Image>();
        //Amount indicator
        tempOb = transform.Find("Amount").gameObject;
        amountText = tempOb.GetComponentInChildren<TextMeshProUGUI>();
        //item seleceted icon
        selectionMark = transform.Find("CheckMark").gameObject;
        selectionMark.SetActive(false);
        //button base on start menu
        buttonBase = GetComponent<Image>();
        

    }

    public void ItemToggle(){
        if(itemSelected){
            itemSelected = false;
            selectionMark.SetActive(false);
        }
        else{
            itemSelected = true;
            selectionMark.SetActive(true);
        }
    }

    public void PopulateButton(SpellItem spellOb){
        spell = spellOb;
        itemIcon.sprite = spell.itemIcon;
        buttonBase.color = spell.itemBackgroundColor;

        
        
        
        
    }
}
