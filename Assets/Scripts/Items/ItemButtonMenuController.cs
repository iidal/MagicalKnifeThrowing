using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButtonMenuController : MonoBehaviour
{
    
    [HideInInspector] public bool itemSelected = false;
    Image itemIcon;
    TextMeshProUGUI amountText;
    GameObject selectionMark;
    Image buttonBase;

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

    public void PopulateButton(Sprite icon, Color buttonColor){
        itemIcon.sprite = icon;
        buttonBase.color = buttonColor;
    }
}
