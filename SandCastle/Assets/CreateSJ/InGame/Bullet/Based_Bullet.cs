using Enemy;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace inGame
{
    public class Based_Bullet : Abstract_Bullet
    {
        public override void Move()
        {
            attackCount = 1;
            StartCoroutine(MoveBullet());
        }
        protected override void Damaged(Enemy_Manager enemymanager)
        {
            enemymanager.Hit(damagePoint);

        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy"))
            {
                collision.TryGetComponent<Enemy_Manager>(out Enemy_Manager enemymanager);
                if(enemymanager is null)
                {
                    return;
                }
                if (attackCount >= 1)
                {
                    attackCount--;
                    Damaged(enemymanager);
                    StopCoroutine(MoveBullet());
                }
                else if(attackCount == 0) 
                {
                    attackCount--;
                    Damaged(enemymanager);
                    StopCoroutine(MoveBullet());
                    gameObject.SetActive(false);
                }
                
            }
        }

        IEnumerator MoveBullet()
        {
            while(true)
            {

                transform.localPosition += (transform.right).normalized * Time.deltaTime*speed;
                yield return null;
            }
        }
    }
}