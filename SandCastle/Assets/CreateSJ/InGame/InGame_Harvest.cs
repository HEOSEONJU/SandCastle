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
                    //Debug.Log("Ÿ�پ���");
                    return false;
                    
                }
                if (iGC.IsAction == true)
                {
                    return false;
                }
                if (search.Target.IsDestory)
                {
                    //Debug.Log("���ε�Ʈ���̵� ");
                    
                    return false;
                }
                if (search.Target.Hp<=0)
                {
                    //Debug.Log("Ķ �ڿ��� ����");

                    return false;
                }
                else
                {
                    return true;
                }

            }

        }




        public void Harvest()//��Ȯ�ִϸ��̼ǽ���
        {
            //if(search.Target is null)
            //{
            //    Debug.Log("Ÿ�پ���");
            //    Do= false;
            //    return;
            //}
            //if (iGC.IsAction == true)
            //{
            //    Debug.Log("�ൿ��");
            //    Do = false;
            //    return;
            //}
            //if (search.Target.IsDestory)
            //{
            //    Debug.Log("���ε�Ʈ���̵� ");
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