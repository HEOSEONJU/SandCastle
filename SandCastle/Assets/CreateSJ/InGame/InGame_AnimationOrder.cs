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


        public void SkillFireEvent() //무기에서 나가는 경우
        {
            inGameChar.InGameAttack.AnimatorWeapon.SetTrigger("Skill");
            
            
        }

        public void SkillEvent() //스킬 애니메이션과 같이 실행되는경우
        {
            
            if (inGameChar.InGameSkill.SettingTarget())
                inGameChar.InGameSkill.ActiveSkill();
        }



        public void SkillEventend()
        {
            inGameChar.IsAction = false;
            
            

        }

    }

}