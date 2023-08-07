using DamageNumbersPro;
using inGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skill;
using TMPro;

public class InGameEvent : MonoBehaviour
{
    static InGameEvent instance;


    [SerializeField]
    Transform poolingParent;

    [SerializeField]
    GameObject expPrefab;

    [SerializeField]
    Canvas LevelUpPrefab;

    [SerializeField]
    DamageNumber numberPrefab;
    [SerializeField]
    Transform numberparent;

    int deathCount;
    [SerializeField]
    TextMeshProUGUI deathCountText;
    [SerializeField]
    CommonSkiillSelect skillSelect;
    [SerializeField]
    HaveSkillList haveSkill;
    [SerializeField]
    MasterController mastercontroller;
    public static InGameEvent Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DeathCount = 0;

            LevelUpPrefab.enabled = false;
        }
        else
        {
            Destroy(instance);
        }
    }


    int DeathCount
    {
        get { return deathCount; }
        set 
        {
            deathCountText.text = value.ToString();
            deathCount = value; 
        }
    }


    public void EXP(Vector3 posi,float value)
    {
        ObjectPooling.Instance.GetObject(expPrefab.gameObject, poolingParent).TryGetComponent<EXP>(out EXP expobject);
        expobject.Init(this.transform, value);
        expobject.transform.position = posi;
        ++DeathCount;

    }

    public void LevelUpEvent()
    {
        Time.timeScale = 0;
        LevelUpPrefab.enabled=true;
        skillSelect.InitSkill();
    }

    public void InitDamage(float value, Vector3 posi)
    {
        //DamageNumber damageNumber = numberPrefab.Spawn(transform.position, value);
        var e= ObjectPooling.Instance.GetObject(numberPrefab.gameObject, numberparent);
        e.transform.position = posi;
        e.GetComponent<DamageNumber>().number = value;


    }

    public void TimeStart()
    {
        Time.timeScale = 1;
        LevelUpPrefab.enabled = false;
    }



    public void SelectSkill(int i)
    {
        //string[] temp=skillSelect.names[i].Split("/");

        haveSkill.InputData(skillSelect.names[i]);
        haveSkill.ApplyBuff(mastercontroller.InGameChar);



        TimeStart();
    }


}
