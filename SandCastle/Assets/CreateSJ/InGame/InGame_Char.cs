using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class InGame_Char : MonoBehaviour
    {
        [SerializeField]
        InGame_Move move;
        [SerializeField]
        InGame_Status status;
        [SerializeField]
        InGame_Camera_Move cameraMove;
        [SerializeField]
        InGame_Harvest harvest;
        [SerializeField]
        InGame_Inventory inventory;
        [SerializeField]
        Animator animator;
        [SerializeField]
        InGameAttack attack;

        [SerializeField]
        bool isAction;

        [SerializeField]
        Transform spriteTransform;
        public bool IsAction
        {
            get { return isAction; }
            set {  isAction= value; }
        }
        public Animator Animator
        {
            get { return animator; }
        }

        private void OnEnable()
        {
            IsAction = false;

            Invoke("BackMainScene", 10);
        }


        void Update()
        {
            if (!IsAction)
            {
                move.MoveChar(Animator);
                attack.PlayAttack(animator);
                harvest.Harvest(Animator);

                float angle = animator.GetFloat("Amount_X");
                if (angle > 0)
                {
                    spriteTransform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

                }
                else if(angle <0)
                    spriteTransform.rotation = Quaternion.identity;
            }
            else
            {
                move.StopChar(Animator);
            }
            cameraMove.Clamp_Camera();
            
            
        }


        public void OrderAttackTrigger()
        {
            attack.Attack(animator);
        }
        public void OrderHarvestTrigget()
        {
            harvest.TargetHarvest();
            IsAction = false;
        }

        string HASH= "Scene";

        string OpenObjectName = "전투버툰캔버스";
        void BackMainScene()
        {
            SceneMoveManager.Instance.ImmediatelyChangeScne("MainMenu");
            PlayerPrefs.SetString(HASH, OpenObjectName);
            Debug.Log(PlayerPrefs.GetString(HASH) + "저장한이름");
            PlayerPrefs.Save();

        }
    }


    

}