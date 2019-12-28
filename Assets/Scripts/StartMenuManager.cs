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
        thisAnimator.Play("FadeStartMenu");
        yield return new WaitForEndOfFrame();
        
        yield return new WaitUntil(() => thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"));
        GameManager.instance.StartGame();
        thisCanvas.enabled = false;
    }
}
