using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public abstract class Abstract_Mine : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> connectList = new List<GameObject>();
        [SerializeField]
        protected string spriteFull;
        [SerializeField]
        protected string spriteDead;


        float collectTime;
        float amount;
        [SerializeField]
        protected float hp;
        [SerializeField]
        protected Image mainImage;
        [SerializeField]
        protected Image filImage;
        public bool IsDestory;
        public bool IsReady;


        public List<GameObject> ConnectList
        {
            get { 
                if(connectList is null)
                {
                    connectList = new List<GameObject>();
                }

                return connectList; }
        }
        public float Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public float CollectTime
        {
            get { return collectTime; }
            set { collectTime = value; }
        }


        public abstract void OnEnable();

        public abstract void Init_Object(float time,float amount);//파밍할오브젝트 추가하기

        public abstract void Collection(float damgepoint,InGame_Inventory inventory);
        public abstract void ReadyMine();
        public abstract void UnReadyMine();

        public abstract void Change_Image(string name);



    }

}