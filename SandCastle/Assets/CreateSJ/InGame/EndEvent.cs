using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndEvent : MonoBehaviour
{
    static EndEvent instance;


    [SerializeField]
    GameObject stop;
    [SerializeField]
    GameObject sucess;

    private void Awake()
    {
        instance = this;
    }

    public static EndEvent Instance
    {
        get 
        {
            return instance; 
        }
    }

    public void EndEventActive()
    {
        stop.SetActive(true);
    }


    public void ReTry()
    {
        SceneMoveManager.Instance.ImmediatelyChangeScne("RetryScene");
    }


    string HASH = "Scene";

    string OpenObjectName = "전투버툰캔버스";
    public void ComeBackHome()
    {
        
        SceneMoveManager.Instance.ImmediatelyChangeScne("MainMenu");
        PlayerPrefs.SetString(HASH, OpenObjectName);
        Debug.Log(PlayerPrefs.GetString(HASH) + "저장한이름");
        PlayerPrefs.Save();
    }


    public void SucessActive()
    {
        if(sucess.activeSelf==true) 
        {
            return;
        }

        sucess.SetActive(true);
        int index = PlayerPrefs.GetInt("Stage");
        PlayerDataManager.Instacne.Data.StageClear[index++] = StageState.Clerar;
        if (PlayerDataManager.Instacne.Data.StageClear.Count > index)
            PlayerDataManager.Instacne.Data.StageClear[index] = StageState.Unlock;
    }
}
