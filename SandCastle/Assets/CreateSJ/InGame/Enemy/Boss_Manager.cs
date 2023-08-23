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

        [SerializeField]
        List<BossBaiscSkillObject> bbso;
        public override void DeleteDistance()
        {
            if (Vector3.Distance(EnemyMove.Target.position, transform.position) >= 30)
            {
                transform.position += ((EnemyMove.Target.position - transform.position).normalized * 30);
            }
        }

        public void InputSkill(List<BossBaiscSkillObject> skills)
        {
            if(skills==null)
            {
                Debug.Log("¿Ö³Î");
            }
            bbso = skills;
        }


        public void ActiveSKill()
        {
            Debug.Log(bbso == null);
            if (bbso != null)
            {
                foreach (BossBaiscSkillObject bb in bbso)
                {
                    bb.Active();
                }
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