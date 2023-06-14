using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using InGameResourceEnums;
public class Enemy_Status : MonoBehaviour
{
    [SerializeField]
    float hp;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float attackSpeed;
    [SerializeField]
    float attackRange;
    [SerializeField]
    string[] resistanceType;
    [SerializeField]
    float resistanceValue;
    [SerializeField]
    ResourceEnum getRewardType;
    [SerializeField]
    int amount;

    int skillPointProbability;

    public void Init(float hp, float movespeed, float attackspeed, float attackrange, string[] resistancetype, float resistancevalue, ResourceEnum getrewardtype, int amount,int skillpoint)
    {
        this.hp = hp;
        this.moveSpeed = movespeed;
        this.attackSpeed = attackspeed;
        this.attackRange = attackrange;
        this.resistanceType = resistancetype;
        this.resistanceValue = resistancevalue;
        this.amount = amount;
        this.getRewardType = getrewardtype;
        skillPointProbability=skillpoint;


    }

    public float MoveSpeed
    {
        get { return this.moveSpeed; }
    }

    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public ResourceEnum GetRewardType
    {
        get { return getRewardType; }
    }
    public int Amount
    {
        get { return amount; }
    }
    public int SkillPointProbability
    {
        get { return skillPointProbability; }
    }
}
