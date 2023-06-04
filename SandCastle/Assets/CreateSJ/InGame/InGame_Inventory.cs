using InGameResourceEnums;
using System;
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
        [SerializeField]
        float waterCount;
        [SerializeField]
        float mudCount;

        [SerializeField]
         TextMeshProUGUI sandText;
        [SerializeField]
         TextMeshProUGUI waterText;
        [SerializeField]
         TextMeshProUGUI mudText;

        private void Start()
        {
            
            sandCount = 0;
            waterCount = 0;
            mudCount = 0;
            sandText.text = sandCount.ToString();
            waterText.text = waterCount.ToString();
            mudText.text = mudCount.ToString();

        }

        public void Getter_Mine(float amount,ResourceEnum resourceenum )
        {
            switch(resourceenum)
            {
                case ResourceEnum.sand:
                    sandCount += amount;
                    sandText.text = sandCount.ToString();
                    break;
                case ResourceEnum.water:
                    waterCount += amount;
                    waterText.text = waterCount.ToString();
                    break;
                case ResourceEnum.mud:
                    mudCount += amount;
                    mudText.text = mudCount.ToString();
                    break;
            }
            
            

        }

        internal void Getter_Mine(int amount, ResourceEnum resourceType)
        {
            throw new NotImplementedException();
        }
    }
}