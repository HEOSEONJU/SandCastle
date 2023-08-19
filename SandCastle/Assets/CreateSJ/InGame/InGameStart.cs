using Skill;
using Enemy;
using inGame;
using MainUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Player;
using System.Linq;

namespace InGame
{
    public class InGameStart : MonoBehaviour
    {


        [SerializeField]
        InGame_Char charPrefab;
        [SerializeField]
        InGame_Char charPrefab2;
        [SerializeField]
        InGame_Char charPrefab3;



        [SerializeField]
        MasterController masterController;
        


        [SerializeField]
        InGameCharInit inGameCharInit;
        [SerializeField]
        InGame_Inventory inventory;
        [SerializeField]
        InGameSkillSensor sensor;

        [SerializeField]
        List<Transform> skillPoolingParent;
        [SerializeField]
        Transform mainBulletPoolingParent;
        [SerializeField]
        List<Transform> subBulletPoolingParent;



        [SerializeField]
        
        ReSpwanSystem reSpwanSystem;
        [SerializeField]

        BossSpwanSystem bossSpwanSystem;
        [SerializeField]
        HaveSkillList haveSkillList;

        [Header("테이블")]
        [SerializeField]
        ObjectTable roundTable;
        [SerializeField]
        ObjectTable skillTable;
        [SerializeField]
        ObjectTable charTable;
        [SerializeField]
        ObjectTable defineTable;

        private void Start()
        {

            int T = PlayerPrefs.GetInt("Stage");
            string stagename = roundTable.values[T + 1].ToString();

            int level = 1;
            float exprange = defineTable.Findfloat("ExpRange", "value");
            InGame_Char charobject;
            switch (PlayerDataManager.Instacne.Data.fightCharIds.Last())
            {
                case '1':
                    charobject= Instantiate(charPrefab).GetComponent<InGame_Char>();
                    break;
                case '2':
                    charobject = Instantiate(charPrefab2).GetComponent<InGame_Char>();
                    break;
                case '3':
                    charobject = Instantiate(charPrefab3).GetComponent<InGame_Char>();
                    break;
                default:
                    charobject = Instantiate(charPrefab).GetComponent<InGame_Char>();
                    break;
            }



            charobject.SettingInfinity(defineTable.Findfloat("Infinity", "value"));
            charobject.InitChar(PlayerDataManager.Instacne.Data.fightCharIds, level, inventory, sensor, skillPoolingParent[0], mainBulletPoolingParent, skillTable, charTable.FindString(PlayerDataManager.Instacne.Data.fightCharIds, "skill"), masterController.transform, exprange); 
            haveSkillList.InitSkill(charobject.InGameSkill);
            masterController.InitMasterController(charobject, inGameCharInit);
            inventory.InitInventroy(0, 0, 0);
            int delay = defineTable.FindInt("waveDelay", "value");//기본라운드 대기시간
            float defaultspeed = defineTable.Findfloat("monsterdefaultspeed", "value");//몬스터기본속도
            reSpwanSystem.WaveInputStart(stagename, delay, defaultspeed, masterController.InGameChar.transform);

            List<int> bossdelay = new List<int>();
            bossdelay.Add(defineTable.FindInt("firstBoss", "value"));
            bossdelay.Add(defineTable.FindInt("secondBoss", "value"));
            bossdelay.Add(defineTable.FindInt("thirdBoss", "value"));
            bossdelay.Add(defineTable.FindInt("lastBoss", "value"));
            bossSpwanSystem.BossInputStart(stagename, delay, defaultspeed, masterController.InGameChar.transform, bossdelay);


        }


    }

}