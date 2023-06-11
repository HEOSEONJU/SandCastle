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

        [SerializeField]
        InGameCharInit inGameCharInit;

        private void Start()
        {
           speed=inGameCharInit.CharInit(inGameChar);
        }

        // Update is called once per frame
        void Update()
        {


            
            rigid.velocity = inputJoystick.inputVector.normalized * speed;
            cameraMove.Clamp_Camera(this.transform);
            


            if (RecallChar(inGameChar))
            {
                return;
            }
            else if (inGameChar.Animator.GetBool("IsAction") is false)
            {
                inGameChar.InGameMove.MoveChar(inGameChar.Animator, inGameChar.InGameStatus.MoveSpeed);
                if (!inGameChar.ActiveSKill())
                {
                    inGameChar.InGameAttack.PlayAttack();
                }
                float  angle = inputJoystick.inputVector.x;
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
            else
            {
                inGameChar.InGameMove.StopChar(inGameChar.Animator);
            }

        }

        bool RecallChar(InGame_Char IGC)
        {
            if (!IGC.Animator.GetBool("RecallEnd"))
            {
                return false;
            }
            if ((IGC.InGameMove.Distance() >= IGC.InGameMove.value))
            {

                float angle = IGC.InGameMove.Angle();

                Debug.Log(angle);

                if (angle < 0)
                {
                    //IGC.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    IGC.SpriteRenderer.flipX = true;

                }
                else if (angle > 0)
                {
                    //IGC.transform.rotation = Quaternion.identity;
                    IGC.SpriteRenderer.flipX = false;
                }

                IGC.InGameMove.MoveChar(IGC.Animator, IGC.InGameStatus.MoveSpeed * 2f);
                
            }
            else
            {
                IGC.InGameMove.StopChar(IGC.Animator);
                IGC.Animator.SetBool("Infinity", false);
                IGC.Animator.SetBool("RecallEnd", false);
            }
            return true;
        }
    }
}
