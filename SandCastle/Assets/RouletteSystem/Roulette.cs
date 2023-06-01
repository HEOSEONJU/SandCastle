using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SandEnums;
using TMPro;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Linq;
using AT.SerializableDictionary;
namespace Roulette
{
    public class Roulette : MonoBehaviour
    {
        [SerializeField]
        ObjectTable shopTable;
        [SerializeField]
        GroupManager groupManager;
        [SerializeField]
        PriceType costType;

        int adCount = 3;
        int maxAdCount = 3;

        [SerializeField]
        int cost = 500;

        

        [SerializeField]
        TextMeshProUGUI costText;

        [SerializeField]
        GameObject rouletteObject;

        [SerializeField]
        string findid;

        [SerializeField]
        int order;
        [SerializeField]
        int ActionCount;
        private void OnEnable()
        {

            
            if(costType==PriceType.None)
            {
                Load();
            }
            
            
            ViewCost();

        }

        public void Load()
        {
            string type = shopTable.FindString(findid, "priceType");
            if (type is null) return;

            

            switch (type)
            {
                case "ads":
                    costType = PriceType.AD;
                    break;
                case "jewel":
                    costType = PriceType.jewel;
                    break;
                
                    
            }

            cost = shopTable.FindInt(findid, "price");

            order = shopTable.FindInt(findid, "order");
            
            if (shopTable.FindString(findid, "giveReward").Last().Equals('1'))
            {
                ActionCount = 1;
            }
            else
            {
                ActionCount = 10;
            }


            
        }
        public void ViewCost()
        {
            switch (costType)
            {
                case PriceType.AD:
                    costText.text = "(" + adCount + "/" + maxAdCount + ")";
                    break;
                case PriceType.jewel:
                    costText.text = cost.ToString();
                    break;


            }
        }


        bool CheckCan(int havegold)
        {
            switch (costType)
            {
                case PriceType.AD:
                    if (adCount is 0) return false;
                    break;
                case PriceType.jewel:
                    if ( cost > havegold) return false;
                    break;
            }
            return true;
        }


        public void Active(int havegold)
        {
            //실질적인뽑기기능
            if (CheckCan(havegold))
            {
                return;
            }



        }
        public void TempActive()
        {
            
            rouletteObject.SetActive(true);


            


            for (int i=0;i<ActionCount;i++)
            {
                int random = Random.Range(1, 101);
                
                //Debug.Log(random + "난수값");
                int appaer=0;
                foreach(SerializableDictionary<string, string> ht in groupManager.Gacha[order])
                {
                    appaer += int.Parse(ht["appearRate"].ToString());

                    //Debug.Log(appaer + "현재등장값");
                    if (appaer>=random)
                    {
                        Debug.Log(ht["giveNum"].ToString()+"획득이름");
                        break;
                    }
                }

                
            }
            




        }

    }
}