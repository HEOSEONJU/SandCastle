using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inGame
{
    public class MasterController : MonoBehaviour
    {
        [SerializeField]  
        List<InGame_Char> inGameCharList;

        [SerializeField]
        Input_Joystikc inputJoystick;

        
        
        [SerializeField]
        Rigidbody2D rigid;

        [SerializeField]
        InGame_Camera_Move cameraMove;
        [SerializeField]
        float speed;
        
        private void Start()
        {
            
            speed = 1;


            
        }

        // Update is called once per frame
        void Update()
        {
            
                float angle = inputJoystick.inputVector.x;
                foreach (InGame_Char IGC in inGameCharList)
                {
                    

                    if (IGC.Animator.GetBool("IsAction") is false)
                    {
                        IGC.InGameMove.MoveChar(IGC.Animator, inputJoystick.inputVector);
                        IGC.InGameAttack.PlayAttack();

                        if (angle > 0)
                        {
                            //IGC.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                            IGC.SpriteRenderer.flipX = true;

                        }
                        else if (angle < 0)
                        {
                            //IGC.transform.rotation = Quaternion.identity;
                            IGC.SpriteRenderer.flipX = false;
                        }
                    }
                    else
                    {
                        IGC.InGameMove.StopChar(IGC.Animator);
                    }

                }


            //harvest.Harvest(Animator);




            rigid.velocity = inputJoystick.inputVector.normalized * speed;
            //rigid.velocity = Vector2.zero;
            cameraMove.Clamp_Camera(this.transform);
        }
    }

}
