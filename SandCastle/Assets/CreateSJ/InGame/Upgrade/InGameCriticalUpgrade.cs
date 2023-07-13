using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InGame
{
    public class InGameCriticalUpgrade : InGameBaseUpgrade
    {

        protected  override void Upgrade(string key, float needsand, float needwater)
        {
            base.Upgrade(key, needsand, needwater);
            float gradevalue = inGameUpgradeTable.Findfloat(key, "upgradeValue");
            
            controller.InGameChar.InGameStatus.LevelUpCRT(gradevalue);
        }
    }

}