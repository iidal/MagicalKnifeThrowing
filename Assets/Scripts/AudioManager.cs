using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    // all audiosources need to be referenced in these lists so they can be all disabled and enabled when audio is toggled
    [SerializeField]
    List<AudioSource> effectAudioSources = new List<AudioSource>();
    [SerializeField]
    List<AudioSource> musicAudioSources = new List<AudioSource>();

    public AudioSource testSource;

    public bool adjustingVolume = false;

    void Start()
    {
        if(instance != null){
            Destroy(this);
        }
        else{
            instance = this;
        }
        
    }

    public void FadeOut(AudioSource audioSource, float fadeSpeed, float targetVolume){
        StartCoroutine(FadeOutSound(audioSource, fadeSpeed, targetVolume));
    }
    IEnumerator FadeOutSound(AudioSource audioSource, float fadeSpeed, float targetVolume){
        adjustingVolume = true;
        float tempVol = audioSource.volume;
        float ogVol = tempVol;
        float t = 0f;
        
        while(tempVol>0f){
            tempVol = Mathf.Lerp(ogVol, targetVolume, t);
            t += Time.deltaTime;
            audioSource.volume = tempVol;
            yield return new WaitForSeconds(fadeSpeed);
        }
        adjustingVolume = false;
        //audioSource.volume = ogVol; // put volume back on so it will play more than once if needed
        

    }
    public void FadeIn(AudioSource audioSource, float fadeSpeed, float targetVolume){
        StartCoroutine(FadeInSound(audioSource, fadeSpeed, targetVolume));
    }
    IEnumerator FadeInSound(AudioSource audioSource, float fadeSpeed, float targetVolume){
        adjustingVolume = true;
        float tempVol = audioSource.volume;
        float t = 0f;
        while(tempVol<targetVolume){
            tempVol = Mathf.Lerp(0, targetVolume, t);
            t += Time.deltaTime;
            audioSource.volume = tempVol;
            yield return new WaitForSeconds(fadeSpeed);
        }
        adjustingVolume = false;
        

    }
}
