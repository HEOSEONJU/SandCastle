using inGame;
using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHP : MonoBehaviour
{

    [SerializeField]
    List<InGame_Char> inGameCharList;

    [SerializeField]
    InGameCharInit inGameCharInit;

    private void Start()
    {
         inGameCharInit.CharsInit(inGameCharList);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = 0;




        foreach (InGame_Char IGC in inGameCharList)
        {


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
    }

}

