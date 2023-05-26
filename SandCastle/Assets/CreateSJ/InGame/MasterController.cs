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
        InGame_Harvest harvest;
        [SerializeField]
        bool isAction;
        [SerializeField]
        Rigidbody2D rigid;

        [SerializeField]
        InGame_Camera_Move cameraMove;
        float speed;
        public bool IsAction
        {
            get { return isAction; }
            set { isAction = value; }
        }
        private void Start()
        {
            IsAction = false;
            speed = 0;
            foreach (InGame_Char IGC in inGameCharList)
            {
                speed += IGC.InGameStatus.MoveSpeed;
            }
            speed /= inGameCharList.Count;

            harvest.Init(inGameCharList);
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsAction)
            {
                float angle = inputJoystick.inputVector.x;
                foreach (InGame_Char IGC in inGameCharList)
                {
                    rigid.velocity = inputJoystick.inputVector.normalized * speed;
                    IGC.InGameMove.MoveChar(IGC.Animator, inputJoystick.inputVector);
                    IGC.InGameAttack.PlayAttack(IGC.Animator);

                    if (angle > 0)
                    {
                        IGC.SpriteRenderer.flipX= true;

                    }
                    else if (angle < 0)
                        IGC.SpriteRenderer.flipX = false;

                }
                

                //harvest.Harvest(Animator);

                
                
            }
            else
            {
                foreach (InGame_Char IGC in inGameCharList)
                {
                    IGC.InGameMove.StopChar(IGC.Animator);

                }
                rigid.velocity = Vector2.zero;
            }
            cameraMove.Clamp_Camera();
        }
    }

}
