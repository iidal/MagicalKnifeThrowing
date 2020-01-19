using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] Canvas thisCanvas;
    Animator startPanelAnimator;
    [SerializeField] Animator fadeAnimator;

    [SerializeField] Button startButton;
    [SerializeField] StartMenuVFX menuEffects;

    [SerializeField] GameObject[] otherPanels;   // panels in start menu other than the first
    GameObject currentPanel; //panel currently showing

    [SerializeField] TextMeshProUGUI highScoreText;

    void Start()
    {
        startPanelAnimator = GetComponent<Animator>();
        thisCanvas.enabled = true;

        StartCoroutine("AbsoluteBeginningofTheGame");

        foreach(GameObject go in otherPanels){
            go.SetActive(false);
        }
    }


    public void StartGame()
    {
        StartCoroutine("Starting");

    }
    IEnumerator Starting()
    {
        startButton.enabled = false;
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

    public void BackToMenu()
    {
        fadeAnimator.Play("FadeStartMenuIn"); //doesnt actually fade start menu in just the panel
        thisCanvas.enabled = true;
        menuEffects.playParticles = true;
    }


    IEnumerator AbsoluteBeginningofTheGame()
    {
        startButton.enabled = false;
        fadeAnimator.Play("FadeStartMenuIn");
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"));
        yield return new WaitUntil(() => AudioManager.instance.adjustingVolume == false);
        startButton.enabled = true;

    }

    public void UpdateHighScore(int score){
        highScoreText.text = score.ToString();
    }

    #region start menu panel switch
    public void SwitchPanel(string panelName){
        StartCoroutine(SwitchView(panelName));
    }
    IEnumerator SwitchView(string panelName)
    {
        if (panelName == "start")
        {   //returning to first panel
            startPanelAnimator.Play("startPanelShow");
            if(currentPanel != null){
                yield return new WaitUntil(()=> startPanelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"));
                currentPanel.SetActive(false);
            }
        }
        else
        {
            switch(panelName){
                case "help":
                    currentPanel = otherPanels[0];
                    break;
                case "settings":
                    currentPanel = otherPanels[1];
                    break;
                default:
                    break;
            }
            currentPanel.SetActive(true);
            startPanelAnimator.Play("startPanelHide");
        }
    }

    #endregion
}
