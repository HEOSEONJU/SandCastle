using Player;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; 
namespace MainUI
{
    public class MoveGameScene : MonoBehaviour
    {
        [SerializeField]
        ObjectTable waveTable;


        [SerializeField]
        StageUI stageUI;


        [SerializeField]
        string moveSceneName = "";

        public void MoveScene()
        {

            PlayerPrefs.SetInt("Stage",stageUI.StageIndex);
            
            string stagename = waveTable.values[stageUI.StageIndex + 1].ToString();
            PlayerDataManager.Instacne.Data.fightCharIds = PlayerDataManager.Instacne.Data.havetCharIds[stageUI.StageIndex].id;
            SceneMoveManager.Instance.AsyncChangeScne(waveTable.FindString(stagename, "stageResourceKey"));


        }
    }

}