using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public  class EnemyMoveState : EnemyBaseState
    {
        public EnemyMoveState(Enemy_Manager em) : base(em)
        {

        }
        public override void OnStateEnter()
        {
        }
        public override void OnStateUpdate()
        {
            em.EnemyMove.MoveEnemy();
            em.DeleteDistance();

        }
        public override void OnStateExit()
        {
            em.EnemyMove.StopMove();
        }
    }
}