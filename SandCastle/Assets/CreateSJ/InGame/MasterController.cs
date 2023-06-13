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


        // Update is called once per frame
        void Update()
        {



            rigid.velocity = inputJoystick.inputVector.normalized * speed;
            cameraMove.Clamp_Camera(this.transform);



            if (inGameChar.RecallChar())
            {
                return;
            }
            if (inGameChar.Animator.GetBool("IsAction") is false)
            {
                
                if (!inGameChar.ActiveSKill())
                {
                    inGameChar.InGameAttack.PlayAttack();

                    
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
                inGameChar.InGameMove.MoveChar(inGameChar.Animator, inGameChar.InGameStatus.MoveSpeed);
            }
            else
            {
                inGameChar.InGameMove.StopChar(inGameChar.Animator);

            }

        }

        
    }
}
