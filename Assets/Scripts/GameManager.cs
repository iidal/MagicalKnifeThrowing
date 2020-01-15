using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public OrbManager orbManager;
    public StartMenuManager startMenuMG;

    public GameObject endMenu;
    [SerializeField]
    TextMeshProUGUI endText;
    [SerializeField]
    TextMeshProUGUI highScoreTextMenu;

    public bool isGameOver = false;
    public int level = 1; //value for changing speeds and spawn delays as game progresses. player wont see this

    //////
    int highScore =0;
    ///

    void Start()
    {
        if(instance != null){
            Destroy(this);
        }
        else{
            instance = this;
        }
        Invoke("LoadGame", 0.1f);
    }

    public void GameOver(){
        isGameOver = true;
        orbManager.DestroyOrbs();
        endMenu.SetActive(true);

        int score = PlayerManager.instance.score;

        if(score > highScore){
            highScore = score;
            endText.text = "New high score!"+ score.ToString() + "! Good job b\nget em coins";
            SaveLoad.SaveHighScore(score);

        }
        else{
            endText.text = "You scored " + score.ToString() + "\nget em coins";
        }
        CoinManager.instance.AddCoins(score);

    }

    public void StartGame(){
        isGameOver = false;
        level = 1;
        orbManager.StartCreatingOrbs();
        PlayerManager.instance.OnGameStart();
        EnvironmentController.instance.StartLoopingEffects();
        
    }
    #region  back to menu
    public void ReturnToMenu(){
        StartCoroutine("Return");
    }
    IEnumerator Return(){
        EnvironmentController.instance.AdjustingBGMusic("Exit");
        yield return new WaitForSeconds(2f);
        EnvironmentController.instance.AdjustingBGMusic("ToMenu");
        startMenuMG.BackToMenu();
    }
    #endregion

    void LoadGame(){
        highScore = SaveLoad.LoadHighScore();
        highScoreTextMenu.text = highScore.ToString();

        CoinManager.instance.LoadCoins();
    }

}
