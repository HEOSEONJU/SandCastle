
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
                //Debug.Log(key + prefabs.Last().name);
                float movespeed = enemyTable.Findfloat(key, "moveSpeed");
                float hp = enemyTable.Findfloat(key, "hp");
                float exp = enemyTable.Findfloat(key, "exp");
                int ap = enemyTable.FindInt(key, "ap");
                movespeed *= defaultspeed;
                prefabs.Last().GetComponent<Enemy_Manager>().EnemyStatus.Init(hp, movespeed, exp, ap);
            }
        }


        public void InitBoss(string enemykey, BossSpwanSystem respwansystem, Transform pooling, float defaultspeed, int counts, float regentimer,float multiply)
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
            prefabs.Last().GetComponent<Enemy_Manager>().EnemyStatus.Init(hp, movespeed, exp, ap);
            
            StartCoroutine(BossSpwan(multiply,regentimer));
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
            a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
            if (!(em is null))
            {
                em.Init(reSpwanSystem.Player, multiply);
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

