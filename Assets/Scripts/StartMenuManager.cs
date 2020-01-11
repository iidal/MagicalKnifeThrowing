using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField]
    Canvas thisCanvas;
    [SerializeField]
    Animator fadeAnimator;
    [SerializeField]
    GameObject beginningBlocker; // jsut a panel that is used to block the game view in the beginning of the game so we can use the same fade animation as everywhere else
    [SerializeField]
    Button startButton;
    [SerializeField]
    StartMenuVFX menuEffects;

    void Start()
    {
        
        thisCanvas.enabled = true;
        
        StartCoroutine("AbsoluteBeginningofTheGame");
    }


    public void StartGame()
    {
        StartCoroutine("Starting");

    }
    IEnumerator Starting()
    {
        startButton.enabled = false;
        beginningBlocker.SetActive(false);  //this is only active after starting the game so this line is useless after that but still needs to be here because we are lazy
        //audio
        EnvironmentController.instance.AdjustingBGMusic("Exit");
        //animation
        fadeAnimator.Play("FadeStartMenu");
        menuEffects.playParticles = false;

        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"));
        
        fadeAnimator.Play("FadeStartMenuIn"); //doesnt actually fade start menu in just the panel
        //audio back
        EnvironmentController.instance.AdjustingBGMusic("ToGame");
       
        startButton.enabled = true;
        thisCanvas.enabled = false;
        yield return new WaitForSeconds(2f);
        GameManager.instance.StartGame();
     
        
        
    }

    public void BackToMenu(){
         fadeAnimator.Play("FadeStartMenuIn"); //doesnt actually fade start menu in just the panel
        thisCanvas.enabled=true;
        menuEffects.playParticles = true;
    }


    IEnumerator AbsoluteBeginningofTheGame()
    {
        startButton.enabled = false;
        fadeAnimator.Play("FadeStartMenuIn");
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"));
        yield return new WaitUntil(()=> AudioManager.instance.adjustingVolume == false);
        startButton.enabled = true;

    }

}
