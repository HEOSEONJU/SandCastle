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


            int haveskilllevel = haveSkillList.FindSkillLevel(key);
            if (haveskilllevel == commonSkillListTable.FindInt(key, "maxLevel") || current.FindIndex(x=>x==temp)!=-1) //�̸̹��� �̰ų� �̹� ���� ��ų
            {
                i--;
                continue;
            }
            current.Add(temp);
            string skillimagekey = commonSkillListTable.FindString(key, "CSkillImage");
            skillImage[i].sprite = Resources.Load<Sprite>("Prefab/CSkill/" + skillimagekey);
            skillName[i].text = commonSkillListTable.FindString(key, "name");
            if (haveskilllevel == -1)//��������������ų
            {
                Debug.Log("������������");
                key += "/"+ 1;                
            }
            else//�����ѽ�ų
            {
                Debug.Log("������");
                key += "/" + (haveskilllevel + 1);
                
            }
            skillExp[i].text = commonSkillTable.FindString(key, "exp");
            names.Add(key);

        }


    }


}

