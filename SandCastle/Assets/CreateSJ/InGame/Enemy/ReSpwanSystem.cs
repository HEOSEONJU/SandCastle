using InGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


        List<float> hpMultiply;
        int gatecount = 0;
        public Transform ReturnGate()
        {
            if(gatecount==childs.Count) { gatecount = 0; }
            
            return childs[Random.Range(0, childs.Count)];

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

            hpMultiply=new List<float>();
            
            string[] templist = roundTable.FindString(stagename, "hpMultiply").Split(",");

            for(int i=0; i < templist.Length;i++)
            {
                hpMultiply.Add (float.Parse(templist[i]));
            }

            


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
                spwan.Init(enemyname, this, pooling, defaultspeed, count);
                spwanList.Add(spwan);


            }

            spwanList[0].Active(hpMultiply[0]);
            WaveCorountine = WaitWaveTime();
        }


        public void PlayNextWave()
        {
            WaveCorountine = WaitWaveTime();
            if (currentWaveCount <= spwanList.Count)
            {
                currentWaveCount++;
            }
            

            
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

            for(int i=0;i<= currentWaveCount;i++)
            {
                spwanList[i].Active(hpMultiply[i]);
            }
            
        }
    }


}