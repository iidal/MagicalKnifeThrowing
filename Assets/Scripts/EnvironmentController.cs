using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public static EnvironmentController instance;
    Animator AnimBG;
    AudioSource BGmusicSource;
    [SerializeField]
    AudioClip menuMusicClip;
    [SerializeField]
    AudioClip gameMusicClip;


    //LIGHTNING STRIKE
    [SerializeField] ParticleSystem lightningParticle;
    [SerializeField] AudioSource lightningAudio;
    float lightningVolume = 0.15f;

    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        AnimBG = GetComponent<Animator>();
        BGmusicSource = GetComponent<AudioSource>();
        BGmusicSource.clip = menuMusicClip;
        BGmusicSource.volume = 0f;
        AudioManager.instance.FadeIn(BGmusicSource, 0.01f, 0.3f);
        BGmusicSource.Play();
    }

    public void StartLoopingEffects()
    {


        StartCoroutine("RandomLightning");

    }

    IEnumerator RandomLightning()
    {

        while (GameManager.instance.isGameOver == false)
        {

            LightiningStrike();
            float randDelay = Random.Range(20f, 40f);
            yield return new WaitForSeconds(randDelay);
        }
    }
    public void LightiningStrike()
    {
        lightningParticle.Play();
        lightningAudio.volume = lightningVolume;
        lightningAudio.Play();
        AudioManager.instance.FadeOut(lightningAudio, 0.02f, 0f);
        AnimBG.Play("BGLightning");
    }

    public void AdjustingBGMusic(string adjustment)
    {
        if (adjustment == "Exit")
        {
            AudioManager.instance.FadeOut(BGmusicSource, 0.005f, 0f);
        }
        else if (adjustment == "ToGame")
        {
            BGmusicSource.clip = gameMusicClip;
            BGmusicSource.volume = 0f;
            BGmusicSource.Play();
            AudioManager.instance.FadeIn(BGmusicSource, 0.01f, 0.3f);
        }
        else if (adjustment == "ToMenu")
        {
            BGmusicSource.clip = menuMusicClip;
            BGmusicSource.volume = 0f;
            BGmusicSource.Play();
            AudioManager.instance.FadeIn(BGmusicSource, 0.01f, 0.3f);
        }
    }
}
