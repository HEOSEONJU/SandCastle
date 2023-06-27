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

            //PlayerPrefs.SetInt("Stage",stageUI.StageIndex);
            //int T = PlayerPrefs.GetInt("Stage");
            string stagename = waveTable.values[stageUI.StageIndex + 1].ToString();
            
            SceneMoveManager.Instance.AsyncChangeScne(waveTable.FindString(stagename, "stageResourceKey"));


        }
    }

}