using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AT.SerializableDictionary;
namespace Roulette
{
    public class GroupManager : MonoBehaviour
    {
        [SerializeField]
        ObjectTable rateTable;


        List<List<SerializableDictionary<string, string>>> gacha;

        public List<List<SerializableDictionary<string, string>>> Gacha
        {
            get { return gacha; }
        }


        string colum = "group";
        [SerializeField]
        int tablecount;

        public void OnEnable()
        {

            if (rateTable.ViewTableList is null)
            {
                return;
            }

            if (gacha is null) { }
            {
                gacha = new List<List<SerializableDictionary<string, string>>>();


            }
            gacha.Clear();

            int count = 0;//가장큰 테이블 찾기

            foreach(SerializableDictionary<string, string> h in rateTable.ViewTableList)
            {
                int c=h[colum].ToString().Last();
                {
                    if (c > count)
                    {
                        count = c;
                    }
                }
            }
            tablecount = count;

            for (int i=0;i<= count; i++)
            {
                List<SerializableDictionary<string, string>> templist= new List<SerializableDictionary<string, string>>();
                gacha.Add(templist);

            }


            for(int i=1;i< rateTable.ViewTableList.Count;i++)
            {
                SerializableDictionary<string, string> ht = rateTable.ViewTableList[i];
                int C =  (int)char.GetNumericValue(rateTable.FindString(ht[rateTable.startINDEX_A1], colum). Last());
                
                gacha[C].Add(ht);




            }


        }

        
    }
}
