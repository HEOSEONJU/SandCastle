using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using Unity.VisualScripting;

public class ADManager : MonoBehaviour
{
    static ADManager instance;

    public static ADManager Instance
    {
        get { return instance; }
    }

    public bool isTestMode;
    public Text LogText;
    public Button FrontAdsBtn, RewardAdsBtn;

    const string testDeviecId = "d3b47eeab14d4252";


    public const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    public const string frontTestID = "ca-app-pub-3940256099942544/8691691433";
    public void Awake()
    {

        if (instance == null)
        {
            instance = this;

            MobileAds.RaiseAdEventsOnUnityMainThread = true;//�⺻�����忡�� �ݹ�߻��ϰ����ִ±��


            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>  //�����ʱ�ȭ �۽����� �ѹ��� �����ϸ��
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




}


