using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    
    [SerializeField] ItemButtonMenuController[] buttons;    //start menu buttons
    public SpellItem[] spellItems;

    [SerializeField] ItemButtonGameController[] gameButtons;


    void Start()
    {
        Invoke("PopulateButtons", 0.1f);

    }
    void PopulateButtons(){
        for(int i = 0; i<buttons.Length; i++){
            buttons[i].PopulateButton(spellItems[i]);
            
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
            }
            i++;
        }
        foreach(ItemButtonGameController but in gameButtons){
            if(!but.buttonUsed){
                but.gameObject.SetActive(false);
            }

        }
    }

}
