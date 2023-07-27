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
        [SerializeField]
        float clampPlusY;
        [SerializeField]
        float clampMinusY;
        
        
        [SerializeField]
        Vector3 origin = new Vector3(-0.31f, 0, -10f);
        public void Clamp_Camera(Transform master)
        {
            
            transform.position = Vector3.Lerp(transform.position, master.position + origin,
                                  Time.deltaTime * 1f);

            
            Vector3 Temp = transform.position;

            if(Temp.x<= (clampMinusX) ||Temp.x>= clampPlusX || Temp.y<=(clampMinusY) ||Temp.y>= clampPlusY)
            {
                Temp.x = Mathf.Clamp(Temp.x, clampMinusX, clampPlusX);
                Temp.y = Mathf.Clamp(Temp.y, clampMinusY, clampPlusY);
                
                transform.position = Temp;
                return;
            }
            

        }
        public void TraceChar(Transform posi)
        {
            Vector3 temp = transform.position;
            temp.y=posi.transform.position.y;
            transform.position = temp;
        }
    }
}