
using InGame;

using UnityEngine;

public class BossBullet : MonoBehaviour
{
    


    Transform start;
    Transform end;
    float duration;
    int damage;
    float time = 1f;
    public void InputData(Transform start, Transform end, float duration,int damage)
    {
        this.start = start;
        this.end = end;
        this.duration = duration;
        this.damage = damage;
        transform.position = start.position;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
        
    }


    private void OnEnable()
    {
        if(start!= null) 
        {
            transform.position = start.position;
            time = 1f;
        }

        
    }



    public void Update()
    {
        if (start == null)
        {
            return;
        }
        transform.position = Vector3.Lerp(start.position, end.position, time);
        time -= Time.deltaTime / duration;

        if(time<=0f)
        {
            gameObject.SetActive(false);
        }


        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.TryGetComponent<InGame_Char>(out InGame_Char player))
        {
            if (player.Infitiny == false)
            {
                Debug.Log("보스스킬데미지");
                player.Damaged(damage);
            }
            gameObject.SetActive(false);

        }

        
    }

}
