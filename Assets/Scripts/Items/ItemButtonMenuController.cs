using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButtonMenuController : MonoBehaviour
{

    [SerializeField] Button itemButton;
    public bool itemSelected = false;
    public Image itemIcon;

    public TextMeshProUGUI amountText;
    public GameObject selectionMark;
    //public Image buttonBase;
    public ParticleSystem[] bottleParticles;
    public SpellItem spell;



    int amount;
    public string spellName;

    void Start()
    {
        itemButton = GetComponent<Button>();

        GameObject tempOb = transform.Find("ItemIcon").gameObject;
        //item icon
        itemIcon = GetComponent<Image>();
        //Amount indicator
        tempOb = transform.Find("Amount").gameObject;
        amountText = tempOb.GetComponentInChildren<TextMeshProUGUI>();
        //item seleceted icon
        selectionMark = transform.Find("CheckMark").gameObject;
        selectionMark.SetActive(false);
        //button base on start menu
        //buttonBase = GetComponent<Image>();

        bottleParticles = new ParticleSystem[2];
        tempOb = transform.Find("BottleParticles").gameObject;
        //bottleParticles[0] = tempOb.GetComponent<ParticleSystem>();
        bottleParticles = tempOb.GetComponentsInChildren<ParticleSystem>();


    }

    public void ItemToggle()
    {
        if (itemSelected)
        {
            itemSelected = false;
            selectionMark.SetActive(false);
        }
        else
        {
            itemSelected = true;
            selectionMark.SetActive(true);
        }
    }

    public void PopulateButton(SpellItem spellOb, SavedItems itemamount)
    {
        spell = spellOb;
        itemIcon.sprite = spell.itemIcon;
        //buttonBase.color = spell.itemBackgroundColor;

        if (spell.itemName == "SlowTime")
        {
            amount = itemamount.timeAmount;
        }
        else if (spell.itemName == "DestroyOrbs")
        {
            amount = itemamount.destroyAmount;
        }
        amountText.text = amount.ToString();
        spellName = spell.itemName;

        foreach (ParticleSystem ps in bottleParticles)
        {
            var main = ps.main;
            Color temp = spell.itemBackgroundColor;
            main.startColor = temp;
        }


    }
    public void UpdateAmount(int amount)
    {
        amountText.text = amount.ToString();
        if (amount == 0)
        {
            itemButton.interactable = false;
            
            foreach (ParticleSystem ps in bottleParticles)
            {
                ps.Stop();
            }
        }
        else if (amount != 0 && itemButton.interactable == false)
        {
            itemButton.interactable = true;
            foreach (ParticleSystem ps in bottleParticles)
            {
                ps.Play();
            }
        }
        
        selectionMark.SetActive(false);
    }
}
