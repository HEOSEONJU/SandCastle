using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InGame
{
    public class InGame_AnimationOrder : MonoBehaviour
    {
        InGame_Char inGameChar;


        private void Start()
        {
            inGameChar=  GetComponentInParent<InGame_Char>();
        }





        public void AttackEvent()
        {

            inGameChar.OrderAttackTrigger();
        }
        public void HarvestEventend()
        {
            inGameChar.OrderHarvestTrigget();
            
        }
    }

}