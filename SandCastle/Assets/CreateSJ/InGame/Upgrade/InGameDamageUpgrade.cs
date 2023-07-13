using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InGame
{
    public class InGameDamageUpgrade : InGameBaseUpgrade
{
        protected override void Upgrade(string key, float needsand, float needwater)
        {
            base.Upgrade(key, needsand, needwater);
            int gradevalue = inGameUpgradeTable.FindInt(key, "upgradeValue");            
            controller.InGameChar.InGameStatus.LevelUpDMG(gradevalue);
        }
    }
}