using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InGame
{
    public class InGame_AnimationOrder : MonoBehaviour
    {
        [SerializeField]
        Animator animator;
        [SerializeField]
        InGame_Harvest harvest;
        [SerializeField]
        InGame_Char inGameChar;





        
        public void HarvestEventend()
        {
            harvest.TargetHarvest();
            
        }


        public void SkillFireEvent()
        {
            inGameChar.InGameAttack.AnimatorWeapon.SetTrigger("Skill");
            inGameChar.InGameAttack .ParticleWeapon.SetTrigger("Skill");
            
        }

        
        public void SkillEventend()
        {
            
            animator.SetTrigger("SkillExit");
            //

        }
    }

}