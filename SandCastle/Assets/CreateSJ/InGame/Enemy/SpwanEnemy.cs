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
        ReSpwanSystem reSpwanSystem;



        [SerializeField]
        Transform pooling;

        [SerializeField]
        Transform nextPoint;
        [SerializeField]
        
        
        List<GameObject> GOList;

        PatrolSetting patrolSetting;
        [SerializeField]
        float spwanTime=1f;

        int genCount;

        public void Init(string enemykey, ReSpwanSystem respwansystem,Transform pooling, float hpmultiply, string giveRewardType, int rewardAmount, int skillPointProbability, float defaultspeed, int count)
        {
            reSpwanSystem = respwansystem;

            genCount = count;
            this.pooling=pooling;

            prefab = Resources.Load<GameObject>("Prefab/Enemy/" + enemykey);
            string enemytablekey = enemyResourceTable.FindString(enemykey, "enemyKey");
            string category = enemyTable.FindString(enemytablekey, "category");
            string type = enemyTable.FindString(enemytablekey, "type");
            float movespeed = enemyTable.Findfloat(enemytablekey, "moveSpeed");
            float hp = enemyTable.Findfloat(enemytablekey, "hp");
            float attackspeed = enemyTable.Findfloat(enemytablekey, "attackSpeed");
            float attackrange = enemyTable.Findfloat(enemytablekey, "attackRange");
            string resistancetypetemp = enemyTable.FindString(enemytablekey, "resistanceType");



            string[] resistancetype = null;
            if (resistancetypetemp != null)
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


        IEnumerator Spwan()
        {
            while (genCount > 0)
            {
                Transform point = reSpwanSystem.ReturnGate();
                genCount--;
                var a = ObjectPooling.GetObject(prefab, pooling);
                a.transform.position = point.position;
                a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
                if (!(em is null))
                {
                    em.StartMove(reSpwanSystem.Player);
                }
                yield return new WaitForSeconds(spwanTime);
            }

            reSpwanSystem.PlayNextWave();
            
            
        }
        



    }
}
