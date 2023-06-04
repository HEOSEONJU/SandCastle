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
        


        
        [SerializeField]
        InGame_Inventory inventory;

        public void Init(InGame_Char igc,InGame_Inventory inventory)
        {
            iGC = igc;
            this.inventory = inventory;
        }


        public void Harvest()
        {
            if(search.Target is null)
            {
                Debug.Log("Ÿ�پ���");
                return;
            }
            if (iGC.IsAction == true)
            {
                Debug.Log("�ൿ��");
                return;
            }
            if (search.Target.IsDestory)
            {
                Debug.Log("���ε�Ʈ���̵� ");
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
            
            search.Target.Collection(1,inventory, iGC);
            
            
            
        }

        
    
    }
}