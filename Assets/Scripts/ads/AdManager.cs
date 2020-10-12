using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    string placement = "rewardedVideo";
    string gameID = "3855559";
   
   AudioSource adAudio;
   public AudioClip coinRewardAudio;
   public GameObject confirmReviveObj;

    void Start()
    {   
        confirmReviveObj.SetActive(false);
        adAudio = GetComponent<AudioSource>();
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, true);
    }

    public void ShowAd(string adType){

        Advertisement.Show(adType);

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
       
        if(showResult == ShowResult.Finished){
            
            if(placementId == "CoinsEarned"){
                
                CoinManager.instance.AddCoins(25);
                adAudio.PlayOneShot(coinRewardAudio);
            }
            else if(placementId == "rewardedVideo"){
                
                confirmReviveObj.SetActive(true);
                

            }
        }
        else if(showResult == ShowResult.Failed){
            //o ou
        }

    }


    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
    }
    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

}
