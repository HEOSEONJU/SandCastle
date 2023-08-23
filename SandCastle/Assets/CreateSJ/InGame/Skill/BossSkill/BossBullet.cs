using Enemy;
using InGame;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    


    Transform start;
    Transform end;
    float duration;
    int damage;
    public void InputData(Transform start, Transform end, float duration,int damage)
    {
        this.start = start;
        this.end = end;
        this.duration = duration;
        this.damage = damage;
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        if(start!= null) 
        {
            StartCoroutine(MoveCoroutine());
        }

        
    }
    public  void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveCoroutine()
    {
        transform.position = start.position;
        float time = duration;
        Vector3 dir = (end.position - start.position);
        dir = dir.magnitude * dir.normalized * Time.deltaTime;
        Debug.Log(dir);
        WaitForSeconds delay =new WaitForSeconds(Time.deltaTime);
        while (time>=0) 
        {
            transform.position += dir;
            time -= Time.deltaTime;
            yield return delay;
        }


        gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.TryGetComponent<InGame_Char>(out InGame_Char player))
        {
            if (player.Infitiny == false)
            {
                player.Damaged(damage);
            }
            
            
        }

        gameObject.SetActive(false);
    }

}
