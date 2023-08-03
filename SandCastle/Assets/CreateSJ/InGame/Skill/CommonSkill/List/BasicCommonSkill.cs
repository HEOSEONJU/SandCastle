

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

        [SerializeField]
        Transform pooling;
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


        public virtual void ActiveSkill(Transform pooling)
        {
            this.pooling = pooling;
            Invoke("InvokSkill", 0.5f);
            

        }


        public virtual void InvokSkill()
        {
            Debug.Log(transform.name);
            ObjectPooling.Instance.GetObject(skillEffectPrefab.gameObject, pooling).TryGetComponent<SkillObject>(out SkillObject bulletobject);

            bulletobject.Init(skillData);

            skill.ActiveSkill(bulletobject);





            Invoke("InvokSkill", skillData.Delay);


        }

        public IEnumerator Delay()
        {
            yield return skillData.Delay;
            ActiveSkill(pooling);
        }


        public void  SkillLevelUp(ObjectTable skilltable)
        {
            skillName.Replace(skillName[skillName.Length - 1],Convert.ToChar(SkillLevel));
            
            skillData.LevelUP(skillName, skilltable);
        }




    }
}