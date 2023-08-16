using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Skill { 
public class TopDownMove : SkillMove
    {
        [SerializeField]
        float angle = 15f;


        [SerializeField]
        List<Transform> childs;
        [SerializeField]
        Vector3 rotateValue;
        public override void ObjectMove(float duration, float speed, Vector3 direction)
        {

            if (childs == null) { childs = new List<Transform>(); }
            childs.Clear();
            for (int i = 0; i < transform.childCount; i++)
            {
                childs.Add(transform.GetChild(i));
                childs.Last().rotation=Quaternion.identity;
            }

            foreach (Transform t in childs)
            {
                t.transform.Rotate(rotateValue * Time.deltaTime);
            }
            //Rotating(direction);
            moveCoroutine = Move(duration, speed);

            StartCoroutine(moveCoroutine);
        }
        IEnumerator Move(float duration, float speed)
        {
            Debug.Log("탑다운작동");
            while (duration > 0)
            {
                if (!fix)
                {


                    foreach(Transform t in childs) 
                    {
                        t.transform.Rotate(rotateValue * Time.deltaTime);
                    }
                    
                }
                duration -= Time.deltaTime;
                transform.position += (-transform.up).normalized * Time.deltaTime * speed;
                yield return null;
            }
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