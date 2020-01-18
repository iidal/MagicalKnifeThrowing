using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    void Start()
    {   
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this);
        }
        Invoke("CheckAndApplySettings", 0.2f);
    }

    void CheckAndApplySettings(){

        //AUDIO
        //0 = off, 1 = 0n
        int audioBool = PlayerPrefs.GetInt("audioOn", 1);
        if(audioBool == 1){
            if(!AudioManager.instance.audioOn){
                AudioManager.instance.ToggleAudio();
            }
        }else if(audioBool ==0){
            if(AudioManager.instance.audioOn){
                AudioManager.instance.ToggleAudio();
            }
        }
        else{
            Debug.Log("audio playerprefs is fucked up");
        }

    }
    public void SaveAudioSettings(bool isOn){
        if(isOn)
            PlayerPrefs.SetInt("audioOn", 1);
        else
            PlayerPrefs.SetInt("audioOn", 0);
    }

}
