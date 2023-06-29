using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace InGame
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField]
        ObjectTable roundTable;
        [SerializeField]
        ObjectTable waveSpwanTable;
        [SerializeField]
        PatrolSetting patrolSetting;


        int currentWaveCount;
        float waitTime = 10f;

        List<SpwanEnemy> spwanList;
        [SerializeField]
        GameObject spwanObject;

        IEnumerator WaveCorountine;

        public void WaveInputStart(string stagename)
        {

            float hpmultiply = roundTable.Findfloat(stagename, "hpMultiply");
            string giverewardtype = roundTable.FindString(stagename, "giveRewardType");
            int rewardamount = roundTable.FindInt(stagename, "rewardAmount");
            int skillpointprobability = roundTable.FindInt(stagename, "skillPointProbability");


            patrolSetting.Init();
            currentWaveCount = 0;
            string waveGroup = roundTable.FindString(stagename, "waveGroup");
            string[] WaveList = waveGroup.Split(',');
            spwanList = new List<SpwanEnemy>();
            foreach (string wavespawnkey in WaveList)
            {
                string enemyname = waveSpwanTable.FindString(wavespawnkey, "enemyKey");

                int count = waveSpwanTable.FindInt(wavespawnkey, "count");
                Instantiate(spwanObject, this.transform).TryGetComponent<SpwanEnemy>(out SpwanEnemy spwan);
                
                spwan.Init(enemyname, this, patrolSetting, hpmultiply, giverewardtype, rewardamount, skillpointprobability, count);
                spwanList.Add(spwan);


            }
            spwanList[currentWaveCount++].Active();
            WaveCorountine = WaitWaveTime();
        }


        public void PlayNextWave()
        {
            WaveCorountine = WaitWaveTime();
            StartCoroutine(WaveCorountine);


        }
        IEnumerator WaitWaveTime()
        {
            Debug.Log(currentWaveCount +"/" + spwanList.Count);
            if (currentWaveCount == spwanList.Count)
            {
                yield break;
            }
            yield return new WaitForSeconds(waitTime);


            spwanList[currentWaveCount++].Active();
        }
    }
}