using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;

using UnityEngine.Events;

using  AT.SerializableDictionary;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Linq;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectTable", order = 1)]

public class ObjectTable : ScriptableObject
{
    [SerializeField]
    const string associatedSheet = "1SXq75YN6BygXd7AUDisqXGDNykMw2OtvlAqvVhyZe5s";


    [SerializeField]
    public string associatedWorksheet = "";

    



    //public List<Hashtable> hashTableList;
    //public List<Dictionary<string,string>> TableList;
    public List<SerializableDictionary<string, string>> ViewTableList;
    


    [Header("시작하는A1의 인덱스")]
    public string startINDEX_A1 = "DB_FIELD_NAMES";
    [Header("무시할A열의 값들")]
    public List<string> igonoreNames;
    [Header("읽어올 A열의 값들")]
    public List<string> values;//리스트로 써먹을 수 있음
    //public List<string> hashKeyList;
    public List<string> dictKeyList;


    
    public string AssociatedSheet
    {
        get { return associatedSheet; }
    }
    [SerializeField]
    int listCount;


    public void INIT()
    {
        if (ViewTableList is null)
        {
            ViewTableList = new List<SerializableDictionary<string, string>>();
        }
        ViewTableList.Clear();

        if (igonoreNames is null)
        {
            igonoreNames = new List<string>();
        }

        if (dictKeyList is null)
        {
            dictKeyList = new List<string>();
        }
        dictKeyList.Clear();
    }

    internal void UpdateStats(List<GSTU_Cell> list)
    {
        if (startINDEX_A1 is null)
        {
            return;
        }
        SerializableDictionary<string, string> viewdict= new SerializableDictionary<string, string>();
       foreach (GSTU_Cell cell in list)
        {
            if ((!cell.value.Equals("")) && (!igonoreNames.Contains(cell.columnId)))
            {

                viewdict.Add(cell.columnId, cell.value);
                if (dictKeyList.Contains(cell.columnId) == false)
                {
                    
                    dictKeyList.Add(cell.columnId.ToString());
                }
            }
        }
        ViewTableList.Add(viewdict);
        listCount = ViewTableList.Count;
    }
    public List<SerializableDictionary<string, string>> FindDict(string key,string colum)
    {
        List<SerializableDictionary<string, string>> returnvalue=new List<SerializableDictionary<string, string>>();
        foreach (SerializableDictionary<string, string> table in ViewTableList)
        {
            if (table[key].ToString()== colum)
            {
                returnvalue.Add(table);
            }
        }

        return returnvalue;
    }

    public string FindString(string Key, string colum)//  Key Value의 찾을오브젝트의아이디   colum 해시키리스트의 해시값
    {
        if (ViewTableList == null)
        {

            return null;
        }
        var temp = ViewTableList.Find(x => x[startINDEX_A1].ToString() == Key);
        if(temp ==null)
        {
            return "";
        }
        if(temp.ContainsKey(colum))
        {
            //Debug.Log(temp[colum].ToString());
            return temp[colum].ToString();
            
        }

        return "";
    }
    public float Findfloat(string Key, string colum)//  Key Value의 찾을오브젝트의아이디   colum 해시키리스트의 해시값
    {
        if (ViewTableList == null)
        {

            return 0f;
        }
        var temp = ViewTableList.Find(x => x[startINDEX_A1].ToString() == Key);

        if (temp.ContainsKey(colum))
        {
            //Debug.Log(temp[colum].ToString());
            return  float.Parse( temp[colum].ToString());

        }
        return 0f;
    }
    public int FindInt(string Key, string colum)//  Key Value의 찾을오브젝트의아이디   colum 해시키리스트의 해시값
    {
        if (ViewTableList == null)
        {

            return 0;
        }
        var temp = ViewTableList.Find(x => x[startINDEX_A1].ToString() == Key);

        if (temp.ContainsKey(colum))
        {
            //Debug.Log(temp[colum].ToString());
            return Convert.ToInt32((temp[colum].ToString()));

        }
        return 0;
    }





    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(this.AssociatedSheet, this.associatedWorksheet), callback);
    }

    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        this.values.Clear();
        foreach (GSTU_Cell INDEX in ss.columns[this.startINDEX_A1])
        {
            if (!this.igonoreNames.Contains(INDEX.rowId))
            {
                this.values.Add(INDEX.rowId.ToString());
                
            }
        }
        this.INIT();
        foreach (string dataName in this.values)
        {

            this.UpdateStats(ss.rows[dataName]);
        }

        
        string _path = Application.persistentDataPath + "/" + this.name + ".json";
        string jsonString = JsonConvert.SerializeObject(ViewTableList);
        File.WriteAllText(_path, jsonString);
        


    }
    public void OnLineReading()
    {
        UpdateStats(UpdateMethodOne);
    }
    public int OffLineReading()
    {
        INIT();

        string _path = Application.persistentDataPath + "/" + this.name + ".json";
        string data = File.ReadAllText(_path);
        ViewTableList = JsonConvert.DeserializeObject<List<SerializableDictionary<string, string>>>(data);
        
        foreach (var key in ViewTableList)
        {
            
            values.Add(key.Values.First());
        }
        foreach(var key in ViewTableList.First())
        {
            dictKeyList.Add(key.Key);
        }

        return 1;

    }
}


////////////
///해시인덱스를 이용한 데이터탐색
///
///
///
///
/// string findid = "Object00001";
/// int I = data.items4.FindIndex(x => x[data.StartINDEX_A1].ToString() == findid);
/// Debug.Log(data.items4[I]["resourceType"]);
/////////////////

//인터넷통신안될때 방법 강구하기








