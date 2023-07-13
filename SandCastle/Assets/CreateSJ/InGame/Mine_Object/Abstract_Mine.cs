using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using InGameResourceEnums;
namespace InGame
{
    public abstract class Abstract_Mine : MonoBehaviour
    {
        [SerializeField]
        public ResourceEnum resourceType;
        [SerializeField]
        protected ResourceState State=ResourceState.Full;


        



        [SerializeField]    
        int amount;
        [SerializeField]
        float hp;
        [SerializeField]
        float maxHp;
        [SerializeField]
        int amountMax;




        [SerializeField]
        protected SpriteRenderer mainImage;
        
        public bool IsDestory;
        



        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public float Hp
        {
            get { return hp; }
            set { hp = value; }
        }
        public float MaxHp
        {
            get { return maxHp; }
            set { maxHp = value; }
        }
        public int AmountMax
        {
            get { return amountMax; }
            set { amountMax = value; }
        }



        public abstract void Init_Object(string type,int amount,float maxhp,int amountmax);//�Ĺ��ҿ�����Ʈ �߰��ϱ�

        public abstract void Collection(InGame_Char igc);


        public abstract void Change_Image();



    }

}
