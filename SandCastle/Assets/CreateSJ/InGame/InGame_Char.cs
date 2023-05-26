using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class InGame_Char : MonoBehaviour
    {
        [SerializeField]
        InGame_Move move;
        [SerializeField]
        InGame_Status status;
        [SerializeField]
        Animator animator;
        [SerializeField]
        InGameAttack attack;

        [SerializeField]
        bool isAction;

        [SerializeField]
        SpriteRenderer spriteRenderer;
        public bool IsAction
        {
            get { return isAction; }
            set { isAction = value; }
        }
        public Animator Animator
        {
            get { return animator; }
        }
        public InGame_Move InGameMove
            {
            get {return move; }
            }
        public InGameAttack InGameAttack
        {
            get { return attack; }
        }
        [SerializeField]
        public InGame_Status InGameStatus
        {
            get{ return status; }
        }

        
        public SpriteRenderer SpriteRenderer
        {
            get { return spriteRenderer; }
        }

        private void LateUpdate()
        {
            if(Input.GetKeyUp(KeyCode.Escape)) { BackMainScene(); }
            }




        public void OrderAttackTrigger()
        {
            attack.Attack(animator);
        }
        public void OrderHarvestTrigget()
        {
            //harvest.TargetHarvest();
            IsAction = false;
        }

        string HASH= "Scene";

        string OpenObjectName = "전투버툰캔버스";
        void BackMainScene()
        {
            SceneMoveManager.Instance.ImmediatelyChangeScne("MainMenu");
            PlayerPrefs.SetString(HASH, OpenObjectName);
            Debug.Log(PlayerPrefs.GetString(HASH) + "저장한이름");
            PlayerPrefs.Save();

        }
    }


    

}