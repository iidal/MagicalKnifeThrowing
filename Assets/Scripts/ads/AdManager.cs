using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    string placement = "rewardedVideo";
    string gameID = "3855559";
   

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, true);
        // while(!Advertisement.IsReady(placement))
        //     yield return null;
        // Advertisement.Show(placement);
    }

    public void ShowAd(string adType){

        Advertisement.Show(adType);

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //throw new System.NotImplementedException();
        if(showResult == ShowResult.Finished){
            Debug.Log("reward");
            if(placementId == "CoinsEarned"){
                Debug.Log("coins");
                CoinManager.instance.AddCoins(25);
            }
            else if(placementId == "rewardedVideo"){
                Debug.Log("revive");
                GameManager.instance.WatchAd();

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
