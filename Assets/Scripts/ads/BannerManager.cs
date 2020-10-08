using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerManager : MonoBehaviour
{
    string placement = "MenuBanner";
    string gameID = "3855559";
   

    IEnumerator Start()
    {
      
        Advertisement.Initialize(gameID, true);
        while(!Advertisement.IsReady(placement))
            yield return null;
        
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placement);
    }


    
}
