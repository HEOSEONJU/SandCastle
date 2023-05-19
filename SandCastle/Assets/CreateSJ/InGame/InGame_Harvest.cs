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
        InGame_Char inGameChar;




        [SerializeField]
        string harvestAniamtionBool = "Harvest";
        private void OnEnable()
        {
            
        }


        
        [SerializeField]
        InGame_Inventory inventory;

        public void Harvest(Animator animator)
        {
            if((search.Target is null)|| inGameChar.IsAction )
            {
                return;
            }
            if(search.Target.IsReady==false || search.Target.IsDestory)
            {
                Debug.Log("���ε�Ʈ���̵� && ���� �غ���");
                return;
            }


            
            
            animator.SetBool(harvestAniamtionBool, true);
            inGameChar.IsAction = true;
            
            
        }
        public void HarvestEvent()
        {
            
            
        }
        public void TargetHarvest()
        {
            
            search.Target.Collection(1,inventory);
            inGameChar.IsAction = false;
        }

        
    
    }
}