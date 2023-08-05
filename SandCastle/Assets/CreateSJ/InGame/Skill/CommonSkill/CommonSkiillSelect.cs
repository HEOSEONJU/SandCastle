using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Skill;


public class CommonSkiillSelect : MonoBehaviour
{
    [SerializeField]
    ObjectTable commonSkillTable;
    [SerializeField]
    ObjectTable commonSkillListTable;



    [SerializeField]
    List<Image> skillImage;
    [SerializeField]
    List<TextMeshProUGUI> skillName;
    [SerializeField]
    List<TextMeshProUGUI> skillExp;

    [SerializeField]
    HaveSkillList haveSkillList;


    
    public List<string> names;
    

    int maxlevel = 5;
    public void InitSkill()
    {
        names=new List<string>();
        List<int> current = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            int temp=Random.Range(1, commonSkillListTable.ViewTableList.Count);
            string key = "CSkill/" + temp;

            BasicCommonSkill tempskill = haveSkillList.FindSkill(key);
            int haveskilllevel=-1;
            if (tempskill != null)
            {
                haveskilllevel = tempskill.SkillLevel;
            }
            
            if (haveskilllevel == commonSkillListTable.FindInt(key, "maxLevel") || current.FindIndex(x=>x==temp)!=-1) //이미만렙 이거나 이미 뽑은 스킬
            {
                i--;
                continue;
            }
            current.Add(temp);
            string skillimagekey = commonSkillListTable.FindString(key, "CSkillImage");
            skillImage[i].sprite = Resources.Load<Sprite>("Prefab/CSkill/" + skillimagekey);
            skillName[i].text = commonSkillListTable.FindString(key, "name");

            
            if (haveskilllevel == -1)//보유하지않은스킬
            {
                
                skillExp[i].text = commonSkillTable.FindString(key+("/" + 1), "exp");
                           
            }
            else//보유한스킬
            {
                
                skillExp[i].text = commonSkillTable.FindString(key + ("/" + (haveskilllevel + 1)), "exp");
                
            }
            
            names.Add(key);

        }


    }


}

