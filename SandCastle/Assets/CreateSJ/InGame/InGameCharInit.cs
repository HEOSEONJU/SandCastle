using Google.GData.Client;
using GoogleSheetsToUnity;
using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCharInit : MonoBehaviour
{
    [SerializeField]
    ObjectTable DefineTable;
    [SerializeField]
    ObjectTable CharTable;
    [SerializeField]
    ObjectTable LevelTable;

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



    public float CharsInit(List<InGame_Char> inGameCharList)
    {
        foreach (InGame_Char IGC in inGameCharList)
        {
            CharSingle(IGC, DefaultSpeed, AttackDamage, DefaultCRP, DefaultCRD);
        }
         return 0;

    }
    public float CharInit(InGame_Char IGC)
    {
        return CharSingle(IGC, DefaultSpeed, AttackDamage,DefaultCRP, DefaultCRD);
    }



    float CharSingle(InGame_Char IGC, float defaultspeed, float attackdamage,float crp,float crd)
    {
        float movespeed = CharTable.Findfloat(IGC.CharName, "moveSpeed");
        float animationSpeed = CharTable.Findfloat(IGC.CharName, "animationSpeed");

        float giveDamage = CharTable.Findfloat(IGC.CharName, "giveDamage");
        float sandGet = CharTable.Findfloat(IGC.CharName, "sandGet");
        float waterGet = CharTable.Findfloat(IGC.CharName, "waterGet");
        float mudGet = CharTable.Findfloat(IGC.CharName, "mudGet");
        string localKeyName = CharTable.FindString(IGC.CharName, "localKeyName");

        float range = CharTable.Findfloat(IGC.CharName, "range");
        float moveSpeedLV = CharTable.Findfloat(IGC.CharName, "moveSpeedLV");
        float animationSpeedLV = CharTable.Findfloat(IGC.CharName, "animationSpeedLV");
        float giveDamageLV = CharTable.Findfloat(IGC.CharName, "giveDamageLV");
        float sandGetLV = CharTable.Findfloat(IGC.CharName, "sandGetLV");
        float waterGetLV = CharTable.Findfloat(IGC.CharName, "waterGetLV");
        float mudGetLV = CharTable.Findfloat(IGC.CharName, "mudGetLV");
        int maxMana = CharTable.FindInt(IGC.CharName, "maxMana");
        int startMana = CharTable.FindInt(IGC.CharName, "startMana");
        int maxhp = CharTable.FindInt(IGC.CharName, "maxHP");
        float attackspeed = CharTable.Findfloat(IGC.CharName, "attackSpeed");

        List<float> needexp=new List<float>();
        for(int i=1;i< LevelTable.values.Count;i++)
        {
            
            needexp.Add(float.Parse(LevelTable.ViewTableList[i]["needExp"].ToString()));
        }


        IGC.InGameStatus.Init(movespeed, animationSpeed, giveDamage, sandGet, waterGet, mudGet, range, maxMana, startMana, maxhp,crp,crd);

        IGC.SettingAttack(attackspeed, defaultspeed, attackdamage);
        return movespeed;
    }
}
