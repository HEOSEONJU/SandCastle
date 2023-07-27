using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class ReSpwanSystem : MonoBehaviour
    {
        [SerializeField]
        Transform player;

        [SerializeField]
        List<Transform> childs;

        [SerializeField]
        Transform pooling;


        [SerializeField]
        ObjectTable roundTable;
        [SerializeField]
        ObjectTable waveSpwanTable;



        int currentWaveCount;
        float waitTime = 10f;

        List<SpwanEnemy> spwanList;
        

        IEnumerator WaveCorountine;
        [SerializeField]
        GameObject spwanObject;
        [SerializeField]
        Transform spwanParent;

        int gatecount = 0;
        public Transform ReturnGate()
        {
            if(gatecount==childs.Count) { gatecount = 0; }

            return childs[gatecount++];

        }
        public Transform Player
        {
            get { return player; }
            
        }




        public void WaveInputStart(string stagename, float delay, float defaultspeed,Transform playertransform)
        {
            player = playertransform;
            childs = new List<Transform>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                
                childs.Add(transform.GetChild(i));
                
            }


            float hpmultiply = roundTable.Findfloat(stagename, "hpMultiply");
            string giverewardtype = roundTable.FindString(stagename, "giveRewardType");
            int rewardamount = roundTable.FindInt(stagename, "rewardAmount");
            int skillpointprobability = roundTable.FindInt(stagename, "skillPointProbability");


            currentWaveCount = 0;
            waitTime = delay;
            string waveGroup = roundTable.FindString(stagename, "waveGroup");
            string[] WaveList = waveGroup.Split(',');
            spwanList = new List<SpwanEnemy>();
            foreach (string wavespawnkey in WaveList)
            {
                string enemyname = waveSpwanTable.FindString(wavespawnkey, "enemyKey");

                int count = waveSpwanTable.FindInt(wavespawnkey, "count");
                
                Instantiate(spwanObject, spwanParent).TryGetComponent<SpwanEnemy>(out SpwanEnemy spwan);
                spwan.Init(enemyname, this, pooling, hpmultiply, giverewardtype, rewardamount, skillpointprobability, defaultspeed, count);
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
            Debug.Log(currentWaveCount + "/" + spwanList.Count);
            if (currentWaveCount == spwanList.Count)
            {
                yield break;
            }
            yield return new WaitForSeconds(waitTime);


            
            spwanList[currentWaveCount++].Active();
        }
    }


}