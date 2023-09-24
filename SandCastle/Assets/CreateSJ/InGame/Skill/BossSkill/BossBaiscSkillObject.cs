using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SkillEnums;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class BossBaiscSkillObject : MonoBehaviour
{



    float duration;
    float delay;
    float warnningTime;
    bool stop;

    bool coolTime = false;

    Transform player;



    [SerializeField]
    GameObject warnning;

    [SerializeField]
    List<BossBullet> bossBullet;
    [SerializeField]

    List<Transform> startPoint;
    [SerializeField]
    List<Transform> endPoint;

    public bool CoolTime
    {
        get { return coolTime; }
    }

    public void Init(int damage ,float duration,float delay,float warnnig ,Transform player)//생성
    {


        coolTime = false;
        this.duration = duration;
        this.delay = delay;
        warnningTime= warnnig;
        stop = false;
        this.player = player;
        if(warnning != null)
        {
            warnning.SetActive(false);
        }

        if (bossBullet != null)
        {

            for (int i = 0; i < bossBullet.Count; i++)
            {
                bossBullet[i].InputData(startPoint[i], endPoint[i], duration, damage);

            }
        }

    }
    public void Stop()
    {
        stop= true;
    }

    public void Active()//발동
    {
        if (warnning == null)
        {
            return;
        }
        coolTime = true;
        warnning.SetActive(true);
        StartCoroutine( Warnning());
    }
    public IEnumerator Warnning()//경고
    {

        WaitForSeconds delaytime= new WaitForSeconds(Time.deltaTime);
        float time = warnningTime;
        Vector3 t= new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        while (time > 0) 
        {
            
            t.y = player.transform.position.y;
            
            transform.position = t;

            
            time-= Time.deltaTime;
            yield return delaytime;
        }


        yield return new WaitForSeconds(warnningTime);
        warnning.SetActive(false);
        StartCoroutine(Attack());
    }
    public IEnumerator Attack()//공격
    {
        foreach(var bullet in bossBullet)
        {
            bullet.gameObject.SetActive(true);
                
        }
        yield return new WaitForSeconds(duration);
        
        StartCoroutine(Delay());
    }
    public IEnumerator Delay()//완료
    {
        

        yield return new WaitForSeconds(delay);
        coolTime = false;
        if (stop)
        {
            Destroy(gameObject);
            yield break;
        }
        
    }
}

