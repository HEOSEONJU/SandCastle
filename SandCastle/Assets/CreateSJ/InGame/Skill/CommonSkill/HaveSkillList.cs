using Google.GData.Extensions;
using inGame;
using InGame;
using SkillEnums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Skill
{
    public class HaveSkillList : MonoBehaviour
    {
        [SerializeField]
        ObjectTable skillTable;
        [SerializeField]
        ObjectTable skillListTable;
        [SerializeField]
        Transform skillpooling;


        [SerializeField]
        InGameSkill skill;
        List<BasicCommonSkill> skillList;
        [SerializeField]
        MasterController masterController;

        public int HaveMaxSkillCount
        {
            get 
            {
                Debug.Log(skillList.FindAll(x => x.Max == true).Count + "��ų��������");
                return skillList.FindAll(x => x.Max == true).Count;
                
            }
        }
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
                BCS.InitSkill(skillTable, skill, skillListTable);
                skillList.Add(BCS);
                if (BCS.SpwanPosi == SkillSpwan.Trace)
                {
                    BCS.transform.SetParent(masterController.InGameChar.transform);
                }
                else
                BCS.transform.SetParent(skillpooling);
                
                BCS.ActiveSkill();

            }
            else
            {
                skillList.Remove(temp);
                skillList.Add(temp);
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


        public void ApplyBuff(InGame_Char igc)
        {
            skillList.Last().ApplyBuff(igc);
            foreach (BasicCommonSkill skill in skillList)
            {
                //skill.ApplyBuff(igc);

            }
        }
    }
}