using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public  class EnemyDeathState : EnemyBaseState
    {

        public EnemyDeathState(Enemy_Manager em) : base(em)
        {

        }


        public override void OnStateEnter()
        {
            em.Died();
        }
        public override void OnStateUpdate()
        {

        }
        public override void OnStateExit()
        {

        }
    }



}