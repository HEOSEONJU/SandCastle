using InGameResourceEnums;
using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using InGameResourceEnums;
using InGame;
using Unity.VisualScripting;

namespace Enemy
{
    public class SpwanEnemy : MonoBehaviour
    {
        [SerializeField]
        ObjectTable enemyResourceTable;
        [SerializeField]
        ObjectTable enemyTable;

        [SerializeField]
        ObjectTable defineTable;

        [SerializeField]
        GameObject prefab;


        [SerializeField]
        WaveManager waveManager;




        [SerializeField]
        Transform nextPoint;
        [SerializeField]
        
        
        List<GameObject> GOList;

        PatrolSetting patrolSetting;
        [SerializeField]
        float spwanTime=1f;

        List<int> genList;
        public void Init(string enemykey, WaveManager wavemanager, PatrolSetting patrolsetting, float hpmultiply, string giveRewardType, int rewardAmount, int skillPointProbability,List<int>genlist )
        {
            patrolSetting=patrolsetting; ;
            genList=genlist;
            
            waveManager = wavemanager;
            
            prefab = Resources.Load<GameObject>("Prefab/Enemy/" + enemykey);
            string enemytablekey = enemyResourceTable.FindString(enemykey, "enemyKey");
            string category = enemyTable.FindString(enemytablekey, "category");
            string type = enemyTable.FindString(enemytablekey, "type");
            float movespeed = enemyTable.Findfloat(enemytablekey, "moveSpeed");
            float hp = enemyTable.Findfloat(enemytablekey, "hp");
            float attackspeed = enemyTable.Findfloat(enemytablekey, "attackSpeed");
            float attackrange = enemyTable.Findfloat(enemytablekey, "attackRange");
            string resistancetypetemp = enemyTable.FindString(enemytablekey, "resistanceType");

            

            string[] resistancetype=null;
            if(resistancetypetemp!=null)
            {
                resistancetype = enemyTable.FindString(enemytablekey, "resistanceType").Split(",");
            
            }
            float resistancevalue = 0;
            string resistanceValueTemp = enemyTable.FindString(enemytablekey, "resistanceValue");
            if (resistanceValueTemp != "")
            {
                resistancevalue = float.Parse(resistanceValueTemp);
            }
            movespeed *= defineTable.Findfloat("monsterdefaultspeed", "value");
            Enemy_Manager e = prefab.GetComponent<Enemy_Manager>();
            ResourceEnum givetype= ResourceEnum.mud;
            switch (giveRewardType)
            {
                case "water;":
                    givetype = ResourceEnum.water;
                    break;
                case "sand":
                    givetype = ResourceEnum.sand;
                    break;
            }

            e.EnemyStatus.Init(hp*hpmultiply, movespeed, attackspeed, attackrange, resistancetype, resistancevalue);
            
        }

        public void Active()
        {
            StartCoroutine(Spwan());
        }

        public void Complete()
        {
            
        }

        public int CountGen()
        {
            int count = 0;
            foreach (int t in genList)
            {
                count += t;
            }

            return count;
        }

        IEnumerator Spwan()
        {
            


            while (CountGen() > 0)
            {
                for(int i=0;i<genList.Count;i++)
                {
                    if (genList[i] == 0)
                        continue;

                    PatrolPoint patrolpoint = patrolSetting.SwpanPoint(i);

                    for(int j=0;j<patrolpoint.PatrolPoints.Count;j++)
                    {
                        if (genList[i] == 0)
                            break;

                        var a = ObjectPooling.GetObject(prefab, this.transform);
                        a.transform.position = patrolpoint.ReturnPosition().position;
                        a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
                        if (!(em is null))
                        {
                            em.StartMove(patrolSetting.Nexus);

                        }
                        genList[i]--;
                        GOList.Add(a.gameObject);
                    }
                    



                }
                yield return new WaitForSeconds(spwanTime);



            }
            
            Debug.Log(GOList.Count);
            StartCoroutine(WaveComplete());
            
        }
        
        IEnumerator WaveComplete()
        {

            
            while (GOList.FindIndex(x => x.gameObject.activeSelf == true) >=0)
            {
                
                yield return null;

            }
            Debug.Log("모든웨이브처치완료");
            waveManager.PlayNextWave();
        }

    }
}
