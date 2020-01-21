using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] ItemButtonMenuController[] buttons;
    [SerializeField] SpellItem[] spellItems;

    void Start()
    {
        PopulateButtons();
    }
    void PopulateButtons(){
        for(int i = 0; i<buttons.Length; i++){
            buttons[i].PopulateButton(spellItems[i].itemIcon, spellItems[i].itemBackgroundColor);

        }
    }
    void OnGameStart(){
        //
        foreach(ItemButtonMenuController but in buttons){
            //check waht items have been selected and use those in the game
            
        }
    }

}
