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

        public string skillKey;
        

        [SerializeField]
        SkillData skillData;


        SkillMove moveFunction;

        [SerializeField]
        ObjectTable skillobjecttable;
        [SerializeField]
        SkillPattern pattern;
        [SerializeField]
        bool onMissTarget;


        IEnumerator moveCoroutine;
        IEnumerator destoryCorountine;
        [SerializeField]
        List<GameObject> attakList;



        IEnumerator DestoryTime()
        {

            yield return new WaitForSeconds(skillData.Duration);
            gameObject.SetActive(false);
            //StopCoroutine(moveCoroutine);
        }
        public void Init(SkillData skilldata)
        {

            skillData = skilldata.Clone();

            switch (skillobjecttable.FindString(skilldata.SkillObjectKey, "pattern"))
            {
                case "Straight":
                    pattern = SkillPattern.Straight;
                    moveFunction = transform.AddComponent<StraightMove>();
                    
                    break;
                case "Bounce":
                    pattern = SkillPattern.Bounce;
                    moveFunction = transform.AddComponent<BouncetMove>();
                    break;
                case "Wave":
                    pattern = SkillPattern.Wave;
                    break;
                case "Spin":
                    pattern = SkillPattern.Spin;
                    break;
            }
            if (skillobjecttable.FindString(skilldata.SkillObjectKey, "onMissTarget") == "TRUE")
            {
                onMissTarget = true;
            }
            else
            {
                onMissTarget = false;
            }


            string[] sizelist = skillobjecttable.FindString(skilldata.SkillObjectKey, "hitBoxSize").Split(",");


            Collider2D skillcollider;
            switch (skillobjecttable.FindString(skilldata.SkillObjectKey, "hitBoxShape"))
            {
                case "Square":
                    skillcollider = transform.AddComponent<BoxCollider2D>();
                    skillcollider.isTrigger = true;
                    int Wide = Convert.ToInt32(sizelist[0]);
                    int Height = Convert.ToInt32(sizelist[1]);
                    (skillcollider as BoxCollider2D).size = new Vector2(Wide, Height);
                    break;
                case "Circle":
                    skillcollider = transform.AddComponent<CircleCollider2D>();
                    skillcollider.isTrigger = true;
                    (skillcollider as CircleCollider2D).radius = Convert.ToInt32(sizelist[0]);
                    break;
            }
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (skillData.ApplyDamageTiming != SkillTiming.Enter)
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
                    Debug.Log("µ•πÃ¡ˆ¡‹");
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
            moveFunction.ObjectMove(skillData.Duration, skillData.Speed, direction);
            

            
            if (attakList == null)
                attakList = new List<GameObject>();
            attakList.Clear();
            destoryCorountine = DestoryTime();
            StartCoroutine(destoryCorountine);
        }



       



    }
}