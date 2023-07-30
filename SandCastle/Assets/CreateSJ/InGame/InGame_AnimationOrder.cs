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


        public void SkillFireEvent() //���⿡�� ������ ���
        {
            inGameChar.InGameAttack.AnimatorWeapon.SetTrigger("Skill");
            
            
        }

        public void SkillEvent() //��ų �ִϸ��̼ǰ� ���� ����Ǵ°��
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