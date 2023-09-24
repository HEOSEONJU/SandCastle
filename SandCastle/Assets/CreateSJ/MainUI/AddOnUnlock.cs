using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnUnlock : MonoBehaviour
{
    public void ShowAddOn()
    {
        if ((PlayerDataManager.Instacne.Data.StageClear[PlayerDataManager.Instacne.Data.StageClear.Count-1]==StageState.Unlock) || (PlayerDataManager.Instacne.Data.StageClear[PlayerDataManager.Instacne.Data.StageClear.Count - 1] == StageState.Clerar))
        {
            return;   
        }
        RewardedAdScript.Instance.Showad();

    }
}
