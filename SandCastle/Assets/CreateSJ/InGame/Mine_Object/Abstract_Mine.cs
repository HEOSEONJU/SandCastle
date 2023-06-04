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
        List<GameObject> connectList = new List<GameObject>();
        [SerializeField]
        protected string spriteFull;
        [SerializeField]
        protected string spriteDead;



        [SerializeField]    
        int amount;
        [SerializeField]
        float hp;
        [SerializeField]
        float maxHp;
        [SerializeField]
        int amountMax;




        [SerializeField]
        protected Image mainImage;
        
        public bool IsDestory;
        


        public List<GameObject> ConnectList
        {
            get { 
                if(connectList is null)
                {
                    connectList = new List<GameObject>();
                }

                return connectList; }
        }
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



        public abstract void Init_Object(string type,int amount,float maxhp,int amountmax,string imagefull,string imagedead);//파밍할오브젝트 추가하기

        public abstract void Collection(float damgepoint,InGame_Inventory inventory,InGame_Char igc);


        public abstract void Change_Image(string name);



    }

}
