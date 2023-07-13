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

        
        public InGameMineSearch Search
        {
            get { return search; }
        }

        public bool CanHarveest
        {
            get
            {
                if (search.Target is null)
                {
                    //Debug.Log("타겟없음");
                    return false;
                    
                }
                if (iGC.IsAction == true)
                {
                    return false;
                }
                if (search.Target.IsDestory)
                {
                    //Debug.Log("전부디스트로이됨 ");
                    
                    return false;
                }
                if (search.Target.Hp<=0)
                {
                    //Debug.Log("캘 자원이 없음");

                    return false;
                }
                else
                {
                    return true;
                }

            }

        }




        public void Harvest()//수확애니메이션시작
        {
            //if(search.Target is null)
            //{
            //    Debug.Log("타겟없음");
            //    Do= false;
            //    return;
            //}
            //if (iGC.IsAction == true)
            //{
            //    Debug.Log("행동중");
            //    Do = false;
            //    return;
            //}
            //if (search.Target.IsDestory)
            //{
            //    Debug.Log("전부디스트로이됨 ");
            //    Do = false;
            //    return;
            //}



            //Do = true;



        }
        public void HarvestEvent()
        {
            
            
        }
        public void TargetHarvest()
        {
            
            search.Target.Collection(iGC);
            
            
            
        }

        
    
    }
}