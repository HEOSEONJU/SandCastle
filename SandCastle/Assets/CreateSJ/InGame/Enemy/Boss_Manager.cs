using Google.GData.Extensions;
using InGame;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    
    public class Boss_Manager : Enemy_Manager
    {



        public override void DeleteDistance()
        {
            if (Vector3.Distance(EnemyMove.Target.position, transform.position) >= 30)
            {
                transform.position += ((EnemyMove.Target.position - transform.position).normalized * 30);
            }
        }


        protected override void Update()
        {

            switch (state)
            {

                case EnemyState.Idle:
                    if (EnemyMove.Distance() > distance)
                    {
                        ChangeState(EnemyState.Move);

                    }
                    if (EnemyStatus.Hp <= 0)
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
                    if (EnemyMove.Distance() < distance)
                    {
                        ChangeState(EnemyState.Idle);


                    }
                    if (EnemyStatus.Hp <= 0)
                    {
                        ChangeState(EnemyState.Death);


                    }
                    break;



            }



            fsm.UpdateState();

        }
    }
}