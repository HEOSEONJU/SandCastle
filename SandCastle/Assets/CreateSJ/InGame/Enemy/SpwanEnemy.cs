using InGameResourceEnums;
using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using InGameResourceEnums;

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
        int count = 30;

        [SerializeField]
        Transform nextPoint;
        [SerializeField]
        
        
        List<Enemy_Manager> GOList;

        PatrolSetting patrolSetting;
        [SerializeField]
        float spwanTime=1f;

        List<int> zenList;
        public void Init(string enemykey, int count, WaveManager wavemanager, PatrolSetting patrolsetting, float hpmultiply, string giveRewardType, int rewardAmount, int skillPointProbability,List<int>zenlist )
        {
            patrolSetting=patrolsetting; ;
            zenList=zenlist;
            this.count = count;
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

            e.EnemyStatus.Init(hp*hpmultiply, movespeed, attackspeed, attackrange, resistancetype, resistancevalue,givetype, rewardAmount,skillPointProbability);
            
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

            while (count-- > 0)
            {
                var a = ObjectPooling.GetObject(prefab, this.transform);
                a.transform.position = patrolSetting.SwpanPoint(zenList.First()).position;
                zenList.Remove(zenList.First());

                a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
                if (!(em is null))
                {
                    em.StartMove(patrolSetting.Nexus);
                    
                }
                yield return new WaitForSeconds(spwanTime);
            }
            GOList = transform.GetComponentsInChildren<Enemy_Manager>().ToList();
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
