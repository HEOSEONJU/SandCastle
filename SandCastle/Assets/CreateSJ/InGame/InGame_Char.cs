using System;
using System.Collections;
using System.Linq;
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
        InGame_Harvest harvest;

        [SerializeField]
        InGameSkillSensor sensor;
        [SerializeField]
        InGame_Inventory inventory;


        [SerializeField]
        bool isAction;

        [SerializeField]
        SpriteRenderer spriteRenderer;


        [SerializeField]
        PlayerState state;
        [SerializeField]
        FSM fsm;

        [SerializeField]
        CircleCollider2D expRange;

        

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
        public InGame_Harvest Harvest
        {
            get { return harvest; }
        }
        public InGameSkillSensor Sensor
        {
            get { return sensor; }
        }

        public SpriteRenderer SpriteRenderer
        {
            get { return spriteRenderer; }
        }
        public InGame_Inventory Inventory
        {
            get { return inventory; }
        }
        
        public PlayerState State
        {
            get { return state; }
            set { state = value; }
        }
        public FSM FSM
        {
            get { return fsm; }
            set { fsm = value; }
        }

        public float ExpRange
        {
            set 
            {
                expRange.radius= status.BaseExpRange+value;
            }
        }



        
        public void InitChar(string name,int level,InGame_Inventory inventory,InGameSkillSensor sensor,Transform skillpoolingparent, Transform attackpoolingparent, ObjectTable skilltable, string  skillname, Transform defaultposi,float exprange)
        {
            
            state = PlayerState.Idle;
            fsm= new FSM (new IdleState(this));
            this.inventory= inventory;
            CharName = name;
            this.sensor= sensor;
            InGameStatus.BaseExpRange= exprange;
            move.SettingPosi(defaultposi);

            skill.Init(sensor, skillpoolingparent, skilltable, skillname);
            InGameAttack.AbstractAttack.PoolingParent = attackpoolingparent;
            

            //하베스트인벤토리
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
                
                
                return skill.SettingTarget();
                //skill.ActiveSkill();
            }
            return false;
        }


        public void GetEXP(float value)
        {
            status.GetEXP(value);
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
