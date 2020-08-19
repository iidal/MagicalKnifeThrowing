using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SpellItem", menuName = "ScriptableObjects/SpellItem", order = 1)]
public class SpellItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Color itemBackgroundColor;
    public Sprite itemIcon;
    public Sprite brokenItemLeft;
    public Sprite brokenItemRight;
    public int price;


}
