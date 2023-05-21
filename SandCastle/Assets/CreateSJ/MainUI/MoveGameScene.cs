using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; 
namespace MainUI
{
    public class MoveGameScene : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> openGameSceneObject;
        [SerializeField]
        StageUI stageUI;


        [SerializeField]
        string moveSceneName = "";

        public void MoveScene()
        {

            PlayerPrefs.SetInt("Stage",stageUI.StageIndex);

            SceneMoveManager.Instance.AsyncChangeScne(moveSceneName);


        }
    }

}