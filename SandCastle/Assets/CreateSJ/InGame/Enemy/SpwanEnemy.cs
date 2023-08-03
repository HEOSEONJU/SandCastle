
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using InGame;
using System.Linq;

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
        float Timer = 60f;
        

        public void Init(string[] enemykey, ReSpwanSystem respwansystem,Transform pooling, float defaultspeed, List<int> counts,float regentimer)
        {
            reSpwanSystem = respwansystem;

            this.counts = counts;
            this.pooling=pooling;
            this.regenTimer = regentimer;
            Timer = 60f;
            prefabs =new List<GameObject>();

            foreach(string key in enemykey)
            {
                //Debug.Log("키는"+key);
                prefabs.Add(Resources.Load<GameObject>("Prefab/Enemy/" + key));
                //Debug.Log(key + prefabs.Last().name);
                float movespeed = enemyTable.Findfloat(key, "moveSpeed");
                float hp = enemyTable.Findfloat(key, "hp");
                float exp = enemyTable.Findfloat(key, "exp");
                movespeed *= defaultspeed;
                prefabs.Last().GetComponent<Enemy_Manager>().EnemyStatus.Init(hp, movespeed, exp);
            }
        }



        public void Active(float multiply)
        {
            
            StartCoroutine(Spwan(multiply));
        }


        IEnumerator Spwan(float multiply)
        {
            while (Timer > 0)
            {
                int countactive = pooling.transform.GetComponentsInChildren<Enemy_Manager>().Length;
                //Debug.Log(countactive + "활성화수");
                if (countactive >= 300)
                {
                    yield return new WaitForSeconds(regenTimer);
                    continue;
                }

                
                Timer-= regenTimer;


                for(int i=0;i< prefabs.Count; i++)
                {
                    for(int j = 0; j < counts[i];j++)
                    {
                        var a = ObjectPooling.Instance.GetObject(prefabs[i], pooling);
                        
                        a.transform.position = reSpwanSystem.ReturnGate().position;
                        a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
                        if (!(em is null))
                        {
                            em.Init(reSpwanSystem.Player, multiply);
                        }

                    }

                    
                }



                
                yield return new WaitForSeconds(regenTimer);
            }

            reSpwanSystem.PlayNextWave();
            
            
        }
        



    }
}
