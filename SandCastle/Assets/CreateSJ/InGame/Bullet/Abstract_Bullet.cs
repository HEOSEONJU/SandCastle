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
        protected float crp;
        [SerializeField]
        protected float crd;

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

        public abstract void Init(float defaultspeed, float defaultdamage,float crp,float crd);



        public abstract void Move(Transform target ,InGame_Char igc=null);


        protected abstract void Damaged(IHit target);

        protected abstract void OnTriggerEnter2D(Collider2D collision);


    }
}