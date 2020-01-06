using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField]
    Canvas thisCanvas;
    Animator thisAnimator;
    [SerializeField]
    GameObject beginningBlocker; // jsut a panel that is used to block the game view in the beginning of the game so we can use the same fade animation as everywhere else
    [SerializeField]
    Button startButton;

    void Start()
    {
        thisAnimator = GetComponent<Animator>();
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
        thisAnimator.Play("FadeStartMenu");

        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"));
        
        //audio back
        EnvironmentController.instance.AdjustingBGMusic("ToGame");
        //game starts
        GameManager.instance.StartGame();
        //
        startButton.enabled = true;
        thisCanvas.enabled = false;
    }
    IEnumerator AbsoluteBeginningofTheGame()
    {
        startButton.enabled = false;
        thisAnimator.Play("FadeStartMenuIn");
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"));
        yield return new WaitUntil(()=> AudioManager.instance.adjustingVolume == false);
        startButton.enabled = true;

    }

}
