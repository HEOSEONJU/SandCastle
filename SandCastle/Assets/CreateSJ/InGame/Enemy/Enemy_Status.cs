using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{
    [SerializeField]
    float baseHp;
    [SerializeField]
    float maxHp;
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
    float exp;
    

    public void Init(float hp,float movespeed,float attackspeed,float attackrange,float exp)
    {
        
        baseHp = maxHp = this.hp = hp;
        this.moveSpeed = movespeed;
        this.attackSpeed = attackspeed;
        this.attackRange = attackrange;
        this.exp = exp;

    }

    public void ResetHP(float multiply = 1)
    {
        maxHp=this.hp= baseHp * multiply;
    }



    public float MoveSpeed
    {
        get { return this.moveSpeed; }
    }

    public float EXP
    {
        get { return exp; }
        set { exp = value; }
    }

    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public float HPPercentage
    {
        get { return this.hp/maxHp; }
    }
}
