using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SkillEnums;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using System;
using System.Text;
using Enemy;

public class SkillObject : MonoBehaviour
{

    string skillKey = "Skillobject00001";
    [SerializeField]
    SkillData skillData;


    [SerializeField]
    ObjectTable skillobjecttable;
    [SerializeField]
    SkillPattern pattern;
    [SerializeField]
    bool onMissTarget;
    [SerializeField]
    Collider2D skillcollider;

    IEnumerator moveCoroutine;
    IEnumerator destoryCorountine;
    List<GameObject> attakList;



    IEnumerator DestoryTime()
    {
        Debug.Log("듀레이선시작");
        yield return new WaitForSeconds(skillData.Duration);
        Debug.Log("듀레이선종료");
        gameObject.SetActive(false);
        StopCoroutine(moveCoroutine);
    }
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
        string[] sizelist = skillobjecttable.FindString(skilldata.SkillObjectKey, "hitBoxSize").Split(",");
        StringBuilder temp = new StringBuilder();
        
        switch (skillobjecttable.FindString(skilldata.SkillObjectKey, "hitBoxShape"))
        {
            case "Square":
                skillcollider = transform.AddComponent<BoxCollider2D>();
                skillcollider.isTrigger = true;

                for (int i = 0; i < sizelist[0].Length; i++)
                {
                    temp.Append(sizelist[0][i]);
                }
                
                
                int Wide = Convert.ToInt32(temp.ToString());
                temp.Clear();
                for (int i = 0; i < sizelist[1].Length; i++)
                {
                    temp.Append(sizelist[1][i]);
                }
                
                int Height = Convert.ToInt32(temp.ToString());
                (skillcollider as BoxCollider2D).size = new Vector2(Wide,Height);
                break;
            case "Circle":
                skillcollider = transform.AddComponent<CircleCollider2D>();
                skillcollider.isTrigger = true;
                (skillcollider as CircleCollider2D).radius= Convert.ToInt32(sizelist[0]);
                
                break;
            
        }
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(skillData.ApplyDamageTiming!=SkillTiming.Enter)
        {
            return;
            
        }

        Debug.Log(collision.tag);
        if (collision.CompareTag("Enemy"))
        {
            if (attakList.Contains(collision.gameObject))
            {
                return;
            }

            if (skillData.IsPiercing-- >= 1)
            {

                attakList.Add(collision.gameObject);
                Debug.Log("데미지줌");
                collision.GetComponent<Enemy_Manager>().Hit(skillData.Damage);
                if (skillData.IsPiercing == 0)
                {
                    gameObject.SetActive(false);
                    StopCoroutine(moveCoroutine);
                    StopCoroutine(destoryCorountine);
                }
            }
            return;
        }
    }

    

    public void Active(Transform spwan,Transform target)
    {
        transform.position = spwan.position;
        Vector3 direction = target.position - spwan.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        moveCoroutine = ObjectMove(spwan);

        StartCoroutine(moveCoroutine);
        if (attakList == null)
            attakList = new List<GameObject>();
        attakList.Clear();
        destoryCorountine = DestoryTime();
        StartCoroutine(destoryCorountine);
    }

    IEnumerator  ObjectMove(Transform spwan)
    {
        float duration = skillData.Duration;

        switch (pattern)
        {
            
            case SkillPattern.Straight:
                while (duration>0)
                {
                    duration -= Time.deltaTime;
                    transform.localPosition += (transform.right).normalized * Time.deltaTime * skillData.Speed;
                    yield return null;
                }


                break;
            case SkillPattern.Bounce:
                break;
            case SkillPattern.Wave:
                break;
            case SkillPattern.Spin:
                break;
        }
        
    }

}
