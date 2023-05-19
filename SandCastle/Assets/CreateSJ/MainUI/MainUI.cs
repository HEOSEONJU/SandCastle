using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MainUI
{
    public class MainUI : MonoBehaviour
    {

        [SerializeField]
        Stack<GameObject> openList;


        
        public Stack<GameObject> OpenList
        {
            get {
                if (openList is null)
                {
                    openList = new Stack<GameObject>();
                }
                    return openList; 
            }
        }

        [SerializeField]
        int c = 0;
        public bool Open_Main(GameObject go)
        {


            if (OpenList.Count>0 &&  OpenList.First() == go)
            {
                return false;
            }

            while (OpenList.Count > 0)
            {
                OpenList.Pop().SetActive(false);
            }
            go.SetActive(true);
            OpenList.Push(go);
            c += 1;
            return true;
        }
        public void Open_Sub(GameObject go)
        {

            go.SetActive(true);
            OpenList.Push(go);
            c += 1;
            
        }
        public bool Open_UI(GameObject go)
        {
            

            if (OpenList.Count is 0)
            {
                go.SetActive(true);
                OpenList.Push(go);
                c = 1;
                return true;
            }
            if(OpenList.Last()==go)
            {
                return false;
            }

            while (openList.Count > 0)
            {
                openList.Pop().SetActive(false);
            }
            c += 1;
            go.SetActive(true);
            OpenList.Push(go);
            return true;
        }
        public void CloseUI(GameObject go)
        {
            
            if (openList.Count is 0)
            {
                c = 0;
                return;
            }
            if (openList.Count is 1)
            {
                c = 0;
                OpenList.Pop().SetActive(false);
                return;
            }

            if (string.Compare(openList.Last().name, go.name) is 0)
            {
                c -=1;
                OpenList.Pop().SetActive(false);
                return;
            }

        }


    }
}