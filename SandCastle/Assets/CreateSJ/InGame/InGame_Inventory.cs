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


        public float SandCount
        {
            get { return sandCount; }
            set { sandCount = value; 
                //sandText.text = sandCount.ToString();
            }
        }
        public float WaterCount
        {
            get { return waterCount; }
            set { waterCount = value; 
                //waterText.text = waterCount.ToString();
            }
        }
        public float MudCount
        {
            get { return mudCount; }
            set { mudCount = value; 
            //    mudText.text = mudCount.ToString(); 
            }
        }

        /*
        [SerializeField]
         TextMeshProUGUI sandText;
        [SerializeField]
         TextMeshProUGUI waterText;
        [SerializeField]
         TextMeshProUGUI mudText;
        */

        public void InitInventroy(float sand, float water, float mud)
        {

            SandCount = sand;
            WaterCount = water;
            MudCount = mud;

        }



        public void Getter_Mine(float amount,ResourceEnum resourceenum )
        {
            switch(resourceenum)
            {
                case ResourceEnum.sand:
                    SandCount += amount;
                    
                    break;
                case ResourceEnum.water:
                    WaterCount += amount;
                    
                    break;
                case ResourceEnum.mud:
                    MudCount += amount;
                    
                    break;
            }
            
            

        }


    }
}