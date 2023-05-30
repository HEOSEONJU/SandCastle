using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace InGame
{
    public class InGame_UI : MonoBehaviour
    {
        static InGame_UI instance=null;




        private void Awake()
        {
            if (instance is null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        public static InGame_UI Instance
        {
            get
            {
                

                if (instance is null)
                {
                    return null;
                }
                return instance;
            }

        }
        

    }
}
