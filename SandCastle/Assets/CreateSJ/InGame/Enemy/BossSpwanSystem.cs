
using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class BossSpwanSystem : ReSpwanSystem
    {
        
        [SerializeField]
        protected ObjectTable bossSpwanTable;
        [SerializeField]
        protected ObjectTable bossSkillTable;


        public void BossInputStart(string stagename, int delay, float defaultspeed, Transform playertransform, List<int> bosstimer)
        {
            player = playertransform;
            FindChild();
            InputMultiply(stagename);
            waitTime = delay;


            
            string[] bossList = roundTable.FindString(stagename, "bossGroup").Split(',');

            for (int i=0;i< bossList.Length; i++) 
            {

                string enemynames = bossSpwanTable.FindString(bossList[i], "enemyKey");
                
                Instantiate(spwanObject, spwanParent).TryGetComponent<SpwanEnemy>(out SpwanEnemy spwan);
                spwan.InitBoss(enemynames, this, pooling, defaultspeed, bosstimer[i], hpMultiply[i], bossSkillTable);
                


                spwan.gameObject.name = "보스스폰" + enemynames;
            }



            


        }


    }
}