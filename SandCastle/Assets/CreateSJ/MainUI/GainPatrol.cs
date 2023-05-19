using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainUI
{
    public class GainPatrol : MonoBehaviour
    {
        [SerializeField]
        GameObject gainWindow;


        public void OpenGain()
        {
            gainWindow.SetActive(true);
        }


    }
}
