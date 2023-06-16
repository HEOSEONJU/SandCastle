using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SkillEnums;
using System;
using Google.GData.Extensions;

[System.Serializable]
public class SkillData
{
    [SerializeField]
    string skillObjectKey;
    [SerializeField]
    SkillSpwan spwan;
    [SerializeField]
    SkillTarget target;
    int repeat;
    int repeatInterval;


    bool targetAmount;//리피트할때마다 true면 재계산
    [SerializeField]
    SkillTiming applyDamageTiming;
    float damage;
    float damageTime;
    string AbnormalKey;
    string chainSkillKey;
    [SerializeField]
    SkillTiming chainTiming;
    [SerializeField]
    float duration;
    float speed;
    [SerializeField]
    int isPiercing;


    public string SkillObjectKey
    {
        get { return skillObjectKey; }
    }
    public SkillTiming ApplyDamageTiming
    {
        get { return applyDamageTiming; }
    }
    public float Speed
    {
        get { return speed; }
    }
    public float Duration
    {
        get { return duration; }
    }
    public SkillSpwan Spwan
    {
        get { return spwan; }
    }
    public SkillTarget Target
    {
        get { return target; }
    }
    public int IsPiercing
    {
        get { return isPiercing; }
        set { isPiercing = value; }
    }
    public float Damage
    {
        get { return damage; }
    }

    public void InitData(string key, ObjectTable skillTable)
    {
        skillObjectKey = skillTable.FindString(key, "skillObjectKey");
        switch (skillTable.FindString(key, "spawnType"))
        {
            case "Player":
                spwan = SkillSpwan.Player;
                break;
            case "Target":
                spwan = SkillSpwan.Target;
                break;
            case "Position":
                spwan = SkillSpwan.Position;
                break;
        }
        repeat = skillTable.FindInt(key, "repeat");
        repeatInterval = skillTable.FindInt(key, "repeatInterval");



        switch (skillTable.FindString(key, "targetType"))
        {
            case "Near":
                target = SkillTarget.Near;
                break;
            case "Random":
                target = SkillTarget.Random;
                break;
            case "Far":
                target = SkillTarget.Far;
                break;
        }
        
        if (skillTable.FindString(key, "targetAmount") == "TRUE")
        {
            targetAmount = true;
        }
        else
        {
            targetAmount = false;
        }
        switch (skillTable.FindString(key, "applyDamageTiming"))
        {
            case "Enter":
                applyDamageTiming = SkillTiming.Enter;
                break;
            case "Stay":
                applyDamageTiming = SkillTiming.Stay;
                break;
            case "Exit":
                applyDamageTiming = SkillTiming.Exit;
                break;
        }

        switch (skillTable.FindString(key, "chainTiming"))
        {
            case "Enter":
                chainTiming = SkillTiming.Enter;
                break;
            case "Stay":
                chainTiming = SkillTiming.Stay;
                break;
            case "Exit":
                chainTiming = SkillTiming.Exit;
                break;
        }

        damage = skillTable.Findfloat(key, "damage");
        damageTime = skillTable.Findfloat(key, "damageTime");
        AbnormalKey = skillTable.FindString(key, "AbnormalKey");
        chainSkillKey = skillTable.FindString(key, "chainSkillKey");



        duration = skillTable.Findfloat(key, "duration");
        speed = skillTable.Findfloat(key, "speed");

        isPiercing = 9999;




    }
    public SkillData Clone()
    {
        return (this.MemberwiseClone() as SkillData);
    }
}
    

