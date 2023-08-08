using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace InGame
{
    public class InGame_Camera_Move : MonoBehaviour
    {
        [SerializeField]
        float clampPlusX;
        [SerializeField]
        float clampMinusX;




        public void TraceChar(float x,float y)
        {
            Vector3 temp = transform.position;
            temp.x = x;
            temp.x = Mathf.Clamp(temp.x, clampMinusX, clampPlusX);
            temp.y = y;
            

            transform.position = temp;
        }
    }
}