using SkillEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Skill
{
    public class StraightMove : SkillMove
    {
            
        public override void ObjectMove(float duration, float speed, Vector3 direction, bool fix = false)
        {
            if (fix == false)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            
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
    }
}