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



            




            var main = Instantiate(charPrefab3).GetComponent<InGame_Char>();
            int level = 1;
            
            main.InitChar("character000003", level, inventory, sensor, skillPoolingParent[0], mainBulletPoolingParent, skillTable, charSkillTable, masterController.transform,false); ;
            haveSkillList.InitSkill(main.InGameSkill);
            masterController.InitMasterController(main, inGameCharInit);


            /*
            bool dir = true;
            InGame_Char sub0 = Instantiate(charPrefab).GetComponent<InGame_Char>();
            Debug.Log(baseHp.transform.GetChild(0).name);
            sub0.transform.parent = baseHp.transform.GetChild(0).transform;
            sub0.transform.localPosition = Vector3.zero;
            sub0.InitChar("character000001", level, inventory, baseHp.transform.GetComponentInChildren<InGameSkillSensor>(), skillPoolingParent[1], subBulletPoolingParent[0], skillTable, charSkillTable, masterController.transform, true);
            baseHp.InputChar(sub0, dir);
            dir = !dir;
            sub0.InGameMove.Agent.enabled = false;

            InGame_Char sub1 = Instantiate(charPrefab2).GetComponent<InGame_Char>();
            Debug.Log(baseHp.transform.GetChild(1).name);
            sub1.transform.parent = baseHp.transform.GetChild(1).transform;
            sub1.transform.localPosition = Vector3.zero;
            sub1.InitChar("character000002", level, inventory, baseHp.transform.GetComponentInChildren<InGameSkillSensor>(), skillPoolingParent[2], subBulletPoolingParent[1], skillTable, charSkillTable, masterController.transform, true);
            baseHp.InputChar(sub1, dir);
            dir = !dir;
            sub1.InGameMove.Agent.enabled = false;
            
            
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
            */
            inventory.InitInventroy(0, 0, 0);
            float delay = defineTable.Findfloat("waveDelay", "value");//기본라운드 대기시간
            float defaultspeed = defineTable.Findfloat("monsterdefaultspeed", "value");//몬스터기본속도
            reSpwanSystem.WaveInputStart(stagename, delay, defaultspeed, masterController.InGameChar.transform);
            return;
            //baseHp.InitBaseHP(inGameCharInit,masterController);
            /*
            mineMaker.InputMineData();
            float delay = defineTable.Findfloat("waveDelay", "value");//기본라운드 대기시간
            float multiply= roundTable.Findfloat(stagename, "gateMultiply");//적 게이트 이번 라운드 체력배율
            float defaulthp = defineTable.Findfloat("defaultGateHp", "value");//적 게이트 기본체력
            float defaultspeed= defineTable.Findfloat("monsterdefaultspeed", "value");//몬스터기본속도
            waveManager.WaveInputStart(stagename, delay,defaulthp,multiply,defaultspeed);
            
            for (int i = 0; i <= 3; i++)
            {
                mineMaker.EnableMine(i, waveTable.FindInt(stagename, i + ",sand"), waveTable.FindInt(stagename, i + ",mud"), waveTable.FindInt(stagename, i + ",water"));
            }
            */


        }


    }

}