using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MainUI
{
    public class CharSlotData : MonoBehaviour
    {
        [SerializeField]
        Image mainImage;
        [SerializeField]
        string id;

        public string ID
        {
            get { return id; }
        }

        public void InputData(Sprite sprite, string id)
        {
            
            mainImage.sprite = sprite;
            this.id = id;

        }
    }
}