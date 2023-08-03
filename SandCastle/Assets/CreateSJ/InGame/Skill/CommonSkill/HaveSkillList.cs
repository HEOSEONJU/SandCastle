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
            int temp = skillList.FindIndex(x => x.SkillName == name);
            if (temp == -1)
            {
                ;
                Debug.Log(name.Replace('/', '_'));
                Resources.Load<GameObject>("Prefab/CommonSkillPrefab/" + name.Replace('/', '_')).TryGetComponent<BasicCommonSkill>(out BasicCommonSkill GO);
                GO.InitSkill(skillTable, skill);
                skillList.Add(GO);
                GameObject objectInPool = Instantiate(GO.gameObject) as GameObject;
                
                objectInPool.transform.SetParent(skillpooling);

                GO.ActiveSkill(objectInPool.transform);

            }
            else
            {
                skillList[temp].SkillLevelUp(skillTable);
                

            }


        }

        public int FindSkillLevel(string key)
        {


            int temp = skillList.FindIndex(x => x.SkillName == key);


            if (temp == -1 || skillList.Count == 0)
            {
                return -1;
            }

            return skillList[temp].SkillLevel;



        }
    }
}