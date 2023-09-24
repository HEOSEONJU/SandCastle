
using InGame;

using UnityEngine;

namespace inGame
{
    public class MasterController : MonoBehaviour
    {

        [SerializeField]
        InGame_Camera_Move cameraMove;


        [SerializeField]
        InGame_Char inGameChar;
        [SerializeField]
        Input_Joystikc inputJoystick;



        [SerializeField]
        Rigidbody2D rigid;

        


        
        public InGame_Char InGameChar
        {
            get { return inGameChar; }
        }

        public void InputChar(InGame_Char igc)
        {
            inGameChar = igc;
            GetComponent<Camera>().transform.parent = inGameChar.transform;
            
            inGameChar.transform.parent = null;
            inGameChar.InGameMove.enabled = true;
            
        }



        public void InitMasterController(InGame_Char igc, InGameCharInit igci)
        {
            inGameChar = igc;
            
            igci.CharInit(inGameChar);
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
            cameraMove.TraceChar(this.InGameChar.transform.position.x, this.InGameChar.transform.position.y);




            switch (inGameChar.State)
            {

                case PlayerState.Idle:

                    if (!inGameChar.Live)
                    {
                        ChangeState(PlayerState.Death);
                        break;
                    }
                    if (inGameChar.InGameSkill.SettingTarget() && inGameChar.InGameStatus.CanSkill && inGameChar.Sensor.GameObjects.Count > 0)
                    {
    
                        ChangeState(PlayerState.Skill);
                        break;
                    }
                    if (inGameChar.Harvest.CanHarveest)
                    {
                        ChangeState(PlayerState.Harvest);
                        break;
                    }
                    if (inGameChar.InGameMove.NeedMove())
                    {
                        ChangeState(PlayerState.Move);
                        break;

                    }
                    


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
                    
                    if(!inGameChar.Live)
                    {
                        ChangeState(PlayerState.Death);
                        break;
                    }

                    if(inGameChar.InGameSkill.SettingTarget() && inGameChar.InGameStatus.CanSkill && inGameChar.Sensor.GameObjects.Count > 0 )
                    {
                        ChangeState(PlayerState.Skill);
                        break;
                    }
                    if (inGameChar.Harvest.CanHarveest && !inGameChar.IsAction)
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
                    


                    break;
                
                    
            }


            inGameChar.FSM.UpdateState();

        }




        void ChangeState(PlayerState next)
        {
            
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
