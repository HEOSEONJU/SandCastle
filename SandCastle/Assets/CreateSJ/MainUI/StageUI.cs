using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


namespace MainUI
{
    public class StageUI : MonoBehaviour
    {
        [SerializeField]
        GameObject leftBtn;
        [SerializeField]
        GameObject RightBtn;

        int stageIndex;

        [SerializeField]
        Image mainImage;

        [SerializeField]
        List<Sprite> stageList;

        public int StageIndex
        {
            get { return stageIndex; }
            set { stageIndex = value; }
        }


        private void Start()
        {
            leftBtn.SetActive(false);
            stageIndex = 0;
            mainImage.sprite= stageList[stageIndex];
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
                if (!RightBtn.activeSelf)
                    RightBtn.SetActive(true);
            }
        }
        public void NextStage()
        {
            mainImage.sprite = stageList[++stageIndex];
            if (stageIndex == stageList.Count - 1)
            {
                RightBtn.SetActive(false);
            }
            else
            {
                if (!leftBtn.activeSelf)
                    leftBtn.SetActive(true);

            }
        }







    }
}
