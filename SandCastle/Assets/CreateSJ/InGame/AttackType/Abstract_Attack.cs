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
        
        public AttackType Type
        {
            get { return type; }
        }

        public float CoolTime
        {
            get { return coolTime; }
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

        public abstract void Attack(List<Enemy_Manager> enemymangers,Vector3 direction);
        public abstract bool Require();


        public abstract void Effect();//�߰�ȿ���� ���ٸ� �׳� ����
    }
}