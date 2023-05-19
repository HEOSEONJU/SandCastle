using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InGame
{
    public class InGame_Inventory : MonoBehaviour
    {
        
        [SerializeField]
        float sandCount;


        private void Start()
        {
            
            sandCount = 0;
            InGame_UI.Instance.sandText.text = "0";

        }

        public void Getter_Mine(float amount)
        {
            sandCount += amount;
            InGame_UI.Instance.sandText.text = sandCount.ToString();
            

        }
    }
}