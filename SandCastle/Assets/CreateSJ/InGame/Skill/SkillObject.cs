using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SkillEnums;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using System;

public class SkillObject : MonoBehaviour
{
    SkillData skillData;


    [SerializeField]
    ObjectTable skillobjecttable;
    [SerializeField]
    SkillPattern pattern;
    [SerializeField]
    bool onMissTarget;
    [SerializeField]
    Collider2D collider;
    [SerializeField]
    Vector2 hitBoxSize;



    public void Init(SkillData skilldata)
    {
        skillData= skilldata.Clone();

        switch (skillobjecttable.FindString(skilldata.SkillObjectKey, "pattern"))
        {
            case "Straight":
                pattern = SkillPattern.Straight;
                break;
            case "Bounce":
                pattern = SkillPattern.Bounce;
                break;
            case "Wave":
                pattern = SkillPattern.Wave;
                break;
            case "Spin":
                pattern = SkillPattern.Spin;
                break;
        }
        if(skillobjecttable.FindString(skilldata.SkillObjectKey, "onMissTarget")=="TRUE")
        {
            onMissTarget = true;
        }
        else
        {
            onMissTarget = false;
        }
        switch(skillobjecttable.FindString(skilldata.SkillObjectKey, "hitBoxShape"))
        {
            case "Square":
                collider=transform.AddComponent<BoxCollider2D>();
                break;
            case "Circle":
                collider=transform.AddComponent<CircleCollider2D>();
                break;
            
        }
        collider.isTrigger = true;
        string[] sizelist = skillobjecttable.FindString(skilldata.SkillObjectKey, "hitBoxSize").Split(",");
        
        string temp = "";
        for(int i=1;i< sizelist[0].Length; i++)
        {
            temp += sizelist[0][i];
        }
        int Wide=Convert.ToInt32( temp);
        temp = "";
        for (int i = 0; i < sizelist[1].Length-1; i++)
        {
            temp += sizelist[1][i];
        }

        int Height= Convert.ToInt32(temp);


        hitBoxSize=new Vector2(Wide, Height);   




        //collider.
    }
}
