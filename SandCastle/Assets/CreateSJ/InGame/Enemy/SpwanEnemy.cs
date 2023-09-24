
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using InGame;
using System.Linq;
using Unity.VisualScripting;

namespace Enemy
{
    public class SpwanEnemy : MonoBehaviour
    {

        [SerializeField]
        ObjectTable enemyTable;

        [SerializeField]
        ObjectTable defineTable;

        [SerializeField]
        List<GameObject> prefabs;
        [SerializeField]
        List<int> counts;



        [SerializeField]
        ReSpwanSystem reSpwanSystem;



        [SerializeField]
        Transform pooling;

        [SerializeField]
        Transform nextPoint;
        [SerializeField]


        List<GameObject> GOList;

        PatrolSetting patrolSetting;
        [SerializeField]
        float regenTimer = 1f;
        [SerializeField]
        public float Timer = 60f;


        public void Init(string[] enemykey, ReSpwanSystem respwansystem, Transform pooling, float defaultspeed, List<int> counts, float regentimer, int timer)
        {
            reSpwanSystem = respwansystem;

            this.counts = counts;
            this.pooling = pooling;
            this.regenTimer = regentimer;
            Timer = timer;
            prefabs = new List<GameObject>();

            foreach (string key in enemykey)
            {
                
                prefabs.Add(Resources.Load<GameObject>("Prefab/Enemy/" + key));
                

                
                float movespeed = enemyTable.Findfloat(key, "moveSpeed");
                float hp = enemyTable.Findfloat(key, "hp");
                float exp = enemyTable.Findfloat(key, "exp");
                int ap = enemyTable.FindInt(key, "ap");
                movespeed *= defaultspeed;
                prefabs.Last().GetComponent<Enemy_Manager>().EnemyStatus.Init(hp, movespeed, exp, ap);
            }
        }


        public void InitBoss(string enemykey, BossSpwanSystem respwansystem, Transform pooling, float defaultspeed , float regentimer,float multiply,ObjectTable bossskilltable)
        {
            reSpwanSystem = respwansystem;

            


            this.pooling = pooling;
            
            
            prefabs = new List<GameObject>();
            prefabs.Add(Resources.Load<GameObject>("Prefab/Enemy/Boss/" + enemykey));
            float movespeed = enemyTable.Findfloat(enemykey, "moveSpeed");
            float hp = enemyTable.Findfloat(enemykey, "hp");
            float exp = enemyTable.Findfloat(enemykey, "exp");
            int ap = enemyTable.FindInt(enemykey, "ap");
            movespeed *= defaultspeed;


            
            if (prefabs.Last().TryGetComponent<Boss_Manager>(out Boss_Manager em))
            {
                em.EnemyStatus.Init(hp, movespeed, exp, ap);
                float size = enemyTable.Findfloat(enemykey,"scale");
                em.transform.localScale = new Vector3(size, size, 1);
                List<BossBaiscSkillObject> bbso = new List<BossBaiscSkillObject>();
                if (enemyTable.FindString(enemykey, "skill").Length==0)
                {
                    em.InputSkill(bbso, enemyTable.Findfloat(enemykey, "coolTime"));
                    StartCoroutine(BossSpwan(multiply, regentimer));
                    return;
                }
                string[] bossskills = enemyTable.FindString(enemykey, "skill").Split(",");
                
                
                foreach(string skillname in bossskills)
                {
                    var e = Resources.Load<GameObject>("Prefab/SkillPrefab/BossSkill/" + skillname);
                    
                    
                    int damage = bossskilltable.FindInt(skillname, "damage");
                    
                    float duration = bossskilltable.Findfloat(skillname, "duration");
                    float delay = bossskilltable.Findfloat(skillname, "delay");
                    
                    float warnnig = bossskilltable.Findfloat(skillname, "warnnig");

                    BossBaiscSkillObject temp = Instantiate(e).GetComponent<BossBaiscSkillObject>();
                    temp.Init(damage,duration,delay, warnnig, reSpwanSystem.Player);
                    bbso.Add(temp);
                    
                }
                

                em.InputSkill(bbso, enemyTable.Findfloat(enemykey, "coolTime"));




                StartCoroutine(BossSpwan(multiply, regentimer));
            }
            
        }




        public void Active(float multiply,float dmgmultiply)
        {

            StartCoroutine(Spwan(multiply, dmgmultiply));
        }


        IEnumerator BossSpwan(float multiply,float delay)
        {
            yield return new WaitForSeconds(delay);
            
            var a = ObjectPooling.Instance.GetObject(prefabs.First(), this.transform);
            
            a.transform.position = reSpwanSystem.ReturnGate().position;
            a.TryGetComponent<Boss_Manager>(out Boss_Manager em);
            if (!(em is null))
            {
                em.Init(reSpwanSystem.Player, multiply);
                em.ActiveSKill();
            }
        }

        IEnumerator Spwan(float multiply, float dmgmultiply)
        {
            WaitForSeconds time = new WaitForSeconds(regenTimer);
            while (Timer > 0)
            {
                int countactive = pooling.transform.GetComponentsInChildren<Enemy_Manager>().Length;

                Timer -= regenTimer;
                if (countactive >= 300)
                {
                    yield return time;
                    continue;
                }
                for (int i = 0; i < prefabs.Count; i++)
                {
                    for (int j = 0; j < counts[j]; j++)
                    {
                        
                        var a = ObjectPooling.Instance.GetObject(prefabs[i], this.transform);
                        
                        a.transform.position = reSpwanSystem.ReturnGate().position;
                        a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
                        if (!(em is null))
                        {
                            em.Init(reSpwanSystem.Player, multiply,dmgmultiply);
                        }
                    }
                }
                if (Timer <= 0)
                {
                    reSpwanSystem.PlayNextWave();
                }
                yield return time;
            }
        }
    }
}

