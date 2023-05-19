using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Status : MonoBehaviour
{
    [SerializeField]
    float hp;

    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }
}
