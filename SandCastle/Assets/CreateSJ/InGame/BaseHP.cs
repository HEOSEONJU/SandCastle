using inGame;
using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHP : MonoBehaviour
{

    [SerializeField]
    List<InGame_Char> inGameCharList;

    


    public void InitBaseHP(InGameCharInit igci)
    {
        igci.CharsInit(inGameCharList);
    }
    
    public void InputChar(InGame_Char igc)
    {
        if(inGameCharList ==null)
        {
            inGameCharList=new List<InGame_Char>();
        }
        inGameCharList.Add(igc);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = 0;




        foreach (InGame_Char IGC in inGameCharList)
        {
            
            if (IGC.RecallChar())
            {
                return;
            }

            if (IGC.Animator.GetBool("IsAction") is false)
            {
                
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
            
        }
    }

}

