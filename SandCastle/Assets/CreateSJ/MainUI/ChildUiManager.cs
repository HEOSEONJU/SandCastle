using MainUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainUI
{
    public class ChildUiManager : MonoBehaviour
    {
        
        public SubUI[] subuiList;

        [SerializeField]
        MainUI[] MainList;
        private void Start()
        {
            subuiList = GetComponentsInChildren<SubUI>();
            MainList = GetComponentsInChildren<MainUI>();
            CloseChildUI();
        }

        // Update is called once per frame
        void CloseChildUI()
        {
            foreach (SubUI subui in subuiList)
            {
                subui.CloseMy();
            }
            foreach (MainUI mainui in MainList)
            {
                mainui.OpenList.Clear();
            }
        }
        private void OnDisable()
        {
            CloseChildUI();
        }
    }
}