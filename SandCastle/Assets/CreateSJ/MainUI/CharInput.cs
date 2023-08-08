using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainUI
{
    public class CharInput : MonoBehaviour
    {
        [SerializeField]
        SubUI subUi;
        [SerializeField]
        ZoomCharInfo zoomCharInfo;
        public void InputChar()
        {
            string key = zoomCharInfo.ReturnCharKey();

            int INDEX = PlayerDataManager.Instacne.Data.havetCharIds.FindIndex(x => x.id == key);
            if (PlayerDataManager.Instacne.Data.CharUnlock[INDEX])
            {
                subUi.CloseMy();
                PlayerDataManager.Instacne.Data.fightCharIds = key;
            }

            
        }
    }
}