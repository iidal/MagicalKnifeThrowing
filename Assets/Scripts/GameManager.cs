using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public OrbManager orbManager;

    public GameObject endMenu;
    public bool isGameOver = false;
    public int level = 1; //value for changing speeds and spawn delays as game progresses. player wont see this

    void Start()
    {
        if(instance != null){
            Destroy(this);
        }
        else{
            instance = this;
        }
    }

    public void GameOver(){
        isGameOver = true;
        orbManager.DestroyOrbs();
        endMenu.SetActive(true);
    }

    public void StartGame(){
        isGameOver = false;
        level = 1;
        orbManager.StartCreatingOrbs();
        PlayerManager.instance.OnGameStart();
        
    }

}
