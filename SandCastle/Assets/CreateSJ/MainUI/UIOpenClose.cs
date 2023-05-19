using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MainUI
{
    public class UIOpenClose : MonoBehaviour
    {

        [SerializeField]
        MainUI mainUi;


        [SerializeField]
        GameObject Default;

        [SerializeField]
        ChildUiManager childUiManager;
        

        public void OpenButton()
        {
            if (mainUi.Open_Main(this.gameObject) && Default != null)
            {
                
                Invoke("OpenDefault", Time.deltaTime);
            }
        }
        public void OpenSubButton(int n)
        {
            foreach(SubUI ui in childUiManager.subuiList)
            {
                if(ui != childUiManager.subuiList[n] && ui.CheckActivity())
                {
                    ui.gameObject.SetActive(false);
                }
            }


            if (!childUiManager.subuiList[n].gameObject.activeSelf)
            mainUi.Open_Sub(childUiManager.subuiList[n].gameObject);
        }

        public void OpenDefault()
        {
            Debug.Log("¿€µø");
            mainUi.Open_Sub(Default);
        }
        


    }
}
