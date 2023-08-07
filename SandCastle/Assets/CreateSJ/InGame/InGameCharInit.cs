using Google.GData.Client;
using GoogleSheetsToUnity;
using InGame;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameCharInit : MonoBehaviour
{
    [SerializeField]
    ObjectTable DefineTable;
    [SerializeField]
    ObjectTable CharTable;
    [SerializeField]
    ObjectTable LevelTable;

    [SerializeField]
    List<Slider> SliderList;
    [SerializeField]
    TextMeshProUGUI levelText;
    float defaultSpeed=0;
    float attackDamage=0;
    float defaultCRP = 0;
    float defaultCRD = 0;
    float DefaultSpeed
    {
        get 
        {
            if (defaultSpeed == 0) defaultSpeed = DefineTable.Findfloat("bulletdefaultspeed", "value");
            return defaultSpeed; 
        }
    }
     float AttackDamage
    {
        get
        {
            if (attackDamage == 0) attackDamage = DefineTable.Findfloat("attackdamage", "value");
            return attackDamage;
        }   
    }

    float DefaultCRP
    {
        get
        {
            if (defaultCRP == 0) defaultCRP = DefineTable.Findfloat("defaultCRP", "value");
            return defaultCRP;
        }
    }
    float DefaultCRD
    {
        get
        {
            if (defaultCRD == 0) defaultCRD = DefineTable.Findfloat("defaultCRD", "value");
            return defaultCRD;
        }
    }




    public void CharInit(InGame_Char IGC)
    {
        CharSingle(IGC, DefaultSpeed, AttackDamage,DefaultCRP, DefaultCRD);
    }



    void CharSingle(InGame_Char IGC, float defaultspeed, float attackdamage,float crp,float crd)
    {
        float movespeed = CharTable.Findfloat(IGC.CharName, "moveSpeed");
        

        float giveDamage = CharTable.Findfloat(IGC.CharName, "giveDamage");
        float sandGet = CharTable.Findfloat(IGC.CharName, "sandGet");
        float waterGet = CharTable.Findfloat(IGC.CharName, "waterGet");
        float mudGet = CharTable.Findfloat(IGC.CharName, "mudGet");
        

        float range = CharTable.Findfloat(IGC.CharName, "range");
        
        int maxMana = CharTable.FindInt(IGC.CharName, "maxMana");
        int startMana = CharTable.FindInt(IGC.CharName, "startMana");
        int maxhp = CharTable.FindInt(IGC.CharName, "maxHP");
        float attackspeed = CharTable.Findfloat(IGC.CharName, "attackSpeed");

        List<float> needexp=new List<float>();
        for(int i=1;i< LevelTable.values.Count;i++)
        {
            
            needexp.Add(float.Parse(LevelTable.ViewTableList[i]["needExp"].ToString()));
        }

        IGC.InGameStatus.InputUI(SliderList,levelText);
        IGC.InGameStatus.InputLevel(needexp);
        IGC.InGameStatus.Init(movespeed, giveDamage, sandGet, waterGet, mudGet, range, maxMana, startMana, maxhp,crp,crd);
        
        IGC.SettingAttack(attackspeed, defaultspeed, attackdamage);
        
    }
}
