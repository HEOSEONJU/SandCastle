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
    List<CommonSkillButton> skillButton;
    

    [SerializeField]
    HaveSkillList haveSkillList;


    
    public List<string> names;
    

    int maxlevel = 5;
    public void InitSkill()
    {
        names=new List<string>();
        List<int> current = new List<int>();
        int skillcount = commonSkillListTable.ViewTableList.Count - 1 - haveSkillList.HaveMaxSkillCount;




        if (skillcount>=3)
        {
            skillcount = 3;
        }
        

        for (int i = 0; i < 3; i++)
        {
            if(i<skillcount) 
            {
                InitSkillFunction(current, i);
            }       
            else
            {
                skillButton[i].gameObject.SetActive(false);
            }

        }


    }

    void InitSkillFunction(List<int> current,int i)
    {
        int temp = Random.Range(1, commonSkillListTable.ViewTableList.Count);
        string key = "CSkill/" + temp;

        BasicCommonSkill tempskill = haveSkillList.FindSkill(key);
        int haveskilllevel = -1;
        if (tempskill != null)
        {
            haveskilllevel = tempskill.SkillLevel;
        }


        while(haveskilllevel == commonSkillListTable.FindInt(key, "maxLevel") || current.FindIndex(x => x == temp) != -1) //�̸̹��� �̰ų� �̹� ���� ��ų
        {
            temp = Random.Range(1, commonSkillListTable.ViewTableList.Count);
            key = "CSkill/" + temp;

            tempskill = haveSkillList.FindSkill(key);
            haveskilllevel = -1;
            if (tempskill != null)
            {
                haveskilllevel = tempskill.SkillLevel;
            }

        }


        current.Add(temp);
        string skillimagekey = commonSkillListTable.FindString(key, "CSkillImage");
        if (Resources.Load<Sprite>("Prefab/CSkill/" + skillimagekey) == null)
        {
            Debug.Log(skillimagekey + "��ã��");
        }
        skillButton[i].skillImage.sprite = Resources.Load<Sprite>("CSkillIcon/" + skillimagekey);
        skillButton[i].skillName.text = commonSkillListTable.FindString(key, "name");


        if (haveskilllevel == -1)//��������������ų
        {

            skillButton[i].skillExp.text = commonSkillTable.FindString(key + ("/" + 1), "exp");

        }
        else//�����ѽ�ų
        {

            skillButton[i].skillExp.text = commonSkillTable.FindString(key + ("/" + (haveskilllevel + 1)), "exp");

        }

        names.Add(key);
    }

}

