using SkillEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Skill
{
    public abstract class SkillMove : MonoBehaviour
    {

        protected IEnumerator moveCoroutine;
        public abstract void ObjectMove(float duration, float speed,Vector3 direction);


    }
}