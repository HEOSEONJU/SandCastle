using Google.GData.Extensions;
using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public class HaveSkillList : MonoBehaviour
    {
        [SerializeField]
        ObjectTable skillTable;
        [SerializeField]
        Transform skillpooling;


        [SerializeField]
        InGameSkill skill;
        List<BasicCommonSkill> skillList;
        public void InitSkill(InGameSkill skill)
        {

            skillList = new List<BasicCommonSkill>();
            this.skill = skill;
        }


        public void InputData(string name)
        {
            BasicCommonSkill temp =FindSkill(name);
            
            if (temp == null)
            {
                
                
                
                BasicCommonSkill BCS = ObjectPooling.Instance.GetObject(Resources.Load<GameObject>("Prefab/CommonSkillPrefab/" + name.Replace('/', '_')), transform).GetComponent<BasicCommonSkill>();
                BCS.InitSkill(skillTable, skill);
                skillList.Add(BCS);
                BCS.transform.SetParent(skillpooling);
                BCS.ActiveSkill();

            }
            else
            {
                temp.SkillLevelUp(skillTable);
                

            }


        }

        public BasicCommonSkill FindSkill(string key)
        {
            
            
            foreach (BasicCommonSkill skill in skillList)
            {
                if (skill.SkillName.Contains(key))
                {
                    return skill;
                }

            }
            return null;
        }


    }
}