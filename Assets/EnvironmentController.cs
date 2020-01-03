using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public static EnvironmentController instance;
    Animator AnimBG;
    [SerializeField]
    ParticleSystem lightningParticle;

    void Start()
    {
        if(instance != null){
            Destroy(this);
        }
        else{
            instance = this;
        }

        AnimBG = GetComponent<Animator>();
    }

    public void StartLoopingEffects(){
        StartCoroutine("RandomLightning");
    }

    IEnumerator RandomLightning()
    {

        while (GameManager.instance.isGameOver == false)
        {
            float randDelay = Random.Range(10f, 30f);
            yield return new WaitForSeconds(randDelay);
            lightningParticle.Play();
            AnimBG.Play("BGLightning");
        }
    }


}
