using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonGameController : MonoBehaviour
{
    string itemName;
    Image icon;
    //Image buttonBG;
    public Button thisItemButton;
    bool itemUsed = false;  //one certain item per match so this goes to true when item has been used
    public bool buttonUsed = false;    //is the button populated with an item
    
    ParticleSystem[] bottleParticles;
    Animator buttonAnim;


    void Start()
    {
        thisItemButton = GetComponent<Button>();
        thisItemButton.interactable = true;

        //buttonBG = GetComponent<Image>();

        GameObject temp = transform.Find("IconImage").gameObject;
        icon = temp.GetComponent<Image>();

         bottleParticles = new ParticleSystem[2];
        //temp = transform.Find("BottleParticles").gameObject;
        //bottleParticles[0] = tempOb.GetComponent<ParticleSystem>();
        bottleParticles = temp.GetComponentsInChildren<ParticleSystem>();
        buttonAnim = GetComponent<Animator>();
    }

    public void PopulateButton(SpellItem spell){    //preparing button to be used in game
        gameObject.SetActive(true);
        thisItemButton.interactable = true;
        itemName = spell.itemName;
        icon.sprite = spell.itemIcon;
        //buttonBG.color = spell.itemBackgroundColor;

        foreach(ParticleSystem ps in bottleParticles){
            var main = ps.main;
            Color temp = spell.itemBackgroundColor;
            main.startColor = temp;
            ps.Play();
        }

        buttonUsed = true;
    }
    public void UseItem(){  //using in game
        thisItemButton.interactable = false;
        ItemManager.instance.UseItem(itemName, buttonAnim, this.gameObject);
        
        

        buttonUsed = false;


        //gameObject.SetActive(false); //switch to animation at some point
    }

 
}
