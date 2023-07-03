using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBaseState : BaseState
    {
        protected Enemy_Manager em;

        protected EnemyBaseState(Enemy_Manager em) { this.em = em; }


        
        public override void OnStateEnter()
        {

        }
        public override void OnStateUpdate()
        {

        }
        public override void OnStateExit()
        {

        }
    }



}