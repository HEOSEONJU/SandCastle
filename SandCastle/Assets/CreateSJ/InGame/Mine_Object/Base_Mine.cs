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
        


        InGame_Harvest harvest;
        

        public override void OnEnable()
        {
            
            
            
            //Init_Object(1, 1);
            //hp = 10;
            
        }
        public override void Init_Object(string type, int amount, float maxhp, int amountMax, string imagefull, string imagedead)
        {

            switch (type)
            {
                case "sand":
                    resourceType = ResourceEnum.sand;
                    break;
                case "water":
                    resourceType = ResourceEnum.water;
                    break;
                case "mud":
                    resourceType = ResourceEnum.mud;
                    break;
            }
            Amount = amount;
            Hp=MaxHp = maxhp;
            spriteFull = imagefull;
            spriteDead = imagedead;


            ConnectList.Clear();
            sprites = Resources.LoadAll<Sprite>("mine-resources");

            spriteFull = "mine-resources_0";
            spriteDead = "mine-resources_2";
            Change_Image(spriteFull);

            IsDestory = false;
        }

        public override void ReadyMine()
        {
            
            if (!IsDestory  )
            {
                //StartCoroutine(WaitPlayer());
            }

            
        }
        public override void UnReadyMine()
        {
            if (!IsDestory && ConnectList.Count==0 )
            {
                //StopCoroutine(WaitPlayer());
                
                
            }
            
        }
        


        public override void Collection(float damagepoint,InGame_Inventory inventory,InGame_Char igc)
        {
            
            if(IsDestory)
            {
                
                if(igc.Animator.GetBool("IsAction"))
                igc.Animator.SetTrigger("HarvestExit");
                return;
            }
            if(Hp>0)
            {
                Hp -= damagepoint;
                if (Hp <= 0)
                {
                    inventory.Getter_Mine(AmountMax, resourceType);
                    Change_Image(spriteDead);
                    IsDestory = true;
                    igc.Animator.SetTrigger("HarvestExit");
                    return;
                }
                inventory.Getter_Mine(Amount, resourceType);
                return;
            }


            igc.Animator.SetTrigger("HarvestExit");

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