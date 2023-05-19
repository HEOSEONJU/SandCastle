using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainUI
{
    public class ExtraUI : MonoBehaviour
    {
        public void CloseMy()
        {
            gameObject.SetActive(false);
        }
    }
}