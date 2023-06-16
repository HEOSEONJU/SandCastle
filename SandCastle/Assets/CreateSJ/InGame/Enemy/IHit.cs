using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public interface IHit
    {
        public void Hit(float value);
        public bool Alive();
    }


}