using inGame;
using MainUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InGame
{
    public class InGameStart : MonoBehaviour
    {
        [SerializeField]
        ObjectTable WaveTable;

        [SerializeField]
        GameObject map;
        [SerializeField]
        InGame_Char charPrefab;


        
        
        [SerializeField]
        MasterController masterController;
        
        

        [SerializeField]
        InGameCharInit inGameCharInit;
        [SerializeField]
        InGame_Inventory inventory;
        [SerializeField]
        InGameSkillSensor sensor;

        [SerializeField]
        Transform skillPoolingParent;
        [SerializeField]
        Transform mainBulletPoolingParent;
        [SerializeField]
        List<Transform> subBulletPoolingParent;


        
        MineMaker mineMaker;
        WaveManager waveManager;
        BaseHP baseHp;

        [Header("Å×ÀÌºí")]
        [SerializeField]
        ObjectTable skillTable;

        private void Start()
        {

            int T = PlayerPrefs.GetInt("Stage");
            string stagename=WaveTable.values[T + 1].ToString();
           
            var maps= Instantiate(Resources.Load<GameObject>("Map/" + WaveTable.FindString(stagename, "stageResourceKey")));
            baseHp = maps.GetComponentInChildren<BaseHP>();
            mineMaker= maps.GetComponentInChildren<MineMaker>();
            waveManager= maps.GetComponentInChildren<WaveManager>();

            var main = Instantiate(charPrefab).GetComponent<InGame_Char>();
            main.InitChar("character000001", inventory, sensor, skillPoolingParent, mainBulletPoolingParent, skillTable, masterController.transform); ;
            masterController.InitMasterController(main, inGameCharInit);
            for (int i=0;i< 2;i++) 
            {
                InGame_Char sub = Instantiate(charPrefab, baseHp.transform.GetChild(i)).GetComponent<InGame_Char>();
                sub.InitChar("character000001", inventory, sensor, skillPoolingParent, subBulletPoolingParent[i], skillTable);
                baseHp.InputChar(sub);
            }
            baseHp.InitBaseHP(inGameCharInit);
            inventory.InitInventroy(0, 0, 0);
            mineMaker.InputMineData();
            waveManager.WaveInputStart(stagename);
            mineMaker.EnableMine(0, WaveTable.FindInt(stagename, 0 + ",sand"), WaveTable.FindInt(stagename, 0 + ",mud"), WaveTable.FindInt(stagename, 0 + ",water"));


        }


    }
}