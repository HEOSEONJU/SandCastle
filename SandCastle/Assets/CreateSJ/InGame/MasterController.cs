using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace inGame
{
    public class MasterController : MonoBehaviour
    {
        [SerializeField]
        ObjectTable CharTable;
        [SerializeField]
        ObjectTable DefineTable;


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

            float defaultspeed = DefineTable.Findfloat("bulletdefaultspeed", "value");
            float attackdamage = DefineTable.Findfloat("attackdamage", "value");

            foreach (InGame_Char IGC in inGameCharList)
            {

                float movespeed = CharTable.Findfloat(IGC.CharName, "moveSpeed");
                float animationSpeed = CharTable.Findfloat(IGC.CharName, "animationSpeed");

                float giveDamage = CharTable.Findfloat(IGC.CharName, "giveDamage");
                float sandGet = CharTable.Findfloat(IGC.CharName, "sandGet");
                float waterGet = CharTable.Findfloat(IGC.CharName, "waterGet");
                float mudGet = CharTable.Findfloat(IGC.CharName, "mudGet");
                string localKeyName = CharTable.FindString(IGC.CharName, "localKeyName");

                float range = CharTable.Findfloat(IGC.CharName, "range");
                float moveSpeedLV = CharTable.Findfloat(IGC.CharName, "moveSpeedLV");
                float animationSpeedLV = CharTable.Findfloat(IGC.CharName, "animationSpeedLV");
                float giveDamageLV = CharTable.Findfloat(IGC.CharName, "giveDamageLV");
                float sandGetLV = CharTable.Findfloat(IGC.CharName, "sandGetLV");
                float waterGetLV = CharTable.Findfloat(IGC.CharName, "waterGetLV");
                float mudGetLV = CharTable.Findfloat(IGC.CharName, "mudGetLV");
                int maxMana = CharTable.FindInt(IGC.CharName, "maxMana");
                int startMana = CharTable.FindInt(IGC.CharName, "startMana");
                int maxhp = CharTable.FindInt(IGC.CharName, "maxHP");
                float attackspeed = CharTable.Findfloat(IGC.CharName, "attackSpeed");

                if (speed < movespeed)
                {
                    speed = movespeed;
                }

                IGC.InGameStatus.Init(movespeed, animationSpeed, giveDamage, sandGet, waterGet, mudGet, range, maxMana, startMana, maxhp);

                IGC.SettingAttack(attackspeed, defaultspeed, attackdamage);

            }

            speed += 1;

        }

        // Update is called once per frame
        void Update()
        {

            float angle = inputJoystick.inputVector.x;
            foreach (InGame_Char IGC in inGameCharList)
            {

                if(RecallChar(IGC))
                {
                    continue;
                }


                if (IGC.Animator.GetBool("IsAction") is false)
                {
                    IGC.InGameMove.MoveChar(IGC.Animator, IGC.InGameStatus.MoveSpeed);
                    if (!IGC.ActiveSKill())
                    {
                        IGC.InGameAttack.PlayAttack();
                    }

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