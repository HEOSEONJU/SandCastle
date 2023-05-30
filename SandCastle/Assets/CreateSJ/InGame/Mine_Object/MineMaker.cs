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
            

            string type = MineTable.FindData(mine.name, "resourceType");
            int amount = Convert.ToInt32 (MineTable.FindData(mine.name, "amount"));
            float maxhp = float.Parse(MineTable.FindData(mine.name, "maxHP"));
            int amountmax = Convert.ToInt32(MineTable.FindData(mine.name, "amountMax"));
            string imagefull= MineTable.FindData(mine.name, "imageFull");
            string imagedead= MineTable.FindData(mine.name, "imageDead");
            mine.Init_Object(type, amount, maxhp, amountmax, imagefull, imagedead);
        }


    }
}
