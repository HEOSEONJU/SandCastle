using Enemy;
using InGame;
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
        protected IEnumerator moveCoroutine;

        [SerializeField]
        protected Animator animator;
        [SerializeField]
        protected Collider2D collider2d;

        [SerializeField]
        protected InGame_Char igc;

        public float DamagePoint
        {
            get { return damagePoint; }
            set { damagePoint = value; }
        }
        [SerializeField]
        protected int attackCount=1;

        public abstract void Init(float defaultspeed, float defaultdamage);



        public abstract void Move(Transform target ,InGame_Char igc=null);


        protected abstract void Damaged(Enemy_Manager enemymanager);

        protected abstract void OnTriggerEnter2D(Collider2D collision);


    }
}