using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MineMaker : MonoBehaviour
{
    [SerializeField]
    ObjectTable MineTable;

    List<Abstract_Mine> mineList;

    

    
    private void Start()
    {
        mineList= GetComponentsInChildren<Abstract_Mine>().ToList();

        foreach(Abstract_Mine mine in mineList) 
        {
            

            string type = MineTable.FindString(mine.name, "resourceType");
            MineTable.FindInt(mine.name, "amount");
            int amount = MineTable.FindInt(mine.name, "amount");

            float maxhp = MineTable.Findfloat(mine.name, "maxHP");
            int amountmax = MineTable.FindInt(mine.name, "amountMax");
            string imagefull= MineTable.FindString(mine.name, "imageFull");
            string imagedead= MineTable.FindString(mine.name, "imageDead");
            mine.Init_Object(type, amount, maxhp, amountmax, imagefull, imagedead);
        }


    }
}
