using InGame;
using MainUI;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class ReSpwanSystem : MonoBehaviour
    {
        [SerializeField]
        protected Transform player;

        [SerializeField]
        protected List<Transform> childs;

        [SerializeField]
        protected Transform pooling;


        [SerializeField]
        protected ObjectTable roundTable;
        [SerializeField]
        protected ObjectTable waveSpwanTable;



        
        protected int waitTime = 10;

        protected List<SpwanEnemy> spwanList;
        

        [SerializeField]
        protected GameObject spwanObject;
        [SerializeField]
        protected Transform spwanParent;


        protected List<float> hpMultiply;
        protected List<float> dmgMultiply;

        public Transform ReturnGate()
        {
            
            
            return childs[UnityEngine.Random.Range(0, childs.Count)];

        }
        public Transform Player
        {
            get { return player; }
            
        }




        public virtual void WaveInputStart(string stagename, int delay, float defaultspeed,Transform playertransform,int max)
        {
            player = playertransform;
            waitTime = delay;
            FindChild();
            InputMultiply(stagename);
            InputWaveGroup(stagename, defaultspeed,max);
            PlayNextWave();
            
            
        }
        protected void FindChild()
        {
            childs = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {

                childs.Add(transform.GetChild(i));

            }
        }

        protected void InputMultiply(string stagename)
        {
            hpMultiply = new List<float>();
            dmgMultiply = new List<float>();
            string[] templist = roundTable.FindString(stagename, "hpMultiply").Split(",");
            string[] templistdmg = roundTable.FindString(stagename, "dmgMultiply").Split(",");
            for (int i = 0; i < templist.Length; i++)
            {
                hpMultiply.Add(float.Parse(templist[i]));
                dmgMultiply.Add(float.Parse(templistdmg[i]));
            }
        }
        protected void InputWaveGroup(string stagename, float defaultspeed,int max)
        {
            string waveGroup = roundTable.FindString(stagename, "waveGroup");
            string[] WaveList = waveGroup.Split(',');
            spwanList = new List<SpwanEnemy>();
            foreach (string wavespawnkey in WaveList)
            {

                Debug.Log(waveSpwanTable.FindString(wavespawnkey, "enemyKey"));
                string[] enemynames = waveSpwanTable.FindString(wavespawnkey, "enemyKey").Split(",");


                string[] enemycount = waveSpwanTable.FindString(wavespawnkey, "count").Split(",");
                List<int> counts = new List<int>();

                foreach (string count in enemycount)
                {
                    counts.Add(Convert.ToInt32(count));
                }
                float regentimer = waveSpwanTable.Findfloat(wavespawnkey, "regenTimer");
                Instantiate(spwanObject, spwanParent).TryGetComponent<SpwanEnemy>(out SpwanEnemy spwan);
                
                spwan.Init(enemynames, this, pooling, defaultspeed, counts, regentimer, waitTime,max);
                spwanList.Add(spwan);
            }
        }




        public void PlayNextWave()
        {
            if (spwanList.Count == 0)
            {
                
                PlayerDataManager.Instacne.Data.StageClearFunction(PlayerPrefs.GetInt("Stage"));

                SceneMoveManager.Instance.ImmediatelyChangeScne("MainMenu");

                return;
            }

            spwanList.First().Active(hpMultiply.First(),dmgMultiply.First());
            
            spwanList.Remove(spwanList.First());
            hpMultiply.Remove(hpMultiply.First());
            dmgMultiply.Remove(dmgMultiply.First());







        }

    }


}