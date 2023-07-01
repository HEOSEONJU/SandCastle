using InGameResourceEnums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



namespace InGame
{
    public class Base_Mine : Abstract_Mine
    {
        [SerializeField]
        protected string mineName;

        [SerializeField]
        protected List<Sprite> sprites;



        protected InGame_Harvest harvest;
        


        public override void Init_Object(string type, int amount, float maxhp, int amountmax)
        {
            sprites=new List<Sprite>();
            Sprite[] tempsprites = Resources.LoadAll<Sprite>("Object/Object_Atlasobject_farming/Atlas/");
            
            switch (type)
            {
                case "sand":
                    resourceType = ResourceEnum.sand;
                    sprites.Add(tempsprites[3]);
                    sprites.Add(tempsprites[4]);
                    sprites.Add(tempsprites[5]);
                    
                    break;
                case "water":
                    resourceType = ResourceEnum.water;
                    sprites.Add(tempsprites[6]);
                    sprites.Add(tempsprites[7]);
                    sprites.Add(tempsprites[8]);
                    sprites.Add(tempsprites[9]);
                    sprites.Add(tempsprites[10]);
                    sprites.Add(tempsprites[11]);
                    break;
                case "mud":
                    resourceType = ResourceEnum.mud;
                    sprites.Add(tempsprites[0]);
                    sprites.Add(tempsprites[1]);
                    sprites.Add(tempsprites[2]);
                    
                    break;
            }
            Amount = amount;
            AmountMax = amountmax;
            Hp =MaxHp = maxhp;
            


            




            State = ResourceState.Full;
            Change_Image();

            IsDestory = false;
        }


        


        public override void Collection(float damagepoint,InGame_Char igc)
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
                
                

                igc.Inventory.Getter_Mine(Amount, resourceType);
                
                if (Hp <= 0)
                {
                    igc.Inventory.Getter_Mine(AmountMax, resourceType);
                    State = ResourceState.Dead;
                    Change_Image();
                    IsDestory = true;
                    igc.IsAction = false;

                    return;
                }else if (Hp <= MaxHp / 2f)
                {
                    State= ResourceState.Half;
                    Change_Image();
                }

                return;
            }
            


            //igc.Animator.SetTrigger("HarvestExit");

        }

        public override void Change_Image()
        {
            switch(State)
            {
                case ResourceState.Full:
                    mainImage.sprite = sprites[0];
                    break;
                case ResourceState.Half:
                    mainImage.sprite = sprites[1];
                    break;
                case ResourceState.Dead:
                    mainImage.sprite = sprites[2];
                    break;


            }

        }
    }
}