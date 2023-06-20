using SkillEnums;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Skill
{
    public class BouncetMove : SkillMove
    {
        [SerializeField]
        CircleCollider2D circleCollider;
        
        public override void ObjectMove(float duration, float speed,Vector3 direction)
        {
            transform.GetComponent<Animator>().SetTrigger("Skill");

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            moveCoroutine =Move(duration, speed);
            
            StartCoroutine(moveCoroutine);
        }
        IEnumerator Move(float duration,float speed)
        {
            circleCollider=GetComponent<CircleCollider2D>();
            circleCollider.radius = 1f;
            float time = 0;
            Vector2 S = new Vector2(1, 0);
            Vector2 L = new Vector2(speed,0);

            while (duration > 0)
            {
                time+= Time.deltaTime;
                duration -= Time.deltaTime;
               Vector2 D=Vector2.Lerp(S, L, time);

                circleCollider.radius = D.x;

                yield return null;
            }
        }
    }
}