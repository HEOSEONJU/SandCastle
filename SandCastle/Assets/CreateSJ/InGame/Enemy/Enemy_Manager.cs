using Google.GData.Extensions;
using InGame;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    
    public class Enemy_Manager : MonoBehaviour,IHit
    {


        [SerializeField]
        InGame.FSM fsm;
        [SerializeField]
        protected EnemyState state;

        [SerializeField]
        Enemy_Move enemyMove;
        [SerializeField]
        Enemy_Status enemyStatus;
        [SerializeField]
        Slider hpSlider;

        public Enemy_Move EnemyMove
        { get { return enemyMove; } 
        }
        public Enemy_Status EnemyStatus
        {
            get { return enemyStatus; }
        }



        [SerializeField]
        List<Transform> points;

        public void StartMove(Transform point)//이동명령
        {
            enemyMove.SettingPoint(point);
            enemyMove.MoveEnemy();
            fsm = new FSM(new EnemyMoveState(this));
            

        }
        public void Hit(float value)//공격받음
        {
            enemyStatus.Hp -= value;
            hpSlider.value = enemyStatus.HPPercentage;
            if (enemyStatus.Hp <= 0)
            {
                state = EnemyState.Death;
                
            }
        }

        public void Died()
        {

            //경험치드랍
            enemyMove.StopMove();
            gameObject.SetActive(false);
        }
        public bool Alive()//살아있는지 체크
        {
            if (enemyStatus.Hp <= 0)
            {
                return false;
            }
            else
                return true;

        }

        public void PlayerMeleeAttack()//플레이어 닿았을때공격
        {

            if (enemyStatus.Hp <= 0)
            {
                return;
            }
            
            
            RaycastHit2D[] hitlist = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, Vector2.zero);
            if(hitlist.Length==0)
            {
                return;
            }

            foreach (var hit in hitlist)
            {
                if(hit.collider.transform.TryGetComponent<InGame_Char>(out InGame_Char player))
                {
                    break;
                }
            }
            //player 데미지
            gameObject.SetActive(false);
        }




        void Update()
        {

            switch (state)
            {

                case EnemyState.Idle:

                    break;
                case EnemyState.Skill:

                    break;
                case EnemyState.Attack:

                    break;
                case EnemyState.Death:

                    break;
                case EnemyState.Move:
                    if(enemyMove.Distance()<0.2f)
                    {
                        ChangeState(EnemyState.Idle);
                        
                    }
                    break;



            }


            fsm.UpdateState();

        }







        void ChangeState(EnemyState next)
        {
            //Debug.Log("상태변경"+next);
            state = next;
            switch (state)
            {

                case EnemyState.Idle:
                    fsm.ChangeState(new EnemyIdleState(this));
                    break;
                case EnemyState.Skill:
                    fsm.ChangeState(new EnemySkillState(this));
                    break;
                case EnemyState.Attack:
                    fsm.ChangeState(new EnemyAttackState(this));
                    break;

                case EnemyState.Death:
                    fsm.ChangeState(new EnemyDeathState(this));
                    break;
                case EnemyState.Move:
                    fsm.ChangeState(new EnemyMoveState(this));
                    break;
            }
        }

    }
}