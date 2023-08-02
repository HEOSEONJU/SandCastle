using Enemy;
using InGame;
using System.Collections;
using UnityEngine;

namespace inGame
{
    public class Based_Bullet : Abstract_Bullet
    {

        [SerializeField]
        Transform target;


        public override void Init(float defaultspeed, float defaultdamage, float crp, float crd)
        {
            this.speed = defaultspeed;
            this.crp = 1-crp;
            this.crd = crd;
            

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
            float probability = Random.Range(0f, 1f);

            float value = damagePoint;

            if (crp<=probability)
            {
                value *= crd;
                Debug.Log(probability + "확률 /" +"현재데미지:"+ damagePoint +"/현재치명타데미지"+ value);
            }
            
            target.Hit(value);

            InGameEvent.Instance.InitDamage(damagePoint, transform.position);



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