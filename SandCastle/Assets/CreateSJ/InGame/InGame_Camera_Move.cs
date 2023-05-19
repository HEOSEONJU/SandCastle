using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace InGame
{
    public class InGame_Camera_Move : MonoBehaviour
    {
        float clampX=5f;
        float clampY=2.5f;

        public void Clamp_Camera()
        {
            Vector3 Temp = transform.position;
            Temp.x = Mathf.Clamp(Temp.x, -clampX, clampX);
            Temp.y = Mathf.Clamp(Temp.y, -clampY, clampY);

            transform.position = Temp;
        }

    }
}