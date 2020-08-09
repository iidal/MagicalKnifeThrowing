using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]
    Sprite audioOffIcon;
    [SerializeField]
    Sprite audioOnIcon;
    [SerializeField]
    Image audioIcon, audioIconFront, inGameAudioIcon;
    public bool audioOn = true;


    // all audiosources need to be referenced in these lists so they can be all disabled and enabled when audio is toggled
    //sike not doing that
    //using audiolistener lol
    [SerializeField]
    List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField]
    // List<AudioSource> musicAudioSources = new List<AudioSource>();

    public AudioSource testSource;

    public bool adjustingVolume = false;

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

    }

    public void FadeOut(AudioSource audioSource, float fadeSpeed, float targetVolume)
    {
        StartCoroutine(FadeOutSound(audioSource, fadeSpeed, targetVolume));
    }
    IEnumerator FadeOutSound(AudioSource audioSource, float fadeSpeed, float targetVolume)
    {
        adjustingVolume = true;
        float tempVol = audioSource.volume;
        float ogVol = tempVol;
        float t = 0f;

        while (tempVol > 0f)
        {
            tempVol = Mathf.Lerp(ogVol, targetVolume, t);
            t += Time.deltaTime;
            audioSource.volume = tempVol;
            yield return new WaitForSeconds(fadeSpeed);
        }
        adjustingVolume = false;
        //audioSource.volume = ogVol; // put volume back on so it will play more than once if needed


    }
    public void FadeIn(AudioSource audioSource, float fadeSpeed, float targetVolume)
    {
        StartCoroutine(FadeInSound(audioSource, fadeSpeed, targetVolume));
    }
    IEnumerator FadeInSound(AudioSource audioSource, float fadeSpeed, float targetVolume)
    {
        adjustingVolume = true;
        float tempVol = audioSource.volume;
        float t = 0f;
        while (tempVol < targetVolume)
        {
            tempVol = Mathf.Lerp(0, targetVolume, t);
            t += Time.deltaTime;
            audioSource.volume = tempVol;
            yield return new WaitForSeconds(fadeSpeed);
        }
        adjustingVolume = false;


    }

    public void ToggleAudio()
    {
        if (audioOn)
        {
            audioOn = false;
            audioIcon.sprite = audioOffIcon;
            audioIconFront.sprite = audioOffIcon;
            //inGameAudioIcon.sprite = audioOffIcon;
            AudioListener.volume = 0;
            SettingsManager.instance.SaveAudioSettings(false);

        }
        else if (!audioOn)
        {
            audioOn = true;
            audioIcon.sprite = audioOnIcon;
            audioIconFront.sprite = audioOnIcon;
            //inGameAudioIcon.sprite = audioOnIcon;
            AudioListener.volume = 1;
            SettingsManager.instance.SaveAudioSettings(true);

        }

    }

    public void WarpVolume(bool down)
    {
        StartCoroutine("VolumeWarp", down);
        Debug.Log("warp");
    }
    IEnumerator VolumeWarp(bool down)
    {
        //NO WORKY YET


        //time warp item
        float t = 0;
        float p; //new pitch
        float minimum = 0.6f;
        float maximum = 1f; ;
        float checker = maximum; ; //check when target is reached (maximum value)

        //kinda dumb way to do this but well im dumb too 
        //down == true, pitch goes down
        if (down)
        {   
            p = 1f;
            checker = minimum;
            while (p > checker)
            {
                p = Mathf.Lerp(maximum, minimum, t);


                t += 8f * Time.deltaTime;

                foreach (AudioSource source in audioSources)
                {
                    source.pitch = p;


                }
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {   
            p = 0f;
            checker = maximum;
            while (p < checker)
            {
                p = Mathf.Lerp(minimum, maximum, t);


                t += 8f * Time.deltaTime;

                foreach (AudioSource source in audioSources)
                {
                    source.pitch = p;


                }
                yield return new WaitForSeconds(0.05f);
            }
        }

    }
}