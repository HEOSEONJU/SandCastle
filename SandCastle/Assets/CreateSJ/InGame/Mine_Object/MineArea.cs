using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace InGame
{


    public class MineArea : MonoBehaviour
    {
        [SerializeField]
        List<Abstract_Mine> sandArea;
        [SerializeField]
        List<Abstract_Mine> mudArea;
        [SerializeField]
        List<Abstract_Mine> waterArea;


        public List<Abstract_Mine> SandArea
        {
            get { return sandArea; }
        }
        public List<Abstract_Mine> MudArea
        { get { return mudArea; } }

        public List<Abstract_Mine> WaterArea
        { get { return waterArea; } }



        public void InitArea()
        {




            sandArea = transform.GetChild(0).GetComponentsInChildren<Abstract_Mine>().ToList();
            mudArea = transform.GetChild(1).GetComponentsInChildren<Abstract_Mine>().ToList();
            waterArea = transform.GetChild(2).GetComponentsInChildren<Abstract_Mine>().ToList();




        }
    }
}