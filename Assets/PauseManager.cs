using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool paused = false;
    [SerializeField]
    GameObject pauseMenu;
    void Start()
    {
        pauseMenu.SetActive(false);
    }



    public void PauseToggle(){
        if(paused){
            paused = false;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
        else{
            paused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }

    }
}
