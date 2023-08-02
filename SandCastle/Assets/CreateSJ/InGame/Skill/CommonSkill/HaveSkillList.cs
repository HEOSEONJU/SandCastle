using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveSkillList : MonoBehaviour
{

    [SerializeField]
    List<string> skillNames;
    [SerializeField]
    List<int> skillLevels;


    public void InitSkill()
    {
        skillNames= new List<string>();
        skillLevels= new List<int>();
    }


    public void InputData(string name, int level)
    {
        int temp = skillNames.FindIndex(x => x == name);
        if(temp==-1)
        {
            skillNames.Add(name);
            skillLevels.Add(level);
        }
        else
        {
            skillLevels[temp]=level;
        }


    }

    public int FindSkillLevel(string key)
    {

        
       int temp = skillNames.FindIndex(x=>x==key);

        
        if(temp==-1 || skillNames.Count==0)
        {
            return -1;
        }
        
        return skillLevels[temp];



    }
}
