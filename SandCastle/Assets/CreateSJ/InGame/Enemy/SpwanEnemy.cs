using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Enemy
{
    public class SpwanEnemy : MonoBehaviour
    {
        [SerializeField]
        ObjectTable enemyResourceTable;
        [SerializeField]
        ObjectTable enemyTable;

        [SerializeField]
        GameObject prefab;


        [SerializeField]
        WaveManager waveManager;


        [SerializeField]
        int count = 30;

        [SerializeField]
        Transform nextPoint;
        public void Init(string enemykey, int count, WaveManager wavemanager, Transform nextpoint, float hpmultiply, string giveRewardType, int rewardAmount, int skillPointProbability)
        {

            this.count = count;
            waveManager = wavemanager;
            nextPoint = nextpoint;
            prefab = Resources.Load<GameObject>("Prefab/Enemy/" + enemykey);
            string enemytablekey = enemyResourceTable.FindData(enemykey, "enemyKey");
            string category = enemyTable.FindData(enemytablekey, "category");
            string type = enemyTable.FindData(enemytablekey, "type");
            float movespeed = float.Parse(enemyTable.FindData(enemytablekey, "moveSpeed"));
            float hp = float.Parse(enemyTable.FindData(enemytablekey, "hp"));
            float attackspeed = float.Parse(enemyTable.FindData(enemytablekey, "attackSpeed"));
            float attackrange = float.Parse(enemyTable.FindData(enemytablekey, "attackRange"));
            string resistancetypetemp = enemyTable.FindData(enemytablekey, "resistanceType");

            string[] resistancetype=null;
            if(resistancetypetemp!=null)
            {
                resistancetype = enemyTable.FindData(enemytablekey, "resistanceType").Split(",");
            
            }
            float resistancevalue = 0;
            string resistanceValueTemp = enemyTable.FindData(enemytablekey, "resistanceValue");
            if (resistanceValueTemp != "")
            {
                resistancevalue = float.Parse(resistanceValueTemp);
            }
             
            Enemy_Manager e = prefab.GetComponent<Enemy_Manager>();
            e.EnemyStatus.Init(hp*hpmultiply, movespeed, attackspeed, attackrange, resistancetype, resistancevalue);
        }

        public void Active()
        {
            StartCoroutine(Spwan());
        }

        public void Complete()
        {
            waveManager.PlayNextWave();
        }

        IEnumerator Spwan()
        {

            while (count-- > 0)
            {
                var a = ObjectPooling.GetObject(prefab, this.transform);
                a.transform.position = this.transform.position;

                a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
                if (!(em is null))
                {
                    em.StartMove(nextPoint.transform);
                    //em.Reseting(1, 10);
                }
                yield return new WaitForSeconds(1f);
            }
            Complete();
        }

    }
}
