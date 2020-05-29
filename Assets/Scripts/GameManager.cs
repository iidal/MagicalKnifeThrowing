﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public OrbManager orbManager;
    public StartMenuManager startMenuMG;

    AudioSource gmSource;
    [SerializeField] AudioClip exitingGameClip;
    [SerializeField] AudioClip orbHitSound;

    public GameObject endMenu;
    [SerializeField]
    TextMeshProUGUI endText;
    [SerializeField]
    TextMeshProUGUI highScoreTextMenu;

    [SerializeField]
    GameObject endMenuPart1, endMenuPart2;

    public bool isGameOver = false;
    public int level = 1; //value for changing speeds and spawn delays as game progresses. player wont see this

    bool adWatched = false;

    //////
    int highScore = 0;
    ///

    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        gmSource = GetComponent<AudioSource>();
        Invoke("LoadGame", 0.1f);
    }

    public void EndGameOrAd(){
        gmSource.PlayOneShot(orbHitSound);
        orbManager.DestroyOrbs();
        StartCoroutine("AdOrEndGame");
    }
    IEnumerator AdOrEndGame(){

        

        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.5f);

        endMenu.SetActive(true);
        if (!adWatched)
        {
            
            isGameOver = true;
            
        }
        else
        {
            
            GameOver();
        }
    }

    public void WatchAd()
    {
        StartCoroutine("WatchedAd");
      
    }

    IEnumerator WatchedAd(){
        yield return new WaitForSeconds(1.5f);
          isGameOver = false;
        orbManager.StartCreatingOrbs();
        EnvironmentController.instance.StartLoopingEffects();
        adWatched = true;
    }

    public void GameOver()
    {
        orbManager.DestroyOrbs();
        endMenuPart1.SetActive(false);
        endMenuPart2.SetActive(true);
        isGameOver = true;


        int score = PlayerManager.instance.score;

        if (score > highScore)
        {
            highScore = score;
            endText.text = "New high score!" + score.ToString() + "! Good job b\nget em coins";
            SaveLoad.SaveHighScore(score);
            startMenuMG.UpdateHighScore(score);

        }
        else
        {
            endText.text = "You scored " + score.ToString() + "\nget em coins";
        }
        CoinManager.instance.AddCoins(score);

    }

    public void StartGame()
    {
        isGameOver = false;
        adWatched = false;
        level = 1;
        orbManager.StartCreatingOrbs();
        //PlayerManager.instance.OnGameStart(); //too late if called here
        EnvironmentController.instance.StartLoopingEffects();

        endMenuPart1.SetActive(true);
        endMenuPart2.SetActive(false);

    }
    #region  back to menu
    public void ReturnToMenu()
    {
        StartCoroutine("Return");
    }
    IEnumerator Return()
    {
        //gmSource.PlayOneShot(exitingGameClip);
        EnvironmentController.instance.AdjustingBGMusic("Exit");
        yield return new WaitForSeconds(2f);
        EnvironmentController.instance.AdjustingBGMusic("ToMenu");
        startMenuMG.BackToMenu();
    }
    #endregion

    void LoadGame()
    {
        highScore = SaveLoad.LoadHighScore();
        highScoreTextMenu.text = highScore.ToString();

        CoinManager.instance.LoadCoins();
    }

}
