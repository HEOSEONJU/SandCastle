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
        GameObject map;
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
        HaveSkillList haveSkillList;

        [Header("���̺�")]
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




            charobject.InitChar(PlayerDataManager.Instacne.Data.fightCharIds, level, inventory, sensor, skillPoolingParent[0], mainBulletPoolingParent, skillTable, charTable.FindString(PlayerDataManager.Instacne.Data.fightCharIds, "skill"), masterController.transform, exprange); 
            haveSkillList.InitSkill(charobject.InGameSkill);
            masterController.InitMasterController(charobject, inGameCharInit);
            inventory.InitInventroy(0, 0, 0);
            float delay = defineTable.Findfloat("waveDelay", "value");//�⺻���� ���ð�
            float defaultspeed = defineTable.Findfloat("monsterdefaultspeed", "value");//���ͱ⺻�ӵ�
            reSpwanSystem.WaveInputStart(stagename, delay, defaultspeed, masterController.InGameChar.transform);



        }


    }

}