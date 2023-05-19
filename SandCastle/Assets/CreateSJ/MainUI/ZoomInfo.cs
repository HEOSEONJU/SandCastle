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
        TextMeshProUGUI textMeshProUGUI;

        public void Zoom()
        {
            ZoomWindow.SetActive(true);
        }
        public void Zoom_INDEX()
        {
            ZoomWindow.SetActive(true);
            textMeshProUGUI.text = gameObject.name;
        }
    }
}