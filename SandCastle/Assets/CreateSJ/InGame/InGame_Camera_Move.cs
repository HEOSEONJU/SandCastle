using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace InGame
{
    public class InGame_Camera_Move : MonoBehaviour
    {
        float clampX=4.63f;
        float clampY=1.73f;

        Vector3 origin = new Vector3(-0.31f, 0, -10f);
        public void Clamp_Camera(Transform master)
        {
            transform.position = Vector3.Lerp(transform.position, master.position + origin,
                                  Time.deltaTime * 1f);

            
            Vector3 Temp = transform.position;

            if(Temp.x<= (-clampX) ||Temp.x>=clampX ||Temp.y<=(-clampY) ||Temp.y>=clampY)
            {
                Temp.x = Mathf.Clamp(Temp.x, -clampX, clampX);
                Temp.y = Mathf.Clamp(Temp.y, -clampY, clampY);
                
                transform.position = Temp;
                return;
            }
            
            
        }

    }
}