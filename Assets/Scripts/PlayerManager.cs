using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   


    void Start()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("something is hitting");
        if(other.tag == "SpellOrb"){
            Debug.Log("game over");
            GameManager.instance.GameOver();
        }
    }
}
