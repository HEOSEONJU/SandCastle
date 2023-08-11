using Google.GData.Extensions;
using SkillEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Skill
{
    public class StraightMove : SkillMove
    {
        [SerializeField]
        float angle=15f;
        
        public override void ObjectMove(float duration, float speed, Vector3 direction)
        {

            Rotating(direction);
            moveCoroutine =Move(duration, speed);
            
            StartCoroutine(moveCoroutine);
        }
        IEnumerator Move(float duration,float speed)
        {
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                transform.position += (transform.right).normalized * Time.deltaTime * speed;
                yield return null;
            }
        }

        public void Rotating(Vector3 direction)
        {
            if (fix)
            {
                transform.rotation = Quaternion.identity;
                return;
            }
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            int index = FindIndex(); ;
            if(index==0)
            {
                return;
            }

            float t = angle * Mathf.Pow(-1, index) * ((index/ 2)+1);
            


            transform.Rotate(new Vector3(0, 0, t));











        }


        public int FindIndex()
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i) == transform)
                {
                    return i;
                }
            }
            return 0;
        }

    }
}