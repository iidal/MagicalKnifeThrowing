using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    

    public int score = 0;
    [SerializeField]
    TextMeshProUGUI scoreText;


    void Start()
    {
        if(instance != null){
            Destroy(this);
        }
        else{
            instance = this;
        }
        
    }


    public void AddPoints(){
        score++;
        scoreText.text = score.ToString();
        if(score % 10 == 0){
            GameManager.instance.level++;
        }
    }

    public void OnGameStart(){
        score = 0;
        scoreText.text = score.ToString();
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "SpellOrb"){
            Debug.Log("game over");



            GameManager.instance.EndGameOrAd();
        }
    }


    
}
