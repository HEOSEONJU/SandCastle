
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using InGame;


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

        public void Init(string enemykey, ReSpwanSystem respwansystem,Transform pooling, float defaultspeed, int count)
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
            float exp= enemyTable.Findfloat(enemytablekey, "exp");



            movespeed *= defaultspeed;

            prefab.GetComponent<Enemy_Manager>().EnemyStatus.Init(hp, movespeed, attackspeed, attackrange,exp );
        }



        public void Active(float multiply)
        {
            
            StartCoroutine(Spwan(multiply));
        }


        IEnumerator Spwan(float multiply)
        {
            while (genCount > 0)
            {
                int countactive = pooling.transform.GetComponentsInChildren<Enemy_Manager>().Length;
                Debug.Log(countactive + "활성화수");
                if (countactive >= 300)
                {
                    yield return new WaitForSeconds(spwanTime);
                    continue;
                }

                Transform point = reSpwanSystem.ReturnGate();
                genCount--;
                var a = ObjectPooling.GetObject(prefab, pooling);
                a.transform.position = point.position;
                a.TryGetComponent<Enemy_Manager>(out Enemy_Manager em);
                if (!(em is null))
                {
                    em.Init(reSpwanSystem.Player, multiply);
                }
                yield return new WaitForSeconds(spwanTime);
            }

            reSpwanSystem.PlayNextWave();
            
            
        }
        



    }
}
