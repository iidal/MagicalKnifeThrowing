using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemButtonGameController : MonoBehaviour
{
    string itemName;
    Image icon;
    public Image [] brokenIcon;
    //Image buttonBG;
    public Button thisItemButton;
    [SerializeField]public bool itemUsed = false;  //one certain item per match so this goes to true when item has been used
    [SerializeField]public bool buttonUsed = false;    //is the button populated with an item
    
    ParticleSystem[] bottleParticles;
    [SerializeField]public Animator buttonAnim;


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
        brokenIcon = temp.GetComponentsInChildren<Image>(true);
        brokenIcon = brokenIcon.Skip(1).ToArray();  // we do not want the first found image, skip it

    }

    public void PopulateButton(SpellItem spell){    //preparing button to be used in game
        gameObject.SetActive(true);
        thisItemButton.interactable = true;
        itemName = spell.itemName;
        icon.sprite = spell.itemIcon;
        brokenIcon[0].sprite = spell.brokenItemLeft;
        brokenIcon[1].sprite = spell.brokenItemRight;

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
