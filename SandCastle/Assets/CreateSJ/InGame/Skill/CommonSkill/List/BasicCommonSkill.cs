

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
        SkillObject skillEffectPrefab;
        
        

        [SerializeField]
        SkillData skillData;
        [SerializeField]
        InGameSkill skill;

        
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
            
            skillData.InitData(skillName, skilltable);



            Resources.Load<GameObject>("Prefab/SkillPrefab/" + skillData.SkillObjectKey).TryGetComponent<SkillObject>(out skillEffectPrefab);

        }


        public virtual void ActiveSkill()
        {
            
            InvokeRepeating("InvokSkill", 0.5f, skillData.Delay);
            
            
            

        }


        public virtual void InvokSkill()
        {

            for (int i = 0; i < skillData.BulletCount; i++)
            {
                if (skill.SettingTarget(skillData))
                {
                    Debug.Log(skill.Target);
                    ObjectPooling.Instance.GetObject(skillEffectPrefab.gameObject, transform).TryGetComponent<SkillObject>(out SkillObject bulletobject);
                    bulletobject.Init(skillData);
                    skill.ActiveSkill(bulletobject);
                }
            }
            

            
        }

        public IEnumerator Delay()
        {
            yield return skillData.Delay;
            ActiveSkill();
        }


        public void  SkillLevelUp(ObjectTable skilltable)
        {

            
            
            
            
            skillData.LevelUP(skillName + "/" + (++skillLevel), skilltable);
        }




    }
}