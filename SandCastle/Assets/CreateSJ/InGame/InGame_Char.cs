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
        InGameSkillSensor sensor;
        
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
        public InGameSkill InGameSkill
        {
            get { return skill; }
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
            
            
                skill.Init();

            


        }

        
        public void SettingAttack(float attackspped,float dspeed,float ddamage)
        {
            attack.AbstractAttack.CoolTime = attackspped;
            attack.AbstractAttack.SettingBulletInfo(dspeed, ddamage);   
        }


        
        public void RegenMana(int n)
        {
            status.CurrentMana += n;

            

        }
        public bool ActiveSKill()
        {
            if (status.CanSkill && sensor.GameObjects.Count > 0)
            {
                status.CurrentMana = 0;
                Animator.CrossFade("CharSkill", 0.01f);
                
                skill.SettingTarget();
                return true;
                //skill.ActiveSkill();
            }
            return false;
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