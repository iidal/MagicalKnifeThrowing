using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    Animator playerAnim;

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

        playerAnim = GetComponent<Animator>();
        
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
        RevivePlayer();
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "SpellOrb"){
            Debug.Log("game over");

            playerAnim.Play("playerDeath");

            GameManager.instance.EndGameOrAd();
        }
    }

    public void RevivePlayer(){
        playerAnim.Play("playerRevive");
    }


    
}
