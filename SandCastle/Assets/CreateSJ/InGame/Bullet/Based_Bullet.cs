using Enemy;
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
        public override void Move(Transform target)
        {
            this.target= target;
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
                attackCount--;
                Damaged(enemymanager);
                StopCoroutine(MoveBullet());
                if (attackCount >= 1)
                {
                    
                    return;
                }
                gameObject.SetActive(false);
                
                    
                
                
            }
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