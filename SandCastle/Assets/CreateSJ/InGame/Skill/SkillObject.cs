using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillEnums;
using Enemy;


namespace Skill
{

    public class SkillObject : MonoBehaviour
    {

        
        

        [SerializeField]
        SkillData skillData;

        
        SkillMove moveFunction;

        [SerializeField]
        ObjectTable skillobjecttable;
        
        


        IEnumerator moveCoroutine;
        IEnumerator destoryCorountine;
        [SerializeField]
        List<GameObject> attackList;

        

        public SkillData SkillData
        {
            get { return skillData; }
        }


        

        IEnumerator DestoryTime()
        {

            yield return new WaitForSeconds(skillData.Duration);
            gameObject.SetActive(false);
            //StopCoroutine(moveCoroutine);
        }
        public void Init(SkillData skilldata)
        {
            
            attackList = new List<GameObject>();
            skillData = skilldata.Clone();

            string[] sizelist = skillobjecttable.FindString(skilldata.SkillObjectKey, "hitBoxSize").Split(",");

            moveFunction = GetComponent<SkillMove>();
            Collider2D skillcollider;
            
            switch (skillobjecttable.FindString(skilldata.SkillObjectKey, "hitBoxShape"))
            {
                case "Square":
                    skillcollider = transform.GetComponentInChildren<BoxCollider2D>();
                    skillcollider.isTrigger = true;
                    float Wide = float.Parse(sizelist[0]);
                    float Height = float.Parse(sizelist[1]);
                    (skillcollider as BoxCollider2D).size = new Vector2(Wide, Height);
                    break;
                case "Circle":
                    skillcollider = transform.GetComponentInChildren<CircleCollider2D>();
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
                if (attackList.Contains(collision.gameObject))
                {
                    return;
                }

                if (skillData.IsPiercing-- >= 1)
                {

                    attackList.Add(collision.gameObject);
                    
                    collision.GetComponent<IHit>().Hit(skillData.Damage,skillData.KnockBack, skillData.KnockBackPower);
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

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (skillData.ApplyDamageTiming != SkillTiming.Stay)
            {
                return;

            }


            if (collision.CompareTag("Enemy"))
            {
                if (attackList.Contains(collision.gameObject))
                {
                    return;
                }

                if (skillData.IsPiercing-- >= 1)
                {

                    attackList.Add(collision.gameObject);
                    

                    collision.GetComponent<IHit>().Hit(skillData.Damage, skillData.KnockBack, skillData.KnockBackPower);
                    StartCoroutine(RemoveEnemy(collision.gameObject));
                    
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


        IEnumerator RemoveEnemy(GameObject GO)
        {
            
            yield return new WaitForSeconds(skillData.DamageDelay);
            if (GO != null)
            {
                attackList.Remove(GO);
            }
        }

        public void Active(Transform spwan, Transform target)
        {
            transform.localScale = new Vector3(SkillData.Size, SkillData.Size, 1);
            transform.position = spwan.position;



            Vector3 direction = Vector3.zero;
            if (target !=null)
            {
                direction = target.position - spwan.position;
            }
            
            moveFunction.ObjectMove(skillData.Duration, skillData.Speed, direction);
            



            //if (attackList == null) attackList = new List<GameObject>();
            attackList.Clear();
            destoryCorountine = DestoryTime();
            StartCoroutine(destoryCorountine);
        }





        private void OnDisable()
        {
            StopAllCoroutines();

            
        }

    }
    
}