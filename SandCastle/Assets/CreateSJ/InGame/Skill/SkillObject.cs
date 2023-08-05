using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SkillEnums;

using System;

using Enemy;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;

namespace Skill
{

    public class SkillObject : MonoBehaviour
    {

        
        

        [SerializeField]
        SkillData skillData;

        
        SkillMove moveFunction;

        [SerializeField]
        ObjectTable skillobjecttable;
        [SerializeField]
        SkillPattern pattern;
        


        IEnumerator moveCoroutine;
        IEnumerator destoryCorountine;
        [SerializeField]
        List<GameObject> attakList;


        public SkillData SkillData
        {
            get { return skillData; }
        }

        [SerializeField]
        bool fix = false;

        IEnumerator DestoryTime()
        {

            yield return new WaitForSeconds(skillData.Duration);
            gameObject.SetActive(false);
            //StopCoroutine(moveCoroutine);
        }
        public void Init(SkillData skilldata)
        {

            skillData = skilldata.Clone();

            string[] sizelist = skillobjecttable.FindString(skilldata.SkillObjectKey, "hitBoxSize").Split(",");

            moveFunction = GetComponent<SkillMove>();
            Collider2D skillcollider;
            switch (skillobjecttable.FindString(skilldata.SkillObjectKey, "hitBoxShape"))
            {
                case "Square":
                    skillcollider = transform.GetComponent<BoxCollider2D>();
                    skillcollider.isTrigger = true;
                    float Wide = float.Parse(sizelist[0]);
                    float Height = float.Parse(sizelist[1]);
                    (skillcollider as BoxCollider2D).size = new Vector2(Wide, Height);
                    break;
                case "Circle":
                    skillcollider = transform.GetComponent<CircleCollider2D>();
                    skillcollider.isTrigger = true;
                    (skillcollider as CircleCollider2D).radius = float.Parse(sizelist[0]);
                    break;
            }
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (skillData.ApplyDamageTiming != SkillTiming.Enter)
            {
                return;

            }

            
            if (collision.CompareTag("Enemy"))
            {
                if (attakList.Contains(collision.gameObject))
                {
                    return;
                }

                if (skillData.IsPiercing-- >= 1)
                {

                    attakList.Add(collision.gameObject);
                    
                    collision.GetComponent<IHit>().Hit(skillData.Damage);
                    if (skillData.IsPiercing == 0)
                    {
                        gameObject.SetActive(false);
                        //StopCoroutine(moveCoroutine);
                        StopCoroutine(destoryCorountine);
                    }
                }
                return;
            }
        }



        public void Active(Transform spwan, Transform target)
        {
            transform.position = spwan.position;
            Vector3 direction = target.position - spwan.position;
            moveFunction.ObjectMove(skillData.Duration, skillData.Speed, direction,fix);
            

            
            if (attakList == null)
                attakList = new List<GameObject>();
            attakList.Clear();
            destoryCorountine = DestoryTime();
            StartCoroutine(destoryCorountine);
        }



       



    }
}