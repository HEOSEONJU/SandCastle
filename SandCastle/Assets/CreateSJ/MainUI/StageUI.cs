using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MainUI
{
    public class StageUI : MonoBehaviour
    {
        [SerializeField]
        GameObject leftBtn;
        [SerializeField]
        GameObject rightBtn;

        int stageIndex;

        [SerializeField]
        Image mainImage;

        [SerializeField]
        List<Sprite> stageList;

        [SerializeField]
        TextMeshProUGUI StageText;

        public int StageIndex
        {
            get { return stageIndex; }
            
        }


        private void Start()
        {
            

            stageIndex=PlayerPrefs.GetInt("Stage");
            PlayerPrefs.DeleteKey("Stage");
            mainImage.sprite = stageList[stageIndex];
            if (stageIndex == 0)
            {
                leftBtn.SetActive(false);
            }
            else if(stageIndex==stageList.Count-1)
            {
                rightBtn.SetActive(false);
            }
            StageText.text = "Stage : " + (stageIndex+1);


        }


        public void PreStage()
        {

            mainImage.sprite = stageList[--stageIndex];
            if (stageIndex == 0)
            {
                leftBtn.SetActive(false);
            }
            else
            {
                if (!rightBtn.activeSelf)
                    rightBtn.SetActive(true);
            }
            StageText.text = ("Stage : " + (stageIndex + 1));
        }
        public void NextStage()
        {
            if (PlayerDataManager.Instacne.Data.StageClear[(stageIndex+1)]==StageState.Lock)
            {
                return;
            }

            mainImage.sprite = stageList[++stageIndex];
            if (stageIndex == stageList.Count - 1)
            {
                rightBtn.SetActive(false);
            }
            else
            {
                if (!leftBtn.activeSelf)
                    leftBtn.SetActive(true);

            }
            StageText.text = ("Stage : " + (stageIndex + 1));
        }







    }
}
