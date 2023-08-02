using DamageNumbersPro;
using inGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameEvent : MonoBehaviour
{
    static InGameEvent instance;


    [SerializeField]
    Transform poolingParent;

    [SerializeField]
    GameObject expPrefab;

    [SerializeField]
    GameObject LevelUpPrefab;

    [SerializeField]
     DamageNumber numberPrefab;
    [SerializeField]
    Transform numberparent;

    [SerializeField]
    CommonSkiillSelect skillSelect;
    [SerializeField]
    HaveSkillList haveSkill;
    public static InGameEvent Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if(instance == null) 
        {
            instance = this;
            LevelUpPrefab.SetActive(false);
        }
        else
        {
            Destroy(instance);
        }
    }


    public void EXP(Vector3 posi,float value)
    {
        ObjectPooling.GetObject(expPrefab.gameObject, poolingParent).TryGetComponent<EXP>(out EXP expobject);
        expobject.Init(this.transform, value);
        expobject.transform.position = posi;
        

    }

    public void LevelUpEvent()
    {
        Time.timeScale = 0;
        LevelUpPrefab.SetActive(true);
        skillSelect.InitSkill();
    }

    public void InitDamage(float value, Vector3 posi)
    {
        //DamageNumber damageNumber = numberPrefab.Spawn(transform.position, value);
        var e= ObjectPooling.GetObject(numberPrefab.gameObject, numberparent);
        e.transform.position = posi;
        e.GetComponent<DamageNumber>().number = value;


    }

    public void TimeStart()
    {
        Time.timeScale = 1;
        LevelUpPrefab.SetActive(false);
    }



    public void SelectSkill(int i)
    {
        string[] temp=skillSelect.names[i].Split("/");

        haveSkill.InputData(temp[0]+"/" +temp[1], Convert.ToInt32(temp[2]));


        LevelUpPrefab.SetActive(false);
        TimeStart();
    }
}
