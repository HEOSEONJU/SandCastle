using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainUI
{
    public class ZoomCharInfo : MonoBehaviour
    {
        [SerializeField]
        ObjectTable charTable;

        [SerializeField]
        Image mainImage;
        
        public void OpenZoom(string key)
        {
            string path = "CharIcon/" + charTable.FindString(key, "imageName");
            Sprite temp = Resources.Load<Sprite>(path);
            mainImage.sprite = temp;
        }
    }
}