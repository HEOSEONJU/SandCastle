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


        public void InitInventroy(float sand,float water,float mud)
        {

            sandCount = sand;
            waterCount = water;
            mudCount = mud;
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


    }
}