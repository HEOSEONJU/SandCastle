using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Roulette
{
    public class GroupManager : MonoBehaviour
    {
        [SerializeField]
        ObjectTable rateTable;


        List<List<Hashtable>> gacha;

        public List<List<Hashtable>> Gacha
        {
            get { return gacha; }
        }


        string colum = "group";
        [SerializeField]
        int tablecount;

        public void OnEnable()
        {

            if (rateTable.hashTableList is null)
            {
                return;
            }

            if (gacha is null) { }
            {
                gacha = new List<List<Hashtable>>();


            }
            gacha.Clear();

            int count = 0;//가장큰 테이블 찾기

            foreach(Hashtable h in rateTable.hashTableList)
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
                List< Hashtable> templist= new List<Hashtable>();
                gacha.Add(templist);

            }


            for(int i=1;i< rateTable.hashTableList.Count;i++)
            {
                Hashtable ht = rateTable.hashTableList[i];



                int C =  (int)char.GetNumericValue(rateTable.FindData(ht[rateTable.startINDEX_A1].ToString(), colum). Last());
                
                gacha[C].Add(ht);




            }


        }

        
    }
}
