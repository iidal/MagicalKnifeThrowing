using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField]
    Canvas thisCanvas;
    Animator thisAnimator;

    void Start()
    {
        thisAnimator = GetComponent<Animator>();
        thisCanvas.enabled = true;
    }


    public void StartGame()
    {
        StartCoroutine("Starting");

    }
    IEnumerator Starting()
    {
        EnvironmentController.instance.AdjustingBGMusic("Exit");
        thisAnimator.Play("FadeStartMenu");

        yield return new WaitForSeconds(2f);
       // EnvironmentController.instance.LightiningStrike(); already happening in normal lightning loop
        yield return new WaitUntil(() => thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"));
        EnvironmentController.instance.AdjustingBGMusic("ToGame");
        GameManager.instance.StartGame();
        thisCanvas.enabled = false;
    }

}
