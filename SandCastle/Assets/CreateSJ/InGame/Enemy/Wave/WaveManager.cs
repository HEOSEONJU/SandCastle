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
        ObjectTable waveTable;
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

            float hpmultiply = waveTable.Findfloat(stagename, "hpMultiply");
            string giverewardtype = waveTable.FindString(stagename, "giveRewardType");
            int rewardamount = waveTable.FindInt(stagename, "rewardAmount");
            int skillpointprobability = waveTable.FindInt(stagename, "skillPointProbability");


            patrolSetting.Init();
            currentWaveCount = 0;
            string waveGroup = waveTable.FindString(stagename, "waveGroup");
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