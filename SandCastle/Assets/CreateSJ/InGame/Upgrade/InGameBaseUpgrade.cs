using inGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class InGameBaseUpgrade : MonoBehaviour
    {
        [SerializeField]
        protected ObjectTable inGameUpgradeTable;
        [SerializeField]
        protected MasterController controller;
        [SerializeField]
        InGame_Inventory inventory;
        [SerializeField]
        string upgradeName;
        [SerializeField]
        int grade = 0;
        [SerializeField]
        int maxGrade = 10;

        public void SettingMaxUpgrade(int n)
        {
            maxGrade = n;
        }
        public void TryUpgrade()
        {
            if (maxGrade <= grade)
            {
                return;
            }

            string key = upgradeName + grade;
            int needsand=inGameUpgradeTable.FindInt(key, "needSand");
            int needwater = inGameUpgradeTable.FindInt(key, "needWater");

            if(!Require(needsand,needwater))
            {
                //조건불만족
                return;
            }
            Upgrade(key, needsand, needwater);
        }
        protected  virtual void Upgrade(string key,float needsand, float needwater)
        {
            grade += 1;
            inventory.SandCount -= needsand;
            inventory.WaterCount -= needwater;
        }


         bool Require( float needsand, float needwater)
        {

            if(inventory.SandCount<needsand || inventory.WaterCount<needwater)
            {
                return false;
            }
            return true;
        }
    }
}
