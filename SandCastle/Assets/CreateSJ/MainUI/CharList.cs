using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainUI
{
    public class CharList : MonoBehaviour
    {
        [SerializeField]
        ObjectTable CharTable;


        [SerializeField]
        Transform parent;
        void Start()
        {

            
            for(int i=1;i< CharTable.values.Count;i++)
            {

                string path = "CharIcon/" + CharTable.FindString(CharTable.values[i], "imageName");
                Sprite temp=Resources.Load<Sprite>(path);
                parent.GetChild(i - 1).GetComponent<CharSlotData>().InputData(temp, CharTable.values[i]);
            }
            


        }

    }
}