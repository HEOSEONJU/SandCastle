using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class EXP : MonoBehaviour
{

    [SerializeField]
    Transform origin;
    public float Value = 0;
    float speed=1;

    bool state = false;
    private void OnEnable()
    {
        state = false;
        speed = 1f;
    }


    public void Init(Transform parent, float value)
    {
        origin=parent;
        Value = value;
        RaycastHit2D[] hitlist = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, Vector2.zero);
        Array.Sort(hitlist, (RaycastHit2D x, RaycastHit2D y) => x.distance.CompareTo(y.distance));
        
        foreach (var hit in hitlist) 
        {
            
            if(hit.collider.CompareTag("Map"))
            {
                transform.parent = hit.transform;
                break;
            }
        }
        

         
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<InGame_Char>(out InGame_Char player) && !state)
        {
            state= true;
            StartCoroutine(Trace(player));

        }
    }


    IEnumerator Trace(InGame_Char player)
    {
        while(Vector3.Distance(player.transform.position,this.transform.position)>0.01f)
        {
            speed += Time.deltaTime*5;
            transform.position += (player.transform.position - transform.position).normalized*Time.deltaTime*speed;
            yield return null;
        }
        player.GetEXP(Value);
        transform.parent = origin;
        gameObject.SetActive(false);
        
    }
}
