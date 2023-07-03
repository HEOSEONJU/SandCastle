using inGame;
using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace InGame
{
    public class BaseHP : MonoBehaviour
    {


        [SerializeField]
        InGame_Char inGameCharLeft;
        [SerializeField]
        InGame_Char inGameCharRight;
        MasterController mc;

        [SerializeField]
        float delay;


        public bool wait = false;


        public IEnumerator WaitCharChangeTime()
        {
            wait = true;
            yield return new WaitForSeconds(delay);
            wait = false;
        }



        public void InitBaseHP(InGameCharInit igci, MasterController mc)
        {
            this.mc = mc;
            igci.CharInit(inGameCharLeft);
            igci.CharInit(inGameCharRight);

        }



        public void InputChar(InGame_Char igc, bool dir)
        {
            if (dir == true)
            {
                inGameCharLeft = igc;
            }
            else
            {
                inGameCharRight = igc;
            }
        }



        public void ChangeChar(InGame_Char igc, bool dir)
        {
            InGame_Char temp;
            igc.InGameMove.Agent.enabled = false;
            igc.InGameMove.Fix = true;
            igc.FSM.ChangeState(new IdleState(igc));
            if (dir == true)
            {
                temp = inGameCharLeft;
                inGameCharLeft = igc;
                inGameCharLeft.transform.parent = transform.GetChild(0);
                inGameCharLeft.transform.localPosition = Vector3.zero;

            }
            else
            {
                temp = inGameCharRight;
                inGameCharRight = igc;
                inGameCharRight.transform.parent = transform.GetChild(1);
                inGameCharRight.transform.localPosition = Vector3.zero;

            }
            mc.InputChar(temp);

        }










        // Update is called once per frame
        void Update()
        {



            ActionChar(inGameCharLeft);
            ActionChar(inGameCharRight);

        }

        void ActionChar(InGame_Char igc)
        {
            switch (igc.State)
            {

                case PlayerState.Idle:


                    if (igc.InGameSkill.SettingTarget() && igc.InGameStatus.CanSkill && igc.Sensor.GameObjects.Count > 0)
                    {

                        ChangeState(igc, PlayerState.Skill);
                        Debug.Log("스킬발동" + igc.name);
                        break;
                    }

                    


                    break;


                case PlayerState.Skill:
                    if (!igc.IsAction)
                    {
                        ChangeState(igc, PlayerState.Idle);
                    }

                    break;

            }
            igc.FSM.UpdateState();

        }


        void ChangeState(InGame_Char igc, PlayerState next)
        {
            //Debug.Log("상태변경"+next);
            igc.State = next;
            switch (igc.State)
            {

                case PlayerState.Idle:
                    igc.FSM.ChangeState(new IdleState(igc));
                    break;
                case PlayerState.Skill:
                    igc.FSM.ChangeState(new SkillState(igc));
                    break;
            }
        }

    }


}