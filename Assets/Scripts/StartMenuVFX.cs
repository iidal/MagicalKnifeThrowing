using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuVFX : MonoBehaviour
{

    public bool playParticles = true;

    [SerializeField]
    ParticleSystem logoEffect;
    [SerializeField]
    Transform[] logoEffectPositions; //play particle on different points on logo
    [SerializeField]ParticleSystem[] bottleEffects;
    [SerializeField]ParticleSystem startButtonEffect;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("ParticlesOnLogo");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    IEnumerator ParticlesOnLogo(){

        yield return new WaitForSeconds(1f);
        while(playParticles == true){
            logoEffect.Play();
            yield return new WaitForSeconds(3f);
            logoEffect.transform.position = logoEffectPositions[Random.Range(0, logoEffectPositions.Length-1)].position;
        }

    }
    public void TurnOnOff(bool on){
        if(on){
            foreach(ParticleSystem ps in bottleEffects){
                ps.Play();
            }
        }
        else{
            foreach(ParticleSystem ps in bottleEffects){
                ps.Stop();
            }
        }
    }

    public void StarButtonEffect(){
        startButtonEffect.Play();
    }
}
