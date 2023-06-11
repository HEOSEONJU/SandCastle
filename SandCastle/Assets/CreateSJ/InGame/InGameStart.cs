using inGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InGame
{
    public class InGameStart : MonoBehaviour
    {
        [SerializeField]
        InGame_Char charPrefab;


        [SerializeField]
        MineMaker mineMaker;


        [SerializeField]
        BaseHP baseHp;
        [SerializeField]
        MasterController masterController;
        [SerializeField]
        WaveManager waveManager;

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


        [Header("Å×ÀÌºí")]
        [SerializeField]
        ObjectTable skillTable;

        private void Start()
        {

            var main = Instantiate(charPrefab).GetComponent<InGame_Char>();
            main.InitChar("character000001", inventory, sensor, skillPoolingParent, mainBulletPoolingParent, skillTable, masterController.transform); ;
            masterController.InitMasterController(main, inGameCharInit);
            for (int i=0;i< 2;i++) 
            {
                InGame_Char sub = Instantiate(charPrefab, baseHp.transform).GetComponent<InGame_Char>();
                sub.InitChar("character000001", inventory, sensor, skillPoolingParent, subBulletPoolingParent[i], skillTable);
                baseHp.InputChar(sub);
            }
            baseHp.InitBaseHP(inGameCharInit);
            inventory.InitInventroy(0, 0, 0);
            mineMaker.InputMineData();
            waveManager.WaveInputStart();



        }


    }
}