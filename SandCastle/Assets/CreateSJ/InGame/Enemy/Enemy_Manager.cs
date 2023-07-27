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
        

        public void Init(Transform player)//FSM ĳ������ġ �Է�
        {
            enemyMove.SettingPlayer(player);
            fsm = new FSM(new EnemyMoveState(this));
            

        }
        public void Hit(float value)//���ݹ���
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

            //����ġ���
            enemyMove.StopMove();
            gameObject.SetActive(false);
        }
        public bool Alive()//����ִ��� üũ
        {
            if (enemyStatus.Hp <= 0)
            {
                return false;
            }
            else
                return true;

        }

        public void PlayerMeleeAttack()//�÷��̾� �����������
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
                    Debug.Log(player.name);
                    break;
                }
            }
            //player ������
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
                        Debug.Log("Idle����");
                        
                    }
                    break;



            }


            
            fsm.UpdateState();
            
        }







        void ChangeState(EnemyState next)
        {
            //Debug.Log("���º���"+next);
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