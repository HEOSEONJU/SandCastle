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
        List<InGame_Char> IGC;



        [SerializeField]
        string harvestAniamtionBool = "Harvest";
        


        
        [SerializeField]
        InGame_Inventory inventory;

        public void Init(List<InGame_Char> igc)
        {
            IGC= igc;
        }


        public void Harvest()
        {
            if((search.Target is null)|| IGC.Find(x=>x.IsAction==true) )
            {
                return;
            }
            if(search.Target.IsReady==false || search.Target.IsDestory)
            {
                Debug.Log("전부디스트로이됨 && 아직 준비중");
                return;
            }


            
            
            
            foreach(InGame_Char c in IGC)
            {
                c.Animator.SetBool(harvestAniamtionBool, true);
                c.IsAction = true;
            }
            
            
            
        }
        public void HarvestEvent()
        {
            
            
        }
        public void TargetHarvest()
        {
            
            search.Target.Collection(1,inventory);
            foreach (InGame_Char c in IGC)
            {
                c.IsAction = false;
            }
            
        }

        
    
    }
}