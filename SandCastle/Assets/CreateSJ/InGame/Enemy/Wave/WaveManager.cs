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
    
    private void Start()
    {

        
        float hpmultiply = float.Parse(waveTable.FindData("Wave000000", "hpMultiply"));
        string giverewardtype = waveTable.FindData("Wave000000", "giveRewardType");
        int rewardamount = Convert.ToInt32(waveTable.FindData("Wave000000", "rewardAmount"));
        int skillpointprobability = Convert.ToInt32(waveTable.FindData("Wave000000", "skillPointProbability"));





        patrolSetting.Init();
        currentWaveCount = 0;
        string waveGroup=waveTable.FindData("Wave000000", "waveGroup");
        string[] WaveList = waveGroup.Split(',');
        spwanList=new List<SpwanEnemy>();
        foreach (string wavespawnkey in WaveList)
        {
            string enemyname = waveSpwanTable.FindData(wavespawnkey, "enemyKey");
            int summoncount = Convert.ToInt32(waveSpwanTable.FindData(wavespawnkey, "count"));
            

            Debug.Log("��ȯ����" + wavespawnkey + "/ ��ȯ��" + summoncount);
            var e = Instantiate(spwanObject, this.transform);
            e.TryGetComponent<SpwanEnemy>(out SpwanEnemy spwan);
            spwan.Init(enemyname, summoncount, this, patrolSetting.FirstPosition(),hpmultiply,giverewardtype, rewardamount, skillpointprobability);
            spwanList.Add(spwan);
                

        }
        spwanList[currentWaveCount++].Active();


    }
    public void PlayNextWave()
    {
        StartCoroutine(WaitWaveTime());
        

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
