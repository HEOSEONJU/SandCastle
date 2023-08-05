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
        float distance=0.1f;
        

        public void Init(Transform player,float multiply)//FSM 캐릭터위치 입력
        {
            enemyMove.Target=player;
            enemyStatus.ResetHP(multiply);
            fsm = new FSM(new EnemyMoveState(this));
            ChangeState(EnemyState.Idle);
            hpSlider.value = enemyStatus.HPPercentage;
        }
        public void Hit(float value)//공격받음
        {
            enemyStatus.Hp -= value;
            hpSlider.value = enemyStatus.HPPercentage;

        }

        public void Died()
        {

            
            InGameEvent.Instance.EXP(transform.position, enemyStatus.EXP);
            Disable();
            
        }
        public void Disable()
        {
            enemyMove.StopMove();
            gameObject.SetActive(false);
        }
        public void DeleteDistance()
        {
            if(Vector3.Distance(enemyMove.Target.position,transform.position)>=30)
            {
                Disable();
            }
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
                    //Debug.Log(player.name);
                    break;
                }
            }
            //player 데미지
            return;
            
        }




        void Update()
        {

            switch (state)
            {

                case EnemyState.Idle:
                    if (enemyMove.Distance() > distance)
                    {
                        ChangeState(EnemyState.Move);

                    }
                    if (enemyStatus.Hp <= 0)
                    {
                        ChangeState(EnemyState.Death);


                    }
                    break;
                case EnemyState.Skill:

                    break;
                case EnemyState.Attack:

                    break;
                case EnemyState.Death:

                    break;
                case EnemyState.Move:
                    if(enemyMove.Distance()< distance)
                    {
                        ChangeState(EnemyState.Idle);
                        
                        
                    }
                    if (enemyStatus.Hp <= 0)
                    {
                        ChangeState(EnemyState.Death);


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