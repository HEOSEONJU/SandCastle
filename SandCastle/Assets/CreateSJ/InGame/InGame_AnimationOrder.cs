using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InGame
{
    public class InGame_AnimationOrder : MonoBehaviour
    {
        [SerializeField]
        InGame_Harvest harvest;






        
        public void HarvestEventend()
        {
            harvest.TargetHarvest();
            
        }
    }

}