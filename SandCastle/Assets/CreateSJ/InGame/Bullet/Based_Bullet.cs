using Enemy;
using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace inGame
{
    public class Based_Bullet : Abstract_Bullet
    {

        [SerializeField]
        Transform target;

        public override void Init(float defaultspeed, float defaultdamage)
        {

            this.speed = defaultspeed;


            damagePoint = defaultdamage;
        }
        public override void Move(Transform target,InGame_Char igc=null)
        {
            if(igc!=null)
            this.igc= igc;
            this.target= target;
            attackCount = 1;
            moveCoroutine = MoveBullet();
            StartCoroutine(moveCoroutine);
            collider2d.enabled = true;
        }
        protected override void Damaged(IHit target)
        {
            target.Hit(damagePoint);

        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy"))
            {
                collision.TryGetComponent<IHit>(out IHit target);
                if(target is null)
                {
                    return;
                }
                attackCount--;


                Damaged(target);
                if (igc != null)
                    igc.RegenMana(1);
                if (attackCount >= 1)
                {
                    
                    return;
                }
                animator.SetTrigger("Fire");

                Vector3 dircetion = collision.transform.position - transform.position;

                float angle = Mathf.Atan2(dircetion.y, dircetion.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
                StopCoroutine(moveCoroutine);
                collider2d.enabled = false;
                
                
                    
                
                
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }


        IEnumerator MoveBullet()
        {
            Vector3 direction = target.transform.position - transform.position;
            while (true)
            {
                transform.position += direction * Time.deltaTime*speed;
                yield return null;
            }
        }
    }
}