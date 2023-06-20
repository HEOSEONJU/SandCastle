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
        InGameSkillSensor sensor;
        [SerializeField]
        InGame_Inventory inventory;
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
        public InGame_Inventory Inventory
        {
            get { return inventory; }
        }

        private void LateUpdate()
        {
            if(Input.GetKeyUp(KeyCode.Escape)) { BackMainScene(); }
            }


        
        public void InitChar(string name,int level,InGame_Inventory inventory,InGameSkillSensor sensor,Transform skillpoolingparent, Transform attackpoolingparent, ObjectTable skilltable, ObjectTable charskilltable, Transform defaultposi=null)
        {
            this.inventory= inventory;
            CharName = name;
            this.sensor= sensor;
            move.SettingPosi(defaultposi);

            var skills = charskilltable.FindDict("CharKey", name);
            string skillname1 = "";
            string skillname2 = "";

            
            foreach (var skill in skills)
            {
                if (Convert.ToInt32((skill["LV"].ToString())) <= level)
                {
                    skillname1 = skill["Skill00001"].ToString();

                    if(skill.Keys.Contains("Skill00002"))
                    {
                        skillname2 = skill["Skill00002"].ToString();
                    }
                    else 
                    {
                        skillname2= "";
                    }

                    


                }
                else
                {
                    break;
                }
            }


            
            skill.Init(sensor, skillpoolingparent, skilltable, skillname1);
            if(skillname2!="")
            {
                //skill.Init(sensor, skillpoolingparent, skilltable, skillname2);
            }
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


        public void OrderHarvestTrigget()
        {
            //harvest.TargetHarvest();
            IsAction = false;
        }

        public bool RecallChar()
        {
            if (!Animator.GetBool("RecallEnd"))
            {
                return false;
            }
            if ((InGameMove.Distance() >= InGameMove.value))
            {

                float angle = InGameMove.Angle();

                

                if (angle < 0)
                {
                    //IGC.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    SpriteRenderer.flipX = true;

                }
                else if (angle > 0)
                {
                    //IGC.transform.rotation = Quaternion.identity;
                    SpriteRenderer.flipX = false;
                }   

                InGameMove.MoveChar(Animator, InGameStatus.MoveSpeed * 2f);

            }
            else
            {
                InGameMove.StopChar(Animator);
                Animator.SetBool("Infinity", false);
                Animator.SetBool("RecallEnd", false);
            }
            return true;
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