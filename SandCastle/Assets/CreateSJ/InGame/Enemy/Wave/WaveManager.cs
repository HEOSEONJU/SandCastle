using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    public void WaveInputStart()
    {

        float hpmultiply = waveTable.Findfloat("Wave000000", "hpMultiply");
        string giverewardtype = waveTable.FindString("Wave000000", "giveRewardType");
        int rewardamount = waveTable.FindInt("Wave000000", "rewardAmount");
        int skillpointprobability = waveTable.FindInt("Wave000000", "skillPointProbability");


        patrolSetting.Init();
        currentWaveCount = 0;
        string waveGroup = waveTable.FindString("Wave000000", "waveGroup");
        string[] WaveList = waveGroup.Split(',');
        spwanList = new List<SpwanEnemy>();
        foreach (string wavespawnkey in WaveList)
        {
            string enemyname = waveSpwanTable.FindString(wavespawnkey, "enemyKey");
            int summoncount = waveSpwanTable.FindInt(wavespawnkey, "count");


            Debug.Log("소환할적" + wavespawnkey + "/ 소환수" + summoncount);
            var e = Instantiate(spwanObject, this.transform);
            e.TryGetComponent<SpwanEnemy>(out SpwanEnemy spwan);
            spwan.Init(enemyname, summoncount, this, patrolSetting, hpmultiply, giverewardtype, rewardamount, skillpointprobability);
            spwanList.Add(spwan);


        }
        spwanList[currentWaveCount++].Active();
        WaveCorountine = WaitWaveTime();
    }


    public void PlayNextWave()
    {
        StartCoroutine(WaveCorountine);
        

    }
    IEnumerator WaitWaveTime()
    {
        if (currentWaveCount == spwanList.Count)
        {
            yield break;
        }
        yield return new WaitForSeconds(waitTime);


        spwanList[currentWaveCount++].Active();
    }
}
