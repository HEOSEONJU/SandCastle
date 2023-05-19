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
        string moveSceneName = "";

        public void MoveScene()
        {
            foreach (GameObject go in openGameSceneObject)
            {
                //go.SetActive(true);
            }
            //gameObject.SetActive(false);

            SceneMoveManager.Instance.AsyncChangeScne(moveSceneName);


        }
    }

}