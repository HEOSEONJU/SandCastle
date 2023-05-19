using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class Base_Mine : Abstract_Mine
    {
        [SerializeField]
        Sprite[] sprites;
        public float timer;



        public override void OnEnable()
        {
            if(connectList is null)
            {
                connectList = new List<GameObject>();
            }
            ConnectList.Clear();
            sprites = Resources.LoadAll<Sprite>("mine-resources");
            
            spriteFull = "mine-resources_0";
            spriteDead = "mine-resources_2";
            Change_Image(spriteFull);
            timer = 0;
            IsDestory = false;
            IsReady = false;
            Init_Object(1, 1);
            hp = 10;
        }


        


        public override void Init_Object(float time, float amount)
        {
            CollectTime = time;
            Amount= amount;
            timer = 0f;
        }
        public override void ReadyMine()
        {
            if(!IsDestory)
            StartCoroutine(WaitPlayer());
        }
        public override void UnReadyMine()
        {
            if (!IsDestory)
            {
                StopCoroutine(WaitPlayer());
                IsReady = false;
                filImage.fillAmount = 0f;
            }
        }
        
        IEnumerator  WaitPlayer()
        {
            while (true) 
            {
                timer += Time.deltaTime;

                filImage.fillAmount = timer / CollectTime;
                if (timer>=CollectTime)
                {
                    IsReady = true;
                    
                    yield break;

                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            
            

        }

        public override void Collection(float damagepoint,InGame_Inventory inventory)
        {
            if(IsDestory)
            {
                return;
            }

            inventory.Getter_Mine(Amount);
            hp -= damagepoint;
            if(hp <= 0)
            {
                Change_Image(spriteDead);
                IsDestory = true;
                filImage.fillAmount = 0f;
            }
            
        }

        public override void Change_Image(string name)
        {
            if (spriteFull == name)
            {
                mainImage.sprite = sprites[2];
            }
            else

            {
                mainImage.sprite = sprites[0];
            }
        }
    }
}