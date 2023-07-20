using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MainUI
{
    public class ZoomInfo : MonoBehaviour
    {
        [SerializeField]
        GameObject ZoomWindow;

        [SerializeField]
        CharSlotData charSlotData;


        public void Zoom()
        {
            ZoomWindow.SetActive(true);
        }
        public void Zoom_Char()
        {
            ZoomWindow.SetActive(true);
            ZoomWindow.GetComponent<ZoomCharInfo>().OpenZoom(charSlotData.ID);
        }
    }
}