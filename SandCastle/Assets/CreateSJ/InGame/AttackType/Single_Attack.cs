using Enemy;
using inGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace InGame
{
    public class Single_Attack : Abstract_Attack
    {
        [SerializeField]
        InGame_Status status;

        [SerializeField]
        GameObject bulletPrefab;


        [SerializeField]
        Transform poolingParent;
        [SerializeField]
        Transform attackPoint;
        private void Start()
        {
            CanAttack= true;
        }

        public override void Attack(Transform target,Vector3 direction) 
        {
            

            ObjectPooling.GetObject(bulletPrefab, poolingParent).TryGetComponent<Abstract_Bullet>(out Abstract_Bullet bulletobject);
            if(bulletobject is null)
            {
                return;
            }
            bulletobject.transform.position = attackPoint.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletobject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            bulletobject.DamagePoint = status.GiveDamage;
            bulletobject.Move(target);
            CanAttack = false;
            StartCoroutine(Delay());
        }
        public override bool Require()
        {

            if (CanAttack) return true;
            else return false;

            
        }

        public override void Effect()
        {
            return;
        }

        IEnumerator Delay()
        {
             yield return new WaitForSeconds(CoolTime);
            CanAttack = true;
        }
    }
}