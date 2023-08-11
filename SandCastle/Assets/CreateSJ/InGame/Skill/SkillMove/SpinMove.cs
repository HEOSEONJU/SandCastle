using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



namespace Skill
{
    public class SpinMove : SkillMove
    {
        [SerializeField]
        Vector3 rotateValue;
        [SerializeField]
        Transform child;
        [SerializeField]
        SkillObject skillObject;
        public override void ObjectMove(float duration, float speed, Vector3 direction)
        {
            

            Rotating();


            
            

            moveCoroutine = Move(duration, speed);

            StartCoroutine(moveCoroutine);
        }
        public void OnDisable()
        {
            StopCoroutine(moveCoroutine);
        }

        IEnumerator Move(float duration, float speed)
        {
            
            

            Debug.Log("스핀작동");
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                transform.Rotate(rotateValue*speed*Time.deltaTime);
                child.transform.Rotate(rotateValue*speed*Time.deltaTime);
                yield return null;
            }
        }

        void Rotating()
        {
            int index = FindIndex(); ;
            Debug.Log(transform.GetComponentInParent<BasicCommonSkill>().SkillLevel + "패턴");
            transform.rotation = Quaternion.identity;
            switch (transform.GetComponentInParent<BasicCommonSkill>().SkillLevel)
            {
                case 1:
                    transform.rotation=Quaternion.identity;
                    break;
                case 2:

                    switch (index)
                    {
                        case 0:
                            transform.rotation=Quaternion.identity;
                            break;
                        case 1:
                            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                            break;
                    }
                    break;
                case 3:

                    switch (index)
                    {
                        case 0:
                            transform.rotation = Quaternion.identity;
                            break;
                        case 1:
                            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 120));
                            break;
                        case 2:
                            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 240));
                            break;
                    }
                    break;
                case 4:

                    switch (index)
                    {
                        case 0:
                            transform.rotation = Quaternion.identity;
                            break;
                        case 1:
                            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                            break;
                        case 2:
                            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                            break;
                        case 3:
                            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                            break;
                    }
                    break;
                case 5:

                    switch (index)
                    {
                        case 0:
                            transform.rotation = Quaternion.identity;
                            break;
                        case 1:
                            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 72));
                            break;
                        case 2:
                            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 144));
                            break;
                        case 3:
                            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 216));
                            break;
                        case 4:
                            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 288));
                            break;
                    }
                    break;
            }
        }

        public int FindIndex()
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i) == transform)
                {
                    return  i;
                }
            }
            return 0;
        }
    }
}