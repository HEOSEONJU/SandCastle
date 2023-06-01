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
        ObjectTable CharTable;

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

            foreach (InGame_Char IGC in inGameCharList)
            {
                
                float movespeed = float.Parse(CharTable.FindData(IGC.CharName, "moveSpeed"));
                float animationSpeed = float.Parse(CharTable.FindData(IGC.CharName, "animationSpeed"));
                float giveDamage = float.Parse(CharTable.FindData(IGC.CharName, "giveDamage"));
                float sandGet = float.Parse(CharTable.FindData(IGC.CharName, "sandGet"));
                float waterGet = float.Parse(CharTable.FindData(IGC.CharName, "waterGet"));
                float mudGet = float.Parse(CharTable.FindData(IGC.CharName, "mudGet"));
                string localKeyName = CharTable.FindData(IGC.CharName, "localKeyName");
                
                float range = float.Parse(CharTable.FindData(IGC.CharName, "range"));
                float moveSpeedLV = float.Parse(CharTable.FindData(IGC.CharName, "moveSpeedLV"));
                float animationSpeedLV = float.Parse(CharTable.FindData(IGC.CharName, "animationSpeedLV"));
                float giveDamageLV = float.Parse(CharTable.FindData(IGC.CharName, "giveDamageLV"));
                float sandGetLV = float.Parse(CharTable.FindData(IGC.CharName, "sandGetLV"));
                float waterGetLV = float.Parse(CharTable.FindData(IGC.CharName, "waterGetLV"));
                float mudGetLV = float.Parse(CharTable.FindData(IGC.CharName, "mudGetLV"));
                int maxMana = Convert.ToInt32 (CharTable.FindData(IGC.CharName, "maxMana"));
                int startMana = Convert.ToInt32(CharTable.FindData(IGC.CharName, "startMana"));
                int maxhp = Convert.ToInt32(CharTable.FindData(IGC.CharName, "maxHP"));


                IGC.InGameStatus.Init(movespeed, animationSpeed, giveDamage, sandGet, waterGet, mudGet, range, maxMana, startMana,maxhp);

            }


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
