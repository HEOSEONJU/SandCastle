using Google.GData.Documents;
using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;

namespace inGame
{
    public class MasterController : MonoBehaviour
    {




        [SerializeField]
        InGame_Char inGameChar;
        [SerializeField]
        Input_Joystikc inputJoystick;



        [SerializeField]
        Rigidbody2D rigid;

        [SerializeField]
        InGame_Camera_Move cameraMove;
        [SerializeField]
        float speed;



        public void InitMasterController(InGame_Char igc, InGameCharInit igci)
        {
            inGameChar = igc;

            speed = igci.CharInit(inGameChar);
        }

        public void RotateAngle()
        {
            float angle = inputJoystick.inputVector.x;
            if (angle > 0)
            {
                //IGC.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                inGameChar.SpriteRenderer.flipX = true;
            }
            else if (angle < 0)
            {
                //IGC.transform.rotation = Quaternion.identity;
                inGameChar.SpriteRenderer.flipX = false;
            }
        }

        // Update is called once per frame
        void Update()
        {



            rigid.velocity = inputJoystick.inputVector.normalized * speed;
            cameraMove.Clamp_Camera(this.transform);




            switch(inGameChar.State)
            {

                case PlayerState.Idle:

                    //Debug.Log(inGameChar.InGameStatus.CanSkill + "스킬조건");
                    if (inGameChar.InGameSkill.SettingTarget() && inGameChar.InGameStatus.CanSkill && inGameChar.Sensor.GameObjects.Count > 0)
                    {
                        //Debug.Log("?");
                            
                        ChangeState(PlayerState.Skill);
                        break;
                    }
                    if (inGameChar.Harvest.Do)
                    {
                        ChangeState(PlayerState.Harvest);
                        break;
                    }
                    if (inGameChar.InGameMove.NeedMove())
                    {
                        ChangeState(PlayerState.Move);
                        break;

                    }
                    //체력낮으면사망


                    break;
                case PlayerState.Recall:
                    if (!inGameChar.InGameMove.NeedMove())
                    {
                        ChangeState(PlayerState.Idle);
                    }
                    //inGameChar.RecallChar();
                    break;
                case PlayerState.Skill:
                    if (inGameChar.IsAction==false)//스킬끝났을때 필요함
                    {
                        ChangeState(PlayerState.Recall);
                    }
                    //inGameChar.ActiveSKill();
                    //inGameChar.InGameMove.StopChar(inGameChar.Animator);
                    break;
                case PlayerState.Harvest:
                    if (inGameChar.IsAction == false)//수확끝났을때 필요함
                    {
                        ChangeState(PlayerState.Recall);
                    }
                    break;
                case PlayerState.Death:
                    

                    break;
                case PlayerState.Move:
                    
                    if(inGameChar.InGameSkill.SettingTarget() && inGameChar.InGameStatus.CanSkill && inGameChar.Sensor.GameObjects.Count > 0 )
                    {
                        ChangeState(PlayerState.Skill);
                        break;
                    }
                    if (inGameChar.Harvest.Do && !inGameChar.IsAction)
                    {
                        ChangeState(PlayerState.Harvest);
                        break;
                    }
                    if (!inGameChar.InGameMove.NeedMove())
                    {
                        ChangeState(PlayerState.Idle);
                        break;
                    }
                    float angle = inputJoystick.inputVector.x;
                    if (angle > 0)
                    {
                        //IGC.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                        inGameChar.SpriteRenderer.flipX = true;
                    }
                    else if (angle < 0)
                    {
                        //IGC.transform.rotation = Quaternion.identity;
                        inGameChar.SpriteRenderer.flipX = false;
                    }
                    //체력낮으면사망


                    break;
                
                    
            }


            inGameChar.FSM.UpdateState();

        }

        void ChangeState(PlayerState next)
        {
            Debug.Log("상태변경"+next);
            inGameChar.State = next;
            switch (inGameChar.State)
            {

                case PlayerState.Idle:
                    inGameChar.FSM.ChangeState(new IdleState(inGameChar));
                    break;
                case PlayerState.Recall:
                    inGameChar.FSM.ChangeState(new RecallState(inGameChar));
                    break;
                case PlayerState.Skill:
                    inGameChar.FSM.ChangeState(new SkillState(inGameChar));
                    break;
                case PlayerState.Harvest:
                    inGameChar.FSM.ChangeState(new HarvestState(inGameChar));
                    break;
                case PlayerState.Death:
                    inGameChar.FSM.ChangeState(new DeathState(inGameChar));
                    break;
                case PlayerState.Move:
                    inGameChar.FSM.ChangeState(new MoveState(inGameChar));
                    break;


            }
        }
        
    }

}
