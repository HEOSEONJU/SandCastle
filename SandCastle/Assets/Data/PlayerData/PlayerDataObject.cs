using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerData", order = 1)]

    public class PlayerDataObject : ScriptableObject
    {
        [SerializeField]
        PlayerData data;



        public PlayerData Data
        {
            get { return data; }
        }
    }

}