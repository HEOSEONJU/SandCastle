using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MineMaker : MonoBehaviour
{
    [SerializeField]
    ObjectTable MineTable;

    List<Abstract_Mine> mineList;

    [SerializeField]
    List<MineArea> Area;

    public void InputMineData()
    {

        foreach (MineArea area in Area)
        {
            area.InitArea();
        }


            mineList = GetComponentsInChildren<Abstract_Mine>().ToList();

        foreach (Abstract_Mine mine in mineList)
        {


            string type = MineTable.FindString(mine.name, "resourceType");
            MineTable.FindInt(mine.name, "amount");
            int amount = MineTable.FindInt(mine.name, "amount");

            float maxhp = MineTable.Findfloat(mine.name, "maxHP");
            int amountmax = MineTable.FindInt(mine.name, "amountMax");
            string imagefull = MineTable.FindString(mine.name, "imageFull");
            string imagedead = MineTable.FindString(mine.name, "imageDead");
            mine.Init_Object(type, amount, maxhp, amountmax, imagefull, imagedead);

            mine.gameObject.SetActive(false);
            
        }
    }


    public void EnableMine(int areanum,int sand,int mud,int water)
    {
        while (sand-- > 0)
        {
            Area[areanum].SandArea.Find(x => x.gameObject.activeSelf == false).gameObject.SetActive(true);
            
            
        }
        while (mud-- > 0)
        {
            Area[areanum].MudArea.Find(x => x.gameObject.activeSelf == false).gameObject.SetActive(true);

        }
        while (water-- > 0)
        {
            Area[areanum].WaterArea.Find(x => x.gameObject.activeSelf == false).gameObject.SetActive(true);

        }



    }
    
    
}
