using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainUI
{
    public class SceneStartEventManger : MonoBehaviour
    {

        [SerializeField]
        UIOpenClose defaultGameObject;

        [SerializeField]
        GameObject LastGameObject;
        private void Start()
        {
            if(PlayerPrefs.HasKey("Scene"))
            {
                Debug.Log(PlayerPrefs.GetString("Scene") + "불러올이름");
                ChangeLast(transform.Find(PlayerPrefs.GetString("Scene")));
                PlayerPrefs.DeleteKey("Scene");
                PlayerPrefs.Save();
            }
            
            
            


            if (LastGameObject == null)
            {
                
                StartEvent(defaultGameObject);
            }
            else
            {
                
                StartEvent(LastGameObject.GetComponent<UIOpenClose>());
            }
        }
        void StartEvent(UIOpenClose go)
        {
            go.OpenButton();
            


        }

        public void ChangeLast(Transform go)
        {
            if(go != null)
            LastGameObject = go.gameObject;
        }


    }
}