using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public  class EnemyIdleState : EnemyBaseState
    {

        public EnemyIdleState(Enemy_Manager em) : base(em)
        {

        }
        

        public override void OnStateEnter()
        {
            em.BaseAttack();
        }
        public override void OnStateUpdate()
        {

        }
        public override void OnStateExit()
        {

        }
    }



}