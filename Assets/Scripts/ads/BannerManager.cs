using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerManager : MonoBehaviour
{
    public static BannerManager instance;
    string placement = "MenuBanner";
    string gameID = "3855559";
   
   

    IEnumerator Start()
    {

        if(instance != null){
            Destroy(this);
        }
        else{
            instance = this;
        }

      
        Advertisement.Initialize(gameID, true);
        while(!Advertisement.IsReady(placement))
            yield return null;
        
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        ToggleMenuBanner(true);
        //Advertisement.Banner.Show(placement);
    }

    public void ToggleMenuBanner(bool show){

        if(show){
            Advertisement.Banner.Show(placement);
        }
        else{
            Advertisement.Banner.Hide();
        }
        

    }


    
}
