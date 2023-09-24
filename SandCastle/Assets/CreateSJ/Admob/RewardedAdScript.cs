using GoogleMobileAds.Api;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



//https://developers.google.com/admob/unity/rewarded?hl=ko
public class RewardedAdScript : MonoBehaviour
{
    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
  private string _adUnitId = "unused";
#endif

    static RewardedAdScript instance;


    public void Awake()
    {

        if (instance == null)
        {
            instance = this;

            
            MobileAds.RaiseAdEventsOnUnityMainThread = true;//기본스레드에서 콜백발생하게해주는기능


            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>  //광고초기화 앱실행중 한번만 실행하면됨
            {
                // This callback is called once the MobileAds SDK is initialized.
            });
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }




    }

    static public RewardedAdScript Instance
    {
        get { return instance; }
    }

    [SerializeField]
    bool state = false;
    

    public void Showad(ADFunction adf)
    {
        


        
        LoadRewardedAd(adf);
        
    }
    public void Showad()
    {




        LoadRewardedAd();

    }



    private RewardedAd rewardedAd;


    public void LoadRewardedAd()
    {
        if (state)
        {
            return;
        }

        state = true;
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    if (ad == null)
                    {

                    }
                    if (error != null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    }
                    state = false;
                    return;
                }


                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                state = false;
                ShowRewardedAd();
            });
    }

    public void ShowRewardedAd()//보상형광고 출시
    {
        const string rewardMsg = "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd == null)
        {

        }
        if (!rewardedAd.CanShowAd())
        {

        }


        if (rewardedAd != null && rewardedAd.CanShowAd())
        {

            rewardedAd.Show((Reward reward) =>
            {
                //보상리스트작성하면됨
                PlayerDataManager.Instacne.Data.UnlockChar();
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }



    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    public void LoadRewardedAd(ADFunction adf)
    {
        if(state)
        {
            return;
        }

        state= true;
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");
        
        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
            // if error is not null, the load request failed.
            if (error != null || ad == null)
            {
                    if(ad==null)
                    {
                        
                    }
                    if(error != null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    }
                    state = false;
                    return;
                }
                
                
                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                state = false;
                ShowRewardedAd(adf);
            });
    }



    public void ShowRewardedAd(ADFunction adf)//보상형광고 출시
    {
        const string rewardMsg ="Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
        
        if(rewardedAd == null)
        {
            
        }
        if (!rewardedAd.CanShowAd())
        {
            
        }


        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            
            rewardedAd.Show((Reward reward) =>
            {
                //보상리스트작성하면됨
                adf.Reward();
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        //ad.OnAdFullScreenContentClosed += ()
    {
            Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
          //  LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            //LoadRewardedAd();
        };
    }
}
