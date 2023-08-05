

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
        int skillLevel;


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

        public virtual void InitSkill(ObjectTable skilltable, InGameSkill skill)
        {
            this.skill = skill;
            skillData = new SkillData();
            
            skillData.InitData(skillName.Replace("_","/"), skilltable);
            delayValue = new WaitForSeconds(skillData.Delay);


            Resources.Load<GameObject>("Prefab/SkillPrefab/" + skillData.SkillObjectKey).TryGetComponent<SkillObject>(out skillEffectPrefab);

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
                Effect();
                

                yield return delayValue;
                
            }

            
        }
        
        protected virtual void  Effect()
        {
            Debug.Log("∞°ªÛ¿Ã∆Â∆Æ");
        }


        public void  SkillLevelUp(ObjectTable skilltable)
        {
            skillData.LevelUP(skillName + "/" + (++skillLevel), skilltable);
            delayValue = new WaitForSeconds(skillData.Delay);
        }




    }
}