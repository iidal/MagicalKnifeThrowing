using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public OrbManager orbManager;
    public GameObject endMenu;
    public bool isGameOver = false;

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
        orbManager.StartCreatingOrbs();
        
    }

}
