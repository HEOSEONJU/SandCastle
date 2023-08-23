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
    float warnnig;
    

    Transform player;

    

    [SerializeField]
    GameObject warnning;

    [SerializeField]
    List<BossBullet> bossBullet;
    [SerializeField]

    List<Transform> startPoint;
    [SerializeField]
    List<Transform> endPoint;
    public void Init(int damage ,float duration,float delay,float warnnig ,Transform player)//생성
    {

        
        
        this.duration = duration;
        this.delay = delay;
        this.warnnig= warnnig;
        
        this.player = player;
        warnning.SetActive(false);
        
        for(int i=0;i<bossBullet.Count;i++) 
        {
            bossBullet[i].InputData(startPoint[i], endPoint[i], duration,damage);
            
        }
        

    }
    public void Active()//발동
    {
        warnning.SetActive(true);
        StartCoroutine( Warnning());
    }
    public IEnumerator Warnning()//경고
    {

        WaitForSeconds delaytime= new WaitForSeconds(Time.deltaTime);
        float time = warnnig;

        while (time > 0) 
        {
            transform.position = new Vector3(transform.position.x,player.transform.position.y,transform.position.z);

            
            time-= Time.deltaTime;
            yield return delaytime;
        }


        yield return new WaitForSeconds(warnnig);
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
        Active();
    }
}

