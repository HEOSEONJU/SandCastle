using InGameResourceEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class InGame_Harvest : MonoBehaviour
    {
        [SerializeField]
        InGameMineSearch search;

        [SerializeField]
        InGame_Char iGC;



        [SerializeField]
        string harvestAniamtionBool = "Harvest";
        





        public void Harvest()
        {
            if(search.Target is null)
            {
                Debug.Log("타겟없음");
                return;
            }
            if (iGC.IsAction == true)
            {
                Debug.Log("행동중");
                return;
            }
            if (search.Target.IsDestory)
            {
                Debug.Log("전부디스트로이됨 ");
                return;
            }




            switch (search.Target.resourceType)
            {
                case ResourceEnum.sand:
                    iGC.Animator.CrossFade("CharSand", 0.01f);
                    break;
                case ResourceEnum.water:
                    iGC.Animator.CrossFade("CharWater", 0.01f);
                    break;
                case ResourceEnum.mud:
                    iGC.Animator.CrossFade("CharMud", 0.01f);
                    break;
            }
            


            
            
            
        }
        public void HarvestEvent()
        {
            
            
        }
        public void TargetHarvest()
        {
            
            search.Target.Collection(1, iGC);
            
            
            
        }

        
    
    }
}