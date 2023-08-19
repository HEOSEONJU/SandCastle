
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
        protected ObjectTable bossTable;
 


        public void BossInputStart(string stagename, int delay, float defaultspeed, Transform playertransform, List<int> bosstimer)
        {
            player = playertransform;
            FindChild();
            InputMultiply(stagename);
            waitTime = delay;


            
            string[] bossList = roundTable.FindString(stagename, "bossGroup").Split(',');

            for (int i=0;i<4;i++) 
            {

                string enemynames = bossTable.FindString(bossList[i], "enemyKey");
                int enemycount = bossTable.FindInt(bossList[i], "count");
                Instantiate(spwanObject, spwanParent).TryGetComponent<SpwanEnemy>(out SpwanEnemy spwan);
                spwan.InitBoss(enemynames, this, pooling, defaultspeed, enemycount, bosstimer[i], hpMultiply[i]);
                spwan.gameObject.name = "보스스폰" + enemynames;
            }



            


        }


    }
}