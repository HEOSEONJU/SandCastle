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
using Roulette;

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

        int genCount;
        public void Init(string enemykey, WaveManager wavemanager, PatrolSetting patrolsetting, float hpmultiply, string giveRewardType, int rewardAmount, int skillPointProbability,float defaultspeed,int count )
        {
            patrolSetting=patrolsetting; ;
            
            genCount = count;
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

            prefab.GetComponent<Enemy_Manager>().EnemyStatus.Init(hp * hpmultiply, movespeed, attackspeed, attackrange, resistancetype, resistancevalue);


            
            
        }

        public void Active()
        {
            StartCoroutine(Spwan());
        }

        public void Complete()
        {
            
        }



        IEnumerator Spwan()
        {




            int gennum = 0;

            while (genCount > 0)
            {
                if (patrolSetting.CheckPoint())
                {
                    genCount = 0;
                    AllDestoryGate();
                    yield break;
                }


                if (gennum == patrolSetting.GenCount)
                {
                    gennum = 0;
                }

                

                PatrolPoint patrolpoint = patrolSetting.SwpanPoint(gennum++);
                Transform point = patrolpoint.ReturnPosition();
                if (point == null)
                {
                    continue;
                }
                genCount--;
                var a = ObjectPooling.GetObject(prefab, this.transform);
                a.transform.position = point.position;
                a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
                if (!(em is null))
                {
                    em.StartMove(patrolSetting.PlayerTransform);

                }

                GOList.Add(a.gameObject);


                yield return new WaitForSeconds(spwanTime);



            }
            
            Debug.Log(GOList.Count);
            StartCoroutine(WaveComplete());
            
        }
        string HASH = "Scene";

        string OpenObjectName = "전투버툰캔버스";
        public void AllDestoryGate()
        {
            SceneMoveManager.Instance.ImmediatelyChangeScne("MainMenu");
            PlayerPrefs.SetString(HASH, OpenObjectName);
            Debug.Log(PlayerPrefs.GetString(HASH) + "저장한이름");
            PlayerPrefs.Save();
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
