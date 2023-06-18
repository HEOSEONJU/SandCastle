using Enemy;
using inGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public abstract class Abstract_Attack : MonoBehaviour
    {

        [SerializeField]
        protected InGame_Char igc;
        [SerializeField]
        protected InGame_Status status;

        protected bool CanAttack;
        [SerializeField] float coolTime;

        [SerializeField]
        protected Abstract_Bullet bulletPrefab;

        AttackType type;

        int attackCount;//����Ƚ��
        int multiCount;//��Ƽ��Ƚ��
        int range;//��������


        protected float defaultspeed;
        protected float defaultdamage;
        [SerializeField]
        protected Transform poolingParent;

        public Transform PoolingParent
        {
            set { poolingParent = value; }
        }
        public AttackType Type
        {
            get { return type; }
        }

        public float CoolTime
        {
            get { return coolTime; }
            set { coolTime = value; }
        }
        public int AttackCount
        {
            get { return attackCount; }
        }
        public int MultiCount
        {
            get { return multiCount; }
        }
        public int Range
        {
            get { return range; }
        }

        public abstract void SettingBulletInfo(float defaultspeed, float defaultdamage);

        public abstract void Attack(Transform target, Vector3 direction);
        public abstract bool Require();


        public abstract void Effect();//�߰�ȿ���� ���ٸ� �׳� ����
    }
}