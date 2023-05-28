using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inGame
{
    public abstract class Abstract_Bullet : MonoBehaviour
    {
        [SerializeField]
        protected float speed;

        [SerializeField]
        protected float damagePoint;
        [SerializeField]
        protected int attackCount=1;
        public abstract void Move();


        protected abstract void Damaged(Enemy_Manager enemymanager);

        protected abstract void OnTriggerEnter2D(Collider2D collision);


    }
}