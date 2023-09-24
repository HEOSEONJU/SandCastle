

using InGame;
using SkillEnums;
using System;
using System.Collections;

using UnityEngine;



namespace Skill
{
    public class BasicCommonSkill : MonoBehaviour
    {


        [SerializeField]
        string skillName;
        [SerializeField]
        int skillLevel=0;
        [SerializeField]
        int maxSkillLevel;


        [SerializeField]
        protected SkillObject skillEffectPrefab;
        
        

        [SerializeField]
        protected SkillData skillData;
        [SerializeField]
        protected InGameSkill skill;
        WaitForSeconds delayValue;
        

        public string SkillName
        {
            get { return skillName; }
        }
        public int SkillLevel
        {
            get { return skillLevel; }
            set { skillLevel = value; }
        }

        public bool Max
        {
            get 
            {
                if (skillLevel == maxSkillLevel)
                {
                    return true;
                }
                else
                    return false;

            }
        }

        public SkillSpwan SpwanPosi
        {
            get
            {
                return skillData.Spwan;
            }
        }
        public virtual void InitSkill(ObjectTable skilltable, InGameSkill skill,ObjectTable skilllist)
        {

            this.skill = skill;
            skillData = new SkillData();
            skillLevel = 1;
            skillData.InitData(skillName.Replace("_","/"), skilltable);
            delayValue = new WaitForSeconds(skillData.Delay);
            maxSkillLevel = skilllist.FindInt(SkillName, "maxLevel");

            Resources.Load<GameObject>("Prefab/SkillPrefab/" + skillData.SkillObjectKey).TryGetComponent<SkillObject>(out skillEffectPrefab);

        }

        public void ApplyBuff(InGame_Char igc)
        {
            Debug.Log("어플리적용" + name);
            switch (skillData.BuffType)
            {
                case BuffType.Exp:
                    Debug.Log("경험치버프적용");
                    igc.ExpRange = skillData.Value;
                    break;
                case BuffType.Hp:
                    igc.InGameStatus.MaxHP = Convert.ToInt32( skillData.Value);
                    break;
                case BuffType.Heal:
                    igc.InGameStatus.CurrentHp += igc.InGameStatus.MaxHP / 2;
                    break;
                case BuffType.MP:
                    igc.InGameStatus.CurrentMana = igc.InGameStatus.MaxMana;
                    break;
                case BuffType.Regen:
                    igc.InGameStatus.Regen = Convert.ToInt32(skillData.Value);
                    break;
            }

        }

        public  void ActiveSkill()
        {
            
            //InvokeRepeating("InvokSkill", 0.5f, skillData.Delay);
            StartCoroutine(Active());
            
            

        }
         IEnumerator  Active()
        {
            while(true)
            {
                Effect(skillData.FireDelay);
                

                yield return delayValue;
                
            }

            
        }
        
        protected virtual void  Effect(float delay)
        {
            Debug.Log("가상이펙트");
        }


        public void  SkillLevelUp(ObjectTable skilltable)
        {
            skillData.LevelUP(skillName + "/" + (++skillLevel), skilltable);
            delayValue = new WaitForSeconds(skillData.Delay);
        }




    }
}