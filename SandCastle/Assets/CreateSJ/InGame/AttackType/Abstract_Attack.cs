using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public abstract class Abstract_Attack : MonoBehaviour
    {
        
        protected bool CanAttack;
        [SerializeField] float coolTime;
        
        AttackType type;

        int attackCount;//����Ƚ��
        int multiCount;//��Ƽ��Ƚ��
        int range;//��������


        protected float defaultspeed;
        protected float defaultdamage;
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