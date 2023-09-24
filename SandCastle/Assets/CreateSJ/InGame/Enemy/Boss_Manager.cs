using Google.GData.Extensions;
using InGame;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Enemy
{
    
    public class Boss_Manager : Enemy_Manager
    {

        [SerializeField]
        List<BossBaiscSkillObject> bbso;
        [SerializeField]
        float delayTime;
        public override void DeleteDistance()
        {
            if (Vector3.Distance(EnemyMove.Target.position, transform.position) >= 15)
            {
                
                transform.position += ((EnemyMove.Target.position - transform.position).normalized * 7.5f);
            }
        }

        public void InputSkill(List<BossBaiscSkillObject> skills,float delayTime)
        {
            if(skills==null)
            {
                Debug.Log("왜널");
            }
            this.delayTime = delayTime;
            bbso = skills;
        }


        public void ActiveSKill()
        {
            Debug.Log(bbso == null);
            StartCoroutine(SkillApply());
        }
        IEnumerator SkillApply()
        {
            WaitForSeconds delay= new WaitForSeconds(delayTime);
            while (true)
            {
                yield return  delay;
                if (bbso != null)
                {
                    while (true)
                    {
                        if(bbso.FindIndex(x=>x.CoolTime==false)==-1)//모두쿨타임인 경우
                        {
                            break;
                        }

                        int INDEX = UnityEngine.Random.Range(0, bbso.Count);
                        if (bbso[INDEX].CoolTime == true)
                        {
                            continue;
                        }
                        else
                        {
                            bbso[INDEX].Active();
                            break;
                        }
                    }
                }
            }

        }



        public void StopSkill()
        {
            if (bbso != null)
            {
                foreach (BossBaiscSkillObject bb in bbso)
                {
                    bb.Stop();
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

        public override void Died()
        {

            Debug.Log("오버라이드");
            InGameEvent.Instance.EXP(transform.position, EnemyStatus.EXP);
            StopSkill();
            EnemyMove.StopMove();


            StopAllCoroutines();
            Destroy(gameObject);

        }
    }
}