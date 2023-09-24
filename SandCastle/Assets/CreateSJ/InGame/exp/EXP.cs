using InGame;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class EXP : MonoBehaviour
{

    [SerializeField]
    Transform origin;
    public float Value = 0;
    float speed=1;

    [SerializeField]
    Transform trace;

    
    private void OnEnable()
    {
        trace = null;
        
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
        if(collision.CompareTag("ExpSearch") && trace==null)
        {
            trace = collision.transform.root;
            
        }

    }

    public void Update()
    {
        if(trace !=null)
        {
            speed += Time.deltaTime * 5;
            transform.position += (trace.transform.position - transform.position).normalized * Time.deltaTime * speed;
            if(Vector3.Distance(trace.transform.position, this.transform.position) <= 0.01f)
            {
                trace.GetComponent<InGame_Char>().GetEXP(Value);
                transform.parent = origin;
                trace = null;
                gameObject.SetActive(false);

            }
        }

    }


    IEnumerator Trace(Transform player)
    {
        while(Vector3.Distance(player.transform.position,this.transform.position)>0.01f)
        {
            speed += Time.deltaTime*5;
            transform.position += (player.transform.position - transform.position).normalized*Time.deltaTime*speed;
            yield return null;
        }
        player.GetComponent<InGame_Char>().GetEXP(Value);
        transform.parent = origin;
        gameObject.SetActive(false);
        
    }
}
