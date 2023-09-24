using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public interface IHit
    {
        public void Hit(float value,bool knockback=false,float powoer=0f);
        public bool Alive();
    }


}