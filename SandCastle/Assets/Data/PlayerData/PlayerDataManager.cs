using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace Player
{
    public class PlayerDataManager : MonoBehaviour
    {
        static PlayerDataManager instance;

        [SerializeField]
        PlayerDataObject datatable;

         PlayerData data;


        public static PlayerDataManager Instacne 
        { 
            get 
            {
                return instance; 
            } 
        }

        public  PlayerData Data
        {
            get
            {
                return data;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                data = datatable.Data;
                DontDestroyOnLoad(gameObject);
                return;
            }
            
            Destroy(this);
            

            


        }
    }
}