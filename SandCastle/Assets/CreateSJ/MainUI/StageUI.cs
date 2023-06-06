using System.Collections;
using System.Collections.Generic;

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
        }
        public void NextStage()
        {
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
        }







    }
}
