using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    public void Init(float hp,float movespeed,float attackspeed,float attackrange, string[] resistancetype,float resistancevalue)
    {
        this.hp = hp;
        this.moveSpeed = movespeed;
        this.attackSpeed = attackspeed;
        this.attackRange = attackrange;
        this.resistanceType = resistancetype;
        this.resistanceValue = resistancevalue;

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
}
