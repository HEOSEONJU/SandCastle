using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MainUI
{
    public class SubUI : MonoBehaviour
    {

        public bool CheckActivity()
        {
            return gameObject.activeSelf;
        }

        public void OpenMy()
        {
            gameObject.SetActive(true);
        }
        public void CloseMy()
        {
            gameObject.SetActive(false);
        }

    }
}