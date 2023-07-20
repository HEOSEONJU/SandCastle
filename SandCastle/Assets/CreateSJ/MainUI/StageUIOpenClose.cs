using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

namespace MainUI
{
    public class StageUIOpenClose : UIOpenClose
    {

        [SerializeField]
        StageUI stageUi;
        public override void OpenButton()
        {
            if(!Requrie())
            {
                return;
            }

            if (mainUi.Open_Main(this.gameObject) && Default != null)
            {

                Invoke("OpenDefault", Time.deltaTime);
            }
        }


        bool Requrie()
        {
            if (PlayerDataManager.Instacne.Data.StageClear[stageUi.StageIndex] == StageState.Lock)
            {
                return false;
            }
            return true;
        }
    }
}