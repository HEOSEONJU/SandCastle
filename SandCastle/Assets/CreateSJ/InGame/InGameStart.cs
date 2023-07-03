using inGame;
using MainUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        MineMaker mineMaker;
        [SerializeField]
        WaveManager waveManager;
        [SerializeField]
        BaseHP baseHp;

        [Header("테이블")]
        [SerializeField]
        ObjectTable roundTable;
        [SerializeField]
        ObjectTable skillTable;
        [SerializeField]
        ObjectTable charSkillTable;
        [SerializeField]
        ObjectTable defineTable;

        private void Start()
        {

            int T = PlayerPrefs.GetInt("Stage");
            string stagename = roundTable.values[T + 1].ToString();

            //var maps = Instantiate(Resources.Load<GameObject>("Map/" + waveTable.FindString(stagename, "stageResourceKey")));


            baseHp = map.GetComponentInChildren<BaseHP>();
            mineMaker = map.GetComponentInChildren<MineMaker>();
            waveManager = map.GetComponentInChildren<WaveManager>();

            var main = Instantiate(charPrefab2).GetComponent<InGame_Char>();





            int level = 1;
            bool dir = true;
            main.InitChar("character000002", level, inventory, sensor, skillPoolingParent[0], mainBulletPoolingParent, skillTable, charSkillTable, masterController.transform,false); ;
            masterController.InitMasterController(main, inGameCharInit);
            //transform.parent = masterController.transform;
            for (int i = 0; i < 2; i++)
            {
                InGame_Char sub = Instantiate(charPrefab).GetComponent<InGame_Char>();
                Debug.Log(baseHp.transform.GetChild(i).name);
                sub.transform.parent= baseHp.transform.GetChild(i).transform;
                sub.transform.localPosition = Vector3.zero;
                sub.InitChar("character000001", level, inventory, baseHp.transform.GetComponentInChildren<InGameSkillSensor>(), skillPoolingParent[1+i], subBulletPoolingParent[i], skillTable, charSkillTable, masterController.transform,true);
                baseHp.InputChar(sub,dir);
                dir = !dir;
                sub.InGameMove.Agent.enabled = false;
            }
            baseHp.InitBaseHP(inGameCharInit,masterController);
            inventory.InitInventroy(0, 0, 0);
            mineMaker.InputMineData();
            float delay = defineTable.Findfloat("waveDelay", "value");//기본라운드 대기시간
            float multiply= roundTable.Findfloat(stagename, "gateMultiply");//적 게이트 이번 라운드 체력배율
            float defaulthp = defineTable.Findfloat("defaultGateHp", "value");//적 게이트 기본체력
            float defaultspeed= defineTable.Findfloat("monsterdefaultspeed", "value");//몬스터기본속도
            waveManager.WaveInputStart(stagename, delay,defaulthp,multiply,defaultspeed);

            /*
            for (int i = 0; i <= 3; i++)
            {
                mineMaker.EnableMine(i, waveTable.FindInt(stagename, i + ",sand"), waveTable.FindInt(stagename, i + ",mud"), waveTable.FindInt(stagename, i + ",water"));
            }
            */


        }


    }

}