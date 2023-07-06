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
        Camera camera;


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

        public InGame_Char InGameChar
        {
            get { return inGameChar; }
        }

        public void InputChar(InGame_Char igc)
        {
            inGameChar = igc;
            camera.transform.parent = inGameChar.transform;
            speed = inGameChar.InGameStatus.MoveSpeed;
            inGameChar.transform.parent = null;
            inGameChar.InGameMove.enabled = true;
            inGameChar.InGameMove.Fix = false;
        }



        public void InitMasterController(InGame_Char igc, InGameCharInit igci)
        {
            inGameChar = igc;
            camera.transform.parent = inGameChar.transform;
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



            inGameChar.InGameMove.dir = inputJoystick.inputVector.normalized;
            //rigid.velocity = inputJoystick.inputVector.normalized * speed;
            //cameraMove.Clamp_Camera(this.transform);




            switch (inGameChar.State)
            {

                case PlayerState.Idle:

                    
                    if (inGameChar.InGameSkill.SettingTarget() && inGameChar.InGameStatus.CanSkill && inGameChar.Sensor.GameObjects.Count > 0)
                    {
    
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
                    //Ã¼·Â³·À¸¸é»ç¸Á


                    break;

                case PlayerState.Death:
                    

                    break;
                case PlayerState.Skill:
                    if(!inGameChar.IsAction)
                    {
                        ChangeState(PlayerState.Idle);
                    }

                    break;
                case PlayerState.Harvest:
                    if (!inGameChar.IsAction)
                    {
                        ChangeState(PlayerState.Idle);
                    }
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
                    //Ã¼·Â³·À¸¸é»ç¸Á


                    break;
                
                    
            }


            inGameChar.FSM.UpdateState();

        }

        void ChangeState(PlayerState next)
        {
            //Debug.Log("»óÅÂº¯°æ"+next);
            inGameChar.State = next;
            switch (inGameChar.State)
            {

                case PlayerState.Idle:
                    inGameChar.FSM.ChangeState(new IdleState(inGameChar));
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
