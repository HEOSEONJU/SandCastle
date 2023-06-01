using System;
using System.Collections;
using UnityEngine;

namespace InGame
{
    public class InGame_Char : MonoBehaviour
    {
        public string CharName= "character000001";

        [SerializeField]
        InGame_Move move;
        [SerializeField]
        InGame_Status status;
        [SerializeField]
        Animator animator;
        [SerializeField]
        InGameAttack attack;
        [SerializeField]
        InGameSkill skill;
        [SerializeField]
        bool isAction;

        [SerializeField]
        SpriteRenderer spriteRenderer;



        IEnumerator RegenManaCoroutine;
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


        
        
        private void Start()
        {
            StartRegneMana();
            if(skill==null)
            {
                Debug.Log("스킬없음");
            }
            else
            {
                skill.Init();
            }
            


        }

        public void StartRegneMana()
        {
            if (RegenManaCoroutine == null)
            {
                RegenManaCoroutine = RegenMana();
                StartCoroutine(RegenManaCoroutine);
            }
        }
        
        IEnumerator RegenMana()
        {
            while(true)
            {
                yield return new WaitForSeconds(1);
                status.CurrentMana += 1;
                if (status.CanSkill)
                {
                    status.CurrentMana = 0;
                    StopCoroutine(RegenManaCoroutine);
                    RegenManaCoroutine = null;
                    Animator.CrossFade("CharSkill", 0.01f);
                    skill.ActiveSkill();
                }
                
            }
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