using inGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameEvent : MonoBehaviour
{
    static InGameEvent instance;


    [SerializeField]
    Transform poolingParent;

    [SerializeField]
    GameObject expPrefab;
    public static InGameEvent Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }


    public void EXP(Vector3 posi,float value)
    {
        ObjectPooling.GetObject(expPrefab.gameObject, poolingParent).TryGetComponent<EXP>(out EXP expobject);
        expobject.Init(this.transform, value);
        expobject.transform.position = posi;
        

    }
    
}
