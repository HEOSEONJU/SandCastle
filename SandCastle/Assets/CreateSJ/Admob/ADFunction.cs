using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADFunction : MonoBehaviour
{
    

    public void Showad()
    {
        RewardedAdScript.Instance.Showad(this);
        
    }

    public abstract void Reward();
}
