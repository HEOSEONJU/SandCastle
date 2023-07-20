using Player;
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


        [SerializeField]
        List<GameObject> objectList = new List<GameObject>();
        [SerializeField]
        SubUI Reward;


        private void Awake()
        {
            Camera camera =GetComponent<Camera>();
            Rect rect = camera.rect;

            float height = ((float)Screen.width/Screen.height)/ ((float)9/16);
            float width = 1f / height;
            if(height<1)
            {
                rect.height = height;
                rect.y = (1f - height) / 2f;

            }
            else
            {
                rect.width = width;
                rect.x = (1f - width) / 2f;
            }
            camera.rect = rect;
        }
        private void Start()
        {
            if(PlayerPrefs.HasKey("Scene"))
            {
                
                ChangeLast(PlayerPrefs.GetString("Scene"));
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
                Reward.OpenMy();
            }
        }
        void StartEvent(UIOpenClose go)
        {
            go.OpenButton();
            


        }

        public void ChangeLast(string name)
        {
            LastGameObject = objectList.Find(x=>x.name== name);

            
        }


    }
}