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

        int attackCount;//공격횟수
        int multiCount;//멀티샷횟수
        int range;//범위공격
        
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


        public abstract void Effect();//추가효과가 없다면 그냥 리턴
    }
}