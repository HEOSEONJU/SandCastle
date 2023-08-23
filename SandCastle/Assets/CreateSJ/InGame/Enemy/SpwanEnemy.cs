
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
                //Debug.Log("키는"+key);
                prefabs.Add(Resources.Load<GameObject>("Prefab/Enemy/" + key));
                Debug.Log(prefabs.Last().gameObject.name);

                //Debug.Log(key + prefabs.Last().name);
                float movespeed = enemyTable.Findfloat(key, "moveSpeed");
                float hp = enemyTable.Findfloat(key, "hp");
                float exp = enemyTable.Findfloat(key, "exp");
                int ap = enemyTable.FindInt(key, "ap");
                movespeed *= defaultspeed;
                prefabs.Last().GetComponent<Enemy_Manager>().EnemyStatus.Init(hp, movespeed, exp, ap);
            }
        }


        public void InitBoss(string enemykey, BossSpwanSystem respwansystem, Transform pooling, float defaultspeed, int counts, float regentimer,float multiply,ObjectTable bossskilltable)
        {
            reSpwanSystem = respwansystem;

            this.counts = new List<int>(counts);


            this.pooling = pooling;
            
            
            prefabs = new List<GameObject>();
            prefabs.Add(Resources.Load<GameObject>("Prefab/Enemy/" + enemykey));
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
                string[] bossskills = enemyTable.FindString(enemykey, "skill").Split(",");
                
                List<BossBaiscSkillObject> bbso= new List<BossBaiscSkillObject>();
                foreach(string skillname in bossskills)
                {
                    var e = Resources.Load<GameObject>("Prefab/SkillPrefab/BossSkill/" + skillname);
                    Debug.Log(skillname);
                    
                    int damage = bossskilltable.FindInt(skillname, "damage");
                    
                    float duration = bossskilltable.Findfloat(skillname, "duration");
                    float delay = bossskilltable.Findfloat(skillname, "delay");
                    
                    float warnnig = bossskilltable.Findfloat(skillname, "warnnig");

                    BossBaiscSkillObject temp = Instantiate(e).GetComponent<BossBaiscSkillObject>();
                    temp.Init(damage,duration,delay, warnnig, reSpwanSystem.Player);
                    bbso.Add(temp);
                    
                }
                Debug.Log("갯수" + bbso.Count);
                em.InputSkill(bbso);




                StartCoroutine(BossSpwan(multiply, regentimer));
            }
            
        }




        public void Active(float multiply)
        {

            StartCoroutine(Spwan(multiply));
        }


        IEnumerator BossSpwan(float multiply,float delay)
        {
            yield return new WaitForSeconds(delay);
            
            var a = ObjectPooling.Instance.GetObject(prefabs.First(), this.transform);
            Debug.Log("생적 이름" + a.name);
            a.transform.position = reSpwanSystem.ReturnGate().position;
            a.TryGetComponent<Boss_Manager>(out Boss_Manager em);
            if (!(em is null))
            {
                em.Init(reSpwanSystem.Player, multiply);
                em.ActiveSKill();
            }
        }

        IEnumerator Spwan(float multiply)
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
                    for (int j = 0; j < counts[i]; j++)
                    {
                        Debug.Log("찾는오브젝트네임" + prefabs[i].name);
                        var a = ObjectPooling.Instance.GetObject(prefabs[i], this.transform);
                        Debug.Log("생적 이름" + a.name);
                        a.transform.position = reSpwanSystem.ReturnGate().position;
                        a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
                        if (!(em is null))
                        {
                            em.Init(reSpwanSystem.Player, multiply);
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

